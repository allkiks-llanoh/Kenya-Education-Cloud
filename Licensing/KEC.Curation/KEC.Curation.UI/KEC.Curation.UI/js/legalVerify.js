
let legalVerifyUrl = apiBaseUrl.concat(`/Publications/process`);
$(document).ready(function () {
    $('#LegalApprove').click(function () {
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var kicdnumber = $('#kicd').attr('data-kicdNumber');
        var notes = $('.note-editable').html();
        var userGuid = $('#CurrentUserGuid').val();
        var action = $('#actionTaken').val();
        var stages = "LegalVerification";
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: legalVerifyUrl,
            type: "PATCH",
            data: JSON.stringify({ KICDNumber: kicdnumber, Notes: notes, ActionTaken: action, Stage: stages, UserGuid: userGuid }),
            success: function (response, status, jxhr) {
                $('div.alert-success').toggleClass('hidden');
                $('#LegalApprove').html('Yes');
                $('#confirmlegal').modal('hide');
                $('.modal-backdrop').remove();
                $('#message').html(` ${response}.`)
                ShowAlert("Successfull, Requirements Conformity Verified", "success");
            },
            error: function (response, status, error) {
                $('div.alert-danger').toggleClass('hidden');
                $('#LegalApprove').html('Yes');
                $('#confirmlegal').modal('hide');
                $('.modal-backdrop').remove();
                $('#error').html(response.responseText)
                ShowAlert("Bad Reques, Something went wrong, contact administrator", "error");
            },
            }
        });
    });
},
);

