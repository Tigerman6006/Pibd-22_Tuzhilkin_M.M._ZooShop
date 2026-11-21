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
    public class SaleRepositoryStub : ISaleRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<SaleRepositoryStub> _logger;

        public SaleRepositoryStub(
            DataContext context,
            ILogger<SaleRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Sale? GetById(int id)
        {
            _logger.LogInformation(
                "Получение продажи. ReceiptNumber: {ReceiptNumber}", id);

            try
            {
                var sale = _context.Sales
                    .Include(s => s.SaleItems)
                    .FirstOrDefault(s => s.ReceiptNumber == id);

                if (sale == null)
                {
                    _logger.LogWarning(
                        "Продажа {ReceiptNumber} не найдена", id);
                }

                return sale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения продажи {ReceiptNumber}", id);
                throw;
            }
        }

        public IEnumerable<Sale> GetAll()
        {
            _logger.LogInformation("Получение списка продаж");

            try
            {
                var sales = _context.Sales
                    .Include(s => s.SaleItems)
                    .ToList();

                _logger.LogInformation(
                    "Получено продаж: {Count}", sales.Count);

                return sales;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения продаж");
                throw;
            }
        }

        public IEnumerable<Sale> GetByPeriod(DateTime start, DateTime end)
        {
            _logger.LogInformation(
                "Получение продаж за период с {StartDate} по {EndDate}",
                start, end);

            try
            {
                var sales = _context.Sales
                    .Include(s => s.SaleItems)
                    .Where(s => s.SaleDate >= DateTime.SpecifyKind(start, DateTimeKind.Utc) 
                    && s.SaleDate <= DateTime.SpecifyKind(end,DateTimeKind.Utc))
                    .ToList();

                _logger.LogInformation(
                    "За указанный период найдено продаж: {Count}",
                    sales.Count);

                return sales;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения продаж за период {StartDate} - {EndDate}",
                    start, end);
                throw;
            }
        }

        public void Add(Sale sale)
        {
            _logger.LogInformation(
                "Создание продажи. Amount: {Amount}, ItemsCount: {ItemsCount}",
                sale.Amount,
                sale.SaleItems?.Count ?? 0);

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();

                transaction.Commit();

                _logger.LogInformation(
                    "Продажа {ReceiptNumber} успешно создана. Сумма: {Amount}",
                    sale.ReceiptNumber,
                    sale.Amount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка создания продажи. Выполняется откат транзакции");

                try
                {
                    transaction.Rollback();
                    _logger.LogWarning("Транзакция создания продажи отменена");
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx,
                        "Ошибка при откате транзакции создания продажи");
                }

                throw;
            }
        }
    }
}
