using FirstMvc.Data;
using FrancisPetShopMVC.Services;
using FrancisPetShopMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using FrancisPetShopMVC.Services.Interfaces;

namespace FrancisPetShopMVC
{
    public class Program
    {
		
		public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IAnimalService, AnimalService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            
            var connectionString = builder.Configuration["ConnectionStrings:DataBase"];


            builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));












            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
			//To use images(static file)
			app.UseStaticFiles();

			app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           



            app.Run();
        }
    }
}