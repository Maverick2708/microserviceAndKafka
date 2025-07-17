using System.Threading.Tasks;
using SalesManagement.Domains.Entities;
using SalesManagement.Application.Common.Interfaces;

namespace SalesManagement.Infrastructures.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _context;
        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<OrderDetail> OrderDetails { get; }

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
            Customers = new GenericRepository<Customer>(_context);
            Products = new GenericRepository<Product>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderDetails = new GenericRepository<OrderDetail>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
