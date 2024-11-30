using LogisticaSolutions.Entities;

namespace LogisticaSolutions.Contracts.Service
{
    public interface IProductService
    {

        Task ReadExcelFileAsync(IFormFile file);
        (List<Product> data, int recordsTotal) GetProduct(int pageSize, int skip, string? searchValue, string? sortColumn, string? sortColumnDirection);
        List<Product> GetDataForExport(string? searchValue);
    }
}
