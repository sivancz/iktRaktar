using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace iktraktar.Models
{
    internal class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public OrderItem(Product product, int quantity)
        {
            //Product = product ?? throw new ArgumentNullException(nameof(product));
            if (product == null) 
            {
                throw new ArgumentNullException(nameof(product)); 
            }
            Product = product;


            if (quantity <= 0) throw new ArgumentOutOfRangeException("A mennyiség nagyobb kell legyen mint nulla!");

            Quantity = quantity;

        }
        public int GetTotalPrice()
        {
            return Product.Quantity * Quantity;
        }
    }

}


