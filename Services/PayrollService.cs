using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Course.Services
{
    public class PayrollService
    {
        private FolhaContext _folhacontext;


        public PayrollService(FolhaContext folhacontext)
        {
            _folhacontext = folhacontext;
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

            fgts = Math.Round((pay * 8) / 100, 2);

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

            decimal PayBase = pay - inss;

            //irrf
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

            decimal netSalary = Math.Round(PayBase - irrf, 2);

            payroll.NetSalary = netSalary;
            payroll.INSS = inss;
            payroll.Fgts = fgts;

            var payrollEntity = new Payroll
            {
                GrossSalary = payroll.GrossSalary,
                NetSalary = payroll.NetSalary,
                INSS = payroll.INSS,
                Fgts = payroll.Fgts,
                UserId = payroll.UserId,
                // Preencha outros campos da entidade Payroll conforme necessário
            };

            _folhacontext.Add(payrollEntity);
            await _folhacontext.SaveChangesAsync();
            return pay;
        }


        public async Task<PagedResult<Payroll>> GetPayrollAsync(int pageNumber, int pageSize)
        {
            var result = _folhacontext.Payrolls.OrderBy(x => x.Id);// fazendo um retorno mas dinamico obs corrigir no user service.
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

    }
}
