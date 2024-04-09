using Microsoft.EntityFrameworkCore;
using HipHopPizzaNWings.Models;

    public class HipHopPizzaNWingsDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


    public HipHopPizzaNWingsDbContext(DbContextOptions<HipHopPizzaNWingsDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Item>().HasData(new Item[]
        {
            new Item { Id = 1, Name = "Large Pepperoni Pizza", Price = 12.00M},
            new Item { Id = 2, Name = "Large Supreme Pizza", Price = 14.00M},
            new Item { Id = 3, Name = "Large Cheese Pizza", Price = 10.00M},
            new Item { Id = 4, Name = "8 Piece Buffalo Wings", Price = 7.00M},
            new Item { Id = 5, Name = "12 Piece Buffalo Wings", Price = 11.00M}
        });

        modelBuilder.Entity<OrderType>().HasData(new OrderType[]
        {
            new OrderType { Id = 1, Type = "Online"},
            new OrderType { Id = 2, Type = "In Store"},
            new OrderType { Id = 3, Type = "Phone"}
        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, Uid = "06mu1mmHWrMad67XQeCBnKyYKPw1"}
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CustomerName = "Mick Foley", PhoneNumber = "323-416-5523", Email = "JLee@email.com", OrderTypeId = 1, DateCreated = new DateTime(2024,04,08), DateClosed = null, IsClosed = false},
            new Order { Id = 2, CustomerName = "Stan Hansen", PhoneNumber = "323-512-0833", Email = "SHanson@email.com", OrderTypeId = 3, DateCreated = new DateTime(2024,04,03), DateClosed = new DateTime(2024,04,03), IsClosed = true, Subtotal = 23.00M, Tip = 5.00M },
            new Order { Id = 3, CustomerName = "Brody Lee", PhoneNumber = "323-723-9376", Email = "BrodyForever@email.com", OrderTypeId = 2, DateCreated = new DateTime(2024,04,07), DateClosed = null, IsClosed = false}
        });
    }

}

