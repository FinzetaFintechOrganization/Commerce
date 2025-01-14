public class GetOrderDetailByIdQueryHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
    {
        var value = await _repository.GetByIdAsync(query.Id);
        return new GetOrderDetailByIdQueryResult
        {
            Id = value.Id,
            OrderId = value.OrderId,
            ProductAmount = value.ProductAmount,
            ProductPrice = value.ProductPrice,
            ProductId = value.ProductId,
            ProductName = value.ProductName,
            ProductTotalPrice = value.ProductTotalPrice,
        };
    }

} 