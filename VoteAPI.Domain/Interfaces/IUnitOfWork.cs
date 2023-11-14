namespace VoteAPI.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        IVoteRepository VoteRepository { get; }
        Task<bool> SaveChangesAsync(); 
    }
}