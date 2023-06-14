using FirstMvc.Data;
using FrancisPetShopMVC.Data.Entities;
using FrancisPetShopMVC.Models;
using FrancisPetShopMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FrancisPetShopMVC.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        // To Get relative path we need this interface
        

        public AnimalsController(IAnimalService animalService, ICategoryService categoryService, ICommentService commentService)
        {
            _animalService = animalService;
            _categoryService = categoryService;
            _commentService = commentService;
            
        }
        // GET: AnimalsController

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            var animals = _animalService.GetAllAnimals();

            if (categoryId.HasValue)
            {
                animals = animals.Where(a => a.CategoryId == categoryId.Value);
            }

            return View(animals);
        }


        // GET: AnimalsController/Details/5
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Details(int id)
        {
       
            var animal = _animalService.GetAnimalById(id);
            var category = _categoryService.GetCategoryById(animal.CategoryId);
            var comments = _commentService.GetComments(id);
          
            ViewData["categoryName"] = category.Name;
            ViewData["Comments"] = comments;
            return View(animal);
        }
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalsController/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View();
        }

        // POST: AnimalsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
