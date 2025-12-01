using iktraktar.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iktraktar.Models
{
    internal class Order : IIdentifiable
    {
        private static int idCounter = 1;
        public int Id { get; }

        public List<OrderItem> Items { get; } = new List<OrderItem>();

        public Order()
        {
            Id = idCounter++;
        }


        public bool AddItem(Product product, int quantity)
        {
            if (product == null) return false;
            if (quantity <= 0) return false;
            if (product.Quantity < quantity) return false;

            product.Quantity -= quantity;
            Items.Add(new OrderItem(product, quantity));
            return true;
        }

        public int GetTotalAmount()
        {
            return Items.Sum(i => i.Product.Quantity * i.Quantity);
        }

        public void SaveToFile(string path)
        {
            List<string> lines = new List<string>
            {
                $"Rendelés ID: {Id}",
                "Tételek:"
            };

            foreach (var item in Items)
            {
                lines.Add($"{item.Product.Name} - {item.Quantity} db");

            }

            lines.Add($"Összesen: {GetTotalAmount()} Ft");

            File.WriteAllLines(path, lines);
        }
    }
}
