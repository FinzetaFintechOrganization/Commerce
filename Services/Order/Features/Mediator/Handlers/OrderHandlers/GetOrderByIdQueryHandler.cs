using MediatR;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResult>
{
    private readonly IRepository<Ordering> _repository;

    public GetOrderByIdQueryHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task<GetOrderByIdQueryResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
        return new GetOrderByIdQueryResult{
            Id = value.Id,
            OrderDate = value.OrderDate,
            TotalPrice = value.TotalPrice,
            UserId = value.UserId,
        };
    }
}