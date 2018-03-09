

let principalCuratorHistoryUrl = apiBaseUrl.concat(`/PrincipalCurator/Curation`)

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
    url: principalCuratorHistoryUrl ,
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
    row.append($("<td>" + rowData.description + "</td>"));
    row.append($("<td>" + rowData.kicdNumber + "</td>"));
    row.append($(`<td> <a href="/PrincipalCurator/PrincipalCuratorReview/?Title=${rowData.title}&PGUID=""&Identity=${rowData.id}&KICDN=${rowData.kicdNumber}&Publication=${rowData.url}&Stage=PrincipalCurator" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Reverse</a>`));

    return row[0];
} 
