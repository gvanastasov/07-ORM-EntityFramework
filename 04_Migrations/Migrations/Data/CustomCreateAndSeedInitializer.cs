
namespace Data
{
    using Data.Models;
    using System.Data.Entity;

    class CustomCreateAndSeedInitializer : CreateDatabaseIfNotExists<LocalStoreContext>
    {
        protected override void Seed(LocalStoreContext context)
        {
            var lutenica = context.Products.Add(new Product() { Name = "Lutenica", Price = 2.55m, Quantity = 10 });
            var ketchup = context.Products.Add(new Product(){ Name = "Kechup",Price = 1.55m,Quantity = 1 });
            var domat = context.Products.Add(new Product(){ Name = "Domat",Price = 0.6m,Quantity = 100 });
            context.Products.Add(new Product(){ Name = "Chuska",Price = 0.55m,Quantity = 50 });
            context.Products.Add(new Product(){ Name = "Luk",Price = 0.35m,Quantity = 33 });

            var pesho = context.Customers.Add(new Customer() { FirstName = "Pesho" });
            context.Customers.Add(new Customer() { FirstName = "Ivan" });
            context.Customers.Add(new Customer() { FirstName = "Tosho" });
            var gosho = context.Customers.Add(new Customer() { FirstName = "Gosho" });
            context.Customers.Add(new Customer() { FirstName = "Daniel" });

            var centar = context.StoreLocations.Add(new StoreLocation() { LocationName = "Centar" });
            context.StoreLocations.Add(new StoreLocation() { LocationName = "Krainovo" });
            var dolnovo = context.StoreLocations.Add(new StoreLocation() { LocationName = "Dolnovo" });
            context.StoreLocations.Add(new StoreLocation() { LocationName = "New Dolnovo" });
            context.StoreLocations.Add(new StoreLocation() { LocationName = "Ghost Town" });

            context.Sales.Add(new Sale() { Product = lutenica, Customer = pesho, StoreLocation = centar });
            context.Sales.Add(new Sale() { Product = ketchup, Customer = pesho, StoreLocation = centar });
            context.Sales.Add(new Sale() { Product = domat, Customer = gosho, StoreLocation = dolnovo });
            context.Sales.Add(new Sale() { Product = ketchup, Customer = gosho, StoreLocation = centar });
            context.Sales.Add(new Sale() { Product = lutenica, Customer = gosho, StoreLocation = centar });


            base.Seed(context);
        }

    }
}
