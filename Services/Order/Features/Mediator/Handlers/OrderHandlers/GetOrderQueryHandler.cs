using MediatR;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, List<GetOrderQueryResult>>
{
    private readonly IRepository<Ordering> _repository;

    public GetOrderQueryHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetOrderQueryResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetOrderQueryResult{
            Id = x.Id,
            OrderDate = x.OrderDate,
            TotalPrice = x.TotalPrice,
            UserId = x.UserId,
        }).ToList();
    }
}