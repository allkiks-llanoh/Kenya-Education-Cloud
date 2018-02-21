$select = $('#UserGuid');
//request the JSON data and parse into the select element
$.ajax({
    type: 'GET',
    url: "https://graph.microsoft.com/v1.0/groups/042044dc-a1f4-41c5-9927-02eeece02394/members",
    dataType: 'JSON',
    async: false,
    crossDomain: true,
    success: function (data) {
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val) {
            $select.append(`<option id=${val.id}  value='${val.id}'+ >${val.displayName}</option>`);
        })
    },
    error: function () {
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
    }
});
