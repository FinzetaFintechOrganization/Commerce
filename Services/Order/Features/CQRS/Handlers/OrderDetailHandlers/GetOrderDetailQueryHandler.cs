public class GetOrderDetailQueryHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public GetOrderDetailQueryHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetOrderDetailQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetOrderDetailQueryResult{
            OrderId = x.OrderId,
            Id = x.Id,
            ProductAmount = x.ProductAmount,
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            ProductPrice = x.ProductPrice,
            ProductTotalPrice = x.ProductTotalPrice
        }).ToList();
    }
}