using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Services.Models;


namespace CustomerAccount.Services
{
    public class MapperModelEntity:Profile
    {
        public MapperModelEntity()
        {
            CreateMap<RegisterModel, Customer>();
            CreateMap<RegisterModel, Account>();
           

            CreateMap<Account, AccountModel>()
                .ForMember(des=>des.FirstName,opt=>opt.MapFrom(src=>src.Customer.FirstName))
                .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.Customer.LastName));
        }
    }
}
