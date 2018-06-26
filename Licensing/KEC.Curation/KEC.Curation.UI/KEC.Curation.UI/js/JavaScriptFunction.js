(function () {
    $(document).ready(function () {

        $("#allan").click(deleteAssignment);

    });

    let principalCuratorHistoryUrl = apiBaseUrl.concat(`/PrincipalCurator/Curation`);

    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };
  


//Assigned Table starts here

//Start by getting a list of contents that have been assigned to chief curators
$.ajax({
    url: principalCuratorHistoryUrl,
    type: "GET",
    dataType: 'json',
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
    row.append($(`<td> 

<a href="/PrincipalCurator/Delete?Id=${rowData.id}" class="btn btn-danger" " role="button"><i class="fa fa-trash"></i> Reverse Assignment</a> 

`));
    return row[0];

}
function deleteAssignment() {

    let cGuid = $('#CurrentUserGuid').val();
    let pId = $('#allan').attr('data-assignmentId');
    let url = apiBaseUrl.concat(`/principalcurator/publication/remove/${pId}`)

    $('#allan').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

    console.log($(this).attr('data-assignmentId'));
    $.ajax({
        headers : {
            'Accept' : 'application/json',
            'Content-Type' : 'application/json'
        },

        url: url,
        type: "DELETE",
        contentType: 'application/json',
        crossDomain: true,
        accepts: 'application/json',
        data: JSON.stringify({ UserGuid: CuserGuid, Id: pId }),
        success: function (response, status, jxhr) {
            console.log(response);
            console.log(status);
            ShowAlert('Assignment removed from Chief Curator', 'success');
        },
        error: function (request, status, error) {

            ShowAlert('Something went wrong while loading publication curator assignments', 'error');
            $('#deleteas').html('Remove Assignment');


        }
    });

}
}) ();