using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iktraktar.Models.Interfaces;

namespace iktraktar.Models
{
    internal class Storage : ISearchable<Product>
    {
        private List<Product> items = new List<Product>();

        public void Add(Product product)
        {
            items.Add(product);
        }


        public IEnumerable<Product> FindAll(string name)
        {
            List<Product> searchedProducts = new List<Product>();
            foreach (Product product in items)
            {
                if (product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    searchedProducts.Add(product);
                }
            }
            return searchedProducts;
        }

        public Product? FindById(int Id)
        {
            foreach (var item in items)
            {
                if (item.Id == Id) return item;
            }
            return null;
        }

        public void IncreaseQuantity(int id, int amount)
        {
            Product? product = FindById(id);
            if (product != null)
            {
                product.Quantity += amount;
            }
            else
            {
                Console.WriteLine("Nincs ilyen termék!");
            }
        }
        public void DecreaseQuantity(int id, int amount)
        {
            Product? product = FindById(id);
            if (product != null)
            {
                product.Quantity -= amount;

                if (product.Quantity < 0)
                {
                    product.Quantity = 0;
                }
            }
            else
            {
                Console.WriteLine("Nincs ilyen termék!");
            }
        }

    }



}