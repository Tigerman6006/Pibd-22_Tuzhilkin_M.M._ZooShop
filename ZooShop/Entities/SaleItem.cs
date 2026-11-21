using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShop.Entities
{
    public class SaleItem
    {
        private int id;
        private Salable? salable;
        private int? salableCode;
        private Sale? sale;
        private int? receiptNumber;
        private int quantity;
        private decimal price;
        private decimal discount;
        private SaleItem(int quantity,decimal price, decimal discount)
        {
            this.quantity = quantity;
            this.price = price;
            this.discount = discount;
        }
        public SaleItem()
        {

        }
        public static SaleItem CreateSaleItem( Salable? salable, int quantity, decimal price, decimal discount)
        {
            var saleItem = new SaleItem(quantity, price, discount);
            saleItem.Salable = salable;
            return saleItem;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int? SalableCode
        {
            get { return salableCode; }
            set { salableCode = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public Salable? Salable
        {
            get{ return salable; }
            set { if (value is not null) { salable = value; } }
        }
        public Sale? Sale
        {
            get {return sale; }
            set { sale = value; }
        }
        public int? ReceiptNumber
        {
            get { return receiptNumber; }
            set { receiptNumber = value; }
        }
    }
}
