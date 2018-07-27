
$(document).ready(function () {
    let urldrd = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorsubmissions?chiefCuratorGuid=${chiefCuratorGUID}&publicationId=${publicationId}`);
    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };
    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: urldrd,
        type: "GET",
        dataType: "json",
        success: function (data, status, jqhxr) {
            //This code snipet prepares to append Json Data
            $('#FinanceList').append(tableRows(data));
        }
    });
    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.notes + "</td>"));
        return row[0];
    }
});