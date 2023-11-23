using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext appDbContext, IMapper mapper)
        {
            _db = appDbContext;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Product> ObjList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<Product>>(ObjList);
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
                Product Obj = _db.Products.First(o => o.ProductId == Id);
                _response.Result = _mapper.Map<ProductDto>(Obj);
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
