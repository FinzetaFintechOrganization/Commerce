public class GetOrderDetailByIdQuery
{
    public Guid Id { get; set; }
    public GetOrderDetailByIdQuery(Guid id)
    {
        Id = id;
    }
}