using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.WebAPI.DTOs;

namespace Transaction.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private ITransactionService _service;
    private IMapper _mapper;
    private IMessageSession _session;

    public TransactionController(ITransactionService service, IMessageSession session)
    {
        _service = service;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDTOModel>();
        });
        _mapper = config.CreateMapper();
        _session = session;
    }
    [HttpPost]
    public async Task<ActionResult<bool>> AddTransaction(AddTransactionDTO transactionDTO)
    {
        try
        {
            TransactionModel model = _mapper.Map<TransactionModel>(transactionDTO);
            bool result = await _service.AddTransaction(model, _session);
            return Ok(result);
        }
        catch
        {
            return Ok(false);
        }

    }
}

