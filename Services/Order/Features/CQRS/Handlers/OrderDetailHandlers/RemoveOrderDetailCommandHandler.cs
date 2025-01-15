public class RemoveOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }
    public async Task Handler(RemoveOrderDetailCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        await _repository.DeleteAsync(value);
    }
}