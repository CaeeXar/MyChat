namespace MyChat.App.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using MyChat.Core.Model;
    
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region TEST
            //// TestA
            //modelBuilder.Entity<TestA>().HasKey(u => u.Id);

            //modelBuilder.Entity<TestA>()
            //    .Property(u => u.Price)
            //    .HasColumnType("decimal(6,2)");

            //modelBuilder.Entity<TestA>()
            //    .HasOne(t => t.TestB) // 1:1 relation
            //    .WithOne(t => t.TestA)
            //    .HasForeignKey<TestB>(t => t.TestAId)
            //    .OnDelete(DeleteBehavior.Cascade); // Delete TestB, if TestA gets deleted
            
            //// TestB
            //modelBuilder.Entity<TestB>().HasKey(u => u.Id);

            //modelBuilder.Entity<TestB>()
            //    .HasOne(t => t.TestA) // 1:1 relation
            //    .WithOne(t => t.TestB)
            //    .HasForeignKey<TestB>(t => t.TestAId)
            //    .OnDelete(DeleteBehavior.Cascade); // Delete TestB, if TestA gets deleted
            #endregion TEST
        }
    }
}
