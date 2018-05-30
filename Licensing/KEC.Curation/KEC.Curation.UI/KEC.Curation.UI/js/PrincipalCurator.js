

$(document).ready(function () {
    var publicationID = $('#identity').attr('data-identity');
    let principalCuratorPostUrl = apiBaseUrl.concat(`/PrincipalCurator/publication/${publicationID}/assign`)

    $('#LegalApprove').click(function () {

        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var stage = "PrincipalCurator"
        var notes = $('.note-editable').html();
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
            error: function (response, status, jxhr) {

                console.log(request);
                console.log(status);

                console.log(request.responseText);
                ShowAlert("Something went wrong while saving your notes", "error");
                $('#error').html(request.statusText)
                $('div.alert-danger').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');


            }
        });



    });
},

);
