(function () {
    $(document).ready(function () {
        getPublication();
        getPublicationCurationComments();
        $('#recommend').click(submitChiefNotesAndAction);
    });
    ///Functions Section
    function getPublication() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        var urls = apiBaseUrls.concat(`/CurationManagers/get/publications/${publicationId}`);
        $.ajax({
            url: urls,
            type: 'GET',
            contentType: 'application/json',
            accepts: 'application/json',
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert("Publication record could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to access the requested publication", "warning"); },
                500: () => { ShowAlert("Something went wrong while loading publication", "error"); }
            },
        }).done(function (publications, textStatus, jqXHR) {
            publications.forEach(function (publication) {
                $('#publication-details').replaceWith(
                    `<dl>
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
                            <dd><a href="${publication.curationUrl}">Link to publication</a></dd>
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
        });
    }
    function getPublicationCurationComments() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/CurationManagers/getcomments/fromcuration?publicationId=${publicationId}`);
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
                $('#currator-commets').html(` <dt>Curator Comments</dt><dd> ${submission.curatorComments}</dd>
<br/>
<hr/>
                                                  <dt>Chief Curator Comments</dt><dd>${submission.chiefCuratorComments}</dd>
<br/>
<hr/>
                                                  <dt>Principal Curator Comments</dt><dd>${submission.principalCuratorComments}</dd>
<br/><hr/>`);

            });

        });
    }
    function showChiefCuratorSubmissionSection(publication, parentElementId) {
        if (publication !== null) {
            let chiefCuratorSection = $('#chief-curator-section').html();
            $(parentElementId).html(chiefCuratorSection);
        }
    }
    function submitChiefNotesAndAction(e) {
        e.preventDefault();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let todo = $("#action-selected").val();
        let notes = $('.note-editable').html();
        let url = apiBaseUrl.concat(`/CurationManagers`);
        if (notes === null || notes === "") {
            return ShowAlert("Curation notes cannot be blank", "error");
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
            data: JSON.stringify({ PublicationId: publicationId, Notes: notes, ToDo: todo }),
            statusCode: {
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Curated Publication Processed Succesfully", 'success');
        }).fail(function () {
            ShowAlert("Something went wrong while processing publication", 'error');
        });
    }
    //Functions Section
})();