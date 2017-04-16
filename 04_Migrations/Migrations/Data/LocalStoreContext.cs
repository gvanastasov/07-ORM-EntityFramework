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
            Database.SetInitializer<LocalStoreContext>(new CustomCreateAndSeedInitializer());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<StoreLocation> StoreLocations { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
    }
}