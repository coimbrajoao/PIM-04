using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Status { get; set; }

        public int levelAcesse {get; set; }

        public decimal GrossSalary { get; set; }

        public string Office { get; set; }

        public string Rg { get; set; }
        public string CPF { get; set; } = null!;
        [Required]
        public string Nis { get; set; } = null!;
        [Required]
        public string Pis { get; set; } = null!;

        public DateTime Admissiondate { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }

    }
}
