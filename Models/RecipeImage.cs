using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace theFoodCampus.Models
{
    /// <summary>
    /// this class is for a list of images that we gets from the user
    /// </summary>
    public class RecipeImage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Recipe")]// specifies the parent model
        public int RecipeId { get; set; }
        public virtual Recipe Recipes { get; private set; } //very important
        [Required]
        public string image { get; set; }
        //this field used for a flag. is the image is from URL or from browse
        public bool isUrl { get; set; }

    }
}