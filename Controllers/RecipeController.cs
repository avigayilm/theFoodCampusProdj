﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using theFoodCampus.Data;
using theFoodCampus.Models;

namespace theFoodCampus.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public RecipeController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
    }
        public IActionResult Index()
        {
            List<Recipe> recipes;
            recipes = _context.Recipes.ToList();
            return View(recipes);
        }

        [HttpGet]// emtpy structure so that the user can fill in the details
        public IActionResult Create()
        {
            Recipe recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient() { IngredientId = 1 });
            recipe.Instructions.Add(new Instruction() { Id= 1, });
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
            Recipe recipe = _context.Recipes.Include(e => e.Ingredients)
                .Where(e => e.Id == Id).FirstOrDefault();
            return View(recipe);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            // get me the exprencies from the detail table together with header table.
            // thi is called eager loading
            // there is eager lazy and explicit loading.
            Recipe recipes = _context.Recipes.Include(e => e.Ingredients)
                .Where(e => e.Id == Id).FirstOrDefault();
            return View(recipes);
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
            Recipe recipe= _context.Recipes
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

            // not to save double ingredients when editing
            recipe.Ingredients.RemoveAll(n => n.Name == "");
            // in order not to delete all lines when delteing lower rows
            recipe.Ingredients.RemoveAll(n => n.IsDeleted == true);
            if (recipe.ProfilePhoto != null) // he updates the image so therefore we update it to the root
            {


                string uniqueFileName = GetUploadedFileName(recipe);

                recipe.PhotoUrl = uniqueFileName;
            }
            // we just removed all the ingredients and now we save it again
            // this in is inorder not to save twice.
            _context.Attach(recipe);
            _context.Entry(recipe).State = EntityState.Modified;
            _context.Ingredients.AddRange(recipe.Ingredients);
            _context.SaveChanges();
            return RedirectToAction("index");

        }

    }
}
