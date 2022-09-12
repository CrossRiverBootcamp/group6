using AutoMapper;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;

namespace CustomerAccount.WebAPI;
public class MapperDtoModels : Profile
{
    public MapperDtoModels()
    {
        CreateMap<RegisterDTO, RegisterModel>();
        CreateMap<AccountModel, AccountInfoDTO>();
        CreateMap<AccountModel, ForeignAccountDetailsDTO>();
        CreateMap<LoginResultModel, LoginResultDTO>();
        CreateMap<OperationsHistoryModel, OperationsHistoryDTO>();
    }
}

