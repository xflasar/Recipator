using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipator.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Image {get; set;}

        public Recipe()
        {

        }

        public Recipe(int id, string Title, List<Ingredient> Ingredients, string Instructions, string Image)
        {
            this.Id = id;
            this.Title = Title;
            this.Ingredients = Ingredients;
            this.Instructions = Instructions;
            this.Image = Image;
        }
    }
}
