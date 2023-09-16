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
        [HttpGet("view")]
        public async Task<IActionResult> Get()
        {
            int pageSize = 10;
            int page = 1;
            var result = await _PayrollService.GetPayrollAsync(page, pageSize);
            return Ok(result);
        }

        // POST api/<PayrollController>
        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePayroll(PayrollDto payrollDto)
        {
            try
            {
                var netSalary = await _PayrollService.PayrollGeneration(payrollDto);
                return Ok(new { NetSalary = netSalary });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        //[HttpPost]
        //[Route("/generationpdf")]
        //public async Task<IActionResult> GenerationPayrollPdf()
        //{
        //    return Ok();
        //}

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
