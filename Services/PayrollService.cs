using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Course.Services
{
    public class PayrollService
    {
        private FolhaContext _folhacontext;
        private readonly IConverter _converter;

        public PayrollService(FolhaContext folhacontext, IConverter converter)
        {
            _folhacontext = folhacontext;
            _converter = converter;
        }

        public async Task<decimal> PayrollGeneration(PayrollDto payroll)
        {
            var UserAutenticado = await _folhacontext.Users.AnyAsync(x => x.Id == payroll.UserId);
            if (!UserAutenticado)
            {
                throw new Exception("Usuário não autenticado");
            }

            var pay = payroll.GrossSalary;
            decimal faixa1 = 1903.98M;
            decimal faixa2 = 2826.65M;
            decimal faixa3 = 3751.05M;
            decimal faixa4 = 4664.69M;
            decimal irrf;
            decimal inss;
            decimal fgts;

            // Calcula o INSS
            if (pay <= 1100)
            {
                inss = Math.Round(pay * 0.075m, 2);
            }
            else if (pay > 1100 && pay <= 2203.48m)
            {
                inss = Math.Round(pay * 0.09m, 2);
            }
            else if (pay > 2203.48m && pay <= 3305.22m)
            {
                inss = Math.Round(pay * 0.12m, 2);
            }
            else if (pay > 3305.22m && pay <= 6433.57m)
            {
                inss = Math.Round(pay * 0.14m, 2);
            }
            else
            {
                inss = 751.99m;
            }

            // Calcula o salário base após o desconto do INSS
            decimal PayBase = Math.Round(pay - inss, 2);

            // Calcula o IRRF sobre o salário base
            if (PayBase <= faixa1)
            {
                irrf = 0;
            }
            else if (PayBase > faixa1 && PayBase <= faixa2)
            {
                irrf = Math.Round(((PayBase * 0.075M) - 142.80M), 2);
            }
            else if (PayBase > faixa2 && PayBase <= faixa3)
            {
                irrf = Math.Round(((PayBase * 0.15m) - 354.08M), 2);
            }
            else if (PayBase > faixa3 && PayBase <= faixa4)
            {
                irrf = Math.Round(((PayBase * 0.225M) - 636.13M), 2);
            }
            else
            {
                irrf = Math.Round((PayBase * 0.275m) - 869.36m, 2);
            }

            // Calcula o salário líquido
            decimal netSalary = Math.Round(PayBase - irrf, 2);

            payroll.NetSalary = netSalary;
            payroll.INSS = inss;

            // Calcula o FGTS
            fgts = Math.Round((pay * 8) / 100, 2);
            payroll.Fgts = fgts;
            payroll.Desc = irrf + inss;

            // Supondo que você já tenha uma instância do contexto do banco de dados (_folhacontext)
            var user = await _folhacontext.Users.FirstOrDefaultAsync(u => u.Id == payroll.UserId);

            if (user != null)
            {
                var newPayroll = new Payroll
                {
                    UserId = payroll.UserId,
                    NetSalary = netSalary,
                    GrossSalary = pay,
                    INSS = inss,
                    Fgts = fgts,
                    UserName = user.Name, // Aqui você atribui o UserName com base nos dados do usuário
                    Desc = irrf + inss
                };

                _folhacontext.Add(newPayroll);
                await _folhacontext.SaveChangesAsync();
                return netSalary;
            }
            else
            {
                throw new Exception("Usuário não encontrado"); // Trate o caso em que o usuário não é encontrado
            }

        }



        public async Task<PagedResult<Payroll>> GetPayrollAsync(int pageNumber, int pageSize)
        {
            var result = from Payrol in _folhacontext.Payrolls
                         join user in _folhacontext.Users on Payrol.UserId equals user.Id
                         orderby user.Name
                         select new Payroll
                         {
                             Id = Payrol.Id,
                             UserId = user.Id,
                             NetSalary = Payrol.NetSalary,
                             GrossSalary = Payrol.GrossSalary,
                             INSS = Payrol.INSS,
                             Fgts = Payrol.Fgts,
                             UserName = user.Name,
                             Desc = Payrol.Desc
                         };
                         // fazendo um retorno mas dinamico obs corrigir no user service.
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

        public async Task<Payroll> FindByID( int id)
        {
            var  result = ( from Payrol in _folhacontext.Payrolls
                         join user in _folhacontext.Users on Payrol.UserId equals user.Id
                         orderby user.Name
                         select new Payroll
                         {
                             Id = Payrol.Id,
                             UserId = user.Id,
                             NetSalary = Payrol.NetSalary,
                             GrossSalary = Payrol.GrossSalary,
                             INSS = Payrol.INSS,
                             Fgts = Payrol.Fgts,
                             UserName = user.Name,
                             Desc = Payrol.Desc
                         }).FirstOrDefault();
            var item = result;

            return item;
        }

        public  byte[] GeneratePdf(string HtmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10},
                DocumentTitle = "Holerite"

            };
            var ObjectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = HtmlContent,
                //Page = _hostingEnvironment.ContentRootPath + "\\htmlpagenow.html",
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 12, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                FooterSettings = { FontSize = 12, Line = true, Right = "" + DateTime.Now.Year }
            };

            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettings }
            };

            return _converter.Convert(document);
        }

    }
}
