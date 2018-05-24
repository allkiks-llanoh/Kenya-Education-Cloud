
(function () {
    $(document).ready(function () {
        $('#deletelevel').click(deleteLevel);
    });
    function deleteLevel(e) {
        e.preventDefault();
        let idd = $('#Id').val();
        let name = $('#Name').val();
        let url = apiBaseUrl.concat(`/Levels/${idd}`);
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            type: 'DELETE',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ Id: idd }),
            statusCode: {
                404: () => { ShowAlert("Level could not be found", 'error'); },
                403: () => { ShowAlert("You are not authorized to process this"); },
                500: () => { ShowAlert("Something went wrong while processing", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Level deleted", "success");
            window.location.assign("http://curation-d.kec.ac.ke/Home/ListLevels");
        }).fail(function () {
            ShowAlert("There was an error, please try again", 'error');
        });
    }

})();