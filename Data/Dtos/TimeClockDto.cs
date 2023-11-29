using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class TimeClockDto
    {
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
    }
}
