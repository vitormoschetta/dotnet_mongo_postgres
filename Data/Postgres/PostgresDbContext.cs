using dotnet_mongodb.Application.CreditCard;
using dotnet_mongodb.Application.Expense;
using dotnet_mongodb.Application.Tag;
using dotnet_mongodb.Application.User;
using Microsoft.EntityFrameworkCore;

namespace dotnet_mongodb.Data.Postgres
{
    public class PostgresDbContext: DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<CreditCardEntity> CreditCards { get; set; } = null!;
        public DbSet<ExpenseEntity> Expenses { get; set; } = null!;
        public DbSet<TagEntity> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(u => u.Email);
                       
            modelBuilder.Entity<CreditCardEntity>().HasKey(cc => cc.Id);
            modelBuilder.Entity<CreditCardEntity>().HasOne(cc => cc.User).WithMany(u => u.CreditCards).HasForeignKey(cc => cc.UserEmail);

            modelBuilder.Entity<ExpenseEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<ExpenseEntity>().HasOne(e => e.CreditCard).WithMany(u => u.Expenses).HasForeignKey(e => e.CreditCardId);

            modelBuilder.Entity<TagEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<TagEntity>().HasOne(t => t.User).WithMany(u => u.Tags).HasForeignKey(t => t.UserEmail);
            modelBuilder.Entity<TagEntity>().HasMany(t => t.Expenses).WithMany(e => e.TagsEntity).UsingEntity<Dictionary<string, object>>(
                "ExpenseTag",
                j => j.HasOne<ExpenseEntity>().WithMany().HasForeignKey("ExpenseId"),
                j => j.HasOne<TagEntity>().WithMany().HasForeignKey("TagId"),
                j =>
                {
                    j.Property<int>("Id").ValueGeneratedOnAdd();
                    j.HasKey("Id");
                }
            );

            // Permitir gravação de DateTime sem especificar UTC no Postgres 
            // https://github.com/npgsql/doc/blob/main/conceptual/Npgsql/types/datetime.md/
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}