let financeVerifyUrl = apiBaseUrl.concat(`/Publications/process`);
$(document).ready(function () {
    $('#LegalApprove').click(function () {
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var kicdnumber = $('#kicd').attr('data-kicdNumber');
        var notes = $('.note-editable').html();
        var userGuid = $('#CurrentUserGuid').val();
        var action = "PublicationMoveToNextStage";
        var stages = "PaymentVerification";
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: financeVerifyUrl,
            type: "PATCH",
            data: JSON.stringify({ KICDNumber: kicdnumber, Notes: notes, ActionTaken: action, Stage: stages, UserGuid: userGuid }),
            success: function (response, status, jxhr) {
                $('#message').html(` ${response}.`)
                $('div.alert-success').toggleClass('hidden');
                ShowAlert("Payment Verified", "success");
                $('#LegalApprove').html('Yes');
                $('#confirm').modal('hide');
                $('.modal-backdrop').remove();
            },
            error: function (response, status, error) {
                $('#error').html(response.responseText)
                $('div.alert-danger').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');
            }
        });
    });
},
);
