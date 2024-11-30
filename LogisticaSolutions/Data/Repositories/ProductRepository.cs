using System.Linq.Dynamic.Core;
using EFCore.BulkExtensions;
using LogisticaSolutions.Context;
using LogisticaSolutions.Contracts.Repository;
using LogisticaSolutions.Entities;


namespace LogisticaSolutions.Implement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task BulkInsertInChunksAsync(IEnumerable<Product> entities, int chunkSize = 10000)
        {
            foreach (var chunk in entities.Chunk(chunkSize))
            {
                await _context.BulkInsertOrUpdateAsync(chunk, options =>
                {
                    options.UpdateByProperties = new List<string> { "PartSKU" };
                    options.PropertiesToExcludeOnUpdate = new List<string> { "Id" };
                });
            }
        }

        public List<Product> GetDataForExport(string? searchValue)
        {
            IQueryable<Product> data = _context.Products.Where(m => string.IsNullOrEmpty(searchValue)
               ? true
               : (m.Band.Contains(searchValue) || m.PartSKU.Contains(searchValue) || m.Manufacturer.Contains(searchValue) || m.ItemDescription.Contains(searchValue)));
            return data.ToList();
        }

        public (List<Product> data, int recordsTotal) GetProduct(int pageSize, int skip, string? searchValue, string? sortColumn, string? sortColumnDirection)
        {
            IQueryable<Product> product = _context.Products.Where(m => string.IsNullOrEmpty(searchValue)
                ? true
                : (m.Band.Contains(searchValue) || m.PartSKU.Contains(searchValue) || m.Manufacturer.Contains(searchValue) || m.ItemDescription.Contains(searchValue)));

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                product = product.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

            var data = product.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = product.Count();


            return (data, recordsTotal);
        }
    }
}
