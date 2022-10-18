using theFoodCampus.Data;
using Microsoft.EntityFrameworkCore;

namespace theFoodCampus.Models
{
    public class SeedData
    {

        public static void Intitialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Recipes.Any())
                {
                    return;   //DB has been seeded
                }
                context.Recipes.AddRange(
                    new Recipe
                    {
                        Name = "Salmon",
                        Rating = 4,
                        RKashrut = Kashrut.Dairy,
                        Rdifficulty = Difficulty.Easy,
                        RPrepTime = PrepTime.Quick,
                        RType = Category.Fish,
                        RDiet = Diet.GlutenFree,
                        RBudget = Budget.Expensive,
                        Ingredients = new List<Ingredient>() { new Ingredient { Name = "Fillet Salmon", Amount = 6, MeasurementUnit = MeasurementType.Ounces }, new Ingredient { Name = "Manischewitz Honey", Amount = 1, MeasurementUnit = MeasurementType.Teaspoon }, new Ingredient { Name = "Hadar Dijon Mustard", Amount = 1, MeasurementUnit = MeasurementType.Teaspoon }, new Ingredient { Name = "Gefen Maple Syrup", Amount = 1, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Pereg Japanese Style Fine Panko", Amount = 5, MeasurementUnit = MeasurementType.Cup } },
                        Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "Preheat the oven to 400 ferenheit, Place the Salmon on a baking asheet lined with Gefet Parchment" },
                            new Instruction { Step = "Whist together the honey, mustard and maple syrup" },
                            new Instruction { Step = "Brush the mixture over the salmon fillets, Sprinkle the panko over each fillet, gently pressing down as you go making sure the panko adheres to the fish, Bake for 15 minutes" }
                        }
                    },
                    new Recipe
                    {
                        Name = "Chocolate Chip Cookies",
                        Rating = 5,
                        RKashrut = Kashrut.Pareve,
                        Rdifficulty = Difficulty.Easy,
                        RPrepTime = PrepTime.Medium,
                        RType = Category.CakeAndCookies,
                        RDiet = Diet.Vegeterian,
                        RBudget = Budget.Cheap,
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient { Name = "Oil", Amount = 1, MeasurementUnit = MeasurementType.Cup },
                            new Ingredient { Name = "Sugar", Amount = 5, MeasurementUnit = MeasurementType.Cup },
                            new Ingredient { Name = "Brown Sugar", Amount = 1.5, MeasurementUnit = MeasurementType.Cup },
                            new Ingredient { Name = "Vanilla Extract", Amount = 2.5, MeasurementUnit = MeasurementType.Teaspoon },
                            new Ingredient { Name = "Flour", Amount = 2.5, MeasurementUnit = MeasurementType.Cup },
                            new Ingredient { Name = "Baking Powder", Amount = 1, MeasurementUnit = MeasurementType.Teaspoon },
                            new Ingredient { Name = "Baking Soda", Amount = 1, MeasurementUnit = MeasurementType.Teaspoon },
                            new Ingredient { Name = "Salt", Amount = 1, MeasurementUnit = MeasurementType.Teaspoon },
                            new Ingredient { Name = "Chocolate Chips", Amount = 7, MeasurementUnit = MeasurementType.Ounces }
                        },
                        Instructions = new()
                        {
                            new Instruction { Step = "Preheat oven to 350 ferenheit" },
                            new Instruction { Step = "Combine all ingredients without the chocolate chips untill mixture is solid, then add chocolate chips, combine evenly " },
                            new Instruction { Step = "Roll into balls, place 2 inches apart on cookie sheet, bake for 15 minutes." }
                        }
                    },
                    new Recipe
                    {
                        Name = "Potato Latkes",
                        Rating = 5,
                        RKashrut = Kashrut.Pareve,
                        Rdifficulty = Difficulty.Easy,
                        RPrepTime = PrepTime.Medium,
                        RDiet = Diet.Vegeterian,
                        RBudget = Budget.Cheap,
                        RHoliday = Holiday.Hannuka,

                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient { Name = "Potato", Amount = 4, MeasurementUnit = MeasurementType.None },
                            new Ingredient { Name = "Onion", Amount = 1, MeasurementUnit = MeasurementType.None },
                            new Ingredient { Name = "Eggs", Amount = 2, MeasurementUnit = MeasurementType.None },
                            new Ingredient { Name = "Kosher Salt", Amount = 2, MeasurementUnit = MeasurementType.Grams },
                            new Ingredient { Name = "Black Pepper", Amount = 1, MeasurementUnit = MeasurementType.TableSpoon },
                            new Ingredient { Name = "Baking Powder", Amount = 1, MeasurementUnit = MeasurementType.TableSpoon },
                            new Ingredient { Name = "Flour", Amount = 3, MeasurementUnit = MeasurementType.Teaspoon },
                            new Ingredient { Name = "Oil", Amount = 3, MeasurementUnit = MeasurementType.TableSpoon }
                        },
                        Instructions = new()
                        {
                            new Instruction { Step = "in large bowl combine eggs salt, pepper, and baking powder. Set sside." },
                            new Instruction { Step = "In food precessor fitted with 'kugel' blade, add onion and potatoes, Process." },
                            new Instruction { Step = "with your hands scoop out potato mixture and over the sink or a bowl press your hands together to try and squeez out as much 'juice' as possible." },
                            new Instruction { Step = "Add 'dryer' potato mixture to the egg bowl." },
                            new Instruction { Step = "Repeat until all the potatoes and onion have been added. Mix until combined." },
                            new Instruction { Step = "Add flour and stir until fully incorporated." }
                        }

                    }
                    );
                context.Recipes.Include("Ingredients");
                context.SaveChanges();
            }
        }
    }
}
