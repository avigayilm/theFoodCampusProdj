using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using theFoodCampus.Models;
namespace theFoodCampus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<RecipeImage> Images { get; set; }

    }


}
