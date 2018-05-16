
(function () {
    $(document).ready(function () {
        $('#editsubject').click(updateSubject);
    });
    function updateSubject(e) {
        e.preventDefault();
        let idd = $('#Id').val();
        let name = $('#Name').val();
        let url = apiBaseUrl.concat(`/Subjects/Subject/${idd}?Id=${idd}`);
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
                404: () => { ShowAlert("Subject could not be found", 'error'); },
                403: () => { ShowAlert("You are not authorized to process this"); },
                500: () => { ShowAlert("Something went wrong while processing", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Subject updated", "success");
        }).fail(function () {
            ShowAlert("There was an error, please try again", 'error');
        });
    }

})();