using VoteAPI.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IPersonRepository PersonRepository => throw new NotImplementedException();

        public IScheduleRepository ScheduleRepository => throw new NotImplementedException();

        public IVoteRepository VoteRepository => throw new NotImplementedException();

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}