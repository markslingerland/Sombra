
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


$('#charity').click(function () {
    if ($('#charity').is(':checked')) {
        $("#name-charity").attr("placeholder", "Naam van het goede doel");
        $(".category-dropdown p").text("Selecteer categorieën voor het goede doel…");
    }
});

$('#stichting').click(function () {
    if ($('#stichting').is(':checked')) {
        $("#name-charity").attr("placeholder", "Naam van de stichting");
        $(".category-dropdown p").text("Selecteer categorieën voor de stichting…");
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

    let namecharity = $('#name-charity').is(':filled');
    let kvknumber = $('#kvk-number').is(':filled');
    let firstNumbers = $('#first-numbers').is(':filled');
    let lastNumbers = $('#last-numbers').is(':filled');
    let cardholder = $('#card-holder').is(':filled');
    let nameContact = $('#name-contact').is(':filled');
    let email = $('#email').is(':filled');
    let password = $('#password').is(':filled');
    let passwordrepeat = $('#password-repeat').is(':filled');
    
    if (namecharity == false) {
        $('#name-charity').addClass("not-valid");
       } else {
        $('#name-charity').removeClass("not-valid");
       }
    
       if (kvknumber == false) {
        $('#kvk-number').addClass("not-valid");
       } else {
        $('#kvk-number').removeClass("not-valid");
       }
       
       if (firstNumbers == false) {
        $('#first-numbers').addClass("not-valid");
       } else {
        $('#first-numbers').removeClass("not-valid");
       }

       if (lastNumbers == false) {
        $('#last-numbers').addClass("not-valid");
       } else {
        $('#last-numbers').removeClass("not-valid");
       }

       if (cardholder == false) {
        $('#card-holder').addClass("not-valid");
       } else {
        $('#card-holder').removeClass("not-valid");
       }

       if (nameContact == false) {
        $('#name-contact').addClass("not-valid");
       } else {
        $('#name-contact').removeClass("not-valid");
       }

       if (email == false) {
        $('#email').addClass("not-valid");
       } else {
        $('#email').removeClass("not-valid");
       }

       if (password == false) {
        $('#password').addClass("not-valid");
       } else {
        $('#password').removeClass("not-valid");
       }


       if (passwordrepeat == false) {
        $('#password-repeat').addClass("not-valid");
       } else {
        $('#password-repeat').removeClass("not-valid");
       }

       if (iban_bankChecker && choosePaymentOption && lastNameFilled && postcodeFilled && houseNumberFilled || $('#ideal').is(':checked') && iban_bankChecker || $('#paypall').is(':checked')) {
        $(".section-2").hide();
        $(".section-3").show();
    }
    


    $('.section-01').hide();
    $('.section-02').show();
    $('.section-03').hide();
});

$('#button-toSection-3').click(function(){
    $('.section-01').hide();
    $('.section-02').hide();
    $('.section-03').show();
});

$(".first-section").click(function(){
    $(".section-01").show();
    $(".section-02").hide();
    $(".section-03").hide();
});

$(".second-section").click(function(){
    $(".section-02").show();
    $(".section-01").hide();
    $(".section-03").hide();
});

$(".third-section").click(function(){
    $(".section-03").show();
    $(".section-01").hide();
    $(".section-02").hide();
});

$(".section-02 #go-back").click(function(){
    $(".section-03").hide();
    $(".section-01").show();
    $(".section-02").hide();
});

$('.category-dropdown').click(function(){
    if ($('.checkbox-dropdown').hasClass("shown")) {
     $('.checkbox-dropdown').hide();
     $('.checkbox-dropdown').removeClass("shown");
     $('.category-dropdown').removeClass("category-dropdown-clicked");
 
    } else {
     $('.checkbox-dropdown').show();
     $('.checkbox-dropdown').addClass("shown");
     $('.category-dropdown').addClass("category-dropdown-clicked");
    }
 });

 var limit = 3;
$('input.single-checkbox').on('change', function(evt) {
   if($(this).siblings(':checked').length >= limit) {
       this.checked = false;
   }
});