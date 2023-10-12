$(document).ready(function() {
  var ingredientCounter = 1;

  $("#addIngredient").click(function () {
    ingredientCounter++;
    var newRow = $("<div class='ingredient-row'></div>")
    newRow.append("<input type='text' name='ingredient' class='ingredient-input' placeholder='Ingredient'>")
    newRow.append("<button type='button' class='remove-ingredient'>X</button>")
    $("#ingredientsContainer").append(newRow)
  })

  $("#ingredientsContainer").on("click", ".remove-ingredient", function () {
    $(this).closest(".ingredient-row").remove()
    ingredientCounter--;
  })

  $("#recipeForm").submit(function () {
    console.log('Submitings')
    ingredients = [];
    // Collect ingredient values
    $("#ingredientsContainer .ingredient-input").each(function () {
      console.log($(this).val());
        ingredients.push($(this).val());
    });

    // Set the ingredients as a comma-separated string in the hidden input field
    $("input[name='ingredients']").val(ingredients.join(','));

    return true; // Proceed with the form submission
  });
})