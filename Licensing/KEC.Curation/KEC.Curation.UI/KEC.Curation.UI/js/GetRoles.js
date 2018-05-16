
//get a reference to the select element
$select = $('#RoleName');
//request the JSON data and parse into the select element
$.ajax({
    headers : {
        'Access-Controll-Allow-Origin': '*'
    },
    type: 'POST',
    url: 'https://curation.kec.ac.ke/GetUsers/GetRoles',
    dataType: 'JSON',
    async: false,
    crossDomain: true,
    success: function (data) {
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $select.append(`<option id=${val.Value}  value='${val.Value}'+ >${val.Value}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});
