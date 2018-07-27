(function () {
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
        let publicationId = $('#publication-view').attr('data-publicationId');
        var url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/history`);
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
            data: JSON.stringify({ chiefCuratorGuid: currentUserGuid })

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
                   <dd><a href="${publication.url}">Link to publication</a></dd>
                   <dt>Level</dt>
                   <dd>${publication.level}</dd>
                   <dt>Completion date</dt>
                   <dd>${publication.completiondate}</dd></dl>`);
            $('span#action-taken').html(publication.chiefcuratoractiontaken);
            $('chief-curator-comment').html($.parseHTML(publication.chiefcuratorcomment));
            getPublicationCurationComments();
        });
    }
    function getPublicationCurationComments() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/curatorsubmissions`);
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
            data: JSON.stringify({ chiefCuratorGuid: currentUserGuid })
        }).done(function (submissions, textStatus, jqXHR) {
            let commentsHtml = "";
            submissions.forEach(function (submission) {
                commentsHtml.concat(`<div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="heading-${submission.id}">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#submisssion-${submission.id}" aria-expanded="true" 
                                                    aria-controls="submisssion-${submission.id}">
                                                    Curator Name( ${submission.sectiontocurate})
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="submisssion-${submission.id}" class="panel-collapse collapse in" 
                                          role="tabpanel" aria-labelledby="submisssion-${submission.id}">
                                            <div class="panel-body">
                                                ${$.parseHtml(submission.notes)}
                                            </div>
                                        </div>
                                    </div>`);
            });
            $('#curator-comments').html(`<div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">${commentsHtml}</div>`);
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
    //Functions Section
})();