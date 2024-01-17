using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Customer.Data;
using Services.Customer.Models;
using Services.Customer.Models.Dto;

namespace Services.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CustomerController(AppDbContext db, IMapper mapper)
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
        public ResponseDto Post(CustomersDto ProductDto)
        {
            try
            {
                Customers product = _mapper.Map<Customers>(ProductDto);
                _db.Customers.Add(product);
                _db.SaveChanges();


                _db.Customers.Update(product);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CustomersDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Put(CustomersDto ProductDto)
        {
            try
            {
                Customers product = _mapper.Map<Customers>(ProductDto);
                _db.Customers.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CustomersDto>(product);
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
