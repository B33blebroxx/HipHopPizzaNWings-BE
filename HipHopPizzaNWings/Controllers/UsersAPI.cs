using HipHopPizzaNWings.Models;
using Microsoft.EntityFrameworkCore;
namespace HipHopPizzaNWings.Controllers
{
    public static class UsersAPI
    {
        public static void Map(WebApplication app)
        {
            //Create user if not registered user
            app.MapPut("/checkuser/{uid}", (HipHopPizzaNWingsDbContext db, string uid, User newUser) =>
            {
                var user = db.Users.SingleOrDefault(u => u.Uid == uid);
                if (user == null)
                {
                    try
                    {
                        db.Users.Add(newUser);
                        db.SaveChanges();
                        return Results.Created($"/users/{newUser.Id}", newUser);
                    }
                    catch (DbUpdateException)
                    {
                        return Results.BadRequest("Couldn't save new user to database");
                    }

                }
                return Results.Ok(user);
            });
        }
    }
}
