
(function () {
    $(document).ready(function () {
        $('#fully-assign').click(updatePublication);
    });
    function updatePublication(e) {
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
            ShowAlert("Publication Fully Assigned", "success");
        }).fail(function () {
            ShowAlert("Publication Has no Chief Curator Assignment", 'error');
        });
    }
})();