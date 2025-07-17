using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Application.Common.Models;

namespace SalesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<ProductViewModel>>>> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<ProductViewModel>>> Get(int id)
        {
            var response = await _productService.GetByIdAsync(id);
            if (response.Status != 200)
                return StatusCode(response.Status, response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseModel<ProductViewModel>>> Create(ProductViewModel model)
        {
            var response = await _productService.CreateAsync(model);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<ProductViewModel>>> Update(int id, ProductViewModel model)
        {
            var response = await _productService.UpdateAsync(id, model);
            return StatusCode(response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<object>>> Delete(int id)
        {
            var response = await _productService.DeleteAsync(id);
            return StatusCode(response.Status, response);
        }
    }
}
