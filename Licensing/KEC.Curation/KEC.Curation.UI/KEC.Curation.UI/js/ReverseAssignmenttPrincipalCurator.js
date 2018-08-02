
$(document).ready(function () {
    $('#ReverseAssignmnet').click(reverseAssignment);
});
function reverseAssignment(e) {
    e.preventDefault();
    let publicationId = $('#publicationIdValue').val();
    let currentUser = $('#CurrentUserGuid').val();
    let url = apiBaseUrl.concat(`/principalcurator/publication/remove/${publicationId}`);
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
        data: JSON.stringify({ Id:publicationId,UserGuid:currentUser}),
        statusCode: {
            404: () => { ShowAlert("Category could not be found", 'error'); },
            403: () => { ShowAlert("You are not authorized to process this"); },
            500: () => { ShowAlert("Something went wrong while processing", 'error'); }
        }
    }).success(function (data, textStatus, jqXHR) {
        ShowAlert("Successfull, Category deleted", 'success');
        window.location.assign("https://curation.kec.ac.ke/PrincipalCurator/PrincipalCuratorReverse");
    }).fail(function () {
        ShowAlert("There was an error, please try again", 'error');
    });
}

