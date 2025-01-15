using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]

public class OrderDetailController : ControllerBase
{
    private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
    private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
    private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
    private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
    private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;

    public OrderDetailController(GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler, GetOrderDetailQueryHandler getOrderDetailQueryHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
    {
        _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
        _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
        _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
        _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
        _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetail()
    {
        var values = await _getOrderDetailQueryHandler.Handle();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById(Guid id)
    {
        var value = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
    {
        await _createOrderDetailCommandHandler.Handle(command);
        return Ok("Sipariş Detayı Başarıyla Oluşturuldu.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
    {
        await _updateOrderDetailCommandHandler.Handle(command);
        return Ok("Sipariş Detayı Başarıyla Güncellendi.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderDetail(RemoveOrderDetailCommand command)
    {
        await _removeOrderDetailCommandHandler.Handler(command);
        return Ok("Sipariş Detayı Başarıyla Silindi.");
    }
}