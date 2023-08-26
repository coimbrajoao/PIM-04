using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class CreateUserDto
    {
        [Required] // user name obrigatorio
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]// camo tratado como senha
        public string Password { get; set; }

        [Required]
        [Compare("Password")]//confrimando a senha.
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime DateBirth { get; set; }


        [Required]
        public string CPF { get; set; }

        



    }
}
