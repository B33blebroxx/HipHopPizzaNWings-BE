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
                var orders = db.Orders.Include(o => o.OrderType).ToList();
                if (orders == null)
                {
                    return Results.NotFound("No orders found.");
                }
                return Results.Ok(orders);
            });

            //Get single order and its items
            app.MapGet("/orders/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                var orderAndItems = db.Orders.Include(i => i.Items).ThenInclude(z => z.Item).FirstOrDefault(o => o.Id == orderId);
                if (orderAndItems == null)
                {
                    return Results.NotFound("Order not found.");
                }
                return Results.Ok(orderAndItems);
            });

            //Create a new order
            app.MapPut("/orders/new", (HipHopPizzaNWingsDbContext db, Order newOrder) =>
            {
                try 
                {
                    db.Add(newOrder);
                    db.SaveChanges();
                    return Results.Created($"/newOrder.Id", newOrder);
                }
                catch (DbUpdateException)
                {
                    return Results.BadRequest("Unable to save order to DB.");
                }
            });

            //Delete an order
            app.MapDelete("/orders/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                var orderToDelete = db.Orders.SingleOrDefault(o => o.Id == orderId);
                if (orderToDelete == null)
                {
                    return Results.NotFound("No order found.");
                }

                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
                return Results.Ok("Order successfully deleted.");
            });
        }
    }
}
