
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

    public async Task<List<ResultProductDto>> GetAllProductAsync()
    {
        var values = await _products.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductDto>>(values);
    }

    public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
    {
        var value = await _products.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdProductDto>(value);
    }

    public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        var value = _mapper.Map<Product>(updateProductDto);
        await _products.FindOneAndReplaceAsync(x => x.Id == updateProductDto.Id, value);
    }
}