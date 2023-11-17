using VoteAPI.Domain.Entities;

namespace VoteAPI.Domain.Interfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetById(long id);
        Task<Person> GetByCPF(string cpf);
    }
}