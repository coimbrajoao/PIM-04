using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Course.Models
{
    public class User : IdentityUser<int>
    {
        public User() : base() { }

        public string Name { get; set; }

        public DateTime Datebirth { get; set; }

        public int Registration { get; set; }

        public string UserEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string Publicplace { get; set; }

        public int Number { get; set; }

        [MaxLength(1)]
        public string Status { get; set; }

        [MaxLength(1)]
      
        public string LevelAcesse {get; set; }

        [Precision(17,2)]
        public decimal GrossSalary { get; set; }

        public string Office { get; set; }

        
        public string Neighborhood { get; set; } = null!;//bairro
        [MaxLength(11)]
        [Required]
        public string Rg { get; set; }
        [MaxLength(11)]
        [Required]
        public string CPF { get; set; } = null!;
        [Required]
        [MaxLength(11)]
        public string Nis { get; set; } = null!;
        [Required]
        [MaxLength(11)]
        public string Pis { get; set; } = null!;

        public string? Complement { get; set; }

        [Required]
        public string City { get; set; } = null!;//cidade

        [Required]
        [MaxLength(2)]
        public string Uf { get; set; } = null!;//estado
        public DateTime Admissiondate { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }

    }
}
