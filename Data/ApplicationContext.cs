using Microsoft.EntityFrameworkCore;
using Book_api.Models;
using System;

namespace Book_api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Smith" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book One", Author = "Author One", Price = 9.99m, Genre = "Fiction" },
                new Book { Id = 2, Title = "Book Two", Author = "Author Two", Price = 19.99m, Genre = "Non-Fiction" },
                new Book { Id = 3, Title = "Book Three", Author = "Author Three", Price = 29.99m, Genre = "Fiction" },
                new Book { Id = 4, Title = "Book Four", Author = "Author Four", Price = 39.99m, Genre = "Roman" },
                new Book { Id = 5, Title = "Book Five", Author = "Author Five", Price = 49.99m, Genre = "Action" }
            );

            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, Name = "John's Cart", Addres = "123 Main St", UserId = 1 },
                new Cart { Id = 2, Name = "Jane's Cart", Addres = "456 Elm St", UserId = 2 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = 1, OrderDate = DateTime.Now },
                new Order { Id = 2, UserId = 2, OrderDate = DateTime.Now }
            );
        }
    }
}
