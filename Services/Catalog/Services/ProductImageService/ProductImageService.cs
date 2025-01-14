
using AutoMapper;
using MongoDB.Driver;

public class ProductImageService : IProductImageService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductImage> _productImage;
    public ProductImageService(IMapper mapper,IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _productImage = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
        _mapper = mapper;
    }
    public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
    {
        var value = _mapper.Map<ProductImage>(createProductImageDto);
        await _productImage.InsertOneAsync(value);
    }

    public async Task DeleteProductImageAsync(string id)
    {
        await _productImage.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
    {
        var values = await _productImage.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductImageDto>>(values);
    }

    public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
    {
        var value = await _productImage.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdProductImageDto>(value);
    }

    public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
    {
        var value = _mapper.Map<ProductImage>(updateProductImageDto);
        await _productImage.FindOneAndReplaceAsync(x => x.Id == updateProductImageDto.Id, value);
    }
}