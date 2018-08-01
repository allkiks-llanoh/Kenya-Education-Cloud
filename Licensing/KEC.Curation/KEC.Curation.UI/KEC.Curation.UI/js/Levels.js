let levelsPOSTUrl = apiBaseUrl.concat(`/levels`);
$(document).ready(function () {
    $('#btn-postFile').click(function () {
        $('#btn-postFile').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        var name = $('#Name').val();
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: levelsPOSTUrl,
            type: "POST",
            async: false,
            crossDomain: true,
            data: JSON.stringify({ Name: name }),
            success: function (response, status, jxhr) {
                $('#alert').html(`${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#btn-postFile').html('CREATE LEVEL');
                window.location.assign("http://curation.kec.ac.ke/Home/ListLevels");
            },
            error: function (response, status, error) {
                $('#error').html(response.responseText)
                $('div.alert-danger').toggleClass('hidden');
                $('#btn-postFile').html('CREATE LEVEL');
            }
        });
    });
},
);