$(document).ready(function() {
  var ingredientCounter = 1;

  $("#addIngredient").click(function () {
    ingredientCounter++;
    var newRow = $("<div class='ingredient-row'></div>");
    var hiddenInput = $("<input type='hidden' id='ingredient-id' name='ingredient-id' value='-1' />"); // Set the value to '0'
    newRow.append(hiddenInput);
    newRow.append("<input type='text' name='ingredient' class='ingredient-input' placeholder='Ingredient'>");
    newRow.append("<button type='button' class='remove-ingredient'>X</button>");
    $("#ingredientsContainer").append(newRow);
});


  $("#ingredientsContainer").on("click", ".remove-ingredient", function () {
    $(this).closest(".ingredient-row").remove()
    ingredientCounter--;
  })

  $("#recipeForm").submit(function () {
    console.log('Submitings')
    ingredients = [];
    // Collect ingredient values
    $("#ingredientsContainer .ingredient-row").each(function () {
      var obj = {
        IngredientId: '',
        Name: ''
      }
      console.log($(this).find("#ingredient-id").val());
      console.log($(this).find(".ingredient-input").val());
      obj.IngredientId = parseInt($(this).find("#ingredient-id").val());
      obj.Name = $(this).find(".ingredient-input").val();
      ingredients.push(obj);
    });
    console.log(ingredients);
    var ingredientsJson = JSON.stringify(ingredients);
    // Set the ingredients as a comma-separated string in the hidden input field
    $("input[name='ingredients']").val(ingredientsJson);

    return true; // Proceed with the form submission
  });
})