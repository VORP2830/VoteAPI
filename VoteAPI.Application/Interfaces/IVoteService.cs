using VoteAPI.Application.DTOs;

namespace VoteAPI.Application.Interfaces
{
    public interface IVoteService
    {
        Task<object> Create(VoteDTO model);
        Task FindResult();
    }
}