using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public static class UsersAPI
    {
        public static void Map(WebApplication app)
        {
            //Checkuser
            app.MapGet("/checkuser/{uid}", (HipHopPizzaNWingsDbContext db, string uid) =>
            {
                var user = db.Users.FirstOrDefault(u => u.Uid == uid);
                if (user == null)
                {
                    return Results.NotFound("User not registered");
                }
                return Results.Ok(user);
            });

            //Register user
            app.MapPost("/register", (HipHopPizzaNWingsDbContext db, User newUser) =>
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Results.Created($"/user/{newUser.Id}", newUser);
            });
        }
    }
}
