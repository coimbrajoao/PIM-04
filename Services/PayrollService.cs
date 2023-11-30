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
        private readonly FolhaContext _folhacontext;
        private readonly IConverter _converter;

        public PayrollService(FolhaContext folhacontext, IConverter converter)
        {
            _folhacontext = folhacontext;
            _converter = converter;
        }

        public async Task<decimal> PayrollGeneration(PayrollDto payroll)
        {
            try
            {
                var UserAutenticado = await _folhacontext.Users.AnyAsync(x => x.Id == payroll.UserId);
                if (!UserAutenticado)
                {
                    throw new Exception("Usuário não autenticado");
                }

                decimal grossSalary = (
                                     from User in _folhacontext.Users
                                     where User.Id == payroll.UserId
                                     select new
                                     {
                                         grossSalary = User.GrossSalary


                                     }).FirstOrDefault().grossSalary;




                decimal faixa1 = 1903.98M;
                decimal faixa2 = 2826.65M;
                decimal faixa3 = 3751.05M;
                decimal faixa4 = 4664.69M;
                decimal irrf;
                decimal inss;
                decimal fgts;

                // Calcula o INSS
                if (grossSalary <= 1100)
                {
                    inss = Math.Round(grossSalary * 0.075m, 2);
                }
                else if (grossSalary > 1100 && grossSalary <= 2203.48m)
                {
                    inss = Math.Round(grossSalary * 0.09m, 2);
                }
                else if (grossSalary > 2203.48m && grossSalary <= 3305.22m)
                {
                    inss = Math.Round(grossSalary * 0.12m, 2);
                }
                else if (grossSalary > 3305.22m && grossSalary <= 6433.57m)
                {
                    inss = Math.Round(grossSalary * 0.14m, 2);
                }
                else
                {
                    inss = 751.99m;
                }

                // Calcula o salário base após o desconto do INSS
                decimal PayBase = Math.Round(grossSalary - inss, 2);

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
                fgts = Math.Round((grossSalary * 8) / 100, 2);
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
                        GrossSalary = grossSalary,
                        INSS = inss,
                        Fgts = fgts,
                        UserName = user.Name, // Aqui você atribui o UserName com base nos dados do usuário
                        Desc = irrf + inss,
                        Office = user.Office,
                        Date_of_competence = payroll.Date_of_competence
                   

                    };

                    _folhacontext.Add(newPayroll);
                    await _folhacontext.SaveChangesAsync();
                    return newPayroll.Id;
                }
                else
                {
                    throw new Exception("Usuário não encontrado"); // Trate o caso em que o usuário não é encontrado
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

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
                             Desc = Payrol.Desc,
                             Date_of_competence = Payrol.Date_of_competence

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


        public async Task<Payroll> FindByID(int id)
        {
            var result = (
                     from payroll in _folhacontext.Payrolls
                     join user in _folhacontext.Users on payroll.UserId equals user.Id
                     where payroll.Id == id // Adicione esta condição para filtrar pelo ID específico
                     orderby user.Name
                     select new Payroll
                     {
                         Id = payroll.Id,
                         UserId = payroll.UserId,
                         NetSalary = payroll.NetSalary,
                         GrossSalary = payroll.GrossSalary,
                         INSS = payroll.INSS,
                         Fgts = payroll.Fgts,
                         Desc = payroll.Desc,
                         UserName = user.Name,
                         Office = user.Office,
                         Date_of_competence = payroll.Date_of_competence
                     }).FirstOrDefault();

            var item = result;

            return item;
        }

        public byte[] GeneratePdf(string HtmlContent)
        {
            try
            {


                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar o pdf", ex) ;
            }
        }

        public async Task<Payroll> Delete(int id)
        {
            try
            {

                var payroll = await _folhacontext.Payrolls.FindAsync(id) ?? throw new ApplicationException("Folha nao encontrada");
                var currentMonth = DateTime.Now.Month;
                var currentYear = DateTime.Now.Year;

                if (payroll.Date_of_competence.Month != currentMonth || payroll.Date_of_competence.Year != currentYear)
                {
                    throw new Exception("Não é possível excluir a folha de pagamento do mês atual");
                }

                _folhacontext.Payrolls.Remove(payroll);
                await _folhacontext.SaveChangesAsync();
                return payroll;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar a folha de pagamento", ex);

            }
        }
    }

}

