using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Domains.Entities;

namespace SalesManagement.Infrastructures.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseModel<IEnumerable<CustomerViewModel>>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            var result = customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone
            });
            return new ApiResponseModel<IEnumerable<CustomerViewModel>>
            {
                Status = 200,
                Data = result
            };
        }

        public async Task<ApiResponseModel<CustomerViewModel>> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return new ApiResponseModel<CustomerViewModel> { Status = 404, Message = "Customer not found" };
            var vm = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
            return new ApiResponseModel<CustomerViewModel> { Status = 200, Data = vm };
        }

        public async Task<ApiResponseModel<CustomerViewModel>> CreateAsync(CustomerViewModel model)
        {
            var entity = new Customer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };
            await _unitOfWork.Customers.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;
            return new ApiResponseModel<CustomerViewModel> { Status = 201, Data = model, Message = "Created successfully" };
        }

        public async Task<ApiResponseModel<CustomerViewModel>> UpdateAsync(int id, CustomerViewModel model)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return new ApiResponseModel<CustomerViewModel> { Status = 404, Message = "Customer not found" };
            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<CustomerViewModel> { Status = 200, Data = model, Message = "Updated successfully" };
        }

        public async Task<ApiResponseModel<object>> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return new ApiResponseModel<object> { Status = 404, Message = "Customer not found" };
            _unitOfWork.Customers.Remove(customer);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<object> { Status = 200, Message = "Deleted successfully" };
        }
    }
}
