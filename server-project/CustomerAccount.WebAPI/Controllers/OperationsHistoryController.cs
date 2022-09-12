using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsHistoryController : ControllerBase
    {
        private readonly IOperationsHistoryService _operationsHistoryService;
        private readonly IMapper _mapper;

        public OperationsHistoryController(IOperationsHistoryService operationsHistoryService)
        {
            _operationsHistoryService = operationsHistoryService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperDtoModels>();
            });
            _mapper = config.CreateMapper();
        }
        // GET api/<OperationsHistory>/5
        [HttpGet("OperationsHistory")]
        public async Task<ActionResult<List<OperationsHistoryDTO>>> Get(int id, int page, int records)
        {
            try
            {
                List<OperationsHistoryModel> lsModel = await _operationsHistoryService.GetOperations(id, page, records);
                List<OperationsHistoryDTO> results = _mapper.Map<List<OperationsHistoryModel>, List<OperationsHistoryDTO>>(lsModel);
                return Ok(results);

            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("{accountID}")]
        public async Task<ActionResult<int>> GetNumOfOperation(int accountID)
        {
            int numOfOperations =await _operationsHistoryService.GetNumOfOperations(accountID);
            if (numOfOperations == 0)
            {
                return NoContent();
            }
            return Ok(numOfOperations);
        }




    }
}
