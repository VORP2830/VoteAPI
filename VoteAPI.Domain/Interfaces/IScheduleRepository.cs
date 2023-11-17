using VoteAPI.Domain.Entities;

namespace VoteAPI.Domain.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetById(long id);
        Task<List<Schedule>> GetExcludingIds(List<long> excludedIdIds);
    }
}