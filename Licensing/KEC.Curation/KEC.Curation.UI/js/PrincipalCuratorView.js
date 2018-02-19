function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator`)
//Start by getting Publications list that are at the Principal Curator Level
$.ajax({
    url: principalCuratorGetUrl,
    type: "GET",
    dataType: "jsonp",
    async: false,
    crossDomain: true,
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
    row.append($("<td>" + rowData.kicdNumber + "</td>"));
    row.append($(`<td class="pull-right"> <a href="/PrincipalCurator/PrincipalCuratorReview/?Title=${rowData.title}&Publication=${rowData.kicdNumber}&Stage=PrincipalCurator&Id=${rowData.id}" class="btn btn-w-m btn-info" role="button">Review</a>`));

    return row[0];
};
//Get Publications That are assignd prom the Chief Curators Assignment Repository

function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

let principalCuratorGetAssignedUrl = apiBaseUrl.concat(`/PrincipalCurator/Assigned`)
//Start by getting Publications list that are at the Principal Curator Level
$.ajax({
    url: principalCuratorGetAssignedUrl,
    type: "GET",
    dataType: "jsonp",
    async: false,
    crossDomain: true,
    success: function (data, status, jqhxr) {
        console.log(data);

        //This code snipet prepares to append Json Data
        $('#assigned-publications').append(tableRows(data));
    }
});

//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.id + "</td>"));
    row.append($("<td>" + rowData.title + "</td>"));
    row.append($("<td>" + rowData.description + "</td>"));
    row.append($("<td>" + rowData.kicdNumber + "</td>"));
    row.append($(`<td class="pull-right"> <a href="/PrincipalCurator/PrincipalCuratorReview/?Title=${rowData.title}&Publication=${rowData.kicdNumber}&Stage=PrincipalCurator&Id=${rowData.id}" class="btn btn-w-m btn-info" role="button">Review</a>`));

    return row[0];
};
