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
            app.MapPost("/orders/new", (HipHopPizzaNWingsDbContext db, CreateOrderDTO newOrder) =>
            {
                Order OrderBeingCreated = new()
                {
                    CustomerName = newOrder.CustomerName,
                    PhoneNumber = newOrder.PhoneNumber,
                    Email = newOrder.Email,
                    OrderTypeId = newOrder.OrderTypeId,
                    DateCreated = DateTime.Now,
                    IsClosed = false,
                };
              
                    db.Orders.Add(OrderBeingCreated);
                    db.SaveChanges();
                    return Results.Created($"/newOrder.Id", newOrder);
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

            //Update order details
            app.MapPut("/orders/update/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId, UpdateOrderDTO updatedOrder) =>
            {
                var orderBeingUpdated = db.Orders.SingleOrDefault(o => o.Id == orderId);
                if (orderBeingUpdated == null)
                {
                    return Results.NotFound("Can't find specified order.");
                }

                orderBeingUpdated.CustomerName = updatedOrder.CustomerName;
                orderBeingUpdated.PhoneNumber = updatedOrder.PhoneNumber;
                orderBeingUpdated.Email = updatedOrder.Email;
                orderBeingUpdated.OrderTypeId = updatedOrder.OrderTypeId;
                db.SaveChanges();
                return Results.Ok("Order details successfully updated.");

            });
            
        }
    }
}
