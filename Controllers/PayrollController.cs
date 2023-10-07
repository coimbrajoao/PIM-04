﻿using Course.Data.Dtos;
using Course.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PayrollController : ControllerBase
    {
        private PayrollService _PayrollService;

        public PayrollController(PayrollService payrollService)
        {
            _PayrollService = payrollService;
        }



        // GET api/<PayrollController>/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int pageSize = 10;
            int page = 1;
            var result = await _PayrollService.GetPayrollAsync(page, pageSize);
            return Ok(result);
        }

        // POST api/<PayrollController>
        [HttpPost]
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


        [HttpPost]
        [Route("id")]
        public async Task<IActionResult> GenerationPayrollPdf(int id)
        {
            var payrollInfo = await _PayrollService.FindByID(id);

            if (payrollInfo == null)
            {
                return NotFound();
            }
            string htmlContent = "<html lang=\"pt-BR\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Holerite</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n        }\r\n        .header {\r\n            text-align: center;\r\n            margin-bottom: 20px;\r\n        }\r\n        .tabela {\r\n            width: 100%;\r\n            border-collapse: collapse;\r\n        }\r\n        th, td {\r\n            border: 1px solid #ccc;\r\n            padding: 10px;\r\n        }\r\n        th {\r\n            background-color: #f2f2f2;\r\n        }\r\n        .div-info {\r\n            margin-bottom: 20px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"header\">\r\n        <h1>Holerite</h1>\r\n    </div>\r\n\r\n    <div class=\"div-info\">\r\n        <h2>Informações do Funcionário</h2>\r\n        <p><strong>Nome:</strong>[NOME]</p>\r\n        <p><strong>CPF:</strong>[CPF]</p>\r\n    </div>\r\n\r\n    <h2>Detalhes de Pagamento</h2>\r\n    <table class=\"tabela\">\r\n        <tr>\r\n            <th>Atributos</th>\r\n            <th>Valor (R$)</th>\r\n        </tr>\r\n        <tr>\r\n            <td>Salário Base</td>\r\n            <td>[SALARIO_BASE]</td>\r\n        </tr>\r\n        <tr>\r\n            <td>FGTS</td>\r\n            <td>[FGTS]</td>\r\n        </tr>\r\n        <tr>\r\n            <td>INSS</td>\r\n            <td>[INSS]</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Total de Descontos</td>\r\n            <td>[TOTAL_DESCONTOS]</td>\r\n        </tr>\r\n        <tr>\r\n            <td><strong>Salário Líquido</strong></td>\r\n            <td><strong>[SALARIO_LIQUIDO]</strong></td>\r\n        </tr>\r\n    </table>\r\n\r\n</body>\r\n</html>\r\n";

            htmlContent = htmlContent.Replace("[NOME]", payrollInfo.UserName);
            //htmlContent = htmlContent.Replace("[CPF]", payrollInfo.CPF);
            htmlContent = htmlContent.Replace("[SALARIO_BASE]", payrollInfo.GrossSalary.ToString("C"));
            htmlContent = htmlContent.Replace("[INSS]", payrollInfo.INSS.ToString("C"));
            htmlContent = htmlContent.Replace("[FGTS]", payrollInfo.Fgts.ToString("C"));
            htmlContent = htmlContent.Replace("[TOTAL_DESCONTOS]", payrollInfo.Desc.ToString("C"));
            htmlContent = htmlContent.Replace("[SALARIO_LIQUIDO]", payrollInfo.NetSalary.ToString("C"));

            byte[] pdfBytes = _PayrollService.GeneratePdf(htmlContent);
            return File(pdfBytes, "application/pdf", "generated.pdf");

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
