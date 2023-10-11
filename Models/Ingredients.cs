using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipator.Models
{
  public class Ingredient
  {
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public int RecipeId { get; set; } // Foreign key to link to the Recipe
    public Recipe Recipe { get; set; } // Navigation property
  }
}
