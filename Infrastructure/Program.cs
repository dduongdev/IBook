using Infrastructure.Models;
using Infrastructure.Services;
using Infrastructure.SqlServer.Repositories;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using Infrastructure.SqlServer.Repositories.SqlServer.MapperProfile;
using Infrastructure.SqlServer.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using UseCases;
using UseCases.Repositories;
using UseCases.UnitOfWork;

namespace Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            RegisterInfrastructureServices(builder.Configuration, builder.Services);

            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}",
                defaults: new { area = "Client" })
                .WithStaticAssets();

            app.Run();
        }

        private static void RegisterInfrastructureServices(ConfigurationManager configuration, IServiceCollection services)
        {
            var repositoryOptions = configuration.GetSection("Repository").Get<RepositoryOptions>() ?? throw new Exception("No RepositoryOptions found.");
            if (repositoryOptions.RepositoryType == RepositoryTypes.SqlServer)
            {
                services.AddAutoMapper(typeof(SqlServer2EntityProfile));

                services.AddDbContext<BookDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IBookDb")).UseLazyLoadingProxies());

                services.AddTransient<IBookRepository, SqlServerBookRepository>();
                services.AddTransient<ICartItemRepository, SqlServerCartItemRepository>();
                services.AddTransient<ICartRepository, SqlServerCartRepository>();
                services.AddTransient<ICategoryRepository, SqlServerCategoryRepository>();
                services.AddTransient<IFeedbackRepository, SqlServerFeedbackRepository>();
                services.AddTransient<IOrderItemRepository, SqlServerOrderItemRepository>();
                services.AddTransient<IOrderRepository, SqlServerOrderRepository>();
                services.AddTransient<IPublisherRepository, SqlServerPublisherRepository>();
                services.AddTransient<IUserRepository, SqlServerUserRepository>();

                services.AddTransient<IUserUnitOfWork, SqlServerUserUnitOfWork>();
                services.AddTransient<ICategoryUnitOfWork, SqlServerCategoryUnitOfWork>();
                services.AddTransient<IOrderUnitOfWork, SqlServerOrderUnitOfWork>();
                services.AddTransient<IPublisherUnitOfWork, SqlServerPublisherUnitOfWork>();
            }
            else
            {
                throw new Exception();
            }

            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IBookManager, BookManager>();
            services.AddTransient<ICartItemManager, CartItemManager>();
            services.AddTransient<ICartManager, CartManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IFeedbackManager, FeedbackManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IPublisherManager, PublisherManager>();
            services.AddTransient<IUserManager, UserManager>();

            services.AddTransient<BookService>();
            services.AddTransient<BookMappingService>();
            services.AddTransient<ImageService>();
        }
    }
}
