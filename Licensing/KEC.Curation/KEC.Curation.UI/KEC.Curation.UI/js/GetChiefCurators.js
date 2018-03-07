
//$(function () {

//    AjaxCall('https://curation-d.kec.ac.ke/userprofile/GetTokenForApplication', null).done(function (response) {
//        if (response.length > 0) {

//            $('#UserGuid').html('');
//            var options = '';
            
//                options += '<option value="Select">Select</option>';
//                for (var i = 0; i < response.length; i++) {
//                    options += '<option value="' + response[i].id + '">' + response[i].job + '</option>';
//                }
//                $('#UserGuid').append(options);

          
//        }
//    }).fail(function (error) {
//        alert(error.StatusText);
//    });
//});
//   


//get a reference to the select element
$select = $('#UserGuid');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: 'https://curation-d.kec.ac.ke/userprofile/GetTokenForApplication',
    dataType: 'JSON',
    async: false,
    crossDomain: true,
    success: function (data) {
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        options += '<option value="Select">Select</option>';
        for (var i = 0; i < data.length; i++) {
            options += '<option value="' + data[i].id + '">' + data[i].job + '</option>';
        }
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});
