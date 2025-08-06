using Microsoft.EntityFrameworkCore;
using PatchManager.Data;
using PatchManager.DTOs;
using PatchManager.Models;

namespace PatchManager.Services
{
    public class QueryService
    {
        private readonly PatchDbContext _context;

        public QueryService(PatchDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerPatchQueryDto>> GetCustomerPatchDetailsAsync(int customerId)
        {
            var details = new List<CustomerPatchQueryDto>();
            var customers = await _context.Customers
                   .Include(c => c.Products)
                   .ToListAsync(); ;

            if (customerId > 0)
            {
                customers = customers
                    .Where(c => c.Id == customerId).
                    ToList();
            }
 

                foreach (var customer in customers)
                {
                    foreach (var product in customer.Products)
                    {
                        // Get latest patch for the product
                        var latestPatch = await _context.Patches
                            .Where(p => p.ProductId == product.Id)
                            .OrderByDescending(p => p.ReleasedOn)
                            .FirstOrDefaultAsync();

                        // Get patch status for this customer and latest patch
                        PatchStatusEnum? patchStatus = null;

                        if (latestPatch != null)
                        {
                            patchStatus = await _context.PatchStatuses
                                .Where(ps => ps.CustomerId == customer.Id && ps.PatchId == latestPatch.Id)
                                .Select(ps => (PatchStatusEnum?)ps.Status)
                                .FirstOrDefaultAsync();
                        }

                        details.Add(new CustomerPatchQueryDto
                        {
                            CustomerName = customer.Username,
                            ProductName = product.Name,
                            LatestPatchVersion = latestPatch?.Version ?? "N/A",
                            LatestPatchDescription = latestPatch?.Description ?? "N/A",
                            LatestPatchReleaseDate = latestPatch?.ReleasedOn ?? DateTime.MinValue,
                            PatchStatus = patchStatus ?? PatchStatusEnum.Pending,
                            StatusText = patchStatus?.ToString() ?? "Pending"
                        });
                    }
                }

            return details;
        }


        public List<AdminPatchQueryDto> GetAdminPatchDetails()
        {
            var patches = _context.Patches
                .Include(p => p.Product)
                .Include(p => p.PatchStatuses)
                .ToList();

            var patchDtos = patches.Select(p => new AdminPatchQueryDto
            {
                ProductName = p.Product.Name,
                PatchName = p.FileName, // or replace with actual PatchName if available
                Version = p.Version,
                Description = p.Description,
                ReleaseDate = p.ReleasedOn,
                CustomersUpdatedCount = p.PatchStatuses
                    .Count(ps => ps.Status == PatchStatusEnum.Installed) // Assuming 'Installed' means updated
            }).ToList();

            return patchDtos;
        }

        public List<PatchNotificationQueryDto> GetPendingPatchesForCustomer(int customerId)
        {
            return _context.PatchStatuses
                .Include(ps => ps.Patch)
                    .ThenInclude(p => p.Product)
                .Where(ps => ps.CustomerId == customerId && ps.Status == PatchStatusEnum.Pending)
                .Select(ps => new PatchNotificationQueryDto
                {
                    ProductName = ps.Patch.Product.Name,
                    PatchName = ps.Patch.FileName,
                    Version = ps.Patch.Version,
                    Description = ps.Patch.Description,
                    ReleaseDate = ps.Patch.ReleasedOn,
                    Status = ps.Status
                })
                .ToList();
        }

        public async Task<List<AgentQueryDto>> GetLatestPatchStatusesForCustomerAsync(int customerId)
        {
            // Get all products for this customer
            var products = await _context.Products
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();

            var result = new List<AgentQueryDto>();

            foreach (var product in products)
            {
                // Manual join between PatchStatus and Patch
                var latestPending = await (
                    from ps in _context.PatchStatuses
                    join p in _context.Patches on ps.PatchId equals p.Id
                    where ps.CustomerId == customerId
                        && ps.Status == PatchStatusEnum.Pending
                        && p.ProductId == product.Id
                    orderby p.ReleasedOn descending
                    select new
                    {
                        ps.PatchId,
                        p.Version
                    })
                    .FirstOrDefaultAsync();

                if (latestPending != null)
                {
                    result.Add(new AgentQueryDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        HasUpdate = true,
                        PatchId = latestPending.PatchId,
                        Version = latestPending.Version,
                        Message = "New update available",
                      
                    });
                }
                else
                {
                    result.Add(new AgentQueryDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        HasUpdate = false,
                        PatchId = null,
                        Version = null,
                        Message = "No updates available"
                    });
                }
            }

            return result;
        }

    }
}
