var highlightFields = function (response, form) {
    $.each(response, function (propName, val) {
        var nameSelector = `[name="${propName}"]`,
            idSelector = `#${propName}`;
        var $el = form.find(nameSelector) || form.find(idSelector);

        if (val.Errors.length > 0) {
            $el.addClass('is-invalid');
            var error = [];
            $.each(val.Errors, function (i, item) {
                error.push(item.ErrorMessage);
            });
            form.find(`span[data-valmsg-for="${propName}"]`).addClass('invalid-feedback').html(error.join('<br />'));
        }
    });
};
var highlightErrors = function (xhr, form) {
    var data = JSON.parse(xhr.responseText);
    highlightFields(data, form);
    showSummary(data, form);
    window.scrollTo(0, 0);
};
var showSummary = function (response, form) {
    var errors = [];
    $.each(response, function (prop, val) {
        $.map(val.Errors, function (error) {
            errors.push(error.ErrorMessage);
        });
    });

    if (errors.length > 1) {
        var list = $('<ul />');
        $.map(errors, function (error) {
            list.append($('<li />').text(error));
        });

        form.find('[data-valmsg-summary="true"]').html(list).addClass('invalid-feedback');
    } else if (errors.length) {
        form.find('[data-valmsg-summary="true"]').html(errors[0]).addClass('invalid-feedback');
    }
};
var redirect = function (data) {
    if (data.redirect) {
        window.location.href = data.redirect;
    } else {
        window.scrollTo(0, 0);
        window.location.reload();
    }
};

$(document).on('submit', 'form[method=post]:not(.no-ajax)', function () {
    var $this = $(this);
    var submitBtn = $this.find('button[type="submit"]');
    if (!submitBtn.length) submitBtn = $(`button[type="submit"][form="${this.id}"]`);

    submitBtn.prop('disabled', true);
    $(window).unbind();
    $('*').removeClass('invalid-feedback').removeClass('is-invalid');

    $.ajax({
        url: $this.attr('action'),
        type: 'post',
        data: new FormData(this),
        contentType: false,
        processData: false,
        headers: { '__RequestVerificationToken': $(this).find('input[name="__RequestVerificationToken"]').val() },
        success: redirect,
        complete: function (data) {
            if (!data.responseJSON.redirect) submitBtn.prop('disabled', false);
        },
        error: function (data) {
            highlightErrors(data, $this);
        }
    });

    return false;
});