using FrancisPetShopMVC.Data.Entities;
using FrancisPetShopMVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System;

namespace FrancisPetShopMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IAnimalService animalService, ICategoryService categoryService, ICommentService commentService, IWebHostEnvironment webHostEnvironment)
        {
            _animalService = animalService;
            _categoryService = categoryService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: AdminController
        [HttpGet]
        public ActionResult Index(int? categoryId)
        {
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            var animals = _animalService.GetAllAnimals();

            if (categoryId.HasValue)
            {
                animals = animals.Where(a => a.CategoryId == categoryId.Value);
            }

            return View(animals);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Login")]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
        // GET: AdminController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
                animal.ImageFileName = uniqueFileName;
                _animalService.AddAnimal(animal);
                return RedirectToAction("Index");
            }           
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            return View(animal);
        }
        // GET: AdminController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var animal = _animalService.GetAnimalById(id);
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            return View(animal);
        }
        // POST: AdminController/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animal animal, IFormFile imageFile)
        {
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            if (imageFile != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
                animal.ImageFileName = uniqueFileName;
                _animalService.UpdateAnimal(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        // GET: AdminController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var animal = _animalService.GetAnimalById(id);
            var category = _categoryService.GetCategoryById(animal.CategoryId);
            ViewData["categoryName"] = category.Name;
            return View(animal);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _animalService.DeleteAnimal(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
