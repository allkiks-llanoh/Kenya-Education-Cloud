
(function () {
    $(document).ready(function () {
        
        $('#fully-assign').click(updatePublication);
    });
    function updatePublication(e) {
        $('#fully-assign').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        e.preventDefault();
        let Idd = $('#publication-view').attr('data-publicationId');
        let CuserGuids = $('#CurrentUserGuid').val();
        let url = apiBaseUrl.concat(`/chiefcurator/update/curatorcomments/${Idd}`);
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json'
            },
            url: url,
            type: 'PATCH',
            contentType: 'application/json',
            crossDomain: true,
            accepts: 'application/json',
            data: JSON.stringify({ UserGuid: CuserGuids, Status: true, publicationId: Idd }),
            statusCode: {
                404: () => { ShowAlert("Curators submissions already submitted", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            $('#fully-assign').html('Yes');
            $('#fully').modal('hide');
            $('.modal-backdrop').remove();
            ShowAlert("Publication Fully Assigned", "success");
            }).fail(function () {
                $('#fully-assign').html('Yes');
                $('#fully').modal('hide');
                $('.modal-backdrop').remove();
            ShowAlert("Publication Has no Chief Curator Assignment", 'error');
        });
    }
})();