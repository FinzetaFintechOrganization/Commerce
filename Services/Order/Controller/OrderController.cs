
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrder()
    {
        var values = await _mediator.Send(new GetOrderQuery());
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var value = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        await _mediator.Send(command);
        return Ok("Sipariş başarıyla oluşturuldu.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
    {
        await _mediator.Send(command);
        return Ok("Sipariş başarıyla güncellendi.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _mediator.Send(new RemoveOrderCommand(id));
        return Ok("Sipariş başarıyla silindi.");
    }
}