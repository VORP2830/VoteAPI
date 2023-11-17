

using Microsoft.EntityFrameworkCore;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.Data.Repositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly ApplicationDbContext _context;
        public VoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Vote>> GetAll()
        {
            return await _context.Votes
                                    .Where(v => v.Active)
                                    .ToListAsync();
        }
        public async Task<Vote> GetById(long id)
        {
            return await _context.Votes
                                    .FirstOrDefaultAsync(v => v.Active && 
                                                                v.Id == id);
        }
        public async Task<IEnumerable<Vote>> GetByPersonId(long personId)
        {
            return await _context.Votes.Where(v => v.Active && 
                                                    v.PersonId == personId)
                                        .ToListAsync();
        }
        public async Task<Vote> GetByPersonIdAndScheduleId(long scheduleId, long personId)
        {
            return await _context.Votes
                                    .FirstOrDefaultAsync(v => v.Active && 
                                                            v.PersonId == personId && 
                                                            v.ScheduleId == scheduleId);
        }
        public async Task<IEnumerable<Vote>> GetByScheduleId(long scheduleId)
        {
            return await _context.Votes
                                    .Where(v => v.Active && 
                                            v.ScheduleId == scheduleId)
                                    .ToListAsync();
        }
    }
}