$(function () {
    $(".RemoveLink").click(function () {
        var id = $(this).attr("data-id");

        $.post("/Order/RemoveFromCart", { "id": id }, function (data) {
            $("#update-message").text(data.Message);
            $("#cart-total").text(data.CartTotal);
            $("#item-count-" + data.DeleteId).text(data.ItemCount);

            if (data.ItemCount < 1) {
                $("#item-status-" + data.DeleteId).text("Cancelled");
                $("#link-" + data.DeleteId).remove();
            }
        });

    })
});