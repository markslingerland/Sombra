$(document).ready(function () {
    $('#top-charities').load('@Url.Action("GetTopCharities", "Search", new { Amount = 3})');
});
$(document).ready(function () {
    ;
    var str = '<%= ViewData["CharityImage"] %>';
    $("#svg-charity").attr("xlink:href", str);
});