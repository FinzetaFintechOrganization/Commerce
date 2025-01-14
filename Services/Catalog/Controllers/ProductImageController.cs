using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]

public class ProductImageController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImageController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductImage()
    {
        var values = await _productImageService.GetAllProductImageAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdProductImage(string id)
    {
        var value = await _productImageService.GetByIdProductImageAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
    {
        await _productImageService.CreateProductImageAsync(createProductImageDto);
        return Ok("Ürün resmi Başarıyla Eklendi");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        await _productImageService.DeleteProductImageAsync(id);
        return Ok("Ürün resmi Başarıyla Silindi");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
    {
        await _productImageService.UpdateProductImageAsync(updateProductImageDto);
        return Ok("Ürün resmi Başarıyla Güncellendi");
    }
}