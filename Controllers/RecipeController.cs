using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using theFoodCampus.Data;
using theFoodCampus.Models;
using theFoodCampus.Models.Adapter;

namespace theFoodCampus.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public HomeController _HM;
        public RecipeController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
            _HM = new HomeController(_context, _webHost);
        }
        public IActionResult Index()
        {
            List<Recipe> recipes;
            recipes = _context.Recipes.ToList();
            return View(recipes);
        }

        [HttpGet]// emtpy structure so that the user can fill in the details
        public IActionResult Create(string alert = "false")
        {
            ViewBag.alert = alert;
            Recipe recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient() { IngredientId = 1 });
            recipe.Instructions.Add(new Instruction() { Id = 1, });
            return View(recipe);
        }


        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            recipe.Ingredients.RemoveAll(n => n.Name == "");
            recipe.Instructions.RemoveAll(n => n.Step == "");
            recipe.Ingredients.RemoveAll(n => n.IsDeleted == true);
            ImaggaData data;
            string nameImage;
            if (recipe.ProfilePhoto != null)
            {

                string pathToImagga = @"wwwroot/images/";
                nameImage = _HM.uploadImage(recipe.ProfilePhoto);
                string imageId = _HM.uploadImagga(pathToImagga + nameImage);
                data = new ImaggaData() { ImageUrl = imageId, Title = recipe.Tag };
            }
            else if(recipe.ProfileUrl != null)//upload a url
            {
                nameImage = recipe.ProfileUrl;
                data = new ImaggaData() { ImageUrl = nameImage, Title = recipe.Tag };
            }
            else
            {
                return RedirectToAction("Create", "Recipe", new { alert = "true" });
            }
            var currentModel = new ImaggaAdapter();
            string ImaggaResult = currentModel.Check(data);
            if (ImaggaResult == "true")
            {
                recipe.PhotoUrl = nameImage;

                _context.Add(recipe);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("Create", "Recipe", new { alert = "true" });
            }
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

        public IActionResult Details(int Id)
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe recipe = _context.Recipes
               .Include(e => e.Ingredients)
               .Include(e => e.Instructions)
               .Where(e => e.Id == Id).FirstOrDefault();
            return View(recipe);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe recipe = _context.Recipes
               .Include(e => e.Ingredients)
               .Include(e => e.Instructions)
               .Where(e => e.Id == Id).FirstOrDefault();
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Delete(Recipe recipe)
        {
            _context.AttachRange(recipe);
            _context.Entry(recipe).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe recipe = _context.Recipes
                .Include(e => e.Ingredients)
                .Include(e => e.Instructions)
                .Where(e => e.Id == Id).FirstOrDefault();
            return View(recipe);
        }


        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {

            List<Ingredient> expDetials = _context.Ingredients.Where(d => d.RecipeId == recipe.Id).ToList();
            _context.Ingredients.RemoveRange(expDetials);
            _context.SaveChanges();


            List<Instruction> instDetails = _context.Instructions.Where(d => d.ReceipeId == recipe.Id).ToList();
            _context.Instructions.RemoveRange(instDetails); // we delete it from the  list
            _context.SaveChanges();//update to database.

            // not to save double ingredients when editing
            recipe.Ingredients.RemoveAll(n => n.Name == "");
            // in order not to delete all lines when delteing lower rows
            recipe.Ingredients.RemoveAll(n => n.IsDeleted == true);
            recipe.Instructions.RemoveAll(n => n.IsHidden == true);

            if (recipe.ProfilePhoto != null) // he updates the image so therefore we update it to the root
            {
                string uniqueFileName = GetUploadedFileName(recipe);
                recipe.PhotoUrl = uniqueFileName;
            }
            else if(recipe.ProfileUrl != null)
            {
                recipe.PhotoUrl = recipe.ProfileUrl;
            }
            // we just removed all the ingredients and now we save it again
            // this in is inorder not to save twice.
            _context.Attach(recipe);
            _context.Entry(recipe).State = EntityState.Modified;

            _context.Ingredients.AddRange(recipe.Ingredients);
            _context.Instructions.AddRange(recipe.Instructions);
            _context.SaveChanges();
            return RedirectToAction("index");

        }

    }
}
