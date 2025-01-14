public class GetOrderDetailByIdQueryHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
    {
        
    }

} 