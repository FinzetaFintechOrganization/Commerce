public interface IDiscountService
{
    Task<List<ResultCouponDto>> GetAllCouponAsync();
    Task CreateCouponAsync(CreateCouponDto createCouponDto);
    Task UpdateCouponAsync(UpdateCouponDto updateCouponDto);
    Task DeleteCouponAsync(Guid id);
    Task<GetByIdCouponDto> GetByIdCouponAsync(Guid id);
}