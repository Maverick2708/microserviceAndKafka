using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Application.Common.Models;

namespace SalesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<OrderViewModel>>>> GetAll()
        {
            var response = await _orderService.GetAllAsync();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<OrderViewModel>>> Get(int id)
        {
            var response = await _orderService.GetByIdAsync(id);
            if (response.Status != 200)
                return StatusCode(response.Status, response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<OrderViewModel>>> Create(OrderViewModel model)
        {
            var response = await _orderService.CreateAsync(model);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<OrderViewModel>>> Update(int id, OrderViewModel model)
        {
            var response = await _orderService.UpdateAsync(id, model);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<object>>> Delete(int id)
        {
            var response = await _orderService.DeleteAsync(id);
            return StatusCode(response.Status, response);
        }
    }
}
