
let publicationIds = $('#publication-view').attr('data-publicationId');
let chiefCuratorGuids = $('#CurrentUserGuid').val();
let readCurationCommentsUrl = apiBaseUrl.concat(`/chiefcurator/publication/${publicationIds}/curatorsubmissions?chiefCuratorGuid=${chiefCuratorGuids}&publicationId=${publicationIds}`);
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
    url: readCurationCommentsUrl,
    type: "GET",
    dataType: 'json',
    success: function (data, status, jqhxr) {
        //This code snipet prepares to append Json Data
        $('#comments').append(tableRows(data));
    }
});
//This functionpopulates the tbody inner HTML with json data on call
function drawRow(rowData) {
    var row = $("<tr />")
    row.append($("<td>" + "Curated" + "</td>"));
    row.append($("<td>" + rowData.notes + "</td>"));
    return row[0];
} 
