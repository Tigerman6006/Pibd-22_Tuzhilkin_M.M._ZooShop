using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShop.Entities
{
    public class Product:Salable
    {
        private DateTime expirationDate;
        private StockNorm? stockNorm;
        private StorageConditions? storageConditions;
        private int? categoryCode;
        private ProductCategory? category;
        private Product(string name, decimal purchasePrice, decimal sellingPrice, DateTime expirationDate)
        {
            this.name = name;
            this.purchasePrice = purchasePrice;
            this.sellingPrice = sellingPrice;
            this.expirationDate = expirationDate;
        }
        public static Product CreateProduct(string name, decimal purchasePrice, decimal sellingPrice, DateTime expirationDate, StockNorm stockNorm, StorageConditions storageConditions)
        {
            var product = new Product(name,purchasePrice,sellingPrice, expirationDate);

            product.SetStockNorm(stockNorm);
            product.SetStorageConditions(storageConditions);

            return product;
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { 
                if(value < DateTime.Now)
                {
                    expirationDate = value;
                }
            }
        }
        public StockNorm? StockNorm
        {
            get { return stockNorm; }
            set { if (value is not null) { stockNorm = value; } }
        }
        public StorageConditions? StorageConditions
        {
            get { return storageConditions;}
            set { if (value is not null) { storageConditions = value; } }
        }
        public int? CategoryCode
        {
            get { return categoryCode; }
            set { categoryCode = value; }
        }
        public ProductCategory? Category
        {
            get { return category; }
            set { if(value is not null){ category = value; } }
        }
        public int CalculateRemainingStock()
        {
            return ProductBatch.GetAllBatches()
                .Where(batch => batch.Products.SalableCode == SalableCode)
                .Sum(batch => batch.RemainingQuantity);
        }
        public bool RequiresReplenishment()
        {
            int currentStock = CalculateRemainingStock();

            return StockNorm.isThresholdExceeded(currentStock);
        }
        public void SetStockNorm(StockNorm stockNorm)
        {
            StockNorm = stockNorm;
        }

        public void SetStorageConditions(StorageConditions storageConditions)
        {
            StorageConditions = storageConditions;
        }
    }
}
