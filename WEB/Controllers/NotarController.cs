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
        public async Task<IResult> GetAll([FromQuery]Pagination pagination, CancellationToken cancellationToken)
        {
            var result = await _notarService.GetAllAsync(pagination, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpGet("{id:guid}")]
        public async Task<IResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _notarService.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<IResult> Add([FromBody] CreateNotarRequest request, CancellationToken cancellationToken)
        {
            Result<Guid> result = await _notarService.AddAsync(request, cancellationToken);          

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }        
        
        [HttpPut]
        public async Task<IResult> Update([FromBody] UpdateNotarRequest request, CancellationToken cancellationToken)
        {
            Result result = await _notarService.UdpateAsync(request, cancellationToken);          

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        }   
        
        [HttpDelete("{id:guid}")]
        public async Task<IResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            Result result = await _notarService.DeleteAsync(id, cancellationToken);          

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        }   
    }
}
