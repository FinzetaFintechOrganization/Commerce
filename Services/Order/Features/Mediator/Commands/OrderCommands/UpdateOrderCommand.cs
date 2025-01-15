using MediatR;

public class UpdateOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}