public class GetAddressQueryHandler
{
    private readonly IRepository<Address> _repository;

    public GetAddressQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAddressQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select( x=> new GetAddressQueryResult{
            City = x.City,
            Detail = x.Detail,
            District = x.District,
            Id = x.Id,
            UserId = x.UserId
        }).ToList();
    }
}