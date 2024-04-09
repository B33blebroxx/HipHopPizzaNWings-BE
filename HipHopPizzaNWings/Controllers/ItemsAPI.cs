using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public static class ItemsAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/items", (HipHopPizzaNWingsDbContext db) =>
            {
                var items = db.Items.Include(i => i.Order).ToList();
                if (items == null)
                {
                    return Results.NotFound("No items found.");
                }
                return Results.Ok(items);
            });
        }
    }
}
