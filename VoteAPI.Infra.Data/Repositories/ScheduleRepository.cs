

using Microsoft.EntityFrameworkCore;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.Data.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Schedule>> GetAll()
        {
            return await _context.Schedules
                                    .Where(s => s.Active)
                                    .ToListAsync();
        }
        public async Task<Schedule> GetById(long id)
        {
            return await _context.Schedules
                                    .FirstOrDefaultAsync(s => s.Active && 
                                                                s.Id == id);
        }
        public async Task<List<Schedule>> GetExcludingIds(List<long> excludedIdIds)
        {
            return await _context.Schedules
                .Where(s => s.Active && !excludedIdIds.Contains(s.Id))
                .ToListAsync();
        }

    }
}