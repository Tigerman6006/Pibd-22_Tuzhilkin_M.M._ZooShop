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
    public class ProductBatchRepositoryStub : IProductBatchRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductBatchRepositoryStub> _logger;

        public ProductBatchRepositoryStub(
            DataContext context,
            ILogger<ProductBatchRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ProductBatch? GetById(int id)
        {
            _logger.LogInformation(
                "Получение партии. BatchNumber: {BatchNumber}", id);

            try
            {
                var batch = _context.ProductBatches
                    .Include(b => b.Products)
                    .Include(b => b.Supplier)
                    .FirstOrDefault(b => b.BatchNumber == id);

                if (batch == null)
                {
                    _logger.LogWarning("Партия {BatchNumber} не найдена", id);
                }

                return batch;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения партии {BatchNumber}", id);
                throw;
            }
        }

        public IEnumerable<ProductBatch> GetAll()
        {
            _logger.LogInformation("Получение всех партий товаров");

            try
            {
                var batches = _context.ProductBatches
                    .Include(b => b.Products)
                    .Include(b => b.Supplier)
                    .ToList();

                _logger.LogInformation(
                    "Получено партий: {Count}", batches.Count);

                return batches;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения партий");
                throw;
            }
        }

        public IEnumerable<ProductBatch> GetByProduct(int productCode)
        {
            _logger.LogInformation(
                "Получение партий товара {ProductCode}", productCode);

            try
            {
                var batches = _context.ProductBatches
                    .Include(b => b.Products)
                    .Include(b => b.Supplier)
                    .Where(b => EF.Property<int>(b, "ProductCode") == productCode)
                    .ToList();

                _logger.LogInformation(
                    "Для товара {ProductCode} найдено партий: {Count}",
                    productCode, batches.Count);

                return batches;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения партий товара {ProductCode}",
                    productCode);
                throw;
            }
        }

        public void Add(ProductBatch batch)
        {
            _logger.LogInformation(
                "Добавление партии. ProductCode: {ProductCode}, Quantity: {Quantity}",
                batch.Products?.SalableCode,
                batch.QuantityReceived);

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.ProductBatches.Add(batch);
                _context.SaveChanges();

                transaction.Commit();

                _logger.LogInformation(
                    "Партия {BatchNumber} успешно добавлена и транзакция зафиксирована",
                    batch.BatchNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка добавления партии. Выполняется откат транзакции");

                try
                {
                    transaction.Rollback();
                    _logger.LogWarning("Транзакция добавления партии отменена");
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx,
                        "Ошибка при откате транзакции добавления партии");
                }

                throw;
            }
        }

        public void Update(ProductBatch batch)
        {
            _logger.LogInformation(
                "Обновление партии {BatchNumber}", batch.BatchNumber);

            try
            {
                _context.ProductBatches.Update(batch);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Партия {BatchNumber} обновлена", batch.BatchNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления партии {BatchNumber}",
                    batch.BatchNumber);
                throw;
            }
        }
    }

}
