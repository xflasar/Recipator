$(".recipe-container").on("click", ".recipe-item", function () {
  var item = {
    Id: $(this).data('id'),
    Title: $(this).data('title'),
    Ingredients: $(this).data('ingredients'),
    Instructions: $(this).data('instructions'),
    Image: $(this).data('image'),
  }

  $.ajax({
      url: "/Recipe/RecipeRedirect",
      type: "POST",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(item),
      success: function (data) {
        window.location.replace("/Recipe/Index/" + item.Id)
      }
  });
});