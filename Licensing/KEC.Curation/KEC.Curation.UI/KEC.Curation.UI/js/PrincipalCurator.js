
(function () {
$(document).ready(function () {
    $("#LegalApprove").click(submitAssignment);
});
function submitAssignment(e) {
        e.preventDefault();
        var publicationID = $('#identity').attr('data-identity');
        let principalCuratorPostUrl = apiBaseUrl.concat(`/PrincipalCurator/publication/${publicationID}/assign`)
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var stage = "PrincipalCurator";
        var notes = $('.note-editable').html();
        if (notes === null || notes === "") {
            ShowAlert("Please include instructions ", "error");
        }
        var actiontaken = "PublicationMoveToNextStage"
        var principalCuratorGuid = $('#CurrentUserGuid').val();
        var kicdNumber = $('#kicd').attr('data-kicdNumber');
        var chiefCuratorGuid = $('#UserGuid').val();
        console.log(`${publicationID}`);
        console.log(`${principalCuratorGuid}`);
        console.log(` ${chiefCuratorGuid} `);
        console.log(` ${notes} `);

        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: principalCuratorPostUrl,
            type: "POST",
            statusCode: {
                400: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Specified resource could not be located", "error");
                },
                404: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Specified resource could not be located", "error");
                },
                403: function (data, textStatus, jqXHR) {
                    ShowAlert("You are not authorized to access the specified resource", "warning");
                }
            },
            data: JSON.stringify({ PrincipalCuratorGuid: principalCuratorGuid, ChiefCuratorGuid: chiefCuratorGuid, KICDNumber: kicdNumber, Notes: notes, Stage: stage, ActionTaken: actiontaken }),

            success: function (response, status, jxhr) {
                console.log(response);
                console.log(status);
                $('#message').html(` ${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#LegalApprove').html('Yes');
                $('#confirm').modal('hide');
                $('.modal-backdrop').remove();
                ShowAlert("Title Assigned Succesfully", "success");
            },
            error: function (request, responseTex, jxhr) {

                console.log(request);
                console.log(responseTex);

                console.log(request.responseText);
              
                $('#confirm').modal('hide');
                $('.modal-backdrop').remove();
                $('#LegalApprove').html('APPROVE');
                ShowAlert("Assignment Instructions Are Required", "error");


            }
        });
    }
})();
