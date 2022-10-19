using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using theFoodCampus.Data;
using theFoodCampus.Models;
using theFoodCampus.ViewModel;

namespace theFoodCampus.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: CommentController
        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Person> ObjList = new List<Person>()
            {

                new Person {FirstName="hi", LastName="You" },
                new Person {FirstName="avigayil", LastName="mandel" },
                new Person {FirstName="shira", LastName="segal" },
                new Person {FirstName="yehudit", LastName="flax" },


        };
            //Searching records from list using LINQ query  
            var Name = (from N in ObjList
                        where N.FirstName.StartsWith(Prefix)
                        select new
                        {
                            label = N.FirstName,
                            val = N.FirstName
                        });
            return Json(Name);
        }

        [HttpPost]
        public ActionResult Index(string personName)
        {
            ViewBag.Message = "Selected Person Name: " + personName;
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentViewModel recipe)
        {
            //var comment = vm.Comments;
            //var articleId = vm.ArticleId;
            //var rating = vm.Rating;

            //Comment artComment = new Comment()
            //{
            //    RecipeId = recipe.Id,
            //    CommentDescription = recipe.CommentText,
            //    Rating = recipe.Rating,
            //    CommentedOn = DateTime.Now
            //};


            //int totalrating;
            // maybe better for runtime to keep extra field with amount of comments
            //recipe.RatingSum = recipe.RatingSum + recipe.Rating;
            //recipe.RatingCount++;


       //     _context.Comments.Add(artComment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Article", new { id = recipe.RecipeId });
        }


        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
