using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZooShop.Entities
{
    public class ProductCategory
    {
        private int categoryCode;
        private string categoryName;
        private List<Product>? products = new();
        private ProductCategory(string categoryName)
        {
            this.categoryName = categoryName;
        }
        public ProductCategory()
        {
        }
        public static ProductCategory CreateCategory(string categoryName)
        {
            return new ProductCategory(categoryName);
        }
        
        public int CategoryCode { 
            get { return categoryCode; } 
            set { categoryCode = value; } 
        }
        public string CategoryName { 
            get { return categoryName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    categoryName = value;
                }
            }
        }
        public List<Product>? Products
        {
            get { return products; }
            set {if(value != null) { products = value; } }
        }
    }
}
