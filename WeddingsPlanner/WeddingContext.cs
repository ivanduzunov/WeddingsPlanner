namespace WeddingsPlanner
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeddingContext : DbContext
    {

        public WeddingContext()
            : base("name=WeddingContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}