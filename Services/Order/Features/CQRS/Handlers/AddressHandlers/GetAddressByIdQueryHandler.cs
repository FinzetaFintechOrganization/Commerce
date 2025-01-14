public class GetAddressByIdQueryHandler
{
    private readonly IRepository<Address> _repository;

    public GetAddressByIdQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery query)
    {
        var value = await _repository.GetByIdAsync(query.Id);
        return new GetAddressByIdQueryResult{
            City = value.City,
            Detail = value.Detail,
            District = value.District,
            UserId = value.UserId,
            Id = value.Id
        };
    }
}