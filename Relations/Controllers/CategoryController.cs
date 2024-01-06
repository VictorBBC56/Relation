using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Relations.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService ct;
        public CategoryController(ICategoryService ct)
        {
            this.ct = ct;
        }
        public IActionResult Index()
        {
            var categories = ct.GetAllCategory();
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            var category = ct.GetByIdCategory(id);
            if (category == null)
            {
                TempData["OK"] = true;
            }

            else
            {
                ct.DeleteCategory(category);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();

            ct.AddCategory(category);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = ct.GetByIdCategory(id);

            if (category == null)
            {
                TempData["OK"] = true;
            }

            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid) return View();

            ct.UpdateCategory(category);

            return RedirectToAction("Index");
        }
    }
}
