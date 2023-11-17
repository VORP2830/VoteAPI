namespace VoteAPI.Domain.Entities.Rabbit
{
    public class RabbitMessage
    {
        public long ScheduleId { get; set; }
        public string ScheduleDescription { get; set; }
        public int TotalVoteS { get; set; }
        public int TotalVoteN { get; set; }
        public int TotalVote { get; set; }
        public RabbitMessage(long scheduleId, string scheduleDescription, int totalVoteS, int totalVoteN, int totalVote)
        {
            ScheduleId = scheduleId;
            ScheduleDescription = scheduleDescription;
            TotalVoteS = totalVoteS;
            TotalVoteN = totalVoteN;
            TotalVote = totalVote;
        }
    }
}
