(function () {

    $(document).ready(function () {
        getPublicationsToCurate();
    });
    //Functions Section
    function getPublicationsToCurate() {
        var userGuid = $('#CurrentUserGuid').val();
        let url = apiBaseUrl.concat(`/chiefcurator/curator/tocurate?userGuid=${userGuid}`);
        console.log(`${userGuid}`);
        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            accepts: 'application/json',
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert("Publications could not be retrievd", 'error'); },
                403: () => { ShowAlert("You are not authorized to access the requested resource", "warning"); },
                500: () => { ShowAlert("Something went wrong while loading publications", "error"); }
            },
           
        }).done(function (assigments, textStatus, jqXHR) {
            $('#publications-to-curate').find('tbody').empty();
            if (assigments.length === 0) {
                ShowAlert("There are no publications to curate for now", "info");
            }
            var targetTable = $('#publications-to-curate').find('tbody');
            assigments.forEach(function (assignment) {
                var row = $targetTable.insertRow($targetTable.rows.length);
                row.innerHTML = `<td>${assignment.publicationId}</td>
                                 <td>${assignment.publication}</td>
                                 <td>${assignment.sectionToCurate}</td>
                                 <td>${assignment.assignmentDateUtc}</td>
                                 <td><a href="${curationUrl.concat('/', assignment.assignmentId)}" type="button" data-assignment=${assignment.assignmentId} 
                                 class="btn btn-success publication-action">Curate</a></td>`;
            }).fail(function (jqXHR, textStatus, errorThrow) {
                ShowAlert("Something went wrong while retrieving publications", "error");
            });
        });
    }
    //End Functions Section
})();