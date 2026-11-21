using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShop.Data;
using ZooShop.Entities;
using ZooShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ZooShop.Repositories.Stubs
{
    public class SaleItemRepositoryStub : ISaleItemRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<SaleItemRepositoryStub> _logger;

        public SaleItemRepositoryStub(
            DataContext context,
            ILogger<SaleItemRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public SaleItem? GetById(int id)
        {
            _logger.LogInformation(
                "Получение позиции продажи. Id: {Id}", id);

            try
            {
                var item = _context.SaleItems
                    .FirstOrDefault(i => i.Id == id);

                if (item == null)
                {
                    _logger.LogWarning(
                        "Позиция продажи {Id} не найдена", id);
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения позиции продажи {Id}", id);
                throw;
            }
        }

        public IEnumerable<SaleItem> GetAll()
        {
            _logger.LogInformation("Получение всех позиций продаж");

            try
            {
                var items = _context.SaleItems.ToList();

                _logger.LogInformation(
                    "Получено позиций продаж: {Count}", items.Count);

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения позиций продаж");
                throw;
            }
        }

        public IEnumerable<SaleItem> GetBySale(int receiptNumber)
        {
            _logger.LogInformation(
                "Получение позиций чека {ReceiptNumber}", receiptNumber);

            try
            {
                var items = _context.SaleItems
                    .Where(i =>
                        EF.Property<int>(i, "ReceiptNumber") == receiptNumber)
                    .ToList();

                _logger.LogInformation(
                    "В чеке {ReceiptNumber} найдено позиций: {Count}",
                    receiptNumber, items.Count);

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения позиций чека {ReceiptNumber}",
                    receiptNumber);
                throw;
            }
        }

        public void Add(SaleItem item)
        {
            _logger.LogInformation(
                "Добавление позиции продажи.");

            try
            {
                _context.SaleItems.Add(item);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Позиция продажи добавлена. Id: {Id}", item.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка добавления позиции продажи");
                throw;
            }
        }
    }
}
