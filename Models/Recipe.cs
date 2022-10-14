using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace theFoodCampus.Models
{
    public class Recipe
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int Rating { get; set; } = 0;

        //tags
        public Kashrut RKashrut { get; set; }
        public Difficulty Rdifficulty { get; set; }
        public PrepTime RPrepTime { get; set; }
        public Holiday RHoliday { get; set; }
        public Event REvent { get; set; }
        public Weather RWeather { get; set; }
        public Category RType { get; set; }
        public Diet RDiet { get; set; }
        public Budget RBudget { get; set; }

        public string Tag { get; set; }

        //list of ingredients
        public virtual List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public virtual List<Instruction> Instructions { get; set; } = new List<Instruction>();
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        public string? PhotoUrl { get; set; }
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

        // used to save the text just for the page
        [NotMapped]
        public string CommentText { get; set; }

        [NotMapped]
        public int RatingCount { get; set; }
        [NotMapped]
        public int RatingSum { get; set; }
        [NotMapped]
        public int TempRating { get; set; }

    }
}
