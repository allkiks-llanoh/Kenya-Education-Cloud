
(function () {
    $(document).ready(function () {
        $('#editlevel').click(updateLevel);
    });
    function updateLevel(e) {
        e.preventDefault();
        let idd = $('#Id').val();
        let name = $('#Name').val();
        let url = apiBaseUrl.concat(`/Levels/${idd}?Id=${idd}`);
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            type: 'PATCH',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ Name: name, Id: idd }),
            statusCode: {
                404: () => { ShowAlert("Level could not be found", 'error'); },
                403: () => { ShowAlert("You are not authorized to process this"); },
                500: () => { ShowAlert("Something went wrong while processing", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Level updated", "success");
        }).fail(function () {
            ShowAlert("There was an error, please try again", 'error');
        });
    }

})();