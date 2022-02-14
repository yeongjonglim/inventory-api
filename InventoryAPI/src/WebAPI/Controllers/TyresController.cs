using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Application.Common.Interfaces;
using InventoryAPI.Application.Tyres.Queries;

namespace InventoryAPI.WebAPI.Controllers;

[Authorize]
public class TyresController : ApiControllerBase
{
    private readonly ICurrentUserService _userService;
    private readonly ILogger<TyresController> _logger;

    public TyresController(ICurrentUserService userService, ILogger<TyresController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<TyrePriceResponse>> Get()
    {
        _logger.LogInformation("{User} is getting tyres", _userService.UserId);
        return await Mediator.Send(new GetTyresQuery());
    }
    
    [HttpGet("ThrowException")]
    public Task<ActionResult<TyrePriceResponse>> ThrowException()
    {
        throw new Exception("This is a test exception");
    }

    // [HttpGet("{id}")]
    // public async Task<FileResult> Get(int id)
    // {
    //     var vm = await Mediator.Send(new ExportTodosQuery {ListId = id});
    //
    //     return File(vm.Content, vm.ContentType, vm.FileName);
    // }

    // [HttpPost]
    // public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
    // {
    //     return await Mediator.Send(command);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
    // {
    //     if (id != command.Id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     await Mediator.Send(command);
    //
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     await Mediator.Send(new DeleteTodoListCommand {Id = id});
    //
    //     return NoContent();
    // }
}