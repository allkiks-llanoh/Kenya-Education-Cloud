(function () {
    $(document).ready(function () {
        getPublication();
        //getPublicationCurationComments();
        $('#recommend').click(submitChiefNotesAndAction);
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
        let chiefCuratorGUID = $('#CurrentUserGuid').val();
        let publicationId = $('#publication-view').attr('data-publicationId');
        var url = apiBaseUrl.concat(`/chiefcurator/AssignedPublicationWhenGettingCuration/${publicationId}?chiefCuratorGuid=${chiefCuratorGUID}&publicationId=${publicationId}`);
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
                `<dl id="publication-details" data-stage="${publication.stage}">
                  <div class="row">
                       <div class="col-md-3">
                           <dt>Curation</dt>
                           <dd id="kicd-number">${publication.kicdNumber}</dd>  
                       </div>
                       <div class="col-md-3">
                            <dt>Title</dt>
                            <dd>${publication.title}</dd> 
                       </div>
                         <div class="col-md-3">
                            <dt>Content Location</dt>
                            <dd><a href="${publication.url}">Link to publication</a></dd>
                       </div>
                  </div>
                  <br><br>
                  <div class="row">
                       <div class="col-md-3">
                            <dt>Subject</dt>
                            <dd>${publication.subject}</dd>
                       </div>
                        <div class="col-md-3">
                              <dt>Completion date</dt>
                                <dd>${publication.completionDate}</dd>
                       </div>
                        <div class="col-md-3">
                             <dt>Description</dt>
                            <dd>${publication.description}</dd>
                       </div>
                  </div>
                 </dl>`);
       
        });
    }

    function getPublicationCurationComments() {
        let chiefCuratorGUID = $('#CurrentUserGuid').val();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorsubmissions?chiefCuratorGuid=${chiefCuratorGUID}&publicationId=${publicationId}`);
        //let url = apiBaseUrl.concat(`/ChiefCurator/ChiefCuratorComments/${publicationId}?publicationId=${publicationId}`);
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
                $('#currator-commets').html(`${submission.notes}`);

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
        let publicationId = $('#publication-view').attr('data-publicationId');
        let actionSelected = $("#action-selected").val();
        let notes = $('.note-editable').html();

        let url = apiBaseUrl.concat(`/ChiefCurator/ChiefCuratorComments/${publicationId}?publicationId=${publicationId}`);
        let userGuid = $('#CurrentUserGuid').val();
        if (notes === null || notes === "") {
            return ShowAlert("Curation notes cannot be blank", "error");
        }
        if (actionSelected === null || actionSelected === "") {
            return ShowAlert("Please select an action taken from the list", "error");
        }

        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            type: 'POST',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ ChiefCuratorGuid: userGuid, Notes: notes, ActionTaken: actionSelected, publicationId: publicationId, Status: true }),
            statusCode: {
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Recomendations passed to Principal Curator", "success");
        }).fail(function () {
            ShowAlert("Something went wrong while processing publication", "error");
        });
    }
    //Functions Section
    

})();


