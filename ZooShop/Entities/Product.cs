using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShop.Entities
{
    public class Product:Salable
    {
        private int productCode;
        private DateTime expirationDate;
        private StockNorm? stockNorm;
        private StorageConditions? storageConditions; 
        private Product(int productCode, string name, decimal purchasePrice, decimal sellingPrice, DateTime expirationDate)
        {
            this.productCode = productCode;
            this.name = name;
            this.purchasePrice = purchasePrice;
            this.sellingPrice = sellingPrice;
            this.expirationDate = expirationDate;
        }
        public static Product CreateProduct(int productCode, string name, decimal purchasePrice, decimal sellingPrice, DateTime expirationDate, StockNorm stockNorm, StorageConditions storageConditions)
        {
            var product = new Product(productCode,name,purchasePrice,sellingPrice, expirationDate);

            product.SetStockNorm(stockNorm);
            product.SetStorageConditions(storageConditions);

            return product;
        }
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
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
            set { if (storageConditions is not null) { storageConditions = value; } }
        }
        public int CalculateRemainingStock()
        {
            return ProductBatch.GetAllBatches()
                .Where(batch => batch.Products.ProductCode == ProductCode)
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
