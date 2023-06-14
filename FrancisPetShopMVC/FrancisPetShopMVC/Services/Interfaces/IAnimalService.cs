using FrancisPetShopMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrancisPetShopMVC.Services.Interfaces
{
    public interface IAnimalService
    {
        IEnumerable<Animal> GetAllAnimals();
        Animal GetAnimalById(int id);
        Animal AddAnimal(Animal item);
        Animal DeleteAnimal(int id);
        IEnumerable<Animal> GetAnimalsWithTheMostComments(int count);
        Animal UpdateAnimal(Animal updatedAnimal);


    }
}