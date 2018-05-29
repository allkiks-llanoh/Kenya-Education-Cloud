
let userGuid = $('#CurrentUserGuid').val();
let principalCuratorGetUrl = apiBaseUrl.concat(`/chiefcurator/curator/tocurate?userGuid=${userGuid}`);

//Definition of global draw rows function
function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

//UnAssigned Table starts here

//Start by getting a list of contents that have not been assigned to chief curators
$.ajax({
    url: principalCuratorGetUrl,
    type: "GET",
    dataType: 'json',
    success: function (data, status, jqhxr) {
        console.log(data);

        //This code snipet prepares to append Json Data
        $('#unassigned-publications').append(tableRows(data));

    }
});

//This functionpopulates the tbody inner HTML with json data on call

function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));
    row.append($("<td>" + rowData.publication + "</td>"));
    row.append($("<td>" + rowData.description + "</td>"));
    row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));


    return row[0];
}



