document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.alert-success.sh-alert').forEach(function (alert) {
        setTimeout(function () {
            bootstrap.Alert.getOrCreateInstance(alert).close();
        }, 5000);
    });

    var iconSelect = document.getElementById('Icon');
    var iconPreview = document.getElementById('icon-preview');
    if (iconSelect && iconPreview) {
        iconSelect.addEventListener('change', function () {
            iconPreview.className = 'bi ' + iconSelect.value;
        });
    }
});