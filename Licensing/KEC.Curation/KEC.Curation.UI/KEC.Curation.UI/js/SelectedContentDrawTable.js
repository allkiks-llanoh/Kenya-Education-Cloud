

let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator/PrincipalCurator`)


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
    row.append($("<td>" + rowData.id + "</td>"));
    row.append($("<td>" + rowData.title + "</td>"));
    row.append($("<td>" + rowData.description + "</td>"));
    row.append($("<td>" + rowData.subject + "</td>"));
    row.append($(`<td> <input type='checkbox' id=${rowData.id} data-select=${rowData.id} name=${rowData.id} 'approve' class='approve'/> </td>`));  
    return row[0];
}



