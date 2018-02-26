
//$select = $('#UserGuid');
////request the JSON data and parse into the select element
//$.ajax({
//    headers: {
//        'Accept': 'application / json',
//        'Content-type': 'application/json'
//    },
   
//    type: 'GET',
//    url: "https://localhost:44317/userprofile/GetTokenForApplication",
//    dataType: 'json',
//    async: false,
//    crossDomain: true,
   
//    success: function (data) {
//        //clear the current content of the sel
//        $select.html('');
//        //iterate over the data and append a select option
//        $.each(data, function (index, val) {
//            $select.append(`<option id=${val[0].word}  value='${val[0].word}'+ >${val.givenName}</option>`);
//        })
//    },
//    error: function () {
//        //if there is an error append a 'none available' option
//        $select.html('<option id="-1">none available</option>');
//    }
//});

$(function () {

    AjaxCall('https://localhost:44317/userprofile/GetTokenForApplication', null).done(function (response) {
        if (response.length > 0) {

            $('#UserGuid').html('');
            var options = '';
            
                options += '<option value="Select">Select</option>';
                for (var i = 0; i < response.length; i++) {
                    options += '<option value="' + response.id + '">' + response.dispalyName + '</option>';
                }
                $('#UserGuid').append(options);

          
        }
    }).fail(function (error) {
        alert(error.StatusText);
    });
});
//    
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json',
      
        async: false,
        crossDomain: true,
    });
}  