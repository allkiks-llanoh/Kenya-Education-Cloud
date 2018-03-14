



let chiefCuratorGUID = $('#CurrentUserGuid').val();
let SubjectId = $('#SubjectId').val();

let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorsubmissions?chiefCuratorGuid=${chiefCuratorGUID}&publicationId=${publicationId}`);

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
        row.append($("<td>" + rowData.id + "</td>"));
        row.append($("<td>" + rowData.title + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($(`<td> <a href="/ChiefCurator/AssignPublication/${rowData.id}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Assign</a>`));

        return row[0];
    }
