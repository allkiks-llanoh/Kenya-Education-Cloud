(function () {
    $(document).ready(function () {
        getPublication();
        getPublicationCurationComments();
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
        let publicationIdId = $('#publication-view').attr('data-publicationId');
        var url = apiBaseUrl.concat(`/chiefcurator/publications/${publicationIdId}/curator/comments?publicationId=${publicationIdId}`);
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
       
            $('#publication-details').replaceWith(
                `<dl >
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
                  <div class="row">
                       <div class="col-md-3">
                           <dt>Curation</dt>
                           <dd id="kicd-number">${publication.kicdNumber}</dd>  
                       </div>
                       <div class="col-md-3">
                            <dt>Title</dt>
                            <dd>${publication.title}</dd> 
                       </div>
                        <div class="col-md-4">
                            <dt>Curation Level</dt>
                            <dd>Principal Curator Approval</dd> 
                       </div>
                  </div>
                  <br><br>
                
                 </dl>`);
        });
    }
    function getPublicationCurationComments() {
        let chiefCuratorGUID = $('#CurrentUserGuid').val();
        let publicationId = $('#publication-view').attr('data-publicationId');
        let urlToReadComments = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorcomments?publicationId=${publicationId}`);
        //let urls = basUrl.concat(`/chiefcurator/publication/${publicationId}/curatorcomments?publicationId=${publicationId}`);
        $.ajax({
            url: urlToReadComments,
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
                $('#comments').html(`${submission.notes}`);
            });
        });
    }
    //Functions Section
})();