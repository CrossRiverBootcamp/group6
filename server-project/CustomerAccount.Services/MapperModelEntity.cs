using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Services.Models;
using Messages.Commands;

namespace CustomerAccount.Services;
public class MapperModelEntity : Profile
{
    public MapperModelEntity()
    {
        CreateMap<RegisterModel, Customer>();
        CreateMap<RegisterModel, Account>();
        CreateMap<Account, AccountModel>()
            .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
            .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
            .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Customer.Email));
        CreateMap<UpdateBalance, OperationsHistoryToAddModel>()
            .ReverseMap();
        CreateMap<OperationsHistory, OperationsHistoryModel>()
            .ForMember(des => des.Amount, opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(des => des.Date, opt => opt.MapFrom(src => src.OperationTime))
            .ReverseMap();

    }
}

