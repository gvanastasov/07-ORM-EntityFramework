namespace Data
{
    using Data.Models;
    using System.Data.Entity;

    public class CustomContextInit : DropCreateDatabaseIfModelChanges<LocalStoreContext>
    {
        protected override void Seed(LocalStoreContext context)
        {
            Task2_LocalStoreImprove(context);

            base.Seed(context);
        }

        private static void Task2_LocalStoreImprove(LocalStoreContext context)
        {
            context.Products.Add(new Product()
            {
                Name = "Lutenica",
                Price = 2.60m,
                DistributorName = "Pyrvomai"
            });

            context.Products.Add(new Product()
            {
                Name = "Kashkaval",
                Price = 3.60m,
                DistributorName = "Vitosha"
            });

            context.Products.Add(new Product()
            {
                Name = "Sirene",
                Price = 8.60m,
                DistributorName = "Aprilci"
            });
        }
    }
}
