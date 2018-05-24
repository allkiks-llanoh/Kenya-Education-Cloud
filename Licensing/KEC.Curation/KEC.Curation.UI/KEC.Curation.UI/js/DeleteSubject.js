
(function () {
    $(document).ready(function () {
        $('#deletesubject').click(deleteSubject);
    });
    function deleteSubject(e) {
        e.preventDefault();
        let idd = $('#Id').val();
        let name = $('#Name').val();
        let url = apiBaseUrl.concat(`/Subjects/${idd}`);
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
                404: () => { ShowAlert("Subject could not be found", 'error'); },
                403: () => { ShowAlert("You are not authorized to process this"); },
                500: () => { ShowAlert("Something went wrong while processing", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Subject deleted", "success");
            window.location.assign("http://curation-d.kec.ac.ke/Home/ListSubjects");
        }).fail(function () {
            ShowAlert("There was an error, please try again", 'error');
        });
    }

})();