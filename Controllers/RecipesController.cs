using Microsoft.AspNetCore.Mvc;
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
            _recipes = _context.Recipes.ToList();
            return View(_recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Recipe(Recipe recipe)
        {
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
