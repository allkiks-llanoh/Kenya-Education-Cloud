
$(document).ready(function () {
    var publicationID = parseInt($('#identity').attr('data-identity'));
    let principalCuratorPostUrl = apiBaseUrl.concat(`/PrincipalCurator/assign`)
    
    $('#LegalApprove').click(function () {
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

        var principalCuratorGuid = $('#UserGuid').val();
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
            data: JSON.stringify({ PrincipalCuratorGuid: principalCuratorGuid, ChiefCuratorGuid: chiefCuratorGuid, PublicationID: publicationID}),

            success: function (response, status, jxhr) {
                console.log(response);
                console.log(status);
                $('#message').html(` ${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');

            },
            error: function (request, status, error) {

                console.log(request);
                console.log(status);

                console.log(request.responseText);
                $('#error').html(request.responseText)
                $('div.alert-danger').toggleClass('hidden');
                $('#LegalApprove').html('APPROVE');


            }
        });


    });
},

);
