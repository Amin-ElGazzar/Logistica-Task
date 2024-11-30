using ClosedXML.Excel;
using LogisticaSolutions.Contracts.Repository;
using LogisticaSolutions.Contracts.Service;
using LogisticaSolutions.Entities;



namespace LogisticaSolutions.Implement.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task ReadExcelFileAsync(IFormFile file)
        {

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var workbook = new XLWorkbook(stream))
                {

                    await _productRepository.BulkInsertInChunksAsync(GetItems(workbook));

                    await _unitOfWork.Save();
                }
            }
        }

        public IEnumerable<Product> GetItems(XLWorkbook workbook)
        {
            var sheets = workbook.Worksheets.Take(3).ToList();

            foreach (var sheet in sheets)
            {
                var rows = sheet.RowsUsed().Skip(2);
                foreach (var row in rows)
                {
                    var partSKU = row.Cell(5).GetString()?.Trim();
                    if (string.IsNullOrWhiteSpace(partSKU)) continue;

                    var Product = new Product
                    {
                        Band = row.Cell(2).GetString(),
                        CategoryCode = row.Cell(3).GetString(),
                        Manufacturer = row.Cell(4).GetString(),
                        PartSKU = partSKU,
                        ItemDescription = row.Cell(6).GetString(),
                        ListPrice = TryGetDecimal(row.Cell(7)),
                        MinDiscount = TryGetDecimal(row.Cell(8)),
                        DiscountPrice = TryGetDecimal(row.Cell(9))
                    };

                    yield return Product;
                }

            }

        }
        private static decimal TryGetDecimal(IXLCell cell)
        {
            return decimal.TryParse(cell.GetString(), out var result) ? result : 0m;
        }

        public (List<Product> data, int recordsTotal) GetProduct(int pageSize, int skip, string? searchValue, string? sortColumn, string? sortColumnDirection)
        {
            return _productRepository.GetProduct(pageSize, skip, searchValue, sortColumn, sortColumnDirection);
        }

        public List<Product> GetDataForExport(string? searchValue)
        {
            return _productRepository.GetDataForExport(searchValue);
        }
    }
}


