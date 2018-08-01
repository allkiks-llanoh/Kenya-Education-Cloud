(function () {
    $(document).ready(function () {
        getAssignment();
        $("#save-notes").click(saveNotes);
        $("#save-notes-submit").click(saveNotesAndSubmit);
    });
    //Functions Section
    function getAssignment() {
        let assignmentId = $('#assignment-view').attr('data-assignmentId');
        var userGuid = $('#CurrentUserGuid').val();
        let url = apiBaseUrl.concat(`/chiefcurator/curator/curate/${assignmentId}?userGuid=${userGuid}`);
        console.log(` ${userGuid}`);
        console.log(` ${assignmentId}`);
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
            type: 'GET'
        }).done(function (assignment, textStatus, jqXHR) {
            var date = new Date(assignment.assignmentDateUtc);
            $('#assignment-details').replaceWith(
                `<dl id="assignment-details">
                  <div class="row">
                        <div class="col-md-4">
                            <dt>Publication</dt>
                            <dd>${assignment.publication}</dd>
                        </div>
                        <div class="col-md-4">
                            <dt>Section</dt>
                            <dd>${assignment.sectionToCurate}</dd>
                        </div>
                        <div class="col-md-4">

                             <dt>Assignment date</dt>
                             <dd>${date.toLocaleTimeString()}</dd>
                        </div>
                   </div>`);

            $('.note-editable').html($.parseHTML(assignment.notes))
        })
    }
    function saveNotesAndSubmit(e) {
        e.preventDefault();
        let assignmentId = $('#assignment-view').attr('data-assignmentId');
        let url = apiBaseUrl.concat(`/chiefcurator/curator/curate/${assignmentId}`);
        let notes = $('.note-editable').html();
        let fullName = $('#FullName').val();
        console.log(fullName);
        let subs = "true"
        if (notes === null || notes === "") {
            ShowAlert("Cannot save blank comment", "error");
        }
        var userGuid = $('#CurrentUserGuid').val();
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            crossDomain: true,
            type: 'PATCH',
            statusCode: {
                404: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Specified resource could not be located", "error");
                },
                403: function (data, textStatus, jqXHR) {
                    ShowAlert("You are not authorized to access the specified resource", "warning");
                }
            },
            data: JSON.stringify({ userGuid: userGuid, Notes: notes, Submitted: subs, FullName: fullName }),
           
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert("Curation notes saved and submitted successfully", "success");
            $('#confirmsave').modal('hide');
            $('.modal-backdrop').remove();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while saving your notes", "error");
        });
    }

    function saveNotes(e) {
        e.preventDefault();
        let assignmentId = $('#assignment-view').attr('data-assignmentId');
        let url = apiBaseUrl.concat(`/chiefcurator/curator/curate/${assignmentId}`);
        let notes = $('.note-editable').html();
        let fullName = $('#FullName').val();
        console.log(fullName);
        let sub = "false"
        if (notes === null || notes === "") {
            ShowAlert("Cannot save blank comment", "error");
        }
        var userGuid = $('#CurrentUserGuid').val();
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            crossDomain: true,
            type: 'PATCH',
            statusCode: {
                404: function (jqXHR, textStatus, errorThrown) {
                    ShowAlert("Specified resource could not be located", "error");
                },
                403: function (data, textStatus, jqXHR) {
                    ShowAlert("You are not authorized to access the specified resource", "warning");
                }
            },
            data: JSON.stringify({ userGuid: userGuid, Notes: notes, Submitted: sub, FullName:fullName}),
           
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert("Curation notes saved successfully", "success");
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while saving your notes", "error");
        });
    }
})();