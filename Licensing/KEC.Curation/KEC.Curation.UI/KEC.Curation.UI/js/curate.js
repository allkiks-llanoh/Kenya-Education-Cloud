﻿(function () {
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
           // data: JSON.stringify({ userGuid: userGuid, Id: assignmentId }),
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
            $('#assignment-details').replaceWith(
                `<dl id="assignment-details">
                  <dt>Publication</dt>
                  <dd>${assignment.publication}</dd>
                  <dt>Section</dt>
                   <dd>${assignment.sectionToCurate}</dd>
                   <dt>Assignment date</dt>
                   <dd>${assignment.assignmentDateUtc}</dd>
                   <dt>Url</dt>
                   <dd>${assignment.publicationUrl}</dd></dl>`);
            $('.note-editable').html($.parseHTML(assignment.notes))
        })
    }
    function saveNotesAndSubmit(e) {
        e.preventDefault();
        let assignmentId = $('#assignment-view').attr('data-assignmentId');
        let url = apiBaseUrl.concat(`/chiefcurator/curator/curate/${assignmentId}`);
        let notes = $('.note-editable').html();
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
            data: JSON.stringify({userGuid: userGuid, Notes: notes, Submitted: true }),
           
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert("Curation notes saved successfully", "success");
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while saving your notes", "error");
        });
    }

    function saveNotes(e) {
        e.preventDefault();
        let assignmentId = $('#assignment-view').attr('data-assignmentId');
        let url = apiBaseUrl.concat(`/chiefcurator/curator/curate/${assignmentId}`);
        let notes = $('.note-editable').html();
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
            data: JSON.stringify({ userGuid: userGuid, Notes: notes, Submitted: sub }),
         
        }).done(function (data, textStatus, jqXHR) {
            ShowAlert("Curation notes saved and submitted successfully", "success");
            let url = $('#back-to-assignments').attr('href');
            if (typeof IE_fix != "undefined") // IE8 and lower fix to pass the http referer
            {
                document.write("redirecting..."); // Don't remove this line or appendChild() will fail because it is called before document.onload to make the redirect as fast as possible. Nobody will see this text, it is only a tech fix.
                var referLink = document.createElement("a");
                referLink.href = url;
                document.body.appendChild(referLink);
                referLink.click();
            }
            else { window.location.replace(url); }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert("Something went wrong while saving your notes", "error");
        });
    }
    //End Functions Section
})();