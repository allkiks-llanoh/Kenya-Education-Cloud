
(function () {
    $(document).ready(function () {
        $('#recommend').click(updateChiefCuratorAssignment);
    });
    function updateChiefCuratorAssignment(e) {
        e.preventDefault();   
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
            data: JSON.stringify({ UserGuid: CuserGuid, Status: true }),
            statusCode: {
                404: () => { ShowAlert("Curators submissions could not be retrieved", 'error'); },
                403: () => { ShowAlert("You are not authorized to process publication"); },
                500: () => { ShowAlert("Something went wrong while processing publication", 'error'); }
            }
        }).success(function (data, textStatus, jqXHR) {
            ShowAlert("Publication Fully Curated", "success");
        }).fail(function () {
            ShowAlert("Something went wrong while processing publication", 'error');
        });
    }
})();