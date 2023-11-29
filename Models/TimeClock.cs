using System.ComponentModel.DataAnnotations;

namespace Course.Models
{


    public class TimeClock
    {
        [Key]
        public int IdTimeclock { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }



    }


}
