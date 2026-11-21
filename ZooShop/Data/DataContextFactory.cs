using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ZooShop.Data
{
    public class DataContextFactory
        : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;" +
                "Port=5432;" +
                "Database=ZooShopDb;" +
                "Username=postgres;" +
                "Password=88005553535");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
