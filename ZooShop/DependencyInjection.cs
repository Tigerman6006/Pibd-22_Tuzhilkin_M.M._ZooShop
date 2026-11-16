using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShop.Data;
using ZooShop.Repositories.Interfaces;
using ZooShop.Repositories.Stubs;

namespace ZooShop
{

    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));
            

            // Репозитории
            services.AddScoped<IProductCategoryRepository,
                ProductCategoryRepositoryStub>();

            services.AddScoped<ISupplierRepository,
                SupplierRepositoryStub>();

            services.AddScoped<IProductRepository,
                ProductRepositoryStub>();

            services.AddScoped<IAnimalRepository,
                AnimalRepositoryStub>();

            services.AddScoped<IProductBatchRepository,
                ProductBatchRepositoryStub>();

            services.AddScoped<IReplenishmentRequestRepository,
                ReplenishmentRequestRepositoryStub>();

            services.AddScoped<ISaleRepository,
                SaleRepositoryStub>();

            services.AddScoped<ISaleItemRepository,
                SaleItemRepositoryStub>();

            // Главная форма
            services.AddTransient<MainForm>();

            // Формы справочников
            services.AddTransient<ProductListForm>();
            services.AddTransient<ProductEditForm>();

            services.AddTransient<AnimalListForm>();
            services.AddTransient<AnimalEditForm>();

            services.AddTransient<ProductCategoryListForm>();
            services.AddTransient<ProductCategoryEditForm>();

            services.AddTransient<SupplierListForm>();
            services.AddTransient<SupplierEditForm>();

            // Формы операций
            services.AddTransient<ProductBatchListForm>();
            services.AddTransient<ProductBatchAddForm>();

            services.AddTransient<ReplenishmentRequestListForm>();
            services.AddTransient<ReplenishmentRequestAddForm>();

            services.AddTransient<SaleListForm>();
            services.AddTransient<SaleAddForm>();
            
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(
                    "Host=localhost;" +
                    "Port=5432;" +
                    "Database=ZooShopDb;" +
                    "Username=postgres;" +
                    "Password=88005553535");
            });

        }
    }


}
