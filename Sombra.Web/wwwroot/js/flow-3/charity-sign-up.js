
// SECTION 1
$('#control_01').click(function () {
    if ($('#control_01').is(':checked')) {
        $(".choose-your-payment ").show();
        $(".select-charity").show();
        $(".select-action").hide();
        $(".charity-information").show();
    }
});

$('#control_02').click(function () {
    if ($('#control_02').is(':checked')) {
        $(".select-action").show();
        $(".choose-your-payment ").show();
        $(".select-charity").show();
        $(".charity-information").show();
    }
});

$('input:radio[name="pay-time"]').click(function (){
    $(".section-1 .next-step-holder").css("display", "inline-block");
})

$('#select-your-category').selectize({
    sortField: 'text',
    maxItems: 3,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "Selecteer categorieën voor het goede doel...",
    options: [
        { id: 1, title: 'Cultuur', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'Dieren', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'Geloof', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#select-your-bank').selectize({
    sortField: 'text',
    maxItems: 1,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "BANK",
    options: [
        { id: 1, title: 'INGB', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'RAB', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'BUNQ', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 4, title: 'SNS', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 5, title: 'ABN', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#one-time').on('input', function () {
    var input = $(this);
    var is_name = input.val();
    if (is_name) { input.removeClass("invalid").addClass("valid"); }
    else { input.removeClass("valid").addClass("invalid");
 }
});

// Navigation

$('#button-toSection-2').click(function(){
    $('.section-01').hide();
    $('.section-02').show();
    event.preventDefault();
});

$(".first-section").click(function(){
    $(".section-1").show();
    $(".section-2").hide();
    $(".section-3").hide();
    $(".section-4").hide();
});

$(".second-section").click(function(){
    $(".section-2").show();
    $(".section-1").hide();
    $(".section-3").hide();
    $(".section-4").hide();
});

$(".third-section").click(function(){
    $(".section-3").show();
    $(".section-1").hide();
    $(".section-2").hide();
    $(".section-4").hide();
});

$(".fourth-section").click(function(){
    $(".section-4").show();
    $(".section-1").hide();
    $(".section-2").hide();
    $(".section-3").hide();
});
