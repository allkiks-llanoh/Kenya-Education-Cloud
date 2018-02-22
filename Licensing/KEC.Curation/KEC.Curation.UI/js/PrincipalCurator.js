﻿
$(document).ready(function () {
    var publicationID = $('#identity').attr('data-identity');
    let principalCuratorPostUrl = apiBaseUrl.concat(`/PrincipalCurator/${publicationID}/assign`)
    $('#LegalApprove').click(function () {
        $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

        var principalCuratorGuid = "TODO"
        var chiefCuratorGuid = $('#UserGuid');

        console.log(`${principalCuratorGuid}`);
        console.log(` ${chiefCuratorGuid} `);
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: principalCuratorPostUrl,
            type: "POST",
            data: JSON.stringify({ PrincipalCuratorGuid: principalCuratorGuid, ChiefCuratorGuid: chiefCuratorGuid }),

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
