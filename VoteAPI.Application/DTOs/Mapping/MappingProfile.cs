using AutoMapper;
using VoteAPI.Domain.Entities;

namespace VoteAPI.Application.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Schedule, ScheduleDTO>().ReverseMap();
        }
    }
}