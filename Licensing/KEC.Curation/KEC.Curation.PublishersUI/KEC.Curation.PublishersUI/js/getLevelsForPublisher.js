//get a reference to the select element
$levelId = $('#LevelId');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: "https://publishervm.kec.ac.ke/api/Levels",
    dataType: 'JSON',
    success: function (data) {
        //clear the current content of the select
        $levelId.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $levelId.append(`<option id=${val.id}  value='${val.id}'+ >${val.name}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $levelId.html('<option id="-1">none available</option>');
    }
});