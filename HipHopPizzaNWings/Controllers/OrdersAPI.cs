using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public static class OrdersAPI
    {
        public static void Map(WebApplication app)
        {
            //Get all orders
            app.MapGet("/orders", (HipHopPizzaNWingsDbContext db) =>
            {
                return db.Orders.Include(o => o.OrderType).ToList();
            });

            //Get single order and its items
            app.MapGet("/orders/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                return db.Orders.Include(i => i.Items).ThenInclude(z => z.Item).FirstOrDefault(o => o.Id == orderId);
            });

            //Create a new order
            app.MapPut("/orders/new", (HipHopPizzaNWingsDbContext db, Order newOrder) =>
            {
                db.Add(newOrder);
                db.SaveChanges();
                return Results.Created($"/newOrder.Id", newOrder);
            });

        }
    }
}
