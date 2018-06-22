$(document).ready(function () {
    setTimeout(function () {
        $('#name-charity').val('Tear');
        $('#kvk-number').val('12345678');
        $('#first-numbers').val('01');
        $('#last-numbers').val('0123456789');
        $('#name-contact').val('Lars ter Kraaij');
        $('#card-holder').val('L. ter Kraaij');
        $('#email').val('lars@email.com');
        $('#password').val('passwordLars');
        $('#password-repeat').val('passwordLars');

        var bankSelect = $('#select-your-bank');
        bankSelect[0].selectize.setValue(1, false);

        $('.category-checkbox-holder').find('label[data-category-id="4"] input[type="checkbox"]').prop('checked', true);
        CategoryChanged();
        $('.close-donating-page a').attr('href', 'http://ikdoneer.nu/dashboard');
    }, 5000);
});