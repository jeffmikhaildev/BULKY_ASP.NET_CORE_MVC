using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid) {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        // *** EDIT CATEGORY ***
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //Category? category1 = _db.Categories.Find(id);

            // Alternative way to find the category using LINQ
            Category? category2 = _db.Categories.FirstOrDefault(u => u.Id == id);

            //Category? category3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (category2 == null)
            {
                return NotFound();
            }

            return View(category2);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        // *** DELETE CATEGORY ***
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Category? category1 = _db.Categories.Find(id);

            // Alternative way to find the category using LINQ
            Category? category2 = _db.Categories.FirstOrDefault(u => u.Id == id);

            //Category? category3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (category2 == null)
            {
                return NotFound();
            }

            return View(category2);
        }

        [HttpPost]
        [ActionName("Delete")] // This is to specify that this method is for the Delete action
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
        }
    }
}
