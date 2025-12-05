using iktraktar.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iktraktar.Models
{
    internal class Storage : ISearchable<Product>
    {
        private List<Product> items;

        public void Add(Product product)
        {
            items ??= new List<Product>();
            items.Add(product);
        }

        public IEnumerable<Product> FindAll(string name)
        {
            return items?.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) ?? Enumerable.Empty<Product>();
        }

        public Product? FindById(int id)
        {
            return items?.FirstOrDefault(p => p.Id == id);
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