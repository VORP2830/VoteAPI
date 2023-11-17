namespace VoteAPI.Domain.Entities
{
    public class VotationResult : BaseEntity
    {
        public long ScheduleId { get; protected set; }
        public Schedule Schedule { get; protected set; }
        public bool SendSuccess { get; protected set; }
        protected VotationResult() { }
        public VotationResult(long scheduleId, bool sendSuccess)
        {
            ScheduleId = scheduleId;
            SendSuccess = sendSuccess;
            Active = true;
        }
    }
}