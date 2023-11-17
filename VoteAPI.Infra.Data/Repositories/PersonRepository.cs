using Microsoft.EntityFrameworkCore;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace VoteAPI.Infra.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.People
                                    .Where(p => p.Active)
                                    .ToListAsync();
        }
        public async Task<Person> GetByCPF(string cpf)
        {
            return await _context.People
                                    .FirstOrDefaultAsync(p => p.Active && 
                                                                p.CPF == cpf);
        }
        public async Task<Person> GetById(long id)
        {
            return await _context.People
                                    .FirstOrDefaultAsync(p => p.Active && 
                                                                p.Id == id);
        }
    }
}