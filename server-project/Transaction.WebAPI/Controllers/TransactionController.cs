using AutoMapper;
using Messages.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.WebAPI.DTOs;

namespace Transaction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _service;
        private IMapper _mapper;

        public TransactionController(ITransactionService service)
        {
            _service = service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperDTOModel>();
            });
            _mapper = config.CreateMapper();
        }
        [HttpPost]
        public async Task<ActionResult> Add(AddTransactionDTO transactionDTO)
        {
            TransactionModel model = _mapper.Map<TransactionModel>(transactionDTO);
            int id = await _service.AddTransaction(model);
            TransactionAdded transactionEvent = new TransactionAdded
            {
                TransactionID =id,
                FromAccountID = model.FromAccountId,
                ToAccountID =model.ToAccountID,
                Amount=model.Amount
                
            };
            /*    publish(transactionEvent);*/
            return Ok();

        }
    }
}
