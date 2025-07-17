using System.Threading.Tasks;
using SalesManagement.Domains.Entities;
using SalesManagement.Application.Common.Interfaces;

namespace SalesManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderDetail> OrderDetails { get; }
        Task<int> SaveChangesAsync();
    }
}
