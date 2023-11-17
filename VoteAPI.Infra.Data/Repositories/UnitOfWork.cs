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
        public IPersonRepository PersonRepository => new PersonRepository(_context);
        public IScheduleRepository ScheduleRepository => new ScheduleRepository(_context);
        public IVoteRepository VoteRepository => new VoteRepository(_context);
        public IVotationResultRepository VotationResultRepository => new VotationResultRepository(_context);
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}