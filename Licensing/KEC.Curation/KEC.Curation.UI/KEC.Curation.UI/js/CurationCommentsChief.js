var publicationId = $('#publication-view').attr('data-publicationId');
let chiefCuratorGuid = $('#CurrentUserGuid').val();
let getCommentsUrl = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/comments?publicationId=${publicationId}`);
function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
        return tableRows;
    };
    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: getCommentsUrl,
        type: "GET",
        dataType: "json",
        success: function (data, status, jqhxr) {
    //This code snipet prepares to append Json Data
        $('#chiefcomments').append(tableRows(data));
        }
    });
    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.status + "</td>"));
        row.append($("<td>" + rowData.notes + "</td>"));
        row.append($("<td>" + rowData.recommendation + "</td>"));
       
        return row[0];
    }
