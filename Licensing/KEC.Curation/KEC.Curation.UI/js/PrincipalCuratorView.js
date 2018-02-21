let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator/PrincipalCurator`);

$(document).ready(function () {
    $.ajax({
        headers: {
            contentType: 'application/json',
            accepts: 'application/json',
        },
        url: principalCuratorGetUrl,
        type: "GET",
        dataType: 'JSON',
        async: false,
        crossDomain: true,
        success: function (response) {
            var trHTML = '';
            $.each(response, function (key, value) {
                trHTML +=
                    '<tr><td>' + value.id +
                    '</td><td>' + value.title +
                    '</td><td>' + value.description +
                    '</td><td>' + value.kicdNumber +
                    '</td></tr>';
            });

            $('#unassigned-publications').append(trHTML);
        }
    });
});
