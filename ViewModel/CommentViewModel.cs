using theFoodCampus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theFoodCampus.ViewModel
{
    public class CommentViewModel
    {

        public string Title { get; set; }
        public List<Comment> ListOfCommments { get; set; }
        public string Comments { get; set; }
        public int RecipeId { get; set; }
        public int Rating { get; set; }

    }
}
