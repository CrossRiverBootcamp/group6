using AutoMapper;
using Transaction.Services.Models;

namespace Transaction.Services
{
    public class MapModelEntity:Profile
    {
        public MapModelEntity()
        {
            CreateMap<TransactionModel, Transaction.Data.Entities.Transaction>();
        }
    }
}
