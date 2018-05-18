function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};
//Start by getting voucher list based on batch Id
var d = new Date();
var n = d.getFullYear();
let clientUrl = apiBaseUrl.concat(`/vouchers`)
$.ajax({
    url: clientUrl,
    type: "GET",
    headers :  {
        'Accept' :  'application/json',
        'Content-Type' :  'application/json',
        'Access-Control-Expose-Headers': '*'
    },
    dataType: "json",
    success: function (data, status, jqhxr) {
        console.log(data);
        //This code snipet prepares to append Json Data
        $('#createdvouchers').append(tableRows(data));
    }
});
//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($('<td class="hidden"> + rowData.Id + </td>'));
    row.append($("<td>" + rowData.CountyName + "</td>"));
    row.append($("<td>" + rowData.CountyCode + "</td>"));
    return row[0];
}