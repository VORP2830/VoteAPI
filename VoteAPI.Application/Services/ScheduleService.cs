using AutoMapper;
using VoteAPI.Application.DTOs;
using VoteAPI.Application.Interfaces;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Exceptions;
using VoteAPI.Domain.Interfaces;

namespace VoteAPI.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ScheduleDTO>> GetAll()
        {
            IEnumerable<Schedule> schedules = await _unitOfWork.ScheduleRepository.GetAll();
            return _mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
        }
        public async Task<ScheduleDTO> GetById(long id)
        {
            Schedule schedule = await _unitOfWork.ScheduleRepository.GetById(id);
            return _mapper.Map<ScheduleDTO>(schedule);
        }
        public async Task<ScheduleDTO> Create(ScheduleDTO model)
        {
            if(model.FinishingDate is null) 
            {
                model.FinishingDate = model.StartingDate.AddMinutes(1);
            }
            Schedule schedule = _mapper.Map<Schedule>(model);
            _unitOfWork.ScheduleRepository.Add(schedule);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(schedule);
        }
        public async Task<ScheduleDTO> Update(ScheduleDTO model)
        {
            Schedule schedule = await _unitOfWork.ScheduleRepository.GetById(model.Id);
            if(schedule is null) throw new VoteAPIException("Pauta não encontrada");
            IEnumerable<Vote> votes = await _unitOfWork.VoteRepository.GetByScheduleId(model.Id);
            if(votes != null) throw new VoteAPIException("Não é possivel alterar a pauta pois já existem votos computados");
            _mapper.Map(schedule, model);
            _unitOfWork.ScheduleRepository.Update(schedule);
            await _unitOfWork.SaveChangesAsync();
            return model;
        }
        public async Task<ScheduleDTO> Delete(long id)
        {
            Schedule schedule = await _unitOfWork.ScheduleRepository.GetById(id);
            if(schedule is null) throw new VoteAPIException("Pauta não encontrada");
            IEnumerable<Vote> votes = await _unitOfWork.VoteRepository.GetByScheduleId(id);
            if(votes != null) throw new VoteAPIException("Não é possivel deletar a pauta pois já existem votos computados");
            schedule.SetActive(false);
            _unitOfWork.ScheduleRepository.Update(schedule);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ScheduleDTO>(schedule);
        }
    }
}