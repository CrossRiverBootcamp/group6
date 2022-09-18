using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers;

[Authorize(Roles = "customer")]
[Route("api/[controller]")]
[ApiController]
public class OperationsHistoryController : ControllerBase
{
    private readonly IOperationsHistoryService _operationsHistoryService;
    private readonly IMapper _mapper;

    public OperationsHistoryController(IOperationsHistoryService operationsHistoryService)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDtoModels>();
        });
        _mapper = config.CreateMapper();
        _operationsHistoryService = operationsHistoryService;
    }
    // GET api/<OperationsHistory>/5
    [HttpGet("OperationsHistory")]
    public async Task<ActionResult<List<OperationsHistoryDTO>>> Get(int accountID, int page, int records)
    {
        List<OperationsHistoryModel> operationsModelList = await _operationsHistoryService.GetOperations(accountID, page, records);
        List<OperationsHistoryDTO> operations = _mapper.Map<List<OperationsHistoryModel>, List<OperationsHistoryDTO>>(operationsModelList);
        return Ok(operations);
    }
    [HttpGet("{accountID}")]
    public async Task<ActionResult<int>> GetNumOfOperation(int accountID)
    {
        int numOfOperations = await _operationsHistoryService.GetNumOfOperations(accountID);
        return Ok(numOfOperations);
    }
}

