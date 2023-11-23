using Course.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class PayrollDto
    {
        [Precision(18, 2)]
        public decimal Desc;
                
        [Required]
        public DateTime Date_of_competence { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Precision(18,2)]
        public decimal NetSalary { get; set; }

        [Precision(18, 2)]
        public decimal Fgts { get; set; }

        [Precision(18, 2)]
        public decimal INSS { get; set; }

    }
}
