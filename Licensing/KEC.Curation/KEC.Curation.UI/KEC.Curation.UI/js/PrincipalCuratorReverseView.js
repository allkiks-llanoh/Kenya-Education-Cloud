
let principalCuratorAssignedUrl = apiBaseUrl.concat(`/PrincipalCurator/publications/assignedtochiefs`)
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
            //This code snipet prepares to append Json Data
            $('#assigned-publications').append(tableRows(data));
        }
    });
    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.title + "</td>"));
        row.append($("<td>" + rowData.description + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        return row[0];
    } 
    