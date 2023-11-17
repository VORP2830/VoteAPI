using Microsoft.EntityFrameworkCore;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.Data.Repositories
{
    public class VotationResultRepository : GenericRepository<VotationResult>, IVotationResultRepository
    {
        private readonly ApplicationDbContext _context;
        public VotationResultRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VotationResult>> GetAll()
        {
            return await _context.VotationResults   
                                    .Where(vr => vr.Active)
                                    .ToListAsync();
        }
        public async Task<VotationResult> GetByScheduleId(long scheduleId)
        {
            return await _context.VotationResults
                                .FirstOrDefaultAsync(vr => vr.Active && 
                                                            vr.ScheduleId == scheduleId);
        }
    }
}