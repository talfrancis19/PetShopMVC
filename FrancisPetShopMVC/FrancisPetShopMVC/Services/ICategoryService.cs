﻿using FrancisPetShopMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrancisPetShopMVC.Services
{
    public interface ICategoryService 
    {
        public Category GetCategoryById(int id);
        public IEnumerable<Category> GetAllCategories();
        public SelectList GetCategoryDropDownList();
    }
}
