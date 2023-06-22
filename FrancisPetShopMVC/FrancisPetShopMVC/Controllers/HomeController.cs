using FrancisPetShopMVC.Models;
using FrancisPetShopMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrancisPetShopMVC.Controllers
{
    public class HomeController : Controller
    {    
        private readonly IAnimalService _animalService;
        public HomeController(IAnimalService animalService)
        {
            _animalService = animalService;
        }        
        public IActionResult Index() => View(_animalService.GetAnimalsWithTheMostComments(2));
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}