using Web.Models;

namespace Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string CouponCode);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int Id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto CouponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto CouponDto);
        Task<ResponseDto?> DeleteCouponAsync(int Id);
    }
}
