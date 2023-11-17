namespace VoteAPI.Application.DTOs
{
    public class VoteDTO
    {
        public string CPF { get; set; }
        public long ScheduleId { get; set; }
        public string VoteOption { get; set; }
    }
}