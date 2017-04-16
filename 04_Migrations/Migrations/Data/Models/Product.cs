using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Sale> SalesOfProduct { get; set; }

        public Product()
        {
            this.SalesOfProduct = new HashSet<Sale>();
        }
    }
}
