(function () {
    $(document).ready(function () {
        $('#assign-curator').click(submitCuratorAssignment);
     
        loadPublication();
    
      
    });
    //Functions Section

    function loadPublication() {
        let publicationId = $('#publication-view').attr('data-publicationId');
        let chiefCuratorGuid = $('#CurrentUserGuid').val();
        let url = apiBaseUrl.concat(`/chiefcurator/UnAssignedPublication/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);
        let Geturl = apiBaseUrl.concat(`/chiefcurator/UnAssignedPublications/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);
        let Geturl2 = apiBaseUrl
        $.ajax({
            url: url,
            crossDomain: true,
            statusCode: {
                404: () => { ShowAlert('Publication record could not be retrievd', "error"); } ,
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); },
                500: () => { ShowAlert('Something went wrong while loading publication', 'error'); }
            },
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'GET',
           
        }).done(function (publication, textStatus, jqXHR) {
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
            loadCurators();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert('Something went wrong while loading publication', 'error');
        });
    }

    function submitCuratorAssignment(e) {
        e.preventDefault();
        let curator = $("unassigned-curator");
        if (curator === null || curator === "") {
            return ShowAlert("Please select a curator from the list");
        }
        
        let section = $("#section-assigned").val();
        section = section === null || section === null ? "Whole publication" : section;
        let publicationId = $('#publication-view').attr('data-publicationId');
        let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/assign`);
        var userGuid = $('#CurrentUserGuid').val();
        var fullyAssigned = "True"
        var chiefCuratorGuid = $('#UserGuid').val();
        console.log(`${userGuid}`);
        console.log(`${chiefCuratorGuid}`);
        console.log(`${fullyAssigned}`);
        $.ajax({
            url: url,
            crossDomain: true,
            contentType: 'application/json',
            accepts: 'application/json',
            type: 'POST',
            data: JSON.stringify({ Section: section, AssignedBy: userGuid, Assignee: chiefCuratorGuid, FullyAssign: fullyAssigned }),
            statusCode: {
                201: () => { ShowAlert("Curator already assigned content", 'info'); },
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); },
                500: () => { ShowAlert('Something went wrong while processing curator assignment', 'error'); }
            }
        }).done(function (data, textStatus, jqXHR) {
            $('#assign-curator').html('Yes');
            $('#assign').modal('hide');
            $('.modal-backdrop').remove();
            ShowAlert('Curator assigned sucessfully', 'success');
            loadCurators();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            ShowAlert('Something went wrong while processing curator assignment', 'error');
        });
    }

    //function loadCurators() {
    //    let publicationId = $('#publication-view').attr('data-publicationId');
    //    let url = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/assignments`);
    //    let chiefCuratorGuid = $('#CurrentUserGuid').val();
    //    $.ajax({
    //        url: url,
    //        crossDomain: true,
    //        statusCode: {
    //            404: () => { ShowAlert('Publication assignments could not be retrieved', "error"); }
    //            ,
    //            403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
    //            ,
    //            500: () => { ShowAlert('Something went wrong while loading publication curator assignments', 'error'); }
    //        },
    //        contentType: 'application/json',
    //        accepts: 'application/json',
    //        type: 'GET',
    //        data: JSON.stringify({ chiefCuratorGuid: chiefCuratorGuid })
    //    }).done(function (assignments, textStatus, jqXHR) {
    //        $('#unassigned-publications').find('tbody').empty();
    //        let targetTable = $('#publications-assignments').find('tbody');
    //        assignments.forEach(function (assignment) {
    //            var row = $targetTable.insertRow($targetTable.rows.length);
    //            row.innerHTML = `<td>${assignment.publication}</td>
    //                             <td>${assignment.curator}</td>
    //                             <td>${assignment.sectiontocurate}</td>
    //                             <td>${convertUTCDateToLocalDate(assignment.assignmentdateutc).toLocaleDateString()}</td>
    //                             <td><a href="#" type="button" data-publication=${assignment.Id} 
    //                             class="btn btn-danger remove-assignment">Remove</a></td>`;
    //        });
    //        $('.remove-assignment').click(DeleteCuratorAssignment);
    //    }).fail(function (jqXHR, textStatus, errorThrow) {
    //        ShowAlert('Something went wrong while loading publication curator assignments', 'error');
    //    });
    //}
    let chiefCuratorGuid = $('#CurrentUserGuid').val();
    let publicationId = $('#publication-view').attr('data-publicationId');
    let assignmentGETUrl = apiBaseUrl.concat(`/chiefcurator/publication/${publicationId}/assignments?chiefCuratorGuid=${chiefCuratorGuid}`);
    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };

    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: assignmentGETUrl,
        type: "GET",
    
        dataType: "json",
        success: function (data, status, jqhxr) {
            console.log(data);

            //This code snipet prepares to append Json Data
            $('#publication-assignments').append(tableRows(data));
        }
    });

    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.publication + "</td>"));
        row.append($("<td>" + rowData.assignee + "</td>"));
        row.append($("<td>" + rowData.sectionToCurate + "</td>"));
        row.append($("<td>" + rowData.assignmentDateUtc + "</td>"));
        row.append($(`<td class="pull-right"> <button type="button" data-assignmentId=${rowData.assignmentId} class="btn btn-w-m btn-info btn-md DeleteAssignment" id="delete" role="button">Remove Assignment</button>`));  
        return row[0];
    }
    
    //function DeleteCuratorAssignment(e) {
    //    e.preventDefault();
    //    let publicationId = $(this).attr('data-assignmentId');
    //    let chiefCuratorGuid = $('#CurrentUserGuid').val();
    //    let url = apiBaseUrl.concat(`/chiefcurator/publication/assignment/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);
    //    $.ajax({
    //        url: url,
    //        crossDomain: true,
    //        statusCode: {
    //            404: () => { ShowAlert('Publication assignment could not be retrieved', "error"); }
    //            ,
    //            403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); }
    //            ,
    //            500: () => { ShowAlert('Something went wrong while deleting publication curator assignment', 'error'); }
    //        },
    //        contentType: 'application/json',
    //        accepts: 'application/json',
    //        type: 'GET',
    //        data: JSON.stringify({ chiefCuratorGuid: chiefCuratorGuid })
    //    }).done(function (data, textStatus, jqXHR) {
    //        ShowAlert('Curator assignment deleted successfully', 'success');
    //    }).fail(function (jqXHR, textStatus, errorThrow) {
    //        ShowAlert('Something went wrong while deleting publication curator assignment', 'error');
    //    });
    //}
    //End Function section
    function deleteAssignment() {
        $('#delete').click(function () {
            let url = apiBaseUrl.concat(`/chiefcurator/publication/assignment/${publicationId}?chiefCuratorGuid=${chiefCuratorGuid}`);

            $(this).html('<i class="fa fa-refresh fa-spin"></i> Please wait');

            var publicationId = $(this).attr('data-assignmentId');
            var chiefCuratorGuid = $('#CurrentUserGuid').val();

            console.log($(this).attr('data-county'));
            $.ajax({
                headers : {
                    'Accept' : 'application/json',
                    'Content-Type' : 'application/json'
                },
                beforeSend: function () {
                    if (!$('div.alert-success').attr('class').includes('hidden')) {
                        $('div.alert-success').toggleClass('hidden');
                    }
                },
                url: url,
                type: "POST",

                success: function (response, status, jxhr) {
                    console.log(response);
                    console.log(status);
                    $('#alert').html(`${response}`)
                    $('div.alert-success').toggleClass('hidden');
                    reloadSchoolTypes();

                }
            });
        });
    }
})();

//Start by getting publication list based on Payment Verification Stage
