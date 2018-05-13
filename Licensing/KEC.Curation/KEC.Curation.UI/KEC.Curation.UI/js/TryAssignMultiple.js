
$(document).ready(function () {
    let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator/PrincipalCurator`)
    function ajaxDatas() {
        let principalCuratorPostUrl = apiBaseUrl.concat(`/ChiefCurator/selected/assign`)
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
        console.log(ajaxDatas());
        $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': '*'
            },
            url: principalCuratorPostUrl,
            type: "POST",
            data: ajaxDatas(),
            success: function (data, status, jxhr) {
                consol.log(data);
            }
        });
    });


},

);
