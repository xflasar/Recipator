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

})