using System.ComponentModel.DataAnnotations;

namespace Course.Data.Dtos
{
    public class TimeClockDto
    {
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public bool IsClockIn { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
