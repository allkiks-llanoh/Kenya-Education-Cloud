const apiBaseUrl = "https://curationapi-d.kec.ac.ke/api";
let subjectsUrl = apiBaseUrl.concat(`/Subjects/ForCurators`);


//get a reference to the select element
$select = $('#SubjectTypeId');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: subjectsUrl,
    dataType: 'JSON',
    async: false,
    crossDomain: true,
    success: function (data) {
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $select.append(`<option id=${val.name}  value='${val.name}'+ >${val.name}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});
