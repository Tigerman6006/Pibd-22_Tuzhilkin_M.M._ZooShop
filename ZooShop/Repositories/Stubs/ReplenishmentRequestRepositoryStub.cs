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
using ZooShop.Enums;

namespace ZooShop.Repositories.Stubs
{
    public class ReplenishmentRequestRepositoryStub : IReplenishmentRequestRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ReplenishmentRequestRepositoryStub> _logger;

        public ReplenishmentRequestRepositoryStub(
            DataContext context,
            ILogger<ReplenishmentRequestRepositoryStub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ReplenishmentRequest? GetById(int id)
        {
            _logger.LogInformation(
                "Получение заявки {RequestNumber}", id);

            try
            {
                var request = _context.ReplenishmentRequests
                    .Include(r => r.Product)
                    .Include(r => r.Supplier)
                    .FirstOrDefault(r => r.RequestNumber == id);

                if (request == null)
                {
                    _logger.LogWarning("Заявка {RequestNumber} не найдена", id);
                }

                return request;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения заявки {RequestNumber}", id);
                throw;
            }
        }

        public IEnumerable<ReplenishmentRequest> GetAll()
        {
            _logger.LogInformation("Получение всех заявок на пополнение");

            try
            {
                var requests = _context.ReplenishmentRequests
                    .Include(r => r.Product)
                    .Include(r => r.Supplier)
                    .ToList();

                _logger.LogInformation(
                    "Получено заявок: {Count}", requests.Count);

                return requests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения заявок");
                throw;
            }
        }

        public IEnumerable<ReplenishmentRequest> GetBySupplier(int supplierCode)
        {
            _logger.LogInformation(
                "Получение заявок поставщика {SupplierCode}", supplierCode);

            try
            {
                var requests = _context.ReplenishmentRequests
                    .Include(r => r.Product)
                    .Include(r => r.Supplier)
                    .Where(r =>
                        EF.Property<int?>(r, "SupplierCode") == supplierCode)
                    .ToList();

                _logger.LogInformation(
                    "Найдено заявок поставщика {SupplierCode}: {Count}",
                    supplierCode, requests.Count);

                return requests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка получения заявок поставщика {SupplierCode}",
                    supplierCode);
                throw;
            }
        }

        public void Add(ReplenishmentRequest request)
        {
            _logger.LogInformation(
                "Создание заявки. ProductCode: {ProductCode}, Quantity: {Quantity}",
                request.Product?.SalableCode,
                request.RequiredQuantity);

            try
            {
                _context.ReplenishmentRequests.Add(request);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Заявка {RequestNumber} успешно создана",
                    request.RequestNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка создания заявки на пополнение");
                throw;
            }
        }

        public void Update(ReplenishmentRequest request)
        {
            _logger.LogInformation(
                "Обновление заявки {RequestNumber}. Status: {Status}",
                request.RequestNumber,
                request.Status);

            try
            {
                _context.ReplenishmentRequests.Update(request);
                _context.SaveChanges();

                _logger.LogInformation(
                    "Заявка {RequestNumber} обновлена",
                    request.RequestNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Ошибка обновления заявки {RequestNumber}",
                    request.RequestNumber);
                throw;
            }
        }
        public void CompleteRequest(int requestNumber)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var request = _context.ReplenishmentRequests
                    .Include(r => r.Product)
                    .Include(r => r.Supplier)
                    .FirstOrDefault(r => r.RequestNumber == requestNumber);

                if (request is null)
                    throw new InvalidOperationException("Заявка не найдена.");

                if (request.Status != Status.InOrder)
                    throw new InvalidOperationException(
                        "Выполнить можно только заявку со статусом «InOrder».");

                if (request.Product is null)
                    throw new InvalidOperationException(
                        "У заявки не указан товар.");

                if (request.RequiredQuantity <= 0)
                    throw new InvalidOperationException(
                        "Количество товара должно быть больше нуля.");

                var batch = ProductBatch.CreateBatch(
                    DateTime.UtcNow,
                    request.Product.ExpirationDate,
                    request.RequiredQuantity,
                    request.Product,
                    request.Supplier);

                _context.ProductBatches.Add(batch);

                request.CompleteRequest();

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void ReceiveRequest(int requestNumber)
        {
            var request = _context.ReplenishmentRequests
                .FirstOrDefault(r => r.RequestNumber == requestNumber);

            if (request == null)
                throw new InvalidOperationException("Заявка не найдена.");

            request.ReceiveRequest();

            _context.SaveChanges();
        }
    }
}
