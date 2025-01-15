using MediatR;

public class RemoveOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public RemoveOrderCommand(Guid id)
    {
        Id = id;
    }
}