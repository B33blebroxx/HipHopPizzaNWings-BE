namespace HipHopPizzaNWings.Controllers
{
    public class OrderTypesAPI
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/orderType", (HipHopPizzaNWingsDbContext db) =>
            {
                var orderTypes = db.OrderTypes.ToList();
                if (orderTypes == null)
                {
                    return Results.NotFound("No order types found");
                }
                return Results.Ok(orderTypes);
            });
        }
    }
}
