using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _response = new();
            _mapper = mapper;
        }

        [HttpGet]
        public object Get() 
        {
            try
            {
                IEnumerable<Coupon> ObjList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<Coupon>>(ObjList);
            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{Id:int}")]
        public object Get(int Id)
        {
            try
            {
                Coupon Obj = _db.Coupons.First(o => o.CouponId == Id);
                _response.Result = _mapper.Map<CouponDto>(Obj);
            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{Code}")]
        public object GetByCode(string Code)
        {
            try
            {
                Coupon Obj = _db.Coupons.First(o => o.CouponCode.ToLower() == Code.ToLower());
                if(Obj == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(Obj);
            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] CouponDto Coupon)
        {
            try
            {
                Coupon Obj = _mapper.Map<Coupon>(Coupon);
                _db.Coupons.Add(Obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(Obj);
            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto Coupon)
        {
            try
            {
                Coupon Obj = _mapper.Map<Coupon>(Coupon);
                _db.Coupons.Update(Obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(Obj);
            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{Id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int Id)
        {
            try
            {
                Coupon Obj = _db.Coupons.First(x => x.CouponId == Id);
                _db.Coupons.Remove(Obj);
                _db.SaveChanges();

            }
            catch (Exception Ex)
            {
                _response.IsSuccess = false;
                _response.Message = Ex.Message;
            }
            return _response;
        }

    }
}
