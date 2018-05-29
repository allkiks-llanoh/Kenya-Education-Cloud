
(function () {
    $(document).ready(function () {
        $('#recommend').click(updateChiefCuratorAssignment);
    });
    function updateChiefCuratorAssignment(e) {
        e.preventDefault();
        let Id = $('#publication-view').attr('data-publicationId');
        let CuserGuid = $('#CurrentUserGuid').val();   
        let url = apiBaseUrl.concat(`/principalcurator/update/chiefcuratorcomments/${Id}?publicationId=${Id}`);

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
            data: JSON.stringify({ UserGuid: CuserGuid, publicationId: Id}),
            statusCode: {
                404: () => { ShowAlert("Curators submissions already submitted", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Publication Fully Curated", "success");
        }).fail(function () {
            ShowAlert("Curators submissions already submitted", 'error');
        });
    }

})();