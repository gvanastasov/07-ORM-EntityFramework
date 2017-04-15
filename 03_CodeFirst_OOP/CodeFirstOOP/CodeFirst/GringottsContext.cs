namespace CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using CodeFirst.Models;

    public partial class GringottsContext : DbContext
    {
        public GringottsContext()
            : base("name=GringottsDB")
        {
        }

        public DbSet<WizardDeposit> WizardDeposits { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //}
    }
}
