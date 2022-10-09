using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace theFoodCampus.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [ForeignKey("Recipe")]// specifies the parent model
        public int RecipeId { get; set; }
        public virtual Recipe Recipes { get; private set; } //very important 
        public string CommentDescription { get; set; }
        public int Rating { get; set; }
        public DateTime CommentedOn { get; set; }
    }
}
