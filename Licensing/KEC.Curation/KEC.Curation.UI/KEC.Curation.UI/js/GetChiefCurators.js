
$(function () {

    AjaxCall('https://curation-d.kec.ac.ke/userprofile/GetTokenForApplication', null).done(function (response) {
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