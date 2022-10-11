using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace theFoodCampus.Models
{
    public class Instruction
    {
        [Key]
        public int Id { get; set; }
        public string Step { get; set; }

        [ForeignKey("Recipe")]//very important
        public int ReceipeId { get; set; }
        public virtual Recipe Recipes { get; private set; } //very important 
        [NotMapped]
        public bool IsHidden { get; set; } = false;
    }
}
