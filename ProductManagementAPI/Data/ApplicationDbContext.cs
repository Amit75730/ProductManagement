using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }  // Add DbSet for OrderItem

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Ensure email is unique

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Store price in decimal format

            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Product)
                        .WithMany(p => p.OrderItems)
                        .HasForeignKey(oi => oi.ProductId);


            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)"); // Fixes decimal truncation warning

            // Configuring the relationship between Order and OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)  // Order has many OrderItems
                .WithOne(oi => oi.Order)    // OrderItem has one Order
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Electronics" },
                    new Category { Name = "Groceries" },
                    new Category { Name = "Fashion" },
                    new Category { Name = "Home & Kitchen" },
                    new Category { Name = "Beauty & Personal Care" },
                    new Category { Name = "Sports & Outdoors" },
                    new Category { Name = "Toys & Games" },
                    new Category { Name = "Automotive" },
                    new Category { Name = "Books" },
                    new Category { Name = "Healthcare" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                var savedCategories = context.Categories.ToList();
                var products = new List<Product>();

                foreach (var category in savedCategories)
                {
                    switch (category.Name)
                    {
                        case "Electronics":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Smartphone", Price = 699.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Laptop", Price = 1199.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Headphones", Price = 149.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Smartwatch", Price = 299.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Tablet", Price = 499.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Groceries":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Rice", Price = 2.99m, Unit = "kg", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Milk", Price = 1.50m, Unit = "litre", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Bread", Price = 1.00m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Eggs", Price = 3.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Sugar", Price = 2.49m, Unit = "kg", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Fashion":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "T-Shirt", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Jeans", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Sneakers", Price = 89.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Jacket", Price = 99.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Cap", Price = 14.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Home & Kitchen":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Microwave Oven", Price = 199.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Blender", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Toaster", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Dinner Set", Price = 79.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Cooking Pan", Price = 29.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Beauty & Personal Care":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Shampoo", Price = 9.99m, Unit = "litre", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Face Cream", Price = 14.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Perfume", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Hair Dryer", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Body Lotion", Price = 12.99m, Unit = "litre", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Sports & Outdoors":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Football", Price = 29.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Tennis Racket", Price = 79.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Yoga Mat", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Dumbbells", Price = 49.99m, Unit = "kg", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Cycling Helmet", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Toys & Games":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Lego Set", Price = 59.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Dollhouse", Price = 89.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Board Game", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Remote Car", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Puzzle Set", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Automotive":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Car Battery", Price = 129.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Motor Oil", Price = 29.99m, Unit = "litre", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Wiper Blades", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Tire Inflator", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Car Cover", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                        case "Books":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Science Fiction", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Mystery Novel", Price = 14.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Programming Guide", Price = 39.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Cookbook", Price = 24.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "History Book", Price = 29.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;
                        case "Healthcare":
                            products.AddRange(new List<Product>
                            {
                                new Product { Name = "Vitamin Supplements", Price = 24.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "First Aid Kit", Price = 19.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Blood Pressure Monitor", Price = 49.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Hand Sanitizer", Price = 5.99m, Unit = "litre", Stock = 200, CategoryId = category.CategoryId },
                                new Product { Name = "Face Masks", Price = 9.99m, Unit = "pcs", Stock = 200, CategoryId = category.CategoryId }
                            });
                            break;

                    }
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }

}
