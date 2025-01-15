using MediatR;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IRepository<Ordering> _repository;

    public CreateOrderCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
{
    var order = new Ordering
    {
        OrderDate = DateTime.SpecifyKind(request.OrderDate, DateTimeKind.Utc), 
        UserId = request.UserId,
        TotalPrice = request.TotalPrice,
    };

    await _repository.CreateAsync(order);
}

}