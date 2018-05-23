
let userGuid = $('#CurrentUserGuid').val();
let principalCuratorAssignedUrl = apiBaseUrl.concat(`/principalcurator/withcomments?principalCuratorGuid=${userGuid}`)
//var prGuid = $('#dataGUID').attr('data-pGUID');

function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

//Assigned Table starts here

//Start by getting a list of contents that have been assigned to chief curators
$.ajax({
    url: principalCuratorAssignedUrl,
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
    row.append($(`<td class="hidden"> + rowData.id + </td>`));
    row.append($("<td>" + rowData.title + "</td>"));
    row.append($("<td>" + rowData.kicdNumber + "</td>"));
    row.append($(`<td> <a href="/PrincipalCurator/ViewPublication/${rowData.id}?Pub=${rowData.id}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Read Curation Comments</a>`));


    return row[0];
} 
