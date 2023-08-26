using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class User : IdentityUser
    {
        public User() : base(){}
    }
}
