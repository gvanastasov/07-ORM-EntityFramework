using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class StoreLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public virtual ICollection<Sale> SalesInStore { get; set; }

        public StoreLocation()
        {
            this.SalesInStore = new HashSet<Sale>();
        }
    }
}
