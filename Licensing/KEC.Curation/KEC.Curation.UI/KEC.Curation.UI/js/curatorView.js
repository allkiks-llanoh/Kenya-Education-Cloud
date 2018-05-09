
let userGuid = $('#CurrentUserGuid').val();
let assignmentUrl = apiBaseUrl.concat(`/chiefcurator/curator/tocurate?userGuid=${userGuid}`);
function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

//Start by getting publication list based on Payment Verification Stage
$.ajax({
    url: assignmentUrl,
    type: "GET",
    dataType: "json",
    success: function (data, status, jqhxr) {
        console.log(data);
        //This code snipet prepares to append Json Data
        $('#FinanceList').append(tableRows(data));
    }
});

//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.id + "</td>"));
    row.append($("<td>" + rowData.assignee + "</td>"));
    row.append($("<td>" + rowData.publicationSection + "</td>"));
    row.append($("<td>" + rowData.createdUtc + "</td>"));
    row.append($(`<td> <a href="/Curator/CuratePublication/${rowData.id}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Curate</a>`));
    return row[0];
};
