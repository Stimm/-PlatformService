using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{

    public static void PrepPopulation(IApplicationBuilder app)
    {
        using ( var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("Seeding Data");

            context.Platforms.AddRange(
                new Platform() { Name = "Dot Net", Id = 1, Publisher = "Microsoft", Cost="Free"},
                new Platform() { Name = "SQL Server Express", Id = 1, Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Dot Kuberneties", Id = 1, Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );
            
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Data already present");
        }
    }
}
