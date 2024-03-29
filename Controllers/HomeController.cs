﻿using Microsoft.AspNetCore.Mvc;
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
using System.Text.RegularExpressions;
using RestSharp;
using FireSharp.Extensions;
using System.Runtime.InteropServices;

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
            HebCalData.Root HebCal = ShowHoliday(); // calls the function to show hebrew date on home page
            WeatherData.Root Weather = ShowWeather(); // will show the weather on the home page
            List<Recipe> recipes = new();
            List<Recipe> recipesToShow = new();
            List<Recipe>? WeatherRecipes = null;
            if (Weather != null) 
            {
                WeatherRecipes = _context.Recipes // creates a list of some of the recipes for current weather
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Include(e => e.Comments)
                .Where(e => e.RWeather == Weather.WeatherFeel).Take(3).ToList();
            }
            List<Recipe> HolidayRecipes = new();
            List<Recipe> EventRecipes = new();
            List<Recipe> HeaderRecipes = null;
            if (HebCal != null)
            {
                ViewBag.Hebdate = HebCal.HebrewDate; // display hebrew date on home page
                if (HebCal.Holiday != Holiday.None) // if there is a holiday coming up soon
                {
                    HolidayRecipes = _context.Recipes
                    .Include(e => e.Ingredients)
                    .Include(e => e.Instructions)
                    .Include(e => e.Comments)
                    .Where(e => e.RHoliday == HebCal.Holiday).Take(3).ToList(); // create a list of recipes for coming holiday
                    HeaderRecipes = HolidayRecipes;
                    recipesToShow = HolidayRecipes.Concat(WeatherRecipes).ToList(); // these are the recipes to be displayed on home page
                }
                else // there is no upcoming holiday
                {
                    HeaderRecipes = WeatherRecipes; 
                    recipesToShow = _context.Recipes
                    .Include(e => e.Ingredients)
                    .Include(e => e.Instructions)
                    .Include(e => e.Comments)
                    .Where(e => e.RWeather == Weather.WeatherFeel).Take(6).ToList(); // take recipes only according to the weather
                    var studentEve = calculateEvent();
                    ViewBag.studentEvent = studentEve; // display on home page the students event
                }

            }
            recipes = _context.Recipes
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Include(e => e.Comments).ToList();
            ViewBag.list = recipesToShow;
            //_context.Recipes.Select(x => x.Name).ToArray();
            ViewBag.HeaderList = HeaderRecipes;
            return View(recipes);
        }


        [HttpPost]
        public ActionResult Index(string personName, string unusedValue = "")
        {
            Recipe? recipe = _context.Recipes
            .Include(e => e.Ingredients)
            .Include(e => e.Instructions)
            .Include(e => e.Comments)
            .Where(e => e.Name == personName).FirstOrDefault();
            if (recipe == null)
                throw new Exception();
            return RedirectToAction("RecipePost", "Home", new { id = recipe.Id });
            //ViewBag.Message = "Selected Person Name: " + personName;
            //return View();
        }
        /// <summary>
        /// shows the hebrew date and holdiay if there is one
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public HebCalData.Root ShowHoliday()
        {
            var HebCalModel = new HebCalAdapter();// this gets the string from the gateway.
            var holiday = HebCalModel.Check();// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            HebCalData.Root? result = null;
            if (holiday != null)
                result = JsonConvert.DeserializeObject<HebCalData.Root>(holiday);
            ViewBag.holiday = result;
            if (result == null)
                throw new Exception();
            if (result.Holiday == Holiday.None) // then go according to students schedule
            {
                var studentEve = calculateEvent();
                ViewBag.studentEvent = studentEve;
            }
            ViewBag.holiday = result;
            return result;
        }
       
            
        
        /// <summary>
        /// in case there is no holiday- goes by student event
        /// </summary>
        /// <returns></returns>
        private string calculateEvent()
        {
            var dateTime = DateTime.Now;
            switch (dateTime.Month)
            {
                case 8 or 9: // its vacation
                    return "Its vacation!!!!\n" +
                        "Vacation is having nothing to do and all day to do it:)";// call fuction for rosh hashana receipes

                case 5 or 12: // projects usually
                    {
                        return "Get going now its project time!\n" +
                            "and remember- Done is better then perfect:)";
                       
                    }

                case 1 or 6: // end of semester
                    return "find some cool and easy recipes for the end of semester\n" +
                        "almoste there:)" ;

                case 2 or 7: // exams time
                    return "check out some quick and nutritious recipes for exam time\n" +
                        "Best of luck on exams:)";


                case 3 or 4 or 10 or 11: // beginning of semester
                    return "Recipes to get you started this semester"  ;
                default:
                    return "Check out our student oriented recipes";

            }
        }
        /// <summary>
        /// displays weather currently feels like
        /// </summary>
        /// <returns></returns>
        public WeatherData.Root ShowWeather()
        {
            var WeatherModel = new WeatherAdapter();
            var weather = WeatherModel.Check();// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            WeatherData.Root? result = null;
            if (weather != null)
                result = JsonConvert.DeserializeObject<WeatherData.Root>(weather);
            ViewBag.weather = result;
            return result;
        }


        public IActionResult Blogpost()
        {
            return View();
        }

        /// <summary>
        /// contact function for users
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            List<Recipe> recipes = _context.Recipes.ToList();
            ViewBag.allRecipes = recipes.Count();
            var countDesert = recipes.FindAll(e => e.RType == Category.Desert || e.RType == Category.CakeAndCookies).Count();
            ViewBag.allDesert = countDesert;
            var countMeat = recipes.FindAll(e => e.RType == Category.Meat).Count();
            ViewBag.allMeat = countMeat;
            var countDif = recipes.FindAll(e => e.RType == Category.CakeAndCookies).Count();
            ViewBag.allDif = countDif;
            return View(recipes);
        }

        /// <summary>
        /// this view is called after sending an email succesfully
        /// </summary>
        /// <returns></returns>
        public IActionResult thankyou()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Recipes(string? category)
        {
            ViewBag.Holiday =(Holiday[])Enum.GetValues(typeof(Holiday));
            ViewBag.Difficulty= (Difficulty[])Enum.GetValues(typeof(Difficulty));
            ViewBag.Category_List= (Category[])Enum.GetValues(typeof(Category));
            ViewBag.Weather= (Weather[])Enum.GetValues(typeof(Weather));
            ViewBag.Category = null;
            List<Recipe> recipes = null;
            if (category!=null && category!="None")
            {
                //string newCategory = category.Remove(category.Length - 1, 1); // for some reason it saves the string with ; at the end
                
                if (Enum.IsDefined(typeof(Holiday), category))
                {
                    Holiday categoryEnum = Enum.Parse<Holiday>(category);
                    ViewBag.Category = categoryEnum;
                    recipes = _context.Recipes
                      .Where(x => x.RHoliday == categoryEnum)
                      .Include(e => e.Comments).ToList();
                }

                if (Enum.IsDefined(typeof(Weather), category))
                {
                    Weather categoryEnum = Enum.Parse<Weather>(category);
                    ViewBag.Category = categoryEnum;
                    recipes = _context.Recipes
                       .Where(x => x.RWeather == categoryEnum)
                       .Include(e => e.Comments).ToList();
                }
            }
            else
            {
                recipes = _context.Recipes
                .Include(e => e.Comments).OrderBy(x => x.Rdifficulty).ToList();
            }
            //here I want to call the show weather and holiday funcitons so that I get a list from those functions.
            return View(recipes);
        }

        [HttpPost]
        public ActionResult Recipes(string personName, string unusedValue = "")
        {
            Recipe? recipe = _context.Recipes
    .Include(e => e.Ingredients)
    .Include(e => e.Instructions)
    .Include(e => e.Comments)
    .Where(e => e.Name == personName).FirstOrDefault();
            return RedirectToAction("RecipePost", "Home", new { recipe.Id });
            //ViewBag.Message = "Selected Person Name: " + personName;
            //return View();
        }

        /// <summary>
        /// if a single recipe is clicked, will go to recipes page
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IActionResult Recipepost(int Id, string alert = "false")
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe? recipe = _context.Recipes
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Include(e => e.Comments)
                .Include(e => e.Images)
                .Where(e => e.Id == Id).FirstOrDefault();

            var UsdaModel = new UsdaAdapter();
            if (recipe == null) throw new Exception();
            var myJsonNutrients = UsdaModel.Check(recipe.Tag);// if you have parameters  you put the parameters in check, best ot have it in models as an object the parameters you need
            List<Nutrient.Root>? list = null;
            if (myJsonNutrients != null)
                list = JsonConvert.DeserializeObject<List<Nutrient.Root>>(myJsonNutrients);
            ViewBag.Nutrients = list;

            var bigMLModel = new BigMLAdapter();
            BigMLData data = new BigMLData { LastCategory = recipe.RType.ToString(), Holiday = recipe.RHoliday.ToString(), Weather = recipe.RType.ToString() };
            var nextCategory = Regex.Replace(bigMLModel.Check(data), @"[^0-9a-zA-Z\._]", string.Empty); // predict next category accordig to ML prediction
            Category type = Enum.Parse<Category>(nextCategory);

            List<Recipe> nextRecipes = _context.Recipes // retrieve only those recipes that are of wanted category
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Include(e => e.Comments)
                .Where(e => e.RType == type).ToList();
            ViewBag.nextRecipes = nextRecipes;

            var comments = recipe.Comments;

            if (comments.Count() > 0)
            {
                var sum = comments.Sum(d => d.Rating);
                var count = comments.Count();
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
            ViewBag.alert = alert;
            return View(recipe);
        }
        /// <summary>
        /// for Imagga api conection
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Imagga([Bind("Id,Name,Tag,ImageForm,ImageString")] Recipe recipe)
        {
            ImaggaData data;
            if (recipe.ImageForm != null || recipe.ImageString != null)
            {
                if (recipe.ImageForm != null)//upload from browse
                {
                    string pathToImagga = @"wwwroot/images/";
                    string nameImage = uploadImage(recipe.ImageForm);
                    recipe.ImageString = nameImage;
                    string imageId = uploadImagga(pathToImagga + nameImage);
                    data = new ImaggaData() { ImageUrl = imageId, Title = recipe.Tag };

                }
                else//upload a url
                {
                    data = new ImaggaData() { ImageUrl = recipe.ImageString, Title = recipe.Tag };
                }
                var currentModel = new ImaggaAdapter();
                string ImaggaResult = currentModel.Check(data);
                if (ImaggaResult == "true")
                    return RedirectToAction("AddImage", "Home", recipe);
                else
                {
                    //alert = true mininigs we want to alertt the user that the image wasnt good enough
                    return RedirectToAction("RecipePost", "Home", new { recipe.Id, alert = "true" });
                }
            }
            //alert = true mininigs we want to alertt the user that the image wasnt good enough
            return RedirectToAction("RecipePost", "Home", new { recipe.Id, alert = "true" });
        }
        /// <summary>
        /// This function is used to upload an image to the Imagga image cloud. 
        /// Imagga allows you to upload an image directly from browse, 
        /// but in order to have access to the image, it is not possible to save the image on a personal computer (security issues),
        /// so we will upload the image to the Imagga database and perform the necessary tests from there. 
        /// </summary>
        /// <param name="path">path of image</param>
        /// <returns>the id of the image (for recognition in the Imagga cloud)</returns>
        public string uploadImagga(string path)
        {
            string apiKey = "acc_4858251230dcb88";
            string apiSecret = "c2f19534f3e9c73697368ef9cfe0cae1";

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/uploads");

            var request = new RestRequest(new Uri("https://api.imagga.com/v2/uploads"), Method.Post);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            request.AddFile("image", path);

            RestResponse response = client.Execute(request);
            String[] spearator = { "{", "}", ",", ":{", "},", "}}", ":", @"\", @"'\", "\"" };
            var json = response.Content.ToJson();
            var result = json.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            return result[2];
        }

        /// <summary>
        /// add an image to the list of images that we get from the user
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public ActionResult AddImage(Recipe recipe)
        {
            RecipeImage image = new()
            {
                RecipeId = recipe.Id,
                image = recipe.ImageString
            };
            image.isUrl = recipe.ImageString.StartsWith("http");
            _context.Images.Add(image);
            _context.SaveChanges();
            return RedirectToAction("RecipePost", "Home", new { id = recipe.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Recipe? recipe)
        {
            //var comment = vm.Comments;
            //var articleId = vm.ArticleId;
            //var rating = vm.Rating;
            if (recipe == null) throw new Exception();
            Comment artComment = new Comment()
            {
                RecipeId = recipe.Id,
                CommentDescription = recipe.CommentText,
                Rating = recipe.Rating,
                CommentedOn = DateTime.Now
            };

            //int totalrating;
            
            recipe = _context.Recipes
               .Include(e => e.Ingredients)
               .Include(e => e.Instructions)
               .Include(e => e.Comments)
               .Where(e => e.Id == recipe.Id).FirstOrDefault();
            if (recipe == null) throw new Exception();
            // should actuallu mae field so we don't have to do this again.
            var sum = recipe.Comments.Sum(d => d.Rating);
            var count = recipe.Comments.Count();
            sum = sum + artComment.Rating;
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
            return RedirectToAction("index");
        }

        /// <summary>
        /// to add ProfilePhoto
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns>the name of the image after uploaded to wwwroot</returns>
        private string GetUploadedFileName(Recipe recipe)
        {
            string? uniqueFileName = null;

            if (recipe.ProfilePhoto != null)
            {
                uniqueFileName = uploadImage(recipe.ProfilePhoto);
            }
            return uniqueFileName;
        }

        /// <summary>
        /// This function is used to upload an image to wwwroot
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>the name of the image</returns>
        public string uploadImage(IFormFile image)
        {
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        [HttpGet]// emtpy structure so that the user can fill in the details
        public IActionResult Create()
        {
            Recipe recipe = new();
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

        /// <summary>
        /// this function is for an autocomplete search
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
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

        //**
        public IActionResult Elements()
        {
            return View();
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