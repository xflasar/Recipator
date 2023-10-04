$(".delete-button").click(function () {
  var itemId = $(this).data("id");
  console.log(itemId)

  $.ajax({
      url: "/Recipes/Delete_Recipe/" + itemId,
      type: "POST",
      dataType: "json",
      success: function (data) {
          if (data.success) {
              window.location = data.redirectUrl
          } else {
              alert("Delete operation failed: " + data.message);
          }
      }
  });
});