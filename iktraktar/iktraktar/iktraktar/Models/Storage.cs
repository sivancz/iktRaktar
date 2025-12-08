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
public static bool ProcessOrder(Order order, Storage storage)
        {
             foreach (var item in order.Items)
             {
                    var product = storage.FindById(item.Product.Id);
                    if (product == null || product.Quantity < item.Quantity)
                    {
                        Console.WriteLine($"Hiba: Nincs elegendő készlet a(z) '{item.Product.Name}' termékből. Igényelt: {item.Quantity}, Elérhető: {product?.Quantity ?? 0}");
                        return false;
                    }
             }

                var levontKeszlet = new List<string>();
                foreach (var item in order.Items)
                {
                    var product = storage.FindById(item.Product.Id);
                    product.Quantity -= item.Quantity;
                    levontKeszlet.Add($"#{product.Id} {product.Name} (-{item.Quantity})");
                }

                Console.WriteLine($"Rendelés feldolgozva #{order.Id}");
                Console.WriteLine($"    Levont készlet: {string.Join(", ", levontKeszlet)}");
                return true;
            }
        }
    }