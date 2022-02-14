using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator _mediator = null!;
    
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    [HttpOptions]
    public static void Options()
    {
    }
}