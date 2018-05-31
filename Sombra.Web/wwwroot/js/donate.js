
// SECTION 1


$('#control_01').click(function () {
    if ($('#control_01').is(':checked')) {
        $(".select-action").css("display", "none")
    }
});

$('#control_02').click(function () {
    if ($('#control_02').is(':checked')) {
        $(".select-action").css("display", "block")
    }
});

$('#select-your-charity').selectize({
    sortField: 'text',
    maxItems: 1,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "Naam van het goed doel...",
    options: [
        { id: 1, title: 'Spectrometer', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'Star Chart', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'Electrical Tape', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#select-action').selectize({
    sortField: 'text',
    maxItems: 1,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "Naam van de actie...",
    options: [
        { id: 1, title: 'Spectrometer', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'Star Chart', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'Electrical Tape', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#one-time').on('input', function () {
    var input = $(this);
    var is_name = input.val();
    if (is_name) { input.removeClass("invalid").addClass("valid"); }
    else { input.removeClass("valid").addClass("invalid"); }
});

$('#next-to-section-2').click(function () {

    let chooseCause = $('input:radio[name="select"]').is(':checked');
    let charitySelect = $('#select-your-charity').val();

    if (charitySelect == "") {
        charitySelect = false;
    } else {
        charitySelect = true;
    }
    let chooseAmount = $('input:radio[name="money"]').is(':checked');
    let choosePaymoment = $('input:radio[name="pay-time"]').is(':checked');

    if (chooseCause == false) {
        $('.choose-cause input').addClass("not-valid");
    } else {
        $('.choose-cause input').removeClass("not-valid");
    }

    if (charitySelect == false) {
        $('.select-charity').addClass("not-valid");
    } else {
        $('.select-charity').removeClass("not-valid");
    }

    if (chooseAmount == false) {
        $('.choose-money-amount input').addClass("not-valid");
    } else {
        $('.choose-money-amount input').removeClass("not-valid");
    }

    if (choosePaymoment == false) {
        $('.payment-options input').addClass("not-valid");
    } else {
        $('.payment-options input').removeClass("not-valid");
    }

    if (chooseAmount && charitySelect && chooseCause && choosePaymoment) {
        $(".section-1").hide();
        $(".section-2").show();
    }
    else {

    }

});

// SECTION 2

$('#select-your-bank').selectize({
    sortField: 'text',
    maxItems: 1,
    create: false,
    valueField: 'id',
    labelField: 'title',
    searchField: 'title',
    placeholder: "Naam van uw bank...",
    options: [
        { id: 1, title: 'Rabobank', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 2, title: 'ING', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 3, title: 'Andere Bank', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#ideal').click(function () {
    if ($('#ideal').is(':checked')) {
        $(".your-iban-holder").css("display", "none")
        $(".select-bank").css("display", "inline-block")
    }
});

$('#paypall').click(function () {
    if ($('#paypall').is(':checked')) {
        $(".your-iban-holder").css("display", "none")
        $(".select-bank").css("display", "inline-block")
    }
});

$('#permission').click(function () {
    if ($('#permission').is(':checked')) {
        $(".your-iban-holder").css("display", "block");
        $(".select-bank").css("display", "none")
    }
});


// $('#next-to-section-2').is(':checked');


