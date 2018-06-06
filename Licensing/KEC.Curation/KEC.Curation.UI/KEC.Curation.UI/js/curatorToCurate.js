
(function () {

    $(document).ready(function () {
        getPublicationCurationComments();
    });
function getPublicationCurationComments() {

    let publicationId = $('#publication-view').attr('data-publicationId');
    let url = apiBaseUrl.concat(`/CurationManagers/getcomments/fromcuration?publicationId=${publicationId}`);
    //let url = apiBaseUrl.concat(`/ChiefCurator/ChiefCuratorComments/${publicationId}?publicationId=${publicationId}`);
    $.ajax({
        url: "http://localhost:15177/api/chiefcurator/curator/listings/120",
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
            $('#currator-commets').html(` <dt>Curator Recommendations</dt><dd> ${submission.curatorComments}</dd>
                                                  <dt>Chief Curator Recommendations</dt><dd>${submission.assignmentId}</dd>
                                                  <dt>Principal Curator Recommendations</dt><dd>${submission.publication}</dd>
                                                  <dt>Curation Managers Recommendations</dt><dd>${submission.sectionToCurate}</dd>`);

        });

    });
    }
})();