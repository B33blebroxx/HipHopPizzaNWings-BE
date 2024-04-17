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

                   //Get single order details
            app.MapGet("/orders/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                var order = db.Orders.Include(i => i.OrderType).FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                {
                    return Results.NotFound("Order not found.");
                }
                return Results.Ok(order);
            });

            //Get order items
            app.MapGet("/orders/{orderId}/items", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                var orderItems = db.OrderItems.Where(oi => oi.Order.Id == orderId)
                                 .Select(oi => new
                                 {
                                     OrderId = oi.Order.Id,
                                     Name = oi.Item.Name,
                                     OrderItemId = oi.Id,
                                     Price = oi.Item.Price,
                                     IsClosed = oi.Order.IsClosed
                                 }).ToList();
                if (orderItems == null)
                {
                    return Results.NotFound("No order items found.");
                }
                return Results.Ok(orderItems);
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
                    return Results.Ok("Order created");
            });

            //Delete an order
            app.MapDelete("/orders/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId) =>
            {
                var orderToDelete = db.Orders.Include(o => o.Items).FirstOrDefault(i => i.Id == orderId);

                if (orderToDelete != null)
                {
                    foreach (var item in orderToDelete.Items)
                    {
                        db.OrderItems.Remove(item);
                    }

                    db.Orders.Remove(orderToDelete);
                    db.SaveChanges();
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
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

            //Close an order
            app.MapPut("/order/{orderId}", (HipHopPizzaNWingsDbContext db, int orderId, CloseOrderDTO closedOrder) =>
            {
                var orderToClose = db.Orders.SingleOrDefault(o => o.Id == orderId);
                if (orderToClose == null)
                {
                    return Results.NotFound("Order not found");
                }

                orderToClose.DateClosed = DateTime.Now;
                orderToClose.IsClosed = true;
                orderToClose.Tip = closedOrder.Tip;
                db.SaveChanges();
                return Results.Ok("Order closed");
            });

            //Get order Total
            app.MapGet("/orderTotal/{id}", (HipHopPizzaNWingsDbContext db, int id) => 
            {
                var order = db.Orders
                         .Include(order => order.Items)
                         .ThenInclude(orderItem => orderItem.Item)
                         .SingleOrDefault(order => order.Id == id);


                return new OrderTotalDTO
                {
                    Total = order.Total,
                    SubTotal = order.Subtotal,
                    Tip = order.Tip,
                };
            });

            //Get Revenue
            app.MapGet("/revenue", (HipHopPizzaNWingsDbContext db) =>
            {
                var closedOrders = db.Orders
                                 .Include(o => o.Items)
                                 .ThenInclude(oi => oi.Item)
                                 .Where(o => o.IsClosed)
                                 .ToList();

                if (closedOrders == null || !closedOrders.Any())
                {
                    return Results.NotFound("No closed orders.");
                }

                var revenue = new OrderTotalDTO
                {
                    SubTotal = closedOrders.Sum(o => o.Subtotal),
                    Total = closedOrders.Sum(o => o.Total),
                    Tip = closedOrders.Sum(o => o.Tip)
                };

                return Results.Ok(revenue);
            });

        }
    }
}
