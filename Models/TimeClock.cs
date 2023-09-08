using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class TimeClock
    {
        [Key]
        
        public int IdTimeclock { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        [Required]
        public DateTimeOffset TimeOffset { get; set;}


        public ICollection<User> Users { get; set; }
        public int UserId { get; set; }

    }
}
