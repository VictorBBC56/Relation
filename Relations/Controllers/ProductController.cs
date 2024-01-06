using Microsoft.AspNetCore.Mvc;

namespace Relations.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService ps;
        private readonly ICategoryService ct;
        public ProductController(IProductService ps,ICategoryService ct)
        {
            this.ps = ps;
            this.ct = ct;
        }
        public async Task<IActionResult> Index()
        {
            return View(await ps.GetProducts());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile file)
        {
            ModelState.Remove("file");

            if (ModelState.IsValid) return View();

            await ps.Add(product, file);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await ps.Find(id);

            if (product != null) await ps.Delete(product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await ps.Find(id);

            if (product == null) return RedirectToAction(nameof(Index));

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile file)
        {
            ModelState.Remove("file");

            if (!ModelState.IsValid) return View();

            await ps.Update(product, file);

            return RedirectToAction(nameof(Index));
        }
    }
}
