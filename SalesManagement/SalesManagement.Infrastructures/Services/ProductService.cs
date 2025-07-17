using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Domains.Entities;

namespace SalesManagement.Infrastructures.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseModel<IEnumerable<ProductViewModel>>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var result = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            });
            return new ApiResponseModel<IEnumerable<ProductViewModel>>
            {
                Status = 200,
                Data = result
            };
        }

        public async Task<ApiResponseModel<ProductViewModel>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ApiResponseModel<ProductViewModel> { Status = 404, Message = "Product not found" };
            var vm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
            return new ApiResponseModel<ProductViewModel> { Status = 200, Data = vm };
        }

        public async Task<ApiResponseModel<ProductViewModel>> CreateAsync(ProductViewModel model)
        {
            var entity = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            };
            await _unitOfWork.Products.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;
            return new ApiResponseModel<ProductViewModel> { Status = 201, Data = model, Message = "Created successfully" };
        }

        public async Task<ApiResponseModel<ProductViewModel>> UpdateAsync(int id, ProductViewModel model)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ApiResponseModel<ProductViewModel> { Status = 404, Message = "Product not found" };
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<ProductViewModel> { Status = 200, Data = model, Message = "Updated successfully" };
        }

        public async Task<ApiResponseModel<object>> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ApiResponseModel<object> { Status = 404, Message = "Product not found" };
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<object> { Status = 200, Message = "Deleted successfully" };
        }
    }
}
