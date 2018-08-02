(function () {
          $(document).ready(function () {
          $('#recommend').click(submitChiefNotesAndAction);
    });
    function submitChiefNotesAndAction(e) {
        e.preventDefault();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let actionSelected = $("#action-selected").val(); 
        let notes = $('.note-editable').html();
        let fullName = $('#ChiefFullName').val();
        let url = apiBaseUrl.concat(`/ChiefCurator/ChiefCuratorComments/${publicationId}?publicationId=${publicationId}`);
        let userGuid = $('#CurrentUserGuid').val();
        if (notes === null || notes === "") {
            return ShowAlert("Curation notes cannot be blank", "error");
        }
        if (actionSelected === null || actionSelected === "") {
            return ShowAlert("Please select an action taken from the list", "error");
        }
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            type: 'POST',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ ChiefCuratorGuid: userGuid, Notes: notes, ActionTaken: actionSelected, publicationId: publicationId, Status: true, FullName: fullName }),
            statusCode: {
                400: () => { ShowAlert("Comments aready Exist", 'error'); },
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); },
                501: () => { ShowAlert("Some curators have not submitted their recommendations", 'warning'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            $('#recommend').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
            ShowAlert("Successfull, your recomendations have been passed to the Principal Curator", 'success');
        }).fail(function () {
            $('#recommend').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
            ShowAlert("Confirm that all assigned curators have subbmitted recommendations", 'info');
        });
    }
})();