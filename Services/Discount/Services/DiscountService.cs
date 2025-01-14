
using Dapper;

public class DiscountService : IDiscountService
{
    private readonly AppDbContext _context;

    public DiscountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
    {
        string query = "insert into \"Coupons\" (\"Id\", \"Code\", \"Rate\", \"IsActive\", \"ValidDate\") values (@id,@code,@rate,@isactive,@validdate)";
        var parameters = new DynamicParameters ();
        parameters.Add ("@id", Guid.NewGuid());
        parameters.Add ("@code", createCouponDto.Code);
        parameters.Add ("@rate", createCouponDto.Rate);
        parameters.Add ("@isactive", createCouponDto.IsActive);
        parameters.Add ("@validdate", createCouponDto.ValidDate);
        using (var connection = _context.CreateConnection())    
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteCouponAsync(Guid id)
    {
        string query = "delete from \"Coupons\" where \"Id\"=@id";
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<List<ResultCouponDto>> GetAllCouponAsync()
    {
        string query = "select * from \"Coupons\"";
        using (var connection = _context.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultCouponDto>(query);
            return values.ToList();
        }
    }

    public async Task<GetByIdCouponDto> GetByIdCouponAsync(Guid id)
    {
        string query = "select * from \"Coupons\" where \"Id\"=@id";
        var parameters = new DynamicParameters();
        parameters.Add("@id",id);
        using (var connection = _context.CreateConnection())
        {
            var value = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query,parameters);
            return value;
        }
    }

    public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
    {
        string query = "update \"Coupons\" set \"Code\"=@code, \"Rate\"=@rate, \"IsActive\"=@isactive, \"ValidDate\"=@validdate where \"Id\"=@id";
        var parameters = new DynamicParameters ();
        parameters.Add("@id", updateCouponDto.Id);
        parameters.Add ("@code", updateCouponDto.Code);
        parameters.Add ("@rate", updateCouponDto.Rate);
        parameters.Add ("@isactive", updateCouponDto.IsActive);
        parameters.Add ("@validdate", updateCouponDto.ValidDate);
        using (var connection = _context.CreateConnection())    
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}