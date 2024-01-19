using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Customer.Data;
using Services.Customer.Models;
using Services.Customer.Models.Dto;
using Services.Customer.RabbitMQSender;

namespace Services.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRabbitMQCustomerMessageSender _messageSender;
        private readonly IConfiguration _configuration;
        private ResponseDto _response;
        private IMapper _mapper;
        public CustomerController(AppDbContext db, IMapper mapper, IRabbitMQCustomerMessageSender messageSender, IConfiguration configuration)
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
                IEnumerable<Customers> objList = _db.Customers.ToList();
                _response.Result = _mapper.Map<IEnumerable<CustomersDto>>(objList);
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
                Customers obj = _db.Customers.First(u => u.CustomerId == id);
                _response.Result = _mapper.Map<CustomersDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post(CustomersDto customersDto)
        {
            try
            {
                Customers customer = _mapper.Map<Customers>(customersDto);
                _db.Customers.Add(customer);
                _db.SaveChanges();


                _db.Customers.Update(customer);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CustomersDto>(customer);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            _messageSender.SendMessage(customersDto, _configuration.GetValue<string>("TopicAndQueueNames:AddCustomerQueue"));
            return _response;
        }


        [HttpPut]
        public ResponseDto Put(CustomersDto CustomerDto)
        {
            try
            {
                Customers customer = _mapper.Map<Customers>(CustomerDto);
                _db.Customers.Update(customer);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CustomersDto>(customer);
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
                Customers obj = _db.Customers.First(u => u.CustomerId == id);

                _db.Customers.Remove(obj);
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
