using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Course.Services
{
    public class PayrollService
    {
        private FolhaContext _folhacontext;
        private readonly IConverter converter;

        public PayrollService(FolhaContext folhacontext, IConverter _converter)
        {
            _folhacontext = folhacontext;
            converter = _converter;

        }

        public async Task<decimal> PayrollGeneration(PayrollDto payroll)
        {
            // Verifica se o usuário está autenticado
            var userAutenticado = await _folhacontext.Users.AnyAsync(x => x.Id == payroll.UserId);
            if (!userAutenticado)
            {
                throw new Exception("Usuário não autenticado");
            }

            // Define as faixas de INSS
            var faixasInss = new Dictionary<decimal, decimal>
    {
        { 1100m, 0.075m },
        { 2203.48m, 0.09m },
        { 3305.22m, 0.12m },
        { 6433.57m, 0.14m },
        { decimal.MaxValue, 751.99m }
    };

            // Define as faixas de IRRF
            var faixasIrrf = new Dictionary<decimal, (decimal Aliquota, decimal Deducao)>
    {
        { 1903.98m, (0m, 0m) },
        { 2826.65m, (0.075m, 142.80m) },
        { 3751.05m, (0.15m, 354.08m) },
        { 4664.69m, (0.225m, 636.13m) },
        { decimal.MaxValue, (0.275m, 869.36m) }
    };

            // Calcula o INSS
            var inss = CalcularInss(payroll.GrossSalary, faixasInss);

            // Calcula o salário base após o desconto do INSS
            var payBase = Math.Round(payroll.GrossSalary - inss, 2);

            // Calcula o IRRF sobre o salário base
            var irrf = CalcularIrrf(payBase, faixasIrrf);

            // Calcula o salário líquido
            var netSalary = Math.Round(payBase - irrf, 2);

            // Preenche os valores no objeto PayrollDto
            payroll.NetSalary = netSalary;
            payroll.INSS = inss;
            payroll.Fgts = Math.Round((payroll.GrossSalary * 8) / 100, 2);

            // Cria uma instância da entidade Payroll
            var payrollEntity = new Payroll
            {
                GrossSalary = payroll.GrossSalary,
                NetSalary = payroll.NetSalary,
                INSS = payroll.INSS,
                Fgts = payroll.Fgts,
                UserId = payroll.UserId,
                // Preencha outros campos da entidade Payroll conforme necessário
            };

            // Adiciona a entidade ao contexto e salva as alterações
            _folhacontext.Add(payrollEntity);
            await _folhacontext.SaveChangesAsync();

            return netSalary;
        }

        private decimal CalcularInss(decimal salario, Dictionary<decimal, decimal> faixas)
        {
            foreach (var faixa in faixas)
            {
                if (salario <= faixa.Key)
                {
                    return Math.Round(salario * faixa.Value, 2);
                }
            }

            return 0;
        }

        private decimal CalcularIrrf(decimal salarioBase, Dictionary<decimal, (decimal Aliquota, decimal Deducao)> faixas)
        {
            foreach (var faixa in faixas)
            {
                if (salarioBase <= faixa.Key)
                {
                    var aliquota = faixa.Value.Aliquota;
                    var deducao = faixa.Value.Deducao;
                    return Math.Round((salarioBase * aliquota) - deducao, 2);
                }
            }

            return 0;
        }


        public async Task<PagedResult<Payroll>> GetPayrollAsync(int pageNumber, int pageSize)
        {
            var result = from payroll in _folhacontext.Payrolls
                         join user in _folhacontext.Users on payroll.UserId equals user.Id
                         orderby user.Name
                         select new Payroll
                         {
                             Id = payroll.Id,
                             GrossSalary = payroll.GrossSalary,
                             NetSalary = payroll.NetSalary,
                             Fgts = payroll.Fgts,
                             INSS = payroll.INSS,
                             UserId = payroll.UserId,
                             UserName = user.Name
                             //CPF = user.CPF,
                         };// fazendo um retorno mas dinamico obs corrigir no user service.
            var count = await result.CountAsync();

            var item = await result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedResult = new PagedResult<Payroll>()
            {
                TotalCount = count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = item
            };


            return pagedResult;
        }

        public Byte[] PayrollPDgf(string HtmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                DocumentTitle = "Holerite"
            };

            var objectSeetings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = HtmlContent,
                WebSettings = { DefaultEncoding = "utf -8" },
                HeaderSettings = { FontSize = 12, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                FooterSettings = { FontSize = 12, Right = "" + DateTime.Now.Year }
            };
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSeetings }
            }; 
            
            var result =  converter.Convert(document);
            return result;
        }

        public async Task<Payroll> FindById(int id)
        {
            var result = from payroll in _folhacontext.Payrolls
                         join user in _folhacontext.Users on payroll.UserId equals user.Id
                         orderby user.Name
                         select new Payroll
                         {
                             Id = payroll.Id,
                             GrossSalary = payroll.GrossSalary,
                             NetSalary = payroll.NetSalary,
                             Fgts = payroll.Fgts,
                             INSS = payroll.INSS,
                             UserId = payroll.UserId,
                             UserName = user.Name
                            // CPF = user.CPF
                         };
            var item = result.FirstOrDefault();
            return item;
        }
    }
}
