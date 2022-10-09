using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace theFoodCampus.Models
{
    public class Ingredient
    {

        public Ingredient()
        {
        }

        [Key]
        public int IngredientId { get; set; }

        [ForeignKey("Recipe")]//very important
        public int RecipeId { get; set; }
        public virtual Recipe Recipes { get; private set; } //very important 

        [Required]
        public string Name { get; set; }
        public double Amount { get; set; }

        public MeasurementType MeasurementUnit { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; } = false; // this is for when a line is deleted that not all is lost


    }

}
