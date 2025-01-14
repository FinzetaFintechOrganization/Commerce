public class RemoveOrderDetailCommand
{
    public Guid Id { get; set; }
    public RemoveOrderDetailCommand(Guid id)
    {
        Id = id;
    }
}