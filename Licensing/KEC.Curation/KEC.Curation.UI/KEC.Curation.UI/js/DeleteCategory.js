
    $(document).ready(function () {
        $('#deletecat').click(deleteCategory);
    });
    function deleteCategory(e) {
        e.preventDefault();
        let idd = $('#Id').val();
        let name = $('#Name').val();
        let url = apiBaseUrl.concat(`/SubjectTypes/${idd}`);
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
            data: JSON.stringify({Id: idd }),
            statusCode: {
                404: () => { ShowAlert("Category could not be found", 'error'); },
                403: () => { ShowAlert("You are not authorized to process this"); },
                500: () => { ShowAlert("Something went wrong while processing", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Category deleted", "success");
            window.location.assign("https://curation-d.kec.ac.ke/Home/ListCategory");
        }).fail(function () {
            ShowAlert("There was an error, please try again", 'error');
        });
    }

