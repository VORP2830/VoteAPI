using VoteAPI.Application.DTOs;
using VoteAPI.Application.Interfaces;
using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Entities.Rabbit;
using VoteAPI.Domain.Exceptions;
using VoteAPI.Domain.Interfaces;
using VoteAPI.Domain.Interfaces.Rabbit;

namespace VoteAPI.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExternalAPIService _api;
        private readonly IRabbitRepository _rabbitRepository;
        public VoteService(IUnitOfWork unitOfWork, IExternalAPIService api, IRabbitRepository rabbitRepository)
        {
            _unitOfWork = unitOfWork;
            _api = api;
            _rabbitRepository = rabbitRepository;
        }
        public async Task<object> Create(VoteDTO model)
        {
            bool isValidCPF = await _api.CheckCPFIsValid(model.CPF);
            if(!isValidCPF) throw new VoteAPIException("CPF foi invalido pela API");
            Person person = await _unitOfWork.PersonRepository.GetByCPF(model.CPF.Trim());
            if(person is null)
            {
                person = new Person(model.CPF);
                _unitOfWork.PersonRepository.Add(person);
                await _unitOfWork.SaveChangesAsync();
            }
            Schedule schedule = await _unitOfWork.ScheduleRepository.GetById(model.ScheduleId);
            if(schedule is null) throw new VoteAPIException("Pauta inexistente");
            if(schedule.StartingDate > DateTime.Now) throw new VoteAPIException("Pauta não foi aberta para votação");
            if(schedule.FinishingDate < DateTime.Now) throw new VoteAPIException("Pauta encerrada para votação");
            Vote votation = await _unitOfWork.VoteRepository.GetByPersonIdAndScheduleId(model.ScheduleId, person.Id);
            if(votation != null) throw new VoteAPIException("Só é permitido 1 voto por pessoa em cada pauta");
            Vote vote = new Vote(person.Id, model.ScheduleId, model.VoteOption);
            _unitOfWork.VoteRepository.Add(vote);
            await _unitOfWork.SaveChangesAsync();
            return new 
            {
                message = "Voto computado com sucesso"
            };
        }
        public async Task FindResult()
        {
            IEnumerable<VotationResult> votationResults = await _unitOfWork.VotationResultRepository.GetAll();
            List<long> excludedIds = votationResults.Select(v => v.ScheduleId).ToList();

            IEnumerable<Schedule> schedules = await _unitOfWork.ScheduleRepository.GetExcludingIds(excludedIds);

            var shouldCalculate = schedules
                .Where(schedule => schedule.FinishingDate < DateTime.Now)
                .ToList();

            foreach (var schedule in shouldCalculate)
            {
                var result = await CalculateResult(schedule.Id);

                var rabbitMessage = new RabbitMessage(
                    scheduleId: schedule.Id,
                    scheduleDescription: schedule.Description,
                    totalVoteS: result.VoteS,
                    totalVoteN: result.VoteN,
                    totalVote: result.TotalVote
                );

                _rabbitRepository.SendMessage(rabbitMessage);

                var votationResult = new VotationResult(schedule.Id, true);
                _unitOfWork.VotationResultRepository.Add(votationResult);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task<(int VoteS, int VoteN, int TotalVote)> CalculateResult(long scheduleId)
        {
            IEnumerable<Vote> votes = await _unitOfWork.VoteRepository.GetByScheduleId(scheduleId);
            int voteS = 0;
            int voteN = 0;

            foreach (Vote vote in votes)
            {
                if (vote.VoteOption == "S")
                {
                    voteS += 1;
                }
                else if (vote.VoteOption == "N")
                {
                    voteN += 1;
                }
            }
            return (VoteS: voteS, VoteN: voteN, TotalVote: voteS + voteN);
        }

    }
}