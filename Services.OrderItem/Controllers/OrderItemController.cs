using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.OrderItem.Data;
using Services.OrderItem.Models;
using Services.OrderItem.Models.Dto;

namespace Services.OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public OrderItemController(AppDbContext db, IMapper mapper)
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
                IEnumerable<OrderItems> objList = _db.OrderItems.ToList();
                _response.Result = _mapper.Map<IEnumerable<OrderItemsDto>>(objList);
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
                OrderItems obj = _db.OrderItems.First(u => u.OrderItemId == id);
                _response.Result = _mapper.Map<OrderItemsDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post(OrderItemsDto ProductDto)
        {
            try
            {
                OrderItems product = _mapper.Map<OrderItems>(ProductDto);
                _db.OrderItems.Add(product);
                _db.SaveChanges();


                _db.OrderItems.Update(product);
                _db.SaveChanges();
                _response.Result = _mapper.Map<OrderItemsDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Put(OrderItemsDto ProductDto)
        {
            try
            {
                OrderItems product = _mapper.Map<OrderItems>(ProductDto);
                _db.OrderItems.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<OrderItemsDto>(product);
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
                OrderItems obj = _db.OrderItems.First(u => u.OrderItemId == id);

                _db.OrderItems.Remove(obj);
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
