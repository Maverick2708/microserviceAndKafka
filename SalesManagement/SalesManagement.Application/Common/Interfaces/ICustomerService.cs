using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;

namespace SalesManagement.Application.Common.Interfaces
{
    public interface ICustomerService
    {
        Task<ApiResponseModel<IEnumerable<CustomerViewModel>>> GetAllAsync();
        Task<ApiResponseModel<CustomerViewModel>> GetByIdAsync(int id);
        Task<ApiResponseModel<CustomerViewModel>> CreateAsync(CustomerViewModel model);
        Task<ApiResponseModel<CustomerViewModel>> UpdateAsync(int id, CustomerViewModel model);
        Task<ApiResponseModel<object>> DeleteAsync(int id);
    }
}
