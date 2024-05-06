using Api.Models.Domain.Entities;
using Api.Models.Dto.Account;
using AutoMapper;

namespace Api.Models.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Account, AccountResponseDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        CreateMap<Tasks, TaskReponseDto>();
    }
}
