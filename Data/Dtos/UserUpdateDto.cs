namespace Course.Data.Dtos
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Logadouro { get; set; }

        public int NumberHome { get; set; }
        public string City{ get; set; }
    }   
}
