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
                return db.Items.Include(i => i.Order).ToList();
            });
        }
    }
}
