using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public class OrderItemsAPI
    {
        public static void Map(WebApplication app)
        {
            //Add item to order
            app.MapPost("/order/addItem", (HipHopPizzaNWingsDbContext db, AddItemDTO orderItem) =>
            {
                Order orderBeingAddedTo = db.Orders.SingleOrDefault(o => o.Id == orderItem.OrderId);
                Item itemBeingAdded = db.Items.SingleOrDefault(i => i.Id == orderItem.ItemId);
                OrderItem newOrderItem = new();

                newOrderItem.Item = itemBeingAdded;
                newOrderItem.Order = orderBeingAddedTo;
                orderBeingAddedTo.Items.Add(newOrderItem);
                db.SaveChanges();
                return Results.Ok("Item added to order");
            });

            //Remove item from order
            app.MapDelete("/order/removeItem", (HipHopPizzaNWingsDbContext db, int orderItemId) =>
            { 
                var orderItem = db.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
                if (orderItem == null)
                {
                    return Results.NotFound("Unable to find item");
                }
                db.OrderItems.Remove(orderItem);
                db.SaveChanges();
                return Results.Ok();

            });
        }
    }
}
