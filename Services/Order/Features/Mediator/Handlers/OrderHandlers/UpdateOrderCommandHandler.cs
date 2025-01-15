using MediatR;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IRepository<Ordering> _repository;

    public UpdateOrderCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
        value.OrderDate = DateTime.SpecifyKind(request.OrderDate, DateTimeKind.Utc);
        value.TotalPrice = request.TotalPrice;
        value.UserId = request.UserId;
        await _repository.UpdateAsync(value);
    }
}