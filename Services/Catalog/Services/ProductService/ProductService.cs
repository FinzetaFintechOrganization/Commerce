
using AutoMapper;
using MongoDB.Driver;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Product> _products;
    public ProductService(IMapper mapper,IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _products = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        _mapper = mapper;
    }
    public async Task CreateProductAsync(CreateProductDto createProductDto)
    {
        var value = _mapper.Map<Product>(createProductDto);
        await _products.InsertOneAsync(value);
    }

    public async Task DeleteProductAsync(string id)
    {
        await _products.DeleteOneAsync(x => x.Id == id);
    }

    public Task<List<ResultProductDto>> GetAllProductAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdProductDto> GetByIdProductAsync(string id)
    {
        
    }

    public Task UpdateProductAsync(UpdateProductDto updateProductto)
    {
        throw new NotImplementedException();
    }
}