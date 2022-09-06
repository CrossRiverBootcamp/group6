using AutoMapper;
using Transaction.Services.Models;
using Transaction.WebAPI.DTOs;

namespace Transaction.WebAPI
{
    public class MapperDTOModel:Profile
    {
        public MapperDTOModel()
        {
            CreateMap<AddTransactionDTO, TransactionModel>();
        }
    }
}
