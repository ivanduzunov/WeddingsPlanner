namespace WeddingsPlanner
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class WeddingContext : DbContext
    {

        public WeddingContext()
            : base("name=WeddingContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.Gender).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.Email).IsOptional();
            modelBuilder.Entity<Person>().Property(p => p.Phone).IsOptional();
            modelBuilder.Entity<Person>().Property(p => p.MiddleNameSymbol).IsOptional();
            modelBuilder.Entity<Person>().Property(p => p.Birthday).IsOptional();
            modelBuilder.Entity<Person>().HasOptional(p => p.BrideWeddding).WithRequired(w => w.Bride);
            modelBuilder.Entity<Person>().HasOptional(p => p.BridegroomWeddding).WithRequired(w => w.Bridegroom);

            modelBuilder.Entity<Wedding>().HasRequired(w => w.Bride);
            modelBuilder.Entity<Wedding>().HasRequired(w => w.Bridegroom);
            modelBuilder.Entity<Wedding>().HasMany(w => w.Venues).WithMany(v => v.Weddings).
                Map(wed => {
                    wed.ToTable("Venues and Weddings");
                    wed.MapLeftKey("Wedding");
                    wed.MapRightKey("Venue");});

            modelBuilder.Entity<Cash>().HasRequired(c => c.Invitation).WithOptional(i => i.Cash);
            modelBuilder.Entity<Gift>().HasRequired(c => c.Invitation).WithOptional(i => i.Gift);
            modelBuilder.Entity<Invitation>().HasRequired(i => i.Guest).WithMany(p => p.Invitations);
          
            modelBuilder.Entity<Cash>().HasRequired(c => c.Owner);
            modelBuilder.Entity<Gift>().HasRequired(c => c.Owner);

        }

        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Cash> CashPresents { get; set; }
        public virtual DbSet<Gift> GiftPresents { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<Wedding> Weddings { get; set; }


    }

    
}