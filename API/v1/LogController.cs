using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogApi.Contracts;
using AutoWrapper.Wrappers;
using AutoWrapper.Extensions;
using LogApi.DTO;
using AutoMapper;
using LogApi.Domain.Entity;

namespace LogApi.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly IDataLogManager _dataLogManager;
        private readonly IMapper _mapper;

        public LogController(IDataLogManager dataLogManager, IMapper mapper, ILogger<LogController> logger)
        {
            _dataLogManager = dataLogManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] DataLogDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dataLog = _mapper.Map<DataLog>(dto);
                    return new ApiResponse("Created Successfully", await _dataLogManager.AddAsync(dataLog), 201);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, "Error when trying to insert kafka.");
                    throw;
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }
        
    }
}