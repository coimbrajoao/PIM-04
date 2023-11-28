using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Publicplace { get; set; }

        public int NumberHome { get; set; }
        public string Neighborhood { get; set; } = null!;//bairro


        public string? Complement { get; set; }

        public string City { get; set; } = null!;//cidade

        
        [MaxLength(2)]
        public string Uf { get; set; } = null!;//estado
        public string Office { get; set; }
        [MaxLength(1)]
        public string LevelAcesse { get; set; }
        [Precision(17, 2)]
        public decimal GrossSalary { get; set; }
        [MaxLength(1)]
        public string Status { get; set; }
    }   
}
