
var _stage = "LegalVerification";
var publicationID = $('#identity').attr('data-identity');
let legalGETUrl = apiBaseUrl.concat(`/Publications/${_stage}/Legal`)
function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};
//Start by getting voucher list based on batch Id
$.ajax({
    url: legalGETUrl,
    type: "GET",
    crossDomain: true,
    dataType: 'json',
    success: function (data, status, jqhxr) {
        //This code snipet prepares to append Json Data
        $('#LegalList').append(tableRows(data));
    }
});
//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.id + "</td>"));
    row.append($("<td>" + rowData.title + "</td>"));
    row.append($("<td>" + rowData.description + "</td>"));
    row.append($("<td>" + rowData.kicdNumber + "</td>"));
    row.append($("<td class ='hidden'>" + rowData.type + "</td>"));
    row.append($(`<td> <a href="/Stages/LegalVerify/?Title=${rowData.title}&KICDN=${rowData.kicdNumber}&Publication=${rowData.curationUrl}&Stage=LegalVerification&MimeType=${rowData.type}&Publisher=${rowData.publisher}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Review</a>`));
    return row[0];
}

