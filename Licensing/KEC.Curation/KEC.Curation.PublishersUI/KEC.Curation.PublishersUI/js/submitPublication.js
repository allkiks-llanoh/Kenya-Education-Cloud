const apiBaseUrls = "https://curationapi-d.kec.ac.ke/api";
let publicationSubmitUrl = apiBaseUrls.concat(`/Publications/submit`);

$('#btn-postFile').click(function (e) {

    let titleChech = $('#Title').val();
    let decriptionChech = $('#Description').val();
    let authorNameChech = $('#AuthorName').val();
    let datechech = $('#CompletionDate').val();
    if (titleChech === null || titleChech === "") {

        $('#btn-postFile').html('Yes');
        $('#confirm').modal('hide');
        $('.modal-backdrop').remove();
        ShowAlert("Cannot save blank comment", "error");
    }
    if (decriptionChech === null || decriptionChech === "") {
        $('#btn-postFile').html('Yes');
        $('#confirm').modal('hide');
        $('.modal-backdrop').remove();
        ShowAlert("Cannot save blank comment", "error");
    }
    if (authorNameChech === null || authorNameChech === "") {
        $('#btn-postFile').html('Yes');
        $('#confirm').modal('hide');
        $('.modal-backdrop').remove();
        ShowAlert("Cannot save blank comment", "error");
    }
    if (datechech === null || datechech === "") {
        $('#btn-postFile').html('Yes');
        $('#confirm').modal('hide');
        $('.modal-backdrop').remove();
        ShowAlert("Cannot save blank comment", "error");
    }
    $('#btn-postFile').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
    var form_data = new FormData($('#uploadform')[0]);

    $.ajax({
        type: 'POST',
        url: publicationSubmitUrl,
        processData: false,
        contentType: false,
        async: true,
        cache: false,
        data: form_data,
        beforeSend: function () {


        },
        success: function (response, status, jxhr) {
            ShowAlert("Publication Uploaded successfully", 'success');
            $('#message').html(response);
            $('div.alert-success').toggleClass('hidden');
            $('#btn-postFile').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
            window.location.assign("https://cms.kec.ac.ke");
        },
        error: function (response, status, jxhr) {

            console.log(response);
            console.log(status);

            ShowAlert("Please Fill All Fields", 'error');
            $('#error').html(status)
            $('div.alert-danger').toggleClass('hidden');
            $('#btn-postFile').html('Yes');
            $('#confirm').modal('hide');
            $('.modal-backdrop').remove();
        }
    });
    //}
});