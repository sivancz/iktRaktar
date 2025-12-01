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
        private List<Product> items = new List<Product>();

        public void Add(Product product) { 
            items.Add(product);
        }

        public Product? FindById(int id)
        {
            foreach (var item in items)
            {
                if (item.Id == id) return item;
            }
            return null;
        }

        public IEnumerable<Product> FindAll(string name)
        {
            List<Product> searchedProducs = new List<Product>();
            foreach (Product product in items)
            {
                if (product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    searchedProducs.Add(product);
                }
            }
            return searchedProducs;
        }

    }

}
