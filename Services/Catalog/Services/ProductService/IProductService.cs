public interface IProductService
{
    Task<List<ResultProductDto>> GetAllProductAsync();
    Task CreateProductAsync(CreateProductDto createProductDto);
    Task UpdateProductAsync(UpdateProductDto updateProductto);
    Task DeleteProductAsync(string id);
    Task<GetByIdProductDto> GetByIdProductAsync(string id);
}