using Course.Data.Dtos;
using Course.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course.Controllers
{
    [Route("Payroll")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private PayrollService _PayrollService;

        public PayrollController(PayrollService payrollService)
        {
            _PayrollService = payrollService;
        }



        // GET api/<PayrollController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            int pageSize = 10;
            var result = await _PayrollService.GetPayrollAsync(id, pageSize);
            return Ok(result);
        }

        // POST api/<PayrollController>
        [HttpPost]
        [Route ("/genpayroll")]
        public async Task<IActionResult> GenerationPayroll(PayrollDto payrollDto)
        {
            var result = await _PayrollService.PayrollGeneration(payrollDto);
            return Ok(result);
        }

        // PUT api/<PayrollController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PayrollController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
