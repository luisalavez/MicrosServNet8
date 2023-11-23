using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto CouponDto)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApyType.POST,
                Data = CouponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int Id)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApyType.DELETE, 
                Url = SD.CouponAPIBase + "/api/coupon/" + Id 
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApyType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string CouponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApyType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + CouponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int Id)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApyType.GET, 
                Url = SD.CouponAPIBase + "/api/coupon/" + Id 
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto CouponDto)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApyType.PUT, 
                Data = CouponDto,
                Url = SD.CouponAPIBase + "/api/coupon" 
            });
        }
    }
}
