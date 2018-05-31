//get a reference to the select element
$subjectId = $('#SubjectId');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: "https://curationapi.kec.ac.ke/api/Subjects",
    dataType: 'JSON',
    success: function (data) {
        //clear the current content of the select
        $subjectId.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $subjectId.append(`<option id=${val.id}  value='${val.id}'+ >${val.name}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $subjectId.html('<option id="-1">none available</option>');
    }
});
