﻿
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
    contentType: 'application/json',
    crossDomain: true,
    accepts: 'application/json',
    success: function (data, status, jqhxr) {
        //This code snipet prepares to append Json Data
        $('#unassigned-publications').append(tableRows(data));
    }
});

//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.assignmentId + "</td>"));
    row.append($("<td>" + rowData.publication + "</td>"));
    row.append($("<td>" + rowData.sectionToCurate + "</td>"));
    row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));
    row.append($(`<td> <a href="/Curator/CuratePublication/${rowData.assignmentId}?Urls=${rowData.curationUrl}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Curate</a>`));

    return row[0];
}




