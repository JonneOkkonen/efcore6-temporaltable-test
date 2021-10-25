using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace EFCore6SimpleTest
{
    public class Program
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };

        public static void Main(string[] args)
        {
            using(var context  = new TestDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            // Create
            using (var context = new TestDbContext())
            {
                var mainentity = new MainEntity
                {
                    Description = "Main entity",
                    OwnedEntity = new OwnedEntity
                    {
                        Description = "Owned entity"
                    }
                };
                context.MainEntities?.Add(mainentity);
                context.SaveChanges();
            }
            // Get First MainEntity
            using (var context = new TestDbContext())
            {
                var mainentity = context.MainEntities.First();
                var json = JsonSerializer.Serialize(mainentity, jsonSerializerOptions);
                Console.WriteLine("Original");
                Console.WriteLine(json);
            }

            Thread.Sleep(2000);

            // Change
            using (var context = new TestDbContext())
            {
                var mainentity = context.MainEntities.First();
                mainentity.Description = "Changed main entity";
                mainentity.OwnedEntity.Description = "Changed owned entity";
                context.SaveChanges();
            }
            // Get First MainEntity
            using (var context = new TestDbContext())
            {
                var mainentity = context.MainEntities.First();
                var json = JsonSerializer.Serialize(mainentity, jsonSerializerOptions);
                Console.WriteLine("Changed");
                Console.WriteLine(json);
            }

            // History: Entity before change
            using (var context = new TestDbContext())
            {
                var currentmainentity = context.MainEntities.First();
                if (context.Entry(currentmainentity).Property("StartTime").CurrentValue is DateTime mestarttime)
                {
                    var oldmainentity = context.MainEntities
                        .TemporalAsOf(mestarttime.AddMilliseconds(-1));
                    var json = JsonSerializer.Serialize(oldmainentity, jsonSerializerOptions);
                    Console.WriteLine("History");
                    Console.WriteLine(json);
                }
            }

            //// Remove
            //using (var context = new TestDbContext())
            //{
            //    var currentMainEntity = context.MainEntities.First();
            //    context.Remove(currentMainEntity);
            //    context.SaveChanges();
            //}
            //// Get MainEntity Count
            //using (var context = new TestDbContext())
            //{
            //    var mainentity = context.MainEntities.Count();
            //    var json = JsonSerializer.Serialize(mainentity, jsonSerializerOptions);
            //    Console.WriteLine("Count after delete");
            //    Console.WriteLine(json);
            //}

            //// History: Get all MainEntities
            //using (var context = new TestDbContext())
            //{
            //    var oldmainentity = context.MainEntities
            //        .TemporalAll();
            //    var json = JsonSerializer.Serialize(oldmainentity, jsonSerializerOptions);
            //    Console.WriteLine("History all");
            //    Console.WriteLine(json);
            //}
        }
    }
}
