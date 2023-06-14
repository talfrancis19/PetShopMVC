using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrancisPetShopMVC.Data.Entities
{
    public class Animal
    {
        public int AnimalId { get; set; }
        [Display(Name = "Animal Image")]
        public required string ImageFileName { get; set; }
        [Display(Name = "Pet Name")]
               
        public required string Name { get; set; }
        public required int Age{ get; set; }
        public required string Description { get; set; }
        public required int CategoryId { get; set;}
        [Display(Name = "Category")]
        public required Category  CategoryItem { get; set; }
        public required ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int CommentsCount { get; set; }


    }
}
