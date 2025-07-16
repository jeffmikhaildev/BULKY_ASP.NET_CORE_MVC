﻿using Bulky.DataAccess;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Irepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid) {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();

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

            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

            // Alternative way to find the category using LINQ
            //Category? category2 = _db.Categories.FirstOrDefault(u => u.Id == id);

            //Category? category3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();

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

            Category? category1 = _categoryRepo.Get(u => u.Id == id);

            // Alternative way to find the category using LINQ
            //Category? category2 = _db.Categories.FirstOrDefault(u => u.Id == id);

            //Category? category3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (category1 == null)
            {
                return NotFound();
            }

            return View(category1);
        }

        [HttpPost]
        [ActionName("Delete")] // This is to specify that this method is for the Delete action
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _categoryRepo.Remove(obj);
            _categoryRepo.Save();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
        }
    }
}
