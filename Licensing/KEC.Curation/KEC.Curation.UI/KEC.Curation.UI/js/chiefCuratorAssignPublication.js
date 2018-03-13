(function () {
    $(document).ready(function () {
        $('#assign-curator').click(submitCuratorAssignment);
        loadPublication();
    });
    //Functions Section

    function loadPublication() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        let chiefCuratorGuid = $('#CurrentUserGuid').val();
        let url = apiBaseUrl.concat(`/chiefcurator/UnAssignedPublication/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);
        let Geturl = apiBaseUrl.concat(`/chiefcurator/UnAssignedPublications/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);

        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert('Publication record could not be retrievd', "error"); }
                ,
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
                ,
                500: () => { ShowAlert('Something went wrong while loading publication', 'error'); }
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET',
           
        }).done(function (publication, textStatus, jqXHR) {
            $('#publication-details').replaceWith(
                `<dl id="publication-details" data-stage="${publication.stage}">
                  <dt>KICD Number</dt>
                  <dd id="kicd-number">${publication.kicdnumber}</dd>
                  <dt>Title</dt>
                   <dd>${publication.title}</dd>
                   <dt>Description</dt>
                   <dd>${publication.description}</dd>
                   <dt>Type</dt>
                   <dd>${publication.type}</dd>
                   <dt>Subject</dt>
                   <dd>${publication.subject}</dd>
                   <dt>Url</dt>
                   <dd><a href="${Geturl}">Link to publication</a></dd>
                   <dt>Level</dt>
                   <dd>${publication.level}</dd>
                   <dt>Completion date</dt>
                   <dd>${publication.completiondate}</dd></dl>`);
            loadCurators();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert('Something went wrong while loading publication', 'error');
        });
    }

    function submitCuratorAssignment(e) {
        e.preventDefault();
        let curator = $("unassigned-curator");
        if (curator === null || curator === "") {
            return ShowAlert("Please select a curator from the list");
        }
        let section = $("#section-assigned").val();
        section = section === null || section === null ? "Whole publication" : section;
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/assign`);
        var userGuid = $('#CurrentUserGuid').val();
        var fullyAssigned = "fullyAssigned"
        var chiefCuratorGuid = $('#UserGuid').val();
        console.log(`${userGuid}`);
        console.log(`${chiefCuratorGuid}`);
        $.ajax({
            url: url,
            crossDomain: true,
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'POST',
            data: JSON.stringify({ Section: section, AssignedBy: userGuid, Assignee: chiefCuratorGuid, FullyAssign: fullyAssigned }),
            statusCode: {

                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
                ,
                500: () => { ShowAlert('Something went wrong while processing curator assignment', 'error'); }
            }
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert('Curator assigned sucessfully', 'success');
            loadCurators();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert('Something went wrong while processing curator assignment', 'error');
        });
    }

    function loadCurators() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/assignments`);
        let chiefCuratorGuid = currentUserGuid;
        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert('Publication assignments could not be retrieved', "error"); }
                ,
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
                ,
                500: () => { ShowAlert('Something went wrong while loading publication curator assignments', 'error'); }
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET',
            data: JSON.stringify({ chiefCuratorGuid: chiefCuratorGuid })
        }).done(function (assignments, textStatus, jqXHR) {
            $('#unassigned-publications').find('tbody').empty();
            let targetTable = $('#publications-assignments').find('tbody');
            assignments.forEach(function (assignment) {
                var row = $targetTable.insertRow($targetTable.rows.length);
                row.innerHTML = `<td>${assignment.publication}</td>
                                 <td>${assignment.curator}</td>
                                 <td>${assignment.sectiontocurate}</td>
                                 <td>${convertUTCDateToLocalDate(assignment.assignmentdateutc).toLocaleDateString()}</td>
                                 <td><a href="#" type="button" data-publication=${assignment.Id} 
                                 class="btn btn-danger remove-assignment">Remove</a></td>`;
            });
            $('.remove-assignment').click(DeleteCuratorAssignment);
        }).fail(function (jqXHR, textStatus, errorThrow) {
            ShowAlert('Something went wrong while loading publication curator assignments', 'error');
        });
    }
    function DeleteCuratorAssignment(e) {
        e.preventDefault();
        let publicationId = $(this).attr('data-publication');
        let chiefCuratorGuid = currentUserGuid;
        let url = apiBaseUrl.concat(`/chiefcurator/publication/assignment/${publicationId}`);
        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert('Publication assignment could not be retrieved', "error"); }
                ,
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
                ,
                500: () => { ShowAlert('Something went wrong while deleting publication curator assignment', 'error'); }
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET',
            data: JSON.stringify({ chiefCuratorGuid: chiefCuratorGuid })
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert('Curator assignment deleted successfully', 'success');
        }).fail(function (jqXHR, textStatus, errorThrow) {
            ShowAlert('Something went wrong while deleting publication curator assignment', 'error');
        });
    }
    //End Function section
})();