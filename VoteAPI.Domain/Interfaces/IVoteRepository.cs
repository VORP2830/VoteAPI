using VoteAPI.Domain.Entities;

namespace VoteAPI.Domain.Interfaces
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<IEnumerable<Vote>> GetAll();
        Task<Vote> GetById(long id);
        Task<Vote> GetByPersonIdAndScheduleId(long scheduleId, long personId);
        Task<IEnumerable<Vote>> GetByScheduleId(long scheduleId);
        Task<IEnumerable<Vote>> GetByPersonId(long personId);
    }
}