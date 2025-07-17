using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;

namespace SalesManagement.Application.Common.Interfaces
{
    public interface IOrderDetailService
    {
        Task<ApiResponseModel<IEnumerable<OrderDetailViewModel>>> GetAllAsync();
        Task<ApiResponseModel<OrderDetailViewModel>> GetByIdAsync(int id);
        Task<ApiResponseModel<OrderDetailViewModel>> CreateAsync(OrderDetailViewModel model);
        Task<ApiResponseModel<OrderDetailViewModel>> UpdateAsync(int id, OrderDetailViewModel model);
        Task<ApiResponseModel<object>> DeleteAsync(int id);
    }
}
