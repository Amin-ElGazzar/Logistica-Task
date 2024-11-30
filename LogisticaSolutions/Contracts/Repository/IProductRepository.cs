using LogisticaSolutions.Entities;


namespace LogisticaSolutions.Contracts.Repository
{
    public interface IProductRepository
    {
        Task BulkInsertInChunksAsync(IEnumerable<Product> entities, int chunkSize = 10000);
        (List<Product> data, int recordsTotal) GetProduct(int pageSize, int skip, string? searchValue, string? sortColumn, string? sortColumnDirection);

        List<Product> GetDataForExport(string? searchValue);
    };
}
