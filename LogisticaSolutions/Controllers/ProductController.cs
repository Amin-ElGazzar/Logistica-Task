using System.Linq.Dynamic.Core;
using LogisticaSolutions.Context;
using LogisticaSolutions.Contracts.Service;
using LogisticaSolutions.Dtos;
using LogisticaSolutions.Helper;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaSolutions.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> ImportData(Uploadfile uploadfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    var errors = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .SelectMany(x => x.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();


                    var errorMessage = string.Join("<br>", errors);

                    AlertHelper.SetAlert(this, "error", errorMessage);
                    return RedirectToAction(nameof(Index));

                }


                await _productService.ReadExcelFileAsync(uploadfile.File);

                AlertHelper.SetAlert(this, "success", "عملية ناجحة");

                return RedirectToAction(nameof(DisplayData));
            }
            catch
            {
                AlertHelper.SetAlert(this, "error", "خطا اثناء معالجة الملف!!");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> DisplayData()
        {
            return View();
        }

        [HttpPost("Product/GetData")]
        public IActionResult GetData()
        {

            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"];

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            var data = _productService.GetProduct(pageSize, skip, searchValue, sortColumn, sortColumnDirection);

            var jsonData = new { recordsFiltered = data.recordsTotal, recordsTotal = data.recordsTotal, data.data };

            return Ok(jsonData);

        }

        [HttpPost]
        public IActionResult GetDataForExport(string searchValue)
        {

            var data = _productService.GetDataForExport(searchValue);

            return Json(new { data });
        }

    }
}
