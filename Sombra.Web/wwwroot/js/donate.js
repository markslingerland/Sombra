var slogans = {};

$('#form-donate').on('click', '#next-to-section-4', PostForm);

$(document).ready(function() {
    $.get('donate/getcharities', function( data ) {
        var dropdownContent = $('#select-your-charity');
        $.each(data, function (index, value){
            slogans[value.charityKey] = value.slogan;
            dropdownContent.append(`<option value="${value.charityKey}">${value.name}</option>`);
        });

        $('#select-your-charity').selectize({
            onChange: CharitySelected,
            sortField: 'text',
            maxItems: 1,
            create: false,
            valueField: 'id',
            labelField: 'title',
            searchField: 'title',
            placeholder: "Naam van het goede doel..."
        });        
      });
});

function PostForm()
{
    var formData = new FormData();
    formData.append("DonationType", $('input[name="pay-time"]').val());
    formData.append("Amount", $('input[name="money"]').val());
    formData.append("CharityKey", $('#select-your-charity').val());
    formData.append("CharityActionKey", $('#select-action').val());

    $.ajax({
        type: 'post',
        url: '/doneren',
        data: formData,
        contentType: false,
        processData: false,
        success: null,
        complete: function (data) {
            if (!data.responseJSON.redirect) submitBtn.prop('disabled', false);
        },
        error: function (data) {
            highlightErrors(data, $this);
        }
    });
}

function CharitySelected(charityKey)
{
    if (!charityKey.length) return;
    $('.charity-information').text(slogans[charityKey]);
    if ($('input:radio[name="select"]:checked').val() == "charity-action")
    {
        $.get(`donate/getcharityactions?charityKey=${charityKey}`, function( data ) {
            var dropdownActionContent = $('#select-action');
            dropdownActionContent[0].selectize.destroy();
            $.each(data, function (index, value){
                dropdownActionContent.append(`<option value="${value.key}">${value.name}</option>`);
            });
            InitSelectAction();
             
          });
    }
}

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

function InitSelectAction()
{
    $('#select-action').selectize({
        sortField: 'text',
        maxItems: 1,
        create: false,
        valueField: 'id',
        labelField: 'title',
        searchField: 'title',
        placeholder: "Naam van de actie..."
    });  
}
InitSelectAction();


$('input:radio[name="pay-time"]').click(function (){
    $(".section-1 .next-step-holder").css("display", "inline-block");
})

$('#one-time').on('input', function () {
    var input = $(this);
    var is_name = input.val();
    if (is_name) { input.removeClass("invalid").addClass("valid"); }
    else { input.removeClass("valid").addClass("invalid");
 }
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
        { id: 1, title: 'ABN AMRO', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 2, title: 'ASN Bank', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 3, title: 'Bunq', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 4, title: 'Fortis', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 5, title: 'ING', url: 'http://en.wikipedia.org/wiki/Star_chart' },
        { id: 6, title: 'Knab', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 7, title: 'Rabobank', url: 'http://en.wikipedia.org/wiki/Spectrometers' },
        { id: 8, title: 'SNS Bank', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 9, title: 'Triobos', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 10, title: 'Van Lanschot Bankiers', url: 'http://en.wikipedia.org/wiki/Electrical_tape' },
        { id: 11, title: 'Andere Bank', url: 'http://en.wikipedia.org/wiki/Electrical_tape' }
    ]
});

$('#ideal').click(function () {
    if ($('#ideal').is(':checked')) {
        $(".your-iban-holder").hide();
        $(".select-bank").show();
        $(".personal-information").hide();
        $(".section-2 .next-step-holder").css("display", "inline-block");
        $("#person-detail").show();
    }
});

$('#paypall').click(function () {
    if ($('#paypall').is(':checked')) {
        $(".your-iban-holder").hide();
        $(".select-bank").hide();
        $(".personal-information").hide();
        $(".section-2 .next-step-holder").show();
        $("#person-detail").show();
    }
});

$('#permission').click(function () {
    if ($('#permission').is(':checked')) {
        $(".your-iban-holder").show();
        $(".select-bank").hide();
        $(".personal-information").show();
        $(".section-2 .next-step-holder").show();
        $("#person-detail").hide();
        
    }
});

$(".section-2 #go-back").click(function() {
    $(".section-2").hide();
    $(".section-1").show();
})




$('#next-to-section-3').click(function () { 

    let choosePaymentOption = $('input:radio[name="choose-pay-options"]').is(':checked');
    let iban_bankChecker = Boolean;

    let ibanFilled = $('iban').is(':filled');
    let bankSelected = $('#select-your-bank').val();

    if (bankSelected == "") {
        bankSelected = false;
    } else {
        bankSelected = true;
    }

    let lastNameFilled = $('#person-lastname').is(':filled');
    let postcodeFilled = $('#person-postcode').is(':filled');
    let houseNumberFilled = $('#person-housenumber').is(':filled');


    if (choosePaymentOption == false) {
        $('.pay-option input').addClass("not-valid");
    } else {
        $('.pay-option input').removeClass("not-valid");
    }

   if (bankSelected == true) {
    iban_bankChecker = true;
    $('.your-iban-holder').removeClass("not-valid");
    $('.select-bank').removeClass("not-valid");

   } else if (ibanFilled == true ) {
    iban_bankChecker = true;
    $('.your-iban-holder').removeClass("not-valid");
    $('.select-bank').removeClass("not-valid");

   } else {
    $('.your-iban-holder').addClass("not-valid");
    $('.select-bank').addClass("not-valid");
   }

   if (lastNameFilled == false) {
    $('#person-lastname').addClass("not-valid");
   } else {
    $('#person-lastname').removeClass("not-valid");
   }

   if (postcodeFilled == false) {
    $('#person-postcode').addClass("not-valid");
   } else {
    $('#person-postcode').removeClass("not-valid");
   }

   if (houseNumberFilled == false) { 
    $('#person-housenumber').addClass("not-valid");
   } else {
    $('#person-housenumber').removeClass("not-valid");
   }

   if (iban_bankChecker && choosePaymentOption && lastNameFilled && postcodeFilled && houseNumberFilled || $('#ideal').is(':checked') && iban_bankChecker || $('#paypall').is(':checked')) {
    $(".section-2").hide();
    $(".section-3").show();
}

});

// SEction 3


$(".section-3 #go-back").click(function() {
    $(".section-3").hide();
    $(".section-2").show();
})

$(".section-3 .next-step-holder").show();

$('#next-to-section-4').click(function () { 
    $(".section-3").hide();
    $(".section-4").show();
    $("footer").show();
    $("#form-donate").css("padding-bottom", "0");
 });


//  Section 4 

$(".section-4 #go-back").click(function() {
    $(".section-4").hide();
    $(".section-3").show();
    $("#form-donate").css("padding-bottom", "200px");
});

// Navigation 

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
