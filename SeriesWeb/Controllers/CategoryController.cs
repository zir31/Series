using Microsoft.AspNetCore.Mvc;
using SeriesWeb.Data;
using SeriesWeb.Models;

namespace SeriesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created succsessfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var categoryFromDB = _db.Categories.Find(id);

            if (categoryFromDB is null)
                return NotFound();

            return View(categoryFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category edited succsessfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            var categoryFromDB = _db.Categories.Find(id);

            if (categoryFromDB is null)
                return NotFound();

            return View(categoryFromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _db.Categories.Find(id);
            if (category is null)
                return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted succsessfully";
            return RedirectToAction("Index");
        }
    }
}
