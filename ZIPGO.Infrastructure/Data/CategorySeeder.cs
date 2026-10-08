using Microsoft.EntityFrameworkCore;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Infrastructure.Data
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            
            // MAIN CATEGORIES


            var mainCategoryNames = new[]
            {
                "Hiking & Trekking",
                "International Travel",
                "Bike Trips",
                "Camping & Outdoors",
                "Weekend Getaways",
                "City Explorer",
                "Workcation"
            };

            var mainCategories = new List<MainCategory>();

            foreach (var name in mainCategoryNames)
            {
                MainCategory? mainCategory = null;

                // Find existing category by exact name
                mainCategory = await context.MainCategories
                    .FirstOrDefaultAsync(m => m.Name == name);

                // Rename old test category
                if (mainCategory == null && name == "Hiking & Trekking")
                {
                    mainCategory = await context.MainCategories
                        .FirstOrDefaultAsync(m => m.Name == "Hiking&treking");
                }

                if (mainCategory == null)
                {
                    mainCategory = new MainCategory
                    {
                        Name = name
                    };

                    context.MainCategories.Add(mainCategory);
                }
                else
                {
                    mainCategory.Name = name;
                }

                mainCategories.Add(mainCategory);
            }

            await context.SaveChangesAsync();


           
            // SUB CATEGORIES
            

            var subCategoryNames = new[]
            {
                "Packing & Organizers",
                "Trolleys & Backpacks",
                "Tech & Gadgets",
                "Outdoor & Adventures",
                "Comfort",
                "Toiletries"
            };

            var subCategories = new List<SubCategory>();

            foreach (var name in subCategoryNames)
            {
                SubCategory? subCategory = null;

                // Find existing category
                subCategory = await context.SubCategories
                    .FirstOrDefaultAsync(s => s.Name == name);

                // Rename old test categories
                if (subCategory == null && name == "Toiletries")
                {
                    subCategory = await context.SubCategories
                        .FirstOrDefaultAsync(s => s.Name == "toiletries");
                }

                if (subCategory == null && name == "Tech & Gadgets")
                {
                    subCategory = await context.SubCategories
                        .FirstOrDefaultAsync(s => s.Name == "tech & gadgets");
                }

                if (subCategory == null)
                {
                    subCategory = new SubCategory
                    {
                        Name = name
                    };

                    context.SubCategories.Add(subCategory);
                }
                else
                {
                    subCategory.Name = name;
                }

                subCategories.Add(subCategory);
            }

            await context.SaveChangesAsync();


            
            // MANY-TO-MANY RELATIONSHIP
            

            foreach (var mainCategory in mainCategories)
            {
                foreach (var subCategory in subCategories)
                {
                    var alreadyExists = await context.SubCategories
                        .Where(s => s.Id == subCategory.Id)
                        .SelectMany(s => s.MainCategories)
                        .AnyAsync(m => m.Id == mainCategory.Id);

                    if (!alreadyExists)
                    {
                        mainCategory.SubCategories.Add(subCategory);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}