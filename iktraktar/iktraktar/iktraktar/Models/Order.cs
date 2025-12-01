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
        public int Id { get; }

        public List<OrderItem> Items { get; } = new List<OrderItem>();

        public Order(int id)
        {
            Id = id;
        }
    }
}
