$( document ).ready(function() {
    $('#name-charity').val('Tear');
    $('#kvk-number').val('12345678');
    $('#kvk-number').val('12345678');
    $('#first-numbers').val('01');
    $('#kvk-number').val('12345678');
    $('#last-numbers').val('0123456789');
    $('#name-contact').val('Lars ter Kraaij');
    $('#card-holder').val('L. ter Kraaij');
    $('#email').val('lars@email.com');
    $('#password').val('passwordLars');
    $('#password-repeat').val('passwordLars');    

    var bankSelect = $('#select-your-bank');
    bankSelect[0].selectize.setValue(1, false);

    var categorySelect = $('#select-your-category');
    categorySelect[0].selectize.setValue(4, false);
});