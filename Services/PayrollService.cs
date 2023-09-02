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
            var pay = payroll.GrossSalary;
            decimal faixa1 = 1903.98M;
            decimal faixa2 = 2826.65M;
            decimal faixa3 = 3751.05M;
            decimal faixa4 = 4664.69M;
            decimal irrf;
            decimal inss;
            decimal fgts;

            fgts = (pay * 8) / 100;

            if (pay <= 1100)
            {
                inss = pay * 0.075m;
            }
            else if (pay > 1100 && pay <= 2203.48m)
            {
                inss = pay * 0.09m;
            }
            else if (pay > 2203.48m && pay <= 3305.22m)
            {
                inss = pay * 0.12m;
            }
            else if (pay > 3305.22m && pay <= 6433.57m)
            {
                inss = pay * 0.14m;
            }
            else
            {
                inss = 751.99m;
            }

            pay = pay - inss;

            //irrf
            if (pay <= faixa1)
            {
                irrf = 0;
            }
            else if (pay > faixa1 && pay <= faixa2)
            {
                irrf = ((pay * 0.075M) - 142.80M);
            }
            else if (pay > faixa2 && pay <= faixa3)
            {
                irrf = ((pay * 0.15m) - 354.08M);
            }
            else if (pay >faixa3 && pay <= faixa4)
            {
                irrf = ((pay * 0.225M) - 636.13M);
            }
            else
            {
                irrf = (pay * 0.275m) - 869.36m;
            }

            pay = pay - irrf;

            payroll.NetSalary = pay;
            payroll.INSS = inss;
            payroll.Fgts = fgts;

            var payrollEntity = new Payroll
            {
                GrossSalary = payroll.GrossSalary,
                NetSalary = payroll.NetSalary,
                INSS = payroll.INSS,
                Fgts = payroll.Fgts,
                // Preencha outros campos da entidade Payroll conforme necessário
            };

            _folhacontext.Add(payrollEntity);
            await _folhacontext.SaveChangesAsync();
            return pay;
        }

        public async Task<PagedResult<Payroll>> GetPayrollAsync(int pageNumber,int pageSize)
        {
            var result =  _folhacontext.Payrolls.OrderBy(x =>x.Id);// fazendo um retorno mas dinamico obs corrigir no user service.
            var count = await result.CountAsync();

            var item = await result.Skip((pageNumber - 1)* pageSize).Take(pageSize).ToListAsync();

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
