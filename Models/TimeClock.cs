using System.ComponentModel.DataAnnotations;

namespace Course.Models
{


    public class TimeClock
    {
        [Key]
        public int IdTimeclock { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        [Required]
        public bool IsClockIn { get; set; } // Indica se é uma marcação de entrada (true) ou saída (false)

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }



    }


}
