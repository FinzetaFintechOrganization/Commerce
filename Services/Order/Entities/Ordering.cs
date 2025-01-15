public class Ordering
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public List<OrderDetail> OrderDetails { get; set; }
}