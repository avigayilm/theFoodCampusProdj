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
                    
                    context.Recipes.AddRange(
                    new Recipe
                    {
                        Name = "Fluffy Pancakes",
                        Rating = 5,
                        RKashrut = Kashrut.Dairy,
                        Rdifficulty = Difficulty.Easy,
                        RPrepTime = PrepTime.Quick,
                        RType = Category.Desert,
                        RDiet = Diet.None,
                        RBudget = Budget.Cheap,
                        RHoliday = Holiday.Hannuka,
                        Tag = "pancakes",
                        Ingredients = new List<Ingredient>() { new Ingredient { Name = "Milk", Amount = 1, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Flour", Amount = 1, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Egg", Amount = 1, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Vanilla Sugar", Amount = 1, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Baking Powder", Amount = 3, MeasurementUnit = MeasurementType.Teaspoon } },
                        Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "Mix the flour and baking poder, then create a sort of hole in the middle" },
                            new Instruction { Step = "In the hole add everything else mix in , then mix all together." },
                            new Instruction { Step = "Fry in a little oil" }
                        }
                    },
                     new Recipe
                     {
                         Name = "Cabbage Salad",
                         Rating = 3,
                         RKashrut = Kashrut.Pareve,
                         Rdifficulty = Difficulty.Easy,
                         RPrepTime = PrepTime.Quick,
                         RType = Category.Salad,
                         RDiet = Diet.Healthy,
                         RHoliday = Holiday.Succot,
                         RBudget = Budget.Affordable,
                         Tag = "cabbage salad",
                         Ingredients = new List<Ingredient>() { new Ingredient { Name = "Purple Cabbage", Amount = 1, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Almonds Sliced", Amount = 100, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "Cranberries", Amount = 80, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "Purple Onion", Amount = 1, MeasurementUnit = MeasurementType.None }, },
                         Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "mix all ingredients" },
                            new Instruction { Step = "serve slightly chilled with soy sauce" }
                        }
                     },
                      new Recipe
                      {
                          Name = "Gefilte Fish",
                          Rating = 1,
                          RKashrut = Kashrut.Pareve,
                          Rdifficulty = Difficulty.Difficult,
                          RPrepTime = PrepTime.Long,
                          RType = Category.Fish,
                          RDiet = Diet.GlutenFree,
                          RBudget = Budget.Affordable,
                          Tag = "fish balls",
                          Ingredients = new List<Ingredient>() { new Ingredient { Name = "Musht", Amount = 500, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "salt", Amount = 1, MeasurementUnit = MeasurementType.Pinch }, new Ingredient { Name = "Oil", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Pepper", Amount = 1, MeasurementUnit = MeasurementType.Pinch }, new Ingredient { Name = "Carrot", Amount = 5, MeasurementUnit = MeasurementType.None } },
                          Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "grind fish real well allow to sit in oil and all spices" },
                            new Instruction { Step = "cool" },
                            new Instruction { Step = "roll up in baking paper, cut nicely in thick knife, add carrot on top- very important!!!!" }
                        }
                      },
                       new Recipe
                       {
                           Name = "Alphajores cookies",
                           Rating = 5,
                           RKashrut = Kashrut.Dairy,
                           Rdifficulty = Difficulty.Intermediate,
                           RPrepTime = PrepTime.Long,
                           RType = Category.CakeAndCookies,
                           RDiet = Diet.None,
                           RHoliday = Holiday.Hannuka,
                           RBudget = Budget.Affordable,
                           Tag = "alfajores cookies",
                           Ingredients = new List<Ingredient>() { new Ingredient { Name = "dulce de leche", Amount = 1, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Flour", Amount = 250, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "starch", Amount = 200, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "butter", Amount = 100, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "confectioners", Amount = 200, MeasurementUnit = MeasurementType.Grams } },
                           Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "in mixer mix sugar and butter for a few minutes" },
                            new Instruction { Step = "add everything else in." },
                            new Instruction { Step = "allow to cool for 2 hours" },
                            new Instruction { Step = "make small cookies" },
                            new Instruction { Step = "bake for 6 minutes" },
                            new Instruction { Step = "smear with dulce de leche, refrigerate." }
                        }
                       },
                        new Recipe
                        {
                            Name = "Granola Bars",
                            Rating = 5,
                            RKashrut = Kashrut.Pareve,
                            Rdifficulty = Difficulty.Easy,
                            RPrepTime = PrepTime.Quick,
                            RType = Category.CakeAndCookies,
                            RDiet = Diet.Healthy,
                            Tag = "Granola Bars",
                            RBudget = Budget.Affordable,
                            Ingredients = new List<Ingredient>() { new Ingredient { Name = "Oats", Amount = 200, MeasurementUnit = MeasurementType.Ounces }, new Ingredient { Name = "rice Crispies", Amount = 4, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "salt", Amount = 1, MeasurementUnit = MeasurementType.Pinch }, new Ingredient { Name = "walnuts", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Pecans", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "chocolate chips", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Honey", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Peanut Butter or Oil", Amount = 0.5, MeasurementUnit = MeasurementType.Cup } },
                            Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "mix everything reall well." },
                            new Instruction { Step = "bake for half an hour." },
                            new Instruction { Step = "slice while still hot. enjoy!" }
                        }
                        },
                         new Recipe
                         {
                             Name = "Shepherds Pie",
                             Rating = 5,
                             RKashrut = Kashrut.Meaty,
                             Rdifficulty = Difficulty.Easy,
                             RPrepTime = PrepTime.Medium,
                             RType = Category.Quiche,
                             RDiet = Diet.GlutenFree,
                             RBudget = Budget.Expensive,
                             Tag = "shepherds-pie",
                             Ingredients = new List<Ingredient>() { new Ingredient { Name = "Ground Beef", Amount = 500, MeasurementUnit = MeasurementType.Grams }, new Ingredient { Name = "bag Potatoes", Amount = 0.5, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Tomato sauce", Amount = 2, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Onio", Amount = 4, MeasurementUnit = MeasurementType.None } },
                             Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "Cook meat with everything but potatoes" },
                            new Instruction { Step = "boil potatoes till hard." },
                            new Instruction { Step = "mash potatoes , then spread on pan" },
                            new Instruction { Step = "spread meat on top" },
                            new Instruction { Step = "bake for 40 minutes till top crispy" }
                        }

                         }
                         , new Recipe
                         {
                             Name = "Sweet Potato Quiche",
                             Rating = 4,
                             RKashrut = Kashrut.Pareve,
                             Rdifficulty = Difficulty.Easy,
                             RPrepTime = PrepTime.Quick,
                             RType = Category.Quiche,
                             RDiet = Diet.Healthy,
                             RBudget = Budget.Cheap,
                             Tag = "sweet-potato quiche",
                             Ingredients = new List<Ingredient>() { new Ingredient { Name = "Sweet Potato", Amount = 7, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Egg", Amount = 2, MeasurementUnit = MeasurementType.None }, new Ingredient { Name = "Flour", Amount = 2, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Brown Sugar", Amount = 0.5, MeasurementUnit = MeasurementType.Cup }, new Ingredient { Name = "Oil", Amount = 0.5, MeasurementUnit = MeasurementType.Cup } },
                             Instructions = new List<Instruction>
                        {
                            new Instruction { Step = "Mix cup of flour with 1/4 cup oil and the sugar, spread on pan" },
                            new Instruction { Step = "Juju sweet potato , mix with everything, pour ontop of dough" },
                            new Instruction { Step = "bake for 45 on 200" }
                        }
                         }







                    );
                    context.Recipes.Include("Ingredients");
                    context.SaveChanges();
                    return;   //DB has been added to
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
