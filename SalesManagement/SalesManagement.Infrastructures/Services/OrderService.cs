using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Domains.Entities;

namespace SalesManagement.Infrastructures.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseModel<IEnumerable<OrderViewModel>>> GetAllAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var result = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer?.Name ?? ""
            });
            return new ApiResponseModel<IEnumerable<OrderViewModel>>
            {
                Status = 200,
                Data = result
            };
        }

        public async Task<ApiResponseModel<OrderViewModel>> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return new ApiResponseModel<OrderViewModel> { Status = 404, Message = "Order not found" };
            var vm = new OrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer?.Name ?? ""
            };
            return new ApiResponseModel<OrderViewModel> { Status = 200, Data = vm };
        }

        public async Task<ApiResponseModel<OrderViewModel>> CreateAsync(OrderViewModel model)
        {
            var entity = new Order
            {
                OrderDate = model.OrderDate,
                CustomerId = model.CustomerId
            };
            await _unitOfWork.Orders.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;
            return new ApiResponseModel<OrderViewModel> { Status = 201, Data = model, Message = "Created successfully" };
        }

        public async Task<ApiResponseModel<OrderViewModel>> UpdateAsync(int id, OrderViewModel model)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return new ApiResponseModel<OrderViewModel> { Status = 404, Message = "Order not found" };
            order.OrderDate = model.OrderDate;
            order.CustomerId = model.CustomerId;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<OrderViewModel> { Status = 200, Data = model, Message = "Updated successfully" };
        }

        public async Task<ApiResponseModel<object>> DeleteAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return new ApiResponseModel<object> { Status = 404, Message = "Order not found" };
            _unitOfWork.Orders.Remove(order);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<object> { Status = 200, Message = "Deleted successfully" };
        }
    }
}
