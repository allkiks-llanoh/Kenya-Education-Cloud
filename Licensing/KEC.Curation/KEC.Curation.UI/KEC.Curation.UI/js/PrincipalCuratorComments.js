(function () {
    $(document).ready(function () {
        $('#recommend').click(submitNotesAndAction);
    });
    function submitNotesAndAction(e) {
        $('#recommend').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        e.preventDefault();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let actionSelected = $("#action-selected").val();
        let notes = $('.note-editable').html();
        let fullName = $('#PrincipalFullName').val();
        console.log(fullName);
        let url = apiBaseUrl.concat(`/principalCurator/PrincipalCuratorComments/${publicationId}?publicationId=${publicationId}`);
        let userGuid = $('#CurrentUserGuid').val();
        console.log(`${userGuid}`);
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
            data: JSON.stringify({ PrincipalCuratorGuid: userGuid, Notes: notes, ActionTaken: actionSelected, FullName: fullName  }),
            statusCode: {
                400: () => { ShowAlert("Comments aready Exist", 'error'); },
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication", 'warning'); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            $('#recommend').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
            ShowAlert("Successfull, Your recommendations have been passed to  Curation Managers", "success");
        }).fail(function () {
            $('#recommend').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
            $('.modal-backdrop').remove();
            ShowAlert("Something went wrong while processing publication", 'error');
        });
    }
})();