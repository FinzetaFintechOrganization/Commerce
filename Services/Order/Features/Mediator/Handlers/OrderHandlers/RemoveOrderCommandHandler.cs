using MediatR;

public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand>
{
    private readonly IRepository<Ordering> _repository;

    public RemoveOrderCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(value);
    }
}
