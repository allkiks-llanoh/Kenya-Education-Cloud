
var publicationID = $('#identity').attr('data-identity');
let financeGETUrl = apiBaseUrl.concat(`/Publications/${publicationID}/PaymentVerification`)

    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
        return tableRows;
    };
    var d = new Date();
    var n = d.getFullYear();
    //Start by getting voucher list based on batch Id
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
