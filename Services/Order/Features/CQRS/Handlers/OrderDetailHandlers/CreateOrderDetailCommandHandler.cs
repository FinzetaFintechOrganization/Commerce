public class CreateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task Handle(CreateOrderDetailCommand command)
    {
        await _repository.CreateAsync(new OrderDetail{
            OrderId = command.OrderId,
            ProductAmount = command.ProductAmount,
            ProductName = command.ProductName,
            ProductId = command.ProductId,
            ProductTotalPrice = command.ProductTotalPrice,
            ProductPrice = command.ProductPrice,
        });
    }
}