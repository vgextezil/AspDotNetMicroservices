using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository: IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = _configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection =
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});
        if (coupon == null)
        {
            return new Coupon { ProductName = "No discount", Amount = 0, Description = "" };
        }

        return coupon;
    }

    public Task<bool> CreateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDiscount(string productName)
    {
        throw new NotImplementedException();
    }
}