using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public static class OrdersAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/orders", (HipHopPizzaNWingsDbContext db) =>
            {
                return db.Orders.Include(o => o.OrderType).ToList();
            });
        }
    }
}
