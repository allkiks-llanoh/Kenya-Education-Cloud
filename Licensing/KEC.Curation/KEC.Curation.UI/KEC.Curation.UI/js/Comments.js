(function () {
          $(document).ready(function () {
          $('#recommend').click(submitChiefNotesAndAction);
    });
    function submitChiefNotesAndAction(e) {
        e.preventDefault();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let actionSelected = $("#action-selected").val(); 
        let notes = $('.note-editable').html();
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
            data: JSON.stringify({ ChiefCuratorGuid: userGuid, Notes: notes, ActionTaken: actionSelected, publicationId: publicationId, Status: true }),
            statusCode: {
                400: () => { ShowAlert("Comments aready Exist", 'error'); },
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Recomendations passed to Principal Curator");
        }).fail(function () {
            ShowAlert("Something went wrong while processing publication", 'error');
        });
    }
})();