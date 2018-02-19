const apiBaseUrl = "http://curationapi-d.kec.ac.ke/api";
function ShowAlert(message, type) {
    $(".alert").addClass('hidden');
    if (type === "error") {
        $(".alert-danger").removeClass('hidden');
        $("p#error-message").text(message);
    } else if (type === "warning") {
        $(".alert-warning").removeClass('hidden');
        $("p#warning-message").text(message);
    } else if (type === "success") {
        $(".alert-success").removeClass('hidden');
        $("p#success-message").text(message);

    } else if (type === "info") {
        $(".alert-info").removeClass('hidden');
        $("p#info-message").text(message);
    }
}