using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipator.Data;
using Recipator.Models;

namespace Recipator.Controllers
{
    public class RecipesController : Controller
    {
        private static List<Recipe> _recipes = new List<Recipe>();

        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;          
        }

        public IActionResult Index()
        {
            _recipes = _context.Recipes.Include(r => r.Ingredients).ToList();
            return View(_recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe, string ingredients)
        {            
            List<string> ingredientsList = ingredients.Split(",").ToList(); 
            ingredientsList.ForEach(ingredient => {
                Ingredient ingredientObj = new Ingredient();
                ingredientObj.Name = ingredient;
                ingredientObj.RecipeId = recipe.Id;
                recipe.Ingredients.Add(ingredientObj);
            });

            if (ModelState.IsValid)
            {
                _context.Recipes.Add(recipe);
                _context.SaveChanges();
                _recipes.Add(recipe);

                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Delete_Recipe(int id)
        {
            _context.Remove(_context.Recipes.Find(id));
            _context.SaveChanges();
            _recipes.RemoveAt(_recipes.FindIndex(m => m.Id == id));

            return Json(new { success = true , redirectUrl = "/Recipes"});
        }

        [HttpPost]
        public IActionResult Update_Data([FromBody] Recipe recipe)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(recipe));
            var allIngredients = _context.Ingredients.Where(i => i.RecipeId == recipe.Id).ToList();
            Console.WriteLine(allIngredients.Count());
            /* foreach (var ingredient in recipe.Ingredients)
            {
                var ingredientObj = _context.Ingredients.Find(ingredient.IngredientId);
                ingredientObj.Name = ingredient.Name;
            } */
            return null;
            var result = _context.Recipes.Find(recipe.Id);
            if (result == null)
            {
                Console.WriteLine($"Recipe {recipe.Id} not found");
                return NotFound();
            }
            else
            {
                result = recipe;
                _context.SaveChanges();
                _recipes[_recipes.FindIndex(m => m.Id == recipe.Id)] = recipe;

                return Ok();
            }
        }
    }
}
