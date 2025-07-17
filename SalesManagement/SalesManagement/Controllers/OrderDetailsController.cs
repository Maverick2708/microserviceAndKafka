using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Application.Common.Models;

namespace SalesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<OrderDetailViewModel>>>> GetAll()
        {
            var response = await _orderDetailService.GetAllAsync();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<OrderDetailViewModel>>> Get(int id)
        {
            var response = await _orderDetailService.GetByIdAsync(id);
            if (response.Status != 200)
                return StatusCode(response.Status, response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<OrderDetailViewModel>>> Create(OrderDetailViewModel model)
        {
            var response = await _orderDetailService.CreateAsync(model);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<OrderDetailViewModel>>> Update(int id, OrderDetailViewModel model)
        {
            var response = await _orderDetailService.UpdateAsync(id, model);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<object>>> Delete(int id)
        {
            var response = await _orderDetailService.DeleteAsync(id);
            return StatusCode(response.Status, response);
        }
    }
}
