using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;

namespace SalesManagement.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponseModel<IEnumerable<ProductViewModel>>> GetAllAsync();
        Task<ApiResponseModel<ProductViewModel>> GetByIdAsync(int id);
        Task<ApiResponseModel<ProductViewModel>> CreateAsync(ProductViewModel model);
        Task<ApiResponseModel<ProductViewModel>> UpdateAsync(int id, ProductViewModel model);
        Task<ApiResponseModel<object>> DeleteAsync(int id);
    }
}
