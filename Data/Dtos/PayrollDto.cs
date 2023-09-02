using Course.Models;
using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class PayrollDto
    {
         [Required]
        public decimal GrossSalary { get; set; }
   
        [Required]
        public DateTime date_of_competence { get; set; }

        [Required]
        public int UserId { get; set; }
        public decimal NetSalary { get; set; }
        public decimal Fgts { get; set; }
        public decimal INSS { get; set; }

    }
}
