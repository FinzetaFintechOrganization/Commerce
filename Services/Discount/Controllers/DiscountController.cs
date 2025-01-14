
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCoupon()
    {
        var values = await _discountService.GetAllCouponAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCoupon(Guid id)
    {
        var value = await _discountService.GetByIdCouponAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CreateCouponDto createCouponDto)
    {
        await _discountService.CreateCouponAsync(createCouponDto);
        return Ok("Kupon başarıyla oluşturuldu.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCoupon(Guid id)
    {
        await _discountService.DeleteCouponAsync(id);
        return Ok("Kupon başarıyla silindi.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCoupon(UpdateCouponDto updateCouponDto)
    {
        await _discountService.UpdateCouponAsync(updateCouponDto);
        return Ok("Kupon başarıyla güncellendi.");
    }

}