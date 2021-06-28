using CoreCourse.Spyshop.Domain;
using CoreCourse.Spyshop.Domain.Catalog;
using CoreCourse.Spyshop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreCourse.Spyshop.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository<Category, long> _cRepository;
        private readonly IRepository<Product, long> _pRepository;

        public CatalogController(
            IRepository<Category, long> cRepository,
            IRepository<Product, long> pRepository)
        {
            this._cRepository = cRepository;
            this._pRepository = pRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _cRepository.GetAll()
                .Include(c => c.Products)
                .ToListAsync();

            CatalogIndexViewModel catalogIndexViewModel = new CatalogIndexViewModel();
            catalogIndexViewModel.Categories = categories;

            return View(catalogIndexViewModel);
        }

        [Route("~/Catalog/Product/{id:long}/{*name}")]
        public async Task<IActionResult> Product(long? id, string name)
        {
            if (id.HasValue)
            {
                var catalogProductViewModel = new CatalogProductViewModel();
                catalogProductViewModel.Product = await _pRepository.GetByIdAsync(id.Value);
                if (catalogProductViewModel.Product != null &&
                    catalogProductViewModel.Product.Name?.ToLower().Trim() == name?.ToLower().Trim())
                {
                    return View(catalogProductViewModel);
                }
            }
            return NotFound();
        }
    }
}