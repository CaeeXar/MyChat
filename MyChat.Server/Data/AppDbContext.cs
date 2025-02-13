namespace MyChat.App.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using MyChat.Core.Model;
    using MyChat.Server;

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        //public DbSet<TestA> As { get; set; } = null!;
        //public DbSet<TestB> Bs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = (string?)AppSettings.GetSetting<string>("ConnectionStrings:MyChatDatabase");
            if (string.IsNullOrEmpty(connectionString)) return;
            optionsBuilder.UseNpgsql(connectionString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            // TestA
            modelBuilder.Entity<TestA>().HasKey(u => u.Id);

            modelBuilder.Entity<TestA>()
                .Property(u => u.Price)
                .HasColumnType("decimal(6,2)");

            modelBuilder.Entity<TestA>()
                .HasOne(t => t.TestB) // 1:1 relation
                .WithOne(t => t.TestA)
                .HasForeignKey<TestB>(t => t.TestAId)
                .OnDelete(DeleteBehavior.Cascade); // Delete TestB, if TestA gets deleted
            
            // TestB
            modelBuilder.Entity<TestB>().HasKey(u => u.Id);

            modelBuilder.Entity<TestB>()
                .HasOne(t => t.TestA) // 1:1 relation
                .WithOne(t => t.TestB)
                .HasForeignKey<TestB>(t => t.TestAId)
                .OnDelete(DeleteBehavior.Cascade); // Delete TestB, if TestA gets deleted
        }
    }
}
