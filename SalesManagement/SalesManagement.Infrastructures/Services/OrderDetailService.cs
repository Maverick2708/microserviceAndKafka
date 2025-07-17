using SalesManagement.Application.Common.Interfaces;
using SalesManagement.Application.Common.Models;
using SalesManagement.Application.Common.ViewModels;
using SalesManagement.Domains.Entities;

namespace SalesManagement.Infrastructures.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseModel<IEnumerable<OrderDetailViewModel>>> GetAllAsync()
        {
            var details = await _unitOfWork.OrderDetails.GetAllAsync();
            var result = details.Select(d => new OrderDetailViewModel
            {
                Id = d.Id,
                OrderId = d.OrderId,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                ProductName = d.Product?.Name ?? ""
            });
            return new ApiResponseModel<IEnumerable<OrderDetailViewModel>>
            {
                Status = 200,
                Data = result
            };
        }

        public async Task<ApiResponseModel<OrderDetailViewModel>> GetByIdAsync(int id)
        {
            var detail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
            if (detail == null)
                return new ApiResponseModel<OrderDetailViewModel> { Status = 404, Message = "OrderDetail not found" };
            var vm = new OrderDetailViewModel
            {
                Id = detail.Id,
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice,
                ProductName = detail.Product?.Name ?? ""
            };
            return new ApiResponseModel<OrderDetailViewModel> { Status = 200, Data = vm };
        }

        public async Task<ApiResponseModel<OrderDetailViewModel>> CreateAsync(OrderDetailViewModel model)
        {
            var entity = new OrderDetail
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            };
            await _unitOfWork.OrderDetails.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            model.Id = entity.Id;
            return new ApiResponseModel<OrderDetailViewModel> { Status = 201, Data = model, Message = "Created successfully" };
        }

        public async Task<ApiResponseModel<OrderDetailViewModel>> UpdateAsync(int id, OrderDetailViewModel model)
        {
            var detail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
            if (detail == null)
                return new ApiResponseModel<OrderDetailViewModel> { Status = 404, Message = "OrderDetail not found" };
            detail.OrderId = model.OrderId;
            detail.ProductId = model.ProductId;
            detail.Quantity = model.Quantity;
            detail.UnitPrice = model.UnitPrice;
            _unitOfWork.OrderDetails.Update(detail);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<OrderDetailViewModel> { Status = 200, Data = model, Message = "Updated successfully" };
        }

        public async Task<ApiResponseModel<object>> DeleteAsync(int id)
        {
            var detail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
            if (detail == null)
                return new ApiResponseModel<object> { Status = 404, Message = "OrderDetail not found" };
            _unitOfWork.OrderDetails.Remove(detail);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponseModel<object> { Status = 200, Message = "Deleted successfully" };
        }
    }
}
