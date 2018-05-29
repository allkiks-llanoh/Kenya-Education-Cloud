
let financeGETUrl = apiBaseUrl.concat(`/Subjects/Listing/Category`);

function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};

//Start by getting publication list based on Payment Verification Stage
$.ajax({
    url: financeGETUrl,
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
    row.append($("<td>" + rowData.name + "</td>"));
    row.append($(`<td> 
<a href="/Home/EditCategory/?Id=${rowData.id}&Name=${rowData.name}" class="btn btn-info" style="background-color:#00B95F;" role="button"><i class="fa fa-edit"></i> Edit</a> 
<a href="/Home/DeleteCategory/?Id=${rowData.id}&Name=${rowData.name}" class="btn btn-danger" " role="button"><i class="fa fa-trash"></i> Delete</a> 

`));

    return row[0];
}


