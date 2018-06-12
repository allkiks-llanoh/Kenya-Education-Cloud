

$(document).ready(function () {
    var publicationID = parseInt($('#identity').attr('data-identity'));
    let principalCuratorPostUrl = apiBaseUrl.concat(`/PrincipalCurator/publication/assignment/${publicationID}`)
    
    $('#LegalApprove').click(function () {
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var stage = "PrincipalCurator"
        var notes = "Assigned To Chief Curator"
        var actiontaken = "PublicationMoveToNextStage"
        var kicdNumber = $('#kicd').attr('data-kicdNumber');
        var principalCuratorGuid = "50af779b-7d14-4cb3-a7ac-96655e8b24e5"
        var chiefCuratorGuid = $('#UserGuid').val();
        console.log(`${publicationID}`);
        console.log(`${principalCuratorGuid}`);
        console.log(` ${chiefCuratorGuid} `);
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: principalCuratorPostUrl,
            type: "POST",
            data: JSON.stringify({ Id: publicationID}),

            success: function (response, status, jxhr) {
                console.log(response);
                console.log(status);
                $('#message').html(` ${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');

            },
            error: function (request, status, error) {

             
                $('#error').html(request.statusText)
                $('div.alert-danger').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');


            }
        });


    });
},

);
