namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class LocalStoreContext : DbContext
    {
        public LocalStoreContext()
            : base("name=LocalStoreContext")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}