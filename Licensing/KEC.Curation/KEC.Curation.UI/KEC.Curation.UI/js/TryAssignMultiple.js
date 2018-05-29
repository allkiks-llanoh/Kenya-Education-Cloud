
$(document).ready(function () {

    let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator/PrincipalCurator`)
    function ajaxDatas() {

        var principalCuratorGuid = $('#CurrentUserGuid').val();
        var chiefCuratorGuid = $('#UserGuid').val();
        var publicationIdArray = []
        var checked = $('.approve:checked');
        $('.approve:checked').each((index, row) => {
            publicationIdArray.push(row['id'])
        });
        return JSON.stringify({ PrincipalCuratorGuid: principalCuratorGuid, ChiefCuratorGuid: chiefCuratorGuid, SelectedContent: publicationIdArray })
    };
    $('#assign-p').click(function (e) {
        e.preventDefault();
        $('#assign-p').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
        console.log(ajaxDatas());
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': '*'
            },
            url: "https://curationapi.kec.ac.ke/api/chiefcurator/selected/assign",
            type: "POST",
            data: ajaxDatas(),
            statusCode: {
                400: () => { ShowAlert('Title Had Aready Been Assigned', "Info"); },
                404: () => { ShowAlert('Title record could not be retrievd', "error"); },
                403: () => { ShowAlert("You are not authorized to access the specified resource", "warning"); },
                500: () => { ShowAlert('Something went wrong while assigning the Title', 'error'); }
            },
            success: function (data, status, jxhr) {
                $('#assign-p').html('Assign');
                ShowAlert("Publication processed successfully","success");
            }
        });
    });
});
