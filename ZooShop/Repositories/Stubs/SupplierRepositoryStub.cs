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
    public class SupplierRepositoryStub : RepositoryStub<Supplier, int>, ISupplierRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<SupplierRepositoryStub> _logger;

        public SupplierRepositoryStub(
            DataContext context,
            ILogger<SupplierRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Supplier? GetById(int id)
        {
            _logger.LogInformation(
                "Получение поставщика. SupplierCode: {SupplierCode}", id);

            try
            {
                var supplier = _context.Suppliers
                    .Include(s => s.ReplenishmentRequests)
                    .FirstOrDefault(s => s.SupplierCode == id);

                if (supplier == null)
                {
                    _logger.LogWarning(
                        "Поставщик {SupplierCode} не найден", id);
                }

                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения поставщика {SupplierCode}", id);
                throw;
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            _logger.LogInformation("Получение списка поставщиков");

            try
            {
                var suppliers = _context.Suppliers
                    .Include(s => s.ReplenishmentRequests)
                    .ToList();

                _logger.LogInformation(
                    "Получено поставщиков: {Count}", suppliers.Count);

                return suppliers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения поставщиков");
                throw;
            }
        }

        public void Add(Supplier supplier)
        {
            _logger.LogInformation(
                "Добавление поставщика: {SupplierName}", supplier.Name);

            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Поставщик добавлен. SupplierCode: {SupplierCode}",
                    supplier.SupplierCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка добавления поставщика {SupplierName}",
                    supplier.Name);
                throw;
            }
        }

        public void Update(Supplier supplier)
        {
            _logger.LogInformation(
                "Обновление поставщика {SupplierCode}",
                supplier.SupplierCode);

            try
            {
                _context.Suppliers.Update(supplier);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Поставщик {SupplierCode} обновлён",
                    supplier.SupplierCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления поставщика {SupplierCode}",
                    supplier.SupplierCode);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation(
                "Удаление поставщика {SupplierCode}", id);

            try
            {
                var supplier = _context.Suppliers.Find(id);

                if (supplier == null)
                {
                    _logger.LogWarning(
                        "Поставщик {SupplierCode} не найден для удаления", id);
                    return;
                }

                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Поставщик {SupplierCode} удалён", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка удаления поставщика {SupplierCode}", id);
                throw;
            }
        }
    }
}
