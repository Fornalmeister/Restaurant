using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Data;
using Services.Order.Models;
using Services.Order.Models.Dto;

namespace Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public OrderController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Orders> objList = _db.Orders.ToList();
                _response.Result = _mapper.Map<IEnumerable<OrdersDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Orders obj = _db.Orders.First(u => u.OrderId == id);
                _response.Result = _mapper.Map<OrdersDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post(OrdersDto ProductDto)
        {
            try
            {
                Orders product = _mapper.Map<Orders>(ProductDto);
                _db.Orders.Add(product);
                _db.SaveChanges();


                _db.Orders.Update(product);
                _db.SaveChanges();
                _response.Result = _mapper.Map<OrdersDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Put(OrdersDto ProductDto)
        {
            try
            {
                Orders product = _mapper.Map<Orders>(ProductDto);
                _db.Orders.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<OrdersDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Orders obj = _db.Orders.First(u => u.OrderId == id);

                _db.Orders.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
