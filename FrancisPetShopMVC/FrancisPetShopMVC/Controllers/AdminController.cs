using FrancisPetShopMVC.Data.Entities;
using FrancisPetShopMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrancisPetShopMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        public AdminController(IAnimalService animalService, ICategoryService categoryService, ICommentService commentService)
        {
            _animalService = animalService;
            _categoryService = categoryService;
            _commentService = commentService;

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

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Animal animal)
        {
            _animalService.AddAnimal(animal);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetCategoryDropDownList();
            return View(animal);
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
		{
			var animal = _animalService.GetAnimalById(id);			
			ViewBag.Categories = _categoryService.GetCategoryDropDownList();
			return View(animal);
		}

		// POST: AdminController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Animal animal)
		{
			if (!ModelState.IsValid)
			{
				var updatedAnimal = _animalService.UpdateAnimal(animal);
				if (updatedAnimal != null)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.ErrorMessage = "Failed to update the animal.";
				}
			}

			ViewBag.Categories = _categoryService.GetCategoryDropDownList();
			return View(animal);
		}

		// GET: AdminController/Delete/5
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
        public IActionResult Delete(int id, IFormCollection collection)
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
