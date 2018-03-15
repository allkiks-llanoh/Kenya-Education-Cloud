﻿(function () {
    $(document).ready(function () {
        getPublication();
    });
    $(document).ready(function () {
        $(".btn-select").each(function (e) {
            var value = $(this).find("ul li.selected").html();
            if (value !== undefined) {
                $(this).find(".btn-select-input").val(value);
                $(this).find(".btn-select-value").html(value);
            }
        });
    });



    ///Functions Section
    function getPublication() {
        let PrincipalCuratorGUID = $('#CurrentUserGuid').val();
        let publicationId = $('#publication-view').attr('data-publicationId');
        var url = apiBaseUrl.concat(`/principalcurator/curated?principalCuratorGuid=${PrincipalCuratorGUID}&publicationId=${publicationId}`);
        console.log(url);
        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            accepts: 'application/json',
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert("Publication record could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to access the requested publication", "warning"); },
                500: () => { ShowAlert("Something went wrong while loading publication", "error"); }
            },
          

        }).done(function (publication, textStatus, jqXHR) {
            showChiefCuratorSubmissionSection(publication, "#publication-view");
            $('#publication-details').replaceWith(
                `<dl id="publication-details">
                  <dt>KICD Number</dt>
                  <dd id="kicd-number">${publication.kicdNumber}</dd>
                  <dt>Title</dt>
                   <dd>${publication.title}</dd>
                   <dt>Description</dt>
                   <dd>${publication.description}</dd></dl>`);
          
        });
    }

    function getPublicationCurationComments() {
        let chiefCuratorGUID = $('#CurrentUserGuid').val();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorsubmissions?chiefCuratorGuid=${chiefCuratorGUID}&publicationId=${publicationId}`);

        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            statusCode: {
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to access curator submissions"); },
                500: () => { ShowAlert("Something went wrong while retrieving curator submissions", 'error'); }
            },
          

        }).done(function (submissions, textStatus, jqXHR) {
           
            submissions.forEach(function (submission) {
                $('#currator-commets').html(` ${submission.assignmentId},${submission.notes}`);

            });
        
        });
    }

    function getActionsTaken() {
        var url = apiBaseUrl.concat('/lookups/actions');
        $.ajax({
            url: url,
            type: 'GET',
            accept: 'application/json',
            contentType: 'application/json',
            crossDomain: true


        }).done(function (data, textStatus, jqXHR) {
            let itemsHtml = '';
            data.forEach(function (actionItem) {
                itemsHtml = itemsHtml.concat(`<li id='${actionItem.name}'>${actionItem.description}<li>`);
               
            });
            $('#action-taken').html(itemsHtml);
            hookBtnSelect();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while loading actions", "error");
        });
    }
    function showChiefCuratorSubmissionSection(publication, parentElementId) {
        if (publication !== null && publication.chiefcuratorcanProcess) {
            let chiefCuratorSection = $('#chief-curator-section').html();
            $(parentElementId).html(chiefCuratorSection);
            $('#process-publication').click(submitChiefNotesAndAction);
            getActionsTaken();
        }
    }
    function submitChiefNotesAndAction(e) {
        e.preventDefault();
        let actionSelected = $('#action-taken').find('li.selected').attr('id');
        let notes = $('.note-editable').html();
        let kicdNumber = $("#kicd-number").val();
        let stage = $('#publication-details').attr('data-stage');
        let url = apiBaseUrl.concat('/Publications');
        let userGuid = currentUserGuid;
        if (notes === null || notes === "") {
            return ShowAlert("Curation notes cannot be blank", "error");
        }
        if (actionSelected === null || actionSelected === "") {
            return ShowAlert("Please select an action taken from the list", "error");
        }

        $.ajax({
            url: url,
            type: 'PATCH',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ KICDNumber: kicdNumber, Notes: notes, ActionTaken: actionSelected, Stage: stage, UserGuid: userGuid }),
            statusCode: {
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert("Publication processed successfully");
        }).fail(function () {
            ShowAlert("Something went wrong while processing publication", 'error');
        });
    }
    //Functions Section


})();