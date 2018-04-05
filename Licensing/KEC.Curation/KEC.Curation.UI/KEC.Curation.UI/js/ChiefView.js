


let subjectId = $('#SubjectId').val();
let chiefCuratorGUID = $('#CurrentUserGuid').val();

let chiefCuratorGETUrl = apiBaseUrl.concat(`/chiefcurator/publications/${subjectId}/unassigned?subjectid=${subjectId}&chiefcuratorguid=${chiefCuratorGUID}`);
function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
        return tableRows;
    };
   
    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: chiefCuratorGETUrl,
        type: "GET",
      
        dataType: "json",
        success: function (data, status, jqhxr) {
        console.log(data);

    //This code snipet prepares to append Json Data
        $('#unassigned-publications').append(tableRows(data));
        }
    });

    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.publicationId + "</td>"));
        row.append($("<td>" + rowData.publication + "</td>"));
        row.append($("<td>" + rowData.sectionToCurate + "</td>"));
        row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));
        row.append($(`<td> <a href="/ChiefCurator/AssignPublication/${rowData.publicationId}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Assign</a>`));

        return row[0];
    };
