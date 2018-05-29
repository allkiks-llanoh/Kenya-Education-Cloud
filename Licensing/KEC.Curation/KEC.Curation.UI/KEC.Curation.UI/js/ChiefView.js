


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
    statusCode: {
        201: () => { ShowAlert("Curator already assigned content", 'info'); },
        500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
    },
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
        row.append($("<td>" + rowData.description + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($(`<td> <a href="/ChiefCurator/AssignPublication/${rowData.id}?Urls=${rowData.url}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Assign</a>`));

        return row[0];
    };
