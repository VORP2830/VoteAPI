using VoteAPI.Domain.Entities;

namespace VoteAPI.Domain.Interfaces
{
    public interface IVotationResultRepository : IGenericRepository<VotationResult>
    {
        Task<IEnumerable<VotationResult>> GetAll();
        Task<VotationResult> GetByScheduleId(long scheduleId);
    }
}