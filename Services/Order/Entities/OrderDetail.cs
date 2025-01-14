public class OrderDetail
{
    public Guid Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductAmount { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public Guid OrderId { get; set; }
    public Ordering Ordering { get; set; }
}