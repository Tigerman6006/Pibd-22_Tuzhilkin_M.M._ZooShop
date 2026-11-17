using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShop.Data;
using ZooShop.Entities;
using ZooShop.Repositories.Interfaces;

namespace ZooShop.Repositories.Stubs
{
    public class ProductRepositoryStub : RepositoryStub<Product, int>, IProductRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductRepositoryStub> _logger;

        public ProductRepositoryStub(DataContext context, ILogger<ProductRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }
        public int CalculateRemainingStock(int productCode)
        {
            return _context.ProductBatches
                .Where(batch => batch.Products.ProductCode == productCode)
                .Sum(batch => (int?)batch.RemainingQuantity) ?? 0;
        }
        public Product? GetById(int id)
        {
            _logger.LogInformation(
                "Получение продукта по ID: {ProductCode}", id);

            try
            {
                var product = _context.Products
                    .Include(p => p.StockNorm)
                    .Include(p => p.StorageConditions)
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.ProductCode == id);

                if (product == null)
                {
                    _logger.LogWarning(
                        "Продукт с ID {ProductCode} не найден", id);
                }
                else
                {
                    _logger.LogInformation(
                        "Продукт с ID {ProductCode} успешно получен",
                        id);
                }

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка получения продукта с ID {ProductCode}", id);

                throw;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            _logger.LogInformation("Получение всех продуктов");

            try
            {
                var products = _context.Products
                    .Include(p => p.StockNorm)
                    .Include(p => p.StorageConditions)
                    .Include(p => p.Category)
                    .ToList();

                _logger.LogInformation(
                    "Получено {ProductCount} продуктов",
                    products.Count);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения всех продуктов");
                throw;
            }
        }

        public IEnumerable<Product> GetByCategory(int categoryCode)
        {
            _logger.LogInformation(
                "Получение продуктов по категории: {CategoryCode}",
                categoryCode);

            try
            {
                var products = _context.Products
                    .Where(p => p.CategoryCode == categoryCode)
                    .Include(p => p.StockNorm)
                    .Include(p => p.StorageConditions)
                    .Include(p => p.Category)
                    .ToList();

                _logger.LogInformation(
                    "Найдено {ProductCount} продуктов в категории {CategoryCode}",
                    products.Count,
                    categoryCode);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка получения продуктов для категории {CategoryCode}",
                    categoryCode);

                throw;
            }
        }

        public void Add(Product product)
        {
            _logger.LogInformation(
                "Добавление продукта. Название: {ProductName}",
                product.Name);

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Продукт успешно добавлен. ID: {ProductCode}",
                    product.ProductCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка добавления продукта: {ProductName}",
                    product.Name);

                throw;
            }
        }

        public void Update(Product product)
        {
            _logger.LogInformation(
               "Обновление продукта с ID: {ProductCode}",
               product.ProductCode);

            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Продукт {ProductCode} успешно обновлен",
                    product.ProductCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка обновления продукта {ProductCode}",
                    product.ProductCode);

                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation(
               "Удаление продукта с ID: {ProductCode}", id);

            try
            {
                var product = _context.Products
                    .FirstOrDefault(p => p.ProductCode == id);

                if (product == null)
                {
                    _logger.LogWarning(
                        "Невозможно удалить продукт {ProductCode}: не найден",
                        id);

                    return;
                }

                _context.Products.Remove(product);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Продукт {ProductCode} успешно удален", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ошибка удаления продукта {ProductCode}", id);

                throw;
            }
        }
    }
}
