using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence;

namespace Persistence
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context)
        {
            try
            {
                if (!context.InputTypes.Any())
                {
                    var inputTypesData = File.ReadAllText("../Persistence/SeedData/InputTypes.json");
                    var inputTypes = JsonSerializer.Deserialize<List<InputType>>(inputTypesData);
                    
                    foreach (var item in inputTypes)
                    {
                        context.InputTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText("../Persistence/SeedData/Categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}