namespace VoteAPI.Application.DTOs
{
    public class ScheduleDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? FinishingDate { get; set; }
    }
}