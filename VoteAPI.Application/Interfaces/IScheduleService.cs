using VoteAPI.Application.DTOs;

namespace VoteAPI.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetAll();
        Task<ScheduleDTO> GetById(long id);
        Task<ScheduleDTO> Create(ScheduleDTO model);
        Task<ScheduleDTO> Update(ScheduleDTO model);
        Task<ScheduleDTO> Delete(long id);
    }
}