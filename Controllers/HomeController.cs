using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Xml.Linq;
using theFoodCampus.Data;
using theFoodCampus.Models;
using theFoodCampus.Models.Adapter;
using theFoodCampus.ViewModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using static theFoodCampus.Models.Nutrient;

namespace theFoodCampus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        //private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            HebCalData.Root HebCal=ShowHoliday();
            WeatherData.Root Weather=ShowWeather();
            List<Recipe> recipes;
            List<Recipe> WeatherRecipes = _context.Recipes
    .Include(e => e.Ingredients)
    .Include(e => e.Instructions)
    .Include(e => e.Comments)
    .Where(e => e.RWeather == Weather.WeatherFeel).ToList();
            List<Recipe> HolidayRecipes;
            List<Recipe> HeaderRecipes;
            if (HebCal.Holiday!=Holiday.None)
            {
               HolidayRecipes= _context.Recipes
               .Include(e => e.Ingredients)
               .Include(e => e.Instructions)
               .Include(e => e.Comments)
               .Where(e => e.RHoliday == HebCal.Holiday).ToList();
                HeaderRecipes = HolidayRecipes;
            }
            else
            {
                HeaderRecipes = WeatherRecipes;
            }
            recipes = _context.Recipes.ToList();
            ViewBag.Hebdate=HebCal.HebrewDate;
            ViewBag.HeaderList=HeaderRecipes;
            return View(recipes);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            //Note : you can bind same list from database  

            //Searching records from list using LINQ query  
            var Name = (from N in _context.Recipes
                        where N.Name.Contains(Prefix)
                        select new
                        {
                            label = N.Name,
                            //val = N.FirstName
                        }).ToList();
            return Json(Name);
        }

        [HttpPost]
        public ActionResult Index(string personName)
        {
            Recipe recipe = _context.Recipes
    .Include(e => e.Ingredients)
    .Include(e => e.Instructions)
    .Include(e => e.Comments)
    .Where(e => e.Name == personName).FirstOrDefault();
            return RedirectToAction("RecipePost", "Home", new { id = recipe.Id });
            //ViewBag.Message = "Selected Person Name: " + personName;
            //return View();
        }
        public HebCalData.Root ShowHoliday()
        {
            var HebCalModel = new HebCalAdapter();// this gets the string from the gateway.
            var holiday = HebCalModel.Check();// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            var result = JsonConvert.DeserializeObject<HebCalData.Root>(holiday);
            List<Recipe> HeaderRecipes;
            ViewBag.holiday = result;
            return result;
        }

        public WeatherData.Root ShowWeather()
        {
            var WeatherModel = new WeatherAdapter();
            var weather = WeatherModel.Check();// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            var result = JsonConvert.DeserializeObject<WeatherData.Root>(weather);
            ViewBag.weather = result;
            return result;
        }


        public IActionResult Blogpost()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult thankyou()
        {
            return View();
        }

        public IActionResult Elements()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Recipepost(int Id)
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe recipe = _context.Recipes
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Include(e => e.Comments)
                .Where(e => e.Id == Id).FirstOrDefault();

            var UsdaModel = new UsdaAdapter();
            var myJsonNutrients = UsdaModel.Check(recipe.Tag);// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            var list = JsonConvert.DeserializeObject<List<Nutrient.Root>>(myJsonNutrients);
            ViewBag.Nutrients = list;

            var comments = recipe.Comments;

            if (comments.Count() > 0)
            {
                var sum= comments.Sum(d => d.Rating);
                var count= comments.Count();
                ViewBag.RatingSum = sum;
                ViewBag.RatingCount = count; ;
                ViewBag.RatingAverage = recipe.Rating;

            }

            else
            {
                recipe.RatingCount = 0;
                recipe.RatingSum = 0;
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }
            return View(recipe);
            // return View("RecipePost",recipe);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Recipe recipe)
        {
            //var comment = vm.Comments;
            //var articleId = vm.ArticleId;
            //var rating = vm.Rating;

            Comment artComment = new Comment()
            {
                RecipeId = recipe.Id,
                CommentDescription = recipe.CommentText,
                Rating = recipe.Rating,
                CommentedOn = DateTime.Now
            };

            //int totalrating;
            // maybe better for runtime to keep extra field with amount of comments
            recipe = _context.Recipes
               .Include(e => e.Ingredients)
               .Include(e => e.Instructions)
               .Include(e => e.Comments)
               .Where(e => e.Id == recipe.Id).FirstOrDefault();

            // should actuallu mae field so we don't have to do this again.
            var sum = recipe.Comments.Sum(d => d.Rating);
            var count = recipe.Comments.Count();
            sum = sum + recipe.Rating;
            count++;
            recipe.Rating = sum / count;
                _context.Attach(recipe);
                _context.Entry(recipe).State = EntityState.Modified;

            _context.Comments.Add(artComment);
            _context.SaveChanges();
            return RedirectToAction("RecipePost", "Home", new { id = recipe.Id });
        }


        [HttpPost]
        public IActionResult RecipePost(Recipe recipe)
        {



            //This is for editing

            //List<Ingredient> expDetials = _context.Ingredients.Where(d => d.RecipeId == recipe.Id).ToList();
            //_context.Ingredients.RemoveRange(expDetials);
            //_context.SaveChanges();

            //// not to save double ingredients when editing
            //recipe.Ingredients.RemoveAll(n => n.Name == "");
            //// in order not to delete all lines when delteing lower rows
            //recipe.Ingredients.RemoveAll(n => n.IsDeleted == true);
            //if (recipe.ProfilePhoto != null) // he updates the image so therefore we update it to the root
            //{


            //    string uniqueFileName = GetUploadedFileName(recipe);

            //    recipe.PhotoUrl = uniqueFileName;
            //}
            //// we just removed all the ingredients and now we save it again
            //// this in is inorder not to save twice.
            //_context.Attach(recipe);
            //_context.Entry(recipe).State = EntityState.Modified;
            //_context.Ingredients.AddRange(recipe.Ingredients);
            //_context.SaveChanges();
            return RedirectToAction("index");
        }

        // to upload the image to the wwwroot
        private string GetUploadedFileName(Recipe recipe)
        {
            string uniqueFileName = null;

            if (recipe.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + recipe.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    recipe.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]// emtpy structure so that the user can fill in the details
        public IActionResult Create()
        {
            Recipe recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient() { IngredientId = 1 });
            recipe.Instructions.Add(new Instruction() { Id = 1, });
            return View(recipe);
        }


        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            recipe.Ingredients.RemoveAll(n => n.Name == "");
            recipe.Ingredients.RemoveAll(n => n.IsDeleted == true);

            string uniqueFileName = GetUploadedFileName(recipe);
            recipe.PhotoUrl = uniqueFileName;

            _context.Add(recipe);
            _context.SaveChanges();
            return RedirectToAction("index");

        }

      


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}