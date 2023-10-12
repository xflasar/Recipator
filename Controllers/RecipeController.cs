using Microsoft.AspNetCore.Mvc;
using Recipator.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Recipator.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["recipe"] != null)
            {
                
                Recipe recipe = System.Text.Json.JsonSerializer.Deserialize<Recipe>(
                    TempData["recipe"].ToString()
                );
                TempData.Keep();

                return View(recipe);
            }
            else
            {
                return View();
            }
        }
        public class RecipeFormData {
            public int Id { get; set; }
            public string Title { get; set; }
            public List<Ingredient> Ingredients { get; set; }
            public string Instructions { get; set; }
            public string Image { get; set; }
        }

        [HttpPost]
        public IActionResult RecipeRedirect([FromBody] RecipeFormData recipe)
        {
            Recipe recipeNew = new Recipe
            (
                int.Parse(recipe.Id.ToString()),
                recipe.Title,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.Image
            );
            
            TempData["recipe"] = System.Text.Json.JsonSerializer.Serialize(recipeNew);

            return RedirectToAction("Index", "Recipe");
        }

        public IActionResult Edit()
        {
            Recipe recipe = System.Text.Json.JsonSerializer.Deserialize<Recipe>(
                TempData["recipe"].ToString()
            );

            return View(recipe);
        }

        public class serializedIngredient {}

        [HttpPost]
        public IActionResult Edit(Recipe recipe, string ingredients)
        {
            Console.WriteLine(ingredients);
            recipe.Ingredients = System.Text.Json.JsonSerializer.Deserialize<List<Ingredient>>(ingredients);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(recipe));
            //return null;
            if (ModelState.IsValid)
            {
                TempData["recipe"] = System.Text.Json.JsonSerializer.Serialize(recipe);
                var client = new HttpClient();
                var content = new StringContent(
                    TempData["recipe"].ToString(),
                    Encoding.UTF8,
                    new MediaTypeHeaderValue("application/json").ToString()
                );
                var response = client
                    .PostAsync("http://localhost:5003/Recipes/Update_Data", content)
                    .Result;

                return RedirectToAction("Index", new { id = recipe.Id });
            }
            return RedirectToAction("Index", "Recipes");
        }
    }
}
