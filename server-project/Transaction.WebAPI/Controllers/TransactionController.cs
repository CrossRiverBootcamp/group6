using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using Transaction.WebAPI.DTOs;

namespace Transaction.WebAPI.Controllers;
[Authorize(Roles = "customer")]
[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;
    private readonly IMapper _mapper;
    private readonly IMessageSession _session;

    public TransactionController(ITransactionService service, IMessageSession session)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDTOModel>();
        });
        _mapper = config.CreateMapper();
        _service = service;
        _session = session;
    }
    [HttpPost]
    public async Task<ActionResult<bool>> AddTransaction(AddTransactionDTO transactionDTO)
    {
        TransactionModel model = _mapper.Map<TransactionModel>(transactionDTO);
        bool result = await _service.AddTransaction(model, _session);
        return Ok(result);

    }
}

