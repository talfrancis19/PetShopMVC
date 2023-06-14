using FirstMvc.Data;
using FrancisPetShopMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FrancisPetShopMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MyContext context;
        public CategoryService(MyContext context)
        {
            this.context = context;

        }
        public Category GetCategoryById(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            return category;
        }
        public IEnumerable<Category> GetAllCategories()

        {
            return context.Categories.ToList();
        }
        public SelectList GetCategoryDropDownList()
        {
            var categories = GetAllCategories(); 
            return new SelectList(categories, "Id", "Name");
        }
    }
}
