
var CuratorCommId = $('#CurrentUserGuid').val();
var SubjectId = $('#SubjectId').val();
var publicationID = parseInt($('#identity').attr('data-identity'));
let getComments = apiBaseUrl.concat(`/chiefcurator/publication/${publicationID}/curatorsubmissions?chiefCuratorGuid=${CuratorCommId}&publicationId=${publicationID}`);
function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
        return tableRows;
    };
    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: getComments,
        type: "GET",
        dataType: "json",
        success: function (data, status, jqhxr) {
    //This code snipet prepares to append Json Data
        $('#comments').append(tableRows(data));
        }
    });
    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.assignmentId + "</td>"));
        row.append($("<td>" + rowData.sectionToCurate+ "</td>"));
        row.append($("<td>" + rowData.notes + "</td>"));
        return row[0];
    }
