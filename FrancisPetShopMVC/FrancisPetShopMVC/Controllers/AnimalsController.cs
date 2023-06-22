using FirstMvc.Data;
using FrancisPetShopMVC.Data.Entities;
using FrancisPetShopMVC.Models;
using FrancisPetShopMVC.Services.Interfaces;
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
            if (animal == null)
                return NotFound();
            var category = _categoryService.GetCategoryById(animal.CategoryId);
            var comments = _commentService.GetComments(id);

            ViewData["categoryName"] = category.Name;
            ViewData["Comments"] = comments;
            return View(animal);
        }
        [HttpPost]
        public IActionResult AddComment(int id, string commentText)
        {
            var animal = _animalService.GetAnimalById(id);

            if (animal == null)
                return BadRequest($"No Animal with id = {id}");

            _commentService.AddComment(animal.AnimalId, commentText);           
            return Json(new { success = true });
        } 
    }
}
