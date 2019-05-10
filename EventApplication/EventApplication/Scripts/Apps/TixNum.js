$(function () {
    $(".submitNum").click(function () {
        console.log("Called");
        var count = document.getElementById("mySelect").value;
        console.log(count);
        var Url = "/Order/AddToCart/";
        var id = $(this).attr("data-id");
        var newUrl = Url + id + "?count=" + count;
        console.log(newUrl);
        window.location.href = newUrl;
    });

});