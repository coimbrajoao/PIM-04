using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course.Models
{
    public class Payroll
    {

        [Key]
        public int Id { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public decimal Fgts { get; set; }
        public decimal INSS { get; set; }
        
        public DateTime date_of_competence { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        
        public User User { get; set; }
        
        public string UserName { get; set; }
        public string CPF { get; set; }
    }
}
