using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);    
            }

        }

        private static void SeedData(AppDbContext dbContext, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations");
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"---> Could not run migrations: {ex.Message}");
                }
                
            }
            Console.BackgroundColor = ConsoleColor.Red;
            if(!dbContext.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");
                dbContext.Platforms.AddRange(
                    new Platform() {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() {Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"},
                    new Platform() {Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}

                );

                dbContext.SaveChanges();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("--> we already have data");
            }

        }
    }
}