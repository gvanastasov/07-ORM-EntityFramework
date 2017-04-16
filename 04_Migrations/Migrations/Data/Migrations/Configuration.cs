namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.LocalStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Data.LocalStoreContext";
        }

        protected override void Seed(Data.LocalStoreContext context)
        {
            var products = context.Products.ToList();

            foreach (var item in products)
            {
                if(string.IsNullOrEmpty(item.Description))
                {
                    item.Description = "No description";
                }
            }
        }
    }
}
