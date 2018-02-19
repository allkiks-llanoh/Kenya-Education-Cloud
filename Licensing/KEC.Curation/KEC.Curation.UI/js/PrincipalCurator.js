
////get a reference to the select element
//$userGuid = $('#UserGuids');
//let chiefCuratorsGetUrl = apiBaseUrl.concat(`/PrincipalCurator`)
////request the JSON data and parse into the select element
//$.ajax({
//    type: 'GET',
//    url: "http://curationapi-d.kec.ac.ke/api/Subjects",
//    dataType: 'JSON',
//    success: function (data) {
//        //clear the current content of the select
//        $userGuid.html('');
//        //iterate over the data and append a select option
//        $.each(data, function (index, val) {
//            $userGuid.append(`<option id=${val.id}  value='${val.id}'+ >${val.name}</option>`);
//        })
//    },
//    error: function () {
//        //if there is an error append a 'none available' option
//        $userGuid.html('<option id="-1">none available</option>');
//    }
//});

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
