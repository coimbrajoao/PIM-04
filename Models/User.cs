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

        public int Matricula { get; set; }

        public string UserEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string Logradouro { get; set; }

        public int Numero { get; set; }

        public string Status { get; set; }
        public string CPF { get; set; }

        public ICollection<Payroll> Payrolls { get; set; }

    }
}
