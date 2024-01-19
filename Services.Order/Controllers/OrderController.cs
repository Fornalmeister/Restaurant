using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Order.Data;
using Services.Order.Models;
using Services.Order.Models.Dto;
using Services.Order.RabbitMQSender;

namespace Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMQOrderMessageSender _messageSender;
        private IMapper _mapper;
        public OrderController(AppDbContext db, IMapper mapper, IRabbitMQOrderMessageSender messageSender, IConfiguration configuration)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _messageSender = messageSender;
            _configuration = configuration;
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
        public ResponseDto Post(OrdersDto orderDto)
        {
            try
            {
                Orders order = _mapper.Map<Orders>(orderDto);
                _db.Orders.Add(order);
                _db.SaveChanges();


                _db.Orders.Update(order);
                _db.SaveChanges();
                _response.Result = _mapper.Map<OrdersDto>(order);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            _messageSender.SendMessage(orderDto, _configuration.GetValue<string>("TopicAndQueueNames:AddOrderQueue"));
            return _response;
        }


        [HttpPut]
        public ResponseDto Put(OrdersDto orderDto)
        {
            try
            {
                Orders order = _mapper.Map<Orders>(orderDto);
                _db.Orders.Update(order);
                _db.SaveChanges();

                _response.Result = _mapper.Map<OrdersDto>(order);
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
