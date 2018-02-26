
$select = $('#UserGuid');
//request the JSON data and parse into the select element
$.ajax({
    headers: {
        'Accept': 'application / json',
        'Content-type': 'application/json'
    },
   
    type: 'GET',
    url: "https://localhost:44317/userprofile/GetTokenForApplication",
    dataType: 'json',
    async: false,
    crossDomain: true,
   
    success: function (data) {
        //clear the current content of the sel
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $select.append(`<option id=${val.id}  value='${val.id}'+ >${val.givenName}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});
