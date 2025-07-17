using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;

namespace SalesManagement.Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponseModel<IEnumerable<OrderViewModel>>> GetAllAsync();
        Task<ApiResponseModel<OrderViewModel>> GetByIdAsync(int id);
        Task<ApiResponseModel<OrderViewModel>> CreateAsync(OrderViewModel model);
        Task<ApiResponseModel<OrderViewModel>> UpdateAsync(int id, OrderViewModel model);
        Task<ApiResponseModel<object>> DeleteAsync(int id);
    }
}
