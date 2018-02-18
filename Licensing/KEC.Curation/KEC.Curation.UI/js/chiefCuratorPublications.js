(function () {
    $(document).ready(function () {
        let unassignedSubjectsUrl = apiBaseUrl.concat('chiefcurator/unassigned/subjects');
        let assignedSubjectsUrl = apiBaseUrl.concat('chiefcurator/assigned/subjects');
        loadPublicationSubjects(unassignedSubjectsUrl, '#unassigned-subjects-list', 'unassigned');
        loadPublicationSubjects(assignedSubjectsUrl, '#assigned-subjects-list', 'assigned');
        $('#load-unassigned-publications').click(function () {
            let subjectId = $('#unassigned-subjects-list').val();
            let unassignedPublicationsUrl = apiBaseUrl.concat(`/${subjectId}/unassigned`);
            loadPublications(unassignedPublicationsUrl, '#unassigned-publications', 'Assign', 'unassigned');
        });
        $('#load-assigned-publications').click(function () {
            let subjectId = $('#assigned-subjects-list').val();
            let assignedPublicationsUrl = apiBaseUrl.concat(`/${subjectId}/assigned`);
            loadPublications(unassignedPublicationsUrl, '#assigned-publications', 'View', 'assigned');
        });
    });

    //Begin Functions Section
    function loadPublicationSubjects(url, targetId, publicationType) {

        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Failed to load subjects", "error");
                },
                403: function (data, textStatus, jqXHR) {
                    ShowAlert("You are not authorized to access the specified resource", "warning");
                }

            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET'
        }).done(function (data, textStatus, jqXHR) {
            let subjects = $.parseJSON(data);
            if (subjects.length === 0) {
                if (publicationType === 'assigned') {
                    ShowAlert('There are no publications assigned to curators for now', 'info');
                } else {
                    ShowAlert('There are no publications to be assigned to curators for now', 'info');
                }
            }
            $(targetId).typeahead({
                source: data.map(x => ({ Name: x.Name, Id: x.Id }))
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while processing your request", "error");
        });
    }

    function loadPublications(url, tagetTableId, actionText, publicationType) {
        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Specified resource could not be located", "error");
                },
                403: function (data, textStatus, jqXHR) {
                    ShowAlert("You are not authorized to access the specified resource", "warning");
                }

            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET'
        }).done(function (data, textStatus, jqXHR) {
            let publications = $.parseJSON(data);
            if (publications.length === 0) {
                if (publicationType === 'assigned') {
                    ShowAlert('There are no publications assigned to curators for now', 'info');
                } else {
                    ShowAlert('There are no publications to be assigned to curators for now', 'info');
                }
            }
            var $targetTable = $(tagetTableId).find('tbody');
            publications.forEach(function (publication) {
                var row = $targetTable.insertRow($targetTable.rows.length);
                row.innerHTML = `<td>${publication.Title}</td>
                                 <td>${publication.Description}</td>
                                 <td>${publication.KICDNumber}</td>
                                 <td>${publication.Price}</td>
                                 <td><button type="button" data-publication=${publication.Id} 
                                 class="btn btn-success">${actionText}</button></td>`;
            });

        }).fail(function (jqXHR, textStatus, errorThrow) {
            ShowAlert("You are not authorized to access the specified resource", "warning");
        });
    }
    //End Functions Sections
})();