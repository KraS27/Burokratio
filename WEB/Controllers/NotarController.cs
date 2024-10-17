using Application.DTO.Notar;
using Application.Services;
using Core.Primitives;
using Microsoft.AspNetCore.Mvc;
using WEB.Extensions;

namespace WEB.Controllers
{
    [ApiController]
    [Route("notar")]
    public class NotarController : ControllerBase
    {
        private readonly NotarService _notarService;

        public NotarController(NotarService notarService)
        {
            _notarService = notarService;
        }

        [HttpGet]
        public async Task<IResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _notarService.GetAllAsync(cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{id:guid}")]
        public async Task<IResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _notarService.GetAllAsync(cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<IResult> Add([FromBody] CreateNotarRequest request, CancellationToken cancellationToken)
        {
            Result<Guid> result = await _notarService.AddAsync(request, cancellationToken);          

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }        
    }
}
