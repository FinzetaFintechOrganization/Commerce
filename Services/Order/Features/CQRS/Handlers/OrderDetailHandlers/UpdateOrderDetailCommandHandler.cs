public class UpdateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task Handle(UpdateOrderDetailCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        value.ProductName = command.ProductName;
        value.ProductId = command.ProductId;
        value.ProductPrice = command.ProductPrice;
        value.ProductTotalPrice = command.ProductTotalPrice;
        value.OrderId = command.OrderId;
        value.ProductAmount = command.ProductAmount;
        await _repository.UpdateAsync(value);
    }
}