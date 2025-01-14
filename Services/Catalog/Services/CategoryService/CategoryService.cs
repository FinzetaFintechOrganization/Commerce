
using AutoMapper;
using MongoDB.Driver;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _category;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _category = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var value = _mapper.Map<Category>(createCategoryDto);
        await _category.InsertOneAsync(value);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _category.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        var values = await _category.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
    {
        var values = await _category.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdCategoryDto>(values);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var value = _mapper.Map<Category>(updateCategoryDto);
        await _category.FindOneAndReplaceAsync(x => x.Id == updateCategoryDto.Id,value);
    }
}