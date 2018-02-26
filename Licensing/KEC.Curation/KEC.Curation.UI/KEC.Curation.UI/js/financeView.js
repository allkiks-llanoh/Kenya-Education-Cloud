
var _stage = "PaymentVerification";
let financeGETUrl = apiBaseUrl.concat(`/Publications/${_stage}/Finance`)

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
        row.append($("<td>" + rowData.title + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($(`<td class="pull-right"> <a href="/Stages/FinanceVerify/?Title=${rowData.title}&Publication=${rowData.kicdNumber}&Stage=PaymentVerification" class="btn btn-w-m btn-info" role="button">Review</a>`));


        return row[0];
    }
