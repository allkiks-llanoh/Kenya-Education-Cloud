let subjectsGETUrl = apiBaseUrl.concat(`/SubjectTypes`);
let subjectsPOSTUrl = apiBaseUrl.concat(`/Subjects`);

//get a reference to the select element
$select = $('#SubjectTypeId');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: subjectsGETUrl,
    dataType: 'JSON',
    async: false,
    crossDomain: true,
    success: function (data) {
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $select.append(`<option id=${val.id}  value='${val.id}'+ >${val.name}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});

$(document).ready(function () {
    $('#btn-postFile').click(function () {
        $('#btn-postFile').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

        var name = $('#Name').val();
        var sid = $('#SubjectTypeId').val();

        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: subjectsPOSTUrl,
            type: "POST",
            async: false,
            crossDomain: true,
            data: JSON.stringify({ Name: name, SubjectTypeId: sid }),
            success: function (response, status, jxhr) {
                console.log(response);
                console.log(status);
                $('#alert').html(`${response}.`)
                $('div.alert-success').toggleClass('hidden');
                $('#btn-postFile').html('CREATE SUBJECT');
                window.location.assign("http://curation.kec.ac.ke/Home/ListSubjects");
            },
            error: function (request, status, error) {

                console.log(request.responseText);
                $('#error').html(request.responseText)
                $('div.alert-danger').toggleClass('hidden');
                $('#btn-postFile').html('CREATE SUBJECT');


            }
        });


    });
},

);