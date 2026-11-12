
(function ($) {
    if (!$ || !$.validator || !$.validator.unobtrusive) {
        return;
    }

    $.extend($.validator.messages, {
        required: 'Полето е задължително.',
        number: 'Въведете валидно число.',
        date: 'Въведете валидна дата.',
        url: 'Въведете валиден адрес.'
    });

    $.validator.addMethod('notinfuture', function (value) {
        if (!value) {
            return true;
        }

        var parts = value.split('-');
        var selected = new Date(parts[0], parts[1] - 1, parts[2]);
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        return selected <= today;
    });

    $.validator.unobtrusive.adapters.addBool('notinfuture');
})(window.jQuery);