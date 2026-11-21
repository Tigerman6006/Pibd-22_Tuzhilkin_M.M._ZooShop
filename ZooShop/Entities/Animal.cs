using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShop.Entities
{
    public class Animal:Salable
    {
        private string species;
        private string breed;
        private DateTime dateOfBirth;
        private Animal(string name, decimal purchasePrice, decimal sellingPrice, string species, string breed, DateTime dateOfBirth)
        {
            this.name = name;
            this.purchasePrice = purchasePrice;
            this.sellingPrice = sellingPrice;
            this.species = species;
            this.breed = breed;
            this.dateOfBirth = dateOfBirth;
        }
        public static Animal CreateAnimal(string name, decimal purchasePrice, decimal sellingPrice, string species, string breed, DateTime dateOfBirth)
        {
            return new Animal(name, purchasePrice, sellingPrice, species, breed, dateOfBirth);
        }
        public string Species
        {
            get { return species; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    species = value;
                }
            }
        }
        public string Breed
        {
            get { return breed; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    breed = value;
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (value < DateTime.Now)
                {
                    dateOfBirth = value;
                }
            }
        }
    }
}
