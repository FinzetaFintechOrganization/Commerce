public class Coupon
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; }
    public int Rate { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidDate { get; set; }
}