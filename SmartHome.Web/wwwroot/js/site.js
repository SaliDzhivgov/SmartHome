
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.alert-success.sh-alert').forEach(function (alert) {
        setTimeout(function () {
            bootstrap.Alert.getOrCreateInstance(alert).close();
        }, 5000);
    });
});