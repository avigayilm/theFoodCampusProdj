using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace theFoodCampus.Models
{
    public class RecipeImage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Recipe")]// specifies the parent model
        public int RecipeId { get; set; }
        public virtual Recipe Recipes { get; private set; } //very important
        [Required]
        public string image { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

    }
}