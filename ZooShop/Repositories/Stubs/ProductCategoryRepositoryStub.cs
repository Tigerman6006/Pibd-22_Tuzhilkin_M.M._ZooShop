using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZooShop.Data;
using ZooShop.Entities;
using ZooShop.Repositories.Interfaces;

namespace ZooShop.Repositories.Stubs
{
    public class ProductCategoryRepositoryStub : RepositoryStub<ProductCategory, int>, IProductCategoryRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductCategoryRepositoryStub> _logger;

        public ProductCategoryRepositoryStub(
            DataContext context,
            ILogger<ProductCategoryRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ProductCategory? GetById(int id)
        {
            _logger.LogInformation(
                "Получение категории. CategoryCode: {CategoryCode}", id);

            try
            {
                var category = _context.ProductCategories
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.CategoryCode == id);

                if (category == null)
                {
                    _logger.LogWarning(
                        "Категория с кодом {CategoryCode} не найдена", id);
                }
                else
                {
                    _logger.LogInformation(
                        "Категория {CategoryCode} успешно найдена", id);
                }

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения категории {CategoryCode}", id);
                throw;
            }
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            _logger.LogInformation("Получение всех категорий");

            try
            {
                var categories = _context.ProductCategories
                    .Include(c => c.Products)
                    .ToList();

                _logger.LogInformation(
                    "Получено категорий: {Count}", categories.Count);

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения категорий");
                throw;
            }
        }

        public void Add(ProductCategory category)
        {
            _logger.LogInformation(
                "Добавление категории: {CategoryName}",
                category.CategoryName);

            try
            {
                _context.ProductCategories.Add(category);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Категория успешно добавлена. CategoryCode: {CategoryCode}",
                    category.CategoryCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка добавления категории {CategoryName}",
                    category.CategoryName);
                throw;
            }
        }

        public void Update(ProductCategory category)
        {
            _logger.LogInformation(
                "Обновление категории. CategoryCode: {CategoryCode}",
                category.CategoryCode);

            try
            {
                _context.ProductCategories.Update(category);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Категория {CategoryCode} обновлена",
                    category.CategoryCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления категории {CategoryCode}",
                    category.CategoryCode);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation(
                "Удаление категории. CategoryCode: {CategoryCode}", id);

            try
            {
                var category = _context.ProductCategories
                    .Find(id);

                if (category == null)
                {
                    _logger.LogWarning(
                        "Категория {CategoryCode} не найдена для удаления", id);
                    return;
                }

                _context.ProductCategories.Remove(category);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Категория {CategoryCode} успешно удалена", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка удаления категории {CategoryCode}", id);
                throw;
            }
        }
    }
}
