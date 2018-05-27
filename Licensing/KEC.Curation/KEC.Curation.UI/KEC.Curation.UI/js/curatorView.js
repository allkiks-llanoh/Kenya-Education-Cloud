
let userGuid = $('#CurrentUserGuid').val();
let assignmentUrl = apiBaseUrl.concat(`/chiefcurator/curator/tocurate?userGuid=${userGuid}`);
function tableRows(data) {
    var tableRows = [];
    for (var i = 0; i < data.length; i++) {
        tableRows.push(drawRow(data[i]));
    }
    return tableRows;
};


//Start by getting publication list based on Payment Verification Stage
$.ajax({
    url: assignmentUrl,
    type: "GET",
    contentType: 'application/json',
    crossDomain: true,
    accepts: 'application/json',
    success: function (data, status, jqhxr) {
        console.log(data);

        //This code snipet prepares to append Json Data
        $('#unassigned-publications').append(tableRows(data));
    }
});

//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + rowData.assignmentId + "</td>"));
    row.append($("<td>" + rowData.publication + "</td>"));
    row.append($("<td>" + rowData.sectionToCurate + "</td>"));
    row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));
    row.append($(`<td> <a href="/Curator/CuratePublication/${rowData.assignmentId}?Urls=${rowData.url}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Curate</a>`));

    return row[0];
}




//(function () {
//    $(document).ready(function () {
//        getPublicationCurationComments();
//    });
//    function getPublicationCurationComments() {
//        let userGuid = $('#CurrentUserGuid').val();
//        let assignmentUrl = apiBaseUrl.concat(`/chiefcurator/curator/tocurate?userGuid=${userGuid}`);

//        $.ajax({
//            url: assignmentUrl,
//            type: 'GET',
//            contentType: 'application/json',
//            crossDomain: true,
//            accepts: 'application/json',
//            statusCode: {
//                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
//                403: () => { ShowAlert("You are not authorized to access curator submissions"); },
//                500: () => { ShowAlert("Something went wrong while retrieving curator submissions", 'error'); }
//            },
//        }).done(function (submissions, textStatus, jqXHR) {

//            $.each(submissions,function (submission) {
//                $('#unassigned-publications').html(` ${submissions.assignmentId},${submissions.sectionToCurate},${submissions.status},<a href="/Curator/CuratePublication/${submissions.assignmentId}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Curate</a>`
//                    );

//            });

//        });
//    }


//    //Functions Section


//})();