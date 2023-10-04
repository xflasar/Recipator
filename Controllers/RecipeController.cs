using Microsoft.AspNetCore.Mvc;
using Recipator.Models;
using System.Net.Http.Headers;
using System.Text;

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

        [HttpPost]
        public IActionResult RecipeRedirect([FromBody] Recipe recipe)
        {
            TempData["recipe"] = System.Text.Json.JsonSerializer.Serialize(recipe);

            return RedirectToAction("Index", "Recipe");
        }

        public IActionResult Edit()
        {
            Recipe recipe = System.Text.Json.JsonSerializer.Deserialize<Recipe>(
                TempData["recipe"].ToString()
            );

            return View(recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
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
