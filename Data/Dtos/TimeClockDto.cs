namespace Course.Data.Dtos
{
    public class TimeClockDto
    {
        public DateTime? Time { get; set;}
        public DateTimeOffset? TimeOffset { get; set;}

        public int UserId { get; set;}
    }
}
