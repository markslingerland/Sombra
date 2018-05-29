$(document).ready(function () {
    $('#top-charities').load('@Url.Action("GetTopCharities", "Search", new { Amount = 3})');
});