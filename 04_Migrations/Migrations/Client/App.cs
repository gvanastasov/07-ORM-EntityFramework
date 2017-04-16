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

            //Task1_LocalStoreCreate(ctx);
        }

        private static void Task1_LocalStoreCreate(LocalStoreContext ctx)
        {
            ctx.Products.Add(new Product()
            {
                Name = "Lutenica",
                Price = 2.60m,
            });

            ctx.Products.Add(new Product()
            {
                Name = "Kashkaval",
                Price = 3.60m,
            });

            ctx.Products.Add(new Product()
            {
                Name = "Sirene",
                Price = 8.60m,
            });

            ctx.SaveChanges();
        }
    }
}
