using Microsoft.EntityFrameworkCore;
using PatchManager.Data;
using PatchManager.DTOs;
using PatchManager.Models;

namespace PatchManager.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly PatchDbContext _context;

        public CustomerService(PatchDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return await _context.Customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Username = c.Username,
                ServerIp = c.ServerIp,
     
            }).ToListAsync();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Username = customer.Username,
                ServerIp = customer.ServerIp,
                
            };
        }

        public async Task<CustomerDto?> GetByNameAsync(string Username)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == Username);

            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Username = customer.Username,
                ServerIp = customer.ServerIp,
               
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var customer = new Customer
            {
                Username = dto.Username,
                ServerIp = dto.ServerIp,
               
                
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            dto.Id = customer.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            customer.Username = dto.Username;
            customer.ServerIp = dto.ServerIp;
            

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
