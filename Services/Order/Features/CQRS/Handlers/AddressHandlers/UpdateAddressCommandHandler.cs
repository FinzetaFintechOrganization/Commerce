public class UpdateAddressCommandHandler
{
    private readonly IRepository<Address> _repository;

    public UpdateAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }
    public async Task Handle(UpdateAddressCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        value.Detail = command.Detail;
        value.District = command.District;
        value.City = command.City;
        value.UserId = command.UserId;
        await _repository.UpdateAsync(value);
    }
}