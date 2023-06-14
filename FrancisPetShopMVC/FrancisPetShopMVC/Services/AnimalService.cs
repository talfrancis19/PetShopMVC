using FirstMvc.Data;
using FrancisPetShopMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace FrancisPetShopMVC.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly MyContext context;
        public AnimalService(MyContext context)
        {
            this.context = context;
        }
        public IEnumerable<Animal> GetAllAnimals()
        {
            return context.Animals.ToList();
        }

        public IEnumerable<Animal> GetAnimalsWithTheMostComments(int count)
        {
            var animals = context.Animals.ToList();
            var comments = context.Comments.ToList();

            var topAnimals = animals
                .OrderByDescending(x => x.Comments.Count)
                .Take(count);

            return topAnimals;
        }

        public Animal GetAnimalById(int id)
        {

            return context.Animals.First(x => x.AnimalId == id);

        }

        public Animal AddAnimal(Animal item)
        {

            context.Animals.Add(item);        
            
                context.SaveChanges();
            return item;
        }
      
        public Animal DeleteAnimal(int id)
        {
            var animal = context.Animals.FirstOrDefault(x => x.AnimalId == id);
            if (animal != null)
            {
                context.Animals.Remove(animal);
                context.SaveChanges();
            }
            return animal;
        }
        
		public Animal UpdateAnimal(Animal updatedAnimal)
		{
			var existingAnimal = context.Animals.FirstOrDefault(a => a.AnimalId == updatedAnimal.AnimalId);

			if (existingAnimal != null)
			{
				// Update the properties of the existing animal with the updated values
				existingAnimal.ImageFileName = updatedAnimal.ImageFileName;
				existingAnimal.Name = updatedAnimal.Name;
				existingAnimal.Description = updatedAnimal.Description;
				existingAnimal.Age = updatedAnimal.Age;
				existingAnimal.CategoryId = updatedAnimal.CategoryId;
				context.SaveChanges(); // Save changes to the database

				return existingAnimal;
			}
			else
			{
				throw new ArgumentException("Animal not found.");
			}
		}

	}
}
//This is the same using grouping +joining in sql(better performance)
//{
//    var animalWithMostComments = context.Animals
//        .Join(context.Comments,
//              animal => animal.AnimalId,
//              comment => comment.AnimalId,
//              (animal, comment) => new { Animal = animal, Comment = comment })
//        .GroupBy(animalWithComments => animalWithComments.Animal.AnimalId)
//        .Select(group => new
//        {
//            AnimalId = group.Key,
//            CommentCount = group.Count()
//        })
//        .OrderByDescending(ac => ac.CommentCount)
//        .Take(count)
//        .ToList();

//    var topAnimalIds = animalWithMostComments.Select(x => x.AnimalId).ToList();
//    var topAnimals = context.Animals.AsQueryable().Where(x => topAnimalIds.Contains(x.AnimalId)).ToList();
//}



