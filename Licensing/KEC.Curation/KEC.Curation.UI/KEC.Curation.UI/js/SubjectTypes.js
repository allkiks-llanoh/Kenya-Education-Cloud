let subjectTypesUrl = apiBaseUrl.concat(`/SubjectTypes`)
$(document).ready(function () {
    $('#btn-postFile').click(function () {
        $('#btn-postFile').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var name = $('#Name').val();
        $.ajax({
            headers : {

                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Method': '*'
            },
            url: subjectTypesUrl,
            type: "POST",
            async: false,
            crossDomain: true,
            data: JSON.stringify({ Name: name }),
            success: function (response, status, jxhr) {
                $('#alert').html(`${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#btn-postFile').html('CREATE CATEGORY');
                window.location.assign("http://curation.kec.ac.ke/Home/ListCategory");
            },
            error: function (response, status, error) {
                $('#error').html(response.responseText)
                $('div.alert-danger').toggleClass('hidden');
                $('#btn-postFile').html('CREATE CATEGORY');
            }
        });

    });
},

);