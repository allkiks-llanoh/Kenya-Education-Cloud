(function () {
    $(document).ready(function () {
        let unassignedSubjectsUrl = apiBaseUrl.concat('/chiefcurator/unassigned/subjects');
        let assignedSubjectsUrl = apiBaseUrl.concat('/chiefcurator/assigned/subjects');
        let curationHistoryUrl = apiBaseUrl.concat('/chiefcurator/curationhistory/subjects');
        loadPublicationSubjects(curationHistoryUrl,'#history-subjects-list' ,"history");
        loadPublicationSubjects(unassignedSubjectsUrl, '#unassigned-subjects-list', 'unassigned');
        loadPublicationSubjects(assignedSubjectsUrl, '#assigned-subjects-list', 'assigned');
        $('#load-unassigned-publications').click(function (e) {
            e.preventDefault();
            let subjectId = $('#SubjectId').val();
            let chiefCuratorGUID = $('#CurrentUserGuid').val();
            if (subjectId === null || subjectId === "") {
                return ShowAlert('Please select a subject to load publications', 'error');
            }
            let unassignedPublicationsUrl = apiBaseUrl.concat(`/chiefcurator/publications/${subjectId}/unassigned?subjectid=${subjectId}&chiefcuratorguid=${chiefCuratorGUID}`);
            loadPublications(unassignedPublicationsUrl, '#unassigned-publications', 'Assign', 'unassigned');
        });
        $('#load-assigned-publications').click(function (e) {
            e.preventDefault();
            let subjectId = $('#SubjectId').val();
            if (subjectId === null || subjectId.trim() === "") {
                return ShowAlert("Please select a subject to load publiactions", 'error');
            }
            let assignedPublicationsUrl = apiBaseUrl.concat(`/chiefcurator/publications/${subjectId}/assigned`);
            loadPublications(unassignedPublicationsUrl, '#assigned-publications', 'View', 'assigned');
        });
        $('load-history-publications').click(function (e) {
            e.preventDefault();
            let subjectId = $('#SubjectId').val();
            if (subjectId === null || subjectId.trim() === "") {
                return ShowAlert("Please select a subject to load publiactions history", 'error');
            }
            let historyPublicationsUrl = apiBaseUrl.concat(`/publications/${subjectId}/history`);
            loadPublications(historyPublicationsUrl, '#history-publications', 'View', 'assigned');
        });
    });

    //Begin Functions Section
    function loadPublicationSubjects(url, targetId, publicationType) {
        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert(`Failed to load ${publicationType} publication subjects`, "error"); }
                ,
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
                ,
                500: () => { ShowAlert(`Something went wrong while loading ${publicationType} publication subjects`); }
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET',
            data: JSON.stringify({ chiefCuratorGuid: currentUserGuid })
        }).done(function (data, textStatus, jqXHR) {
            let subjects = data;
            if (subjects.length === 0) {
                if (publicationType === 'assigned') {
                    ShowAlert('There are no publications assigned to curators for now', 'info');
                } else if (publicationType === 'unassigned') {
                    ShowAlert('There are no publications to be assigned to curators for now', 'info');
                } else {
                    ShowAlert('There are no publication curation history for now', 'info');
                }

            }
            $(targetId).typeahead({
                source: data.map(x => ({ Name: x.Name, Id: x.Id }))
            });
            }).fail(function (jqXHR, textStatus, errorThrown) {
                ShowAlert(`Something went wrong while loading ${publicationType} publication subjects`, 'error');
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
                ,
                data: JSON.stringify({ chiefCuratorGuid: currentUserGuid })
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET'
        }).done(function (data, textStatus, jqXHR) {
            let publications = data;
            if (publications.length === 0) {
                if (publicationType === 'assigned') {
                    ShowAlert('There are no publications assigned to curators for now', 'info');
                } else {
                    ShowAlert('There are no publications to be assigned to curators for now', 'info');
                }
            }
            $(tagetTableId).find('tbody').empty();
            var $targetTable = $(tagetTableId).find('tbody');
            var publicationUrl = $(tagetTableId).attr('data-publicationurl');
            publications.forEach(function (publication) {
                var row = $targetTable.insertRow($targetTable.rows.length);
                row.innerHTML = `<td>${publication.title}</td>
                                 <td>${publication.description}</td>
                                 <td>${publication.kicdnumber}</td>
                                 <td>${publication.price}</td>
                                 <td><a href="${publicationUrl.concat('/', publication.id)}" type="button" data-publication=${publication.id} 
                                 class="btn btn-success publication-action">${actionText}</a></td>`;
            });

        }).fail(function (jqXHR, textStatus, errorThrow) {
            ShowAlert("You are not authorized to access the specified resource", "warning");
        });
    }
    //End Functions Sections
})();