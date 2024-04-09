using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public class OrderItemsAPI
    {
        public static void Map(WebApplication app)
        {
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
        }
    }
}
