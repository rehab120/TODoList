using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODoList.IRepositry;
using TODoList.Models;
using TODoList.Repositry;

namespace TODoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ToDoListContext>(

            OptionsBuilder =>
            {
                OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }
            
            ).AddEntityFrameworkStores<ToDoListContext>();
            builder.Services.AddScoped<ITaskRepositry,TaskRepositry>();
            builder.Services.AddScoped<IUserRepositry, UserRepositry>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
