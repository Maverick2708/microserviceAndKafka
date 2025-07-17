using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Application.Common.Models;

namespace SalesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<CustomerViewModel>>>> GetAll()
        {
            var response = await _customerService.GetAllAsync();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<CustomerViewModel>>> Get(int id)
        {
            var response = await _customerService.GetByIdAsync(id);
            if (response.Status != 200)
                return StatusCode(response.Status, response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<CustomerViewModel>>> Create(CustomerViewModel model)
        {
            var response = await _customerService.CreateAsync(model);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<CustomerViewModel>>> Update(int id, CustomerViewModel model)
        {
            var response = await _customerService.UpdateAsync(id, model);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<object>>> Delete(int id)
        {
            var response = await _customerService.DeleteAsync(id);
            return StatusCode(response.Status, response);
        }
    }
}
