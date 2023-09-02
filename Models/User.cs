using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course.Models
{
    public class User : IdentityUser<int>
    {

        public User() : base() { }

        public DateTime Datebirth { get; set; }

        public int Matricula { get; set; }

        public ICollection<Payroll> Payrolls { get; set; }

    }
}
