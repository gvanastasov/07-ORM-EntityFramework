namespace Client
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class App
    {
        static void Main(string[] args)
        {
            var ctx = new LocalStoreContext();
            ctx.Database.Initialize(true);

            ctx.Products.Add(new Product()
            {
                Name = "Lutenica",
                Price = 2.60m,
                DistributorName = "Pyrvomai"
            });

            ctx.Products.Add(new Product()
            {
                Name = "Kashkaval",
                Price = 3.60m,
                DistributorName = "Vitosha"
            });

            ctx.Products.Add(new Product()
            {
                Name = "Sirene",
                Price = 8.60m,
                DistributorName = "Aprilci"
            });

            ctx.SaveChanges();
        }
    }
}
