using MediatR;

public class GetOrderByIdQuery : IRequest<GetOrderByIdQueryResult>
{
    public Guid Id { get; set; }

    public GetOrderByIdQuery(Guid id)
    {
        Id = id;
    }
}