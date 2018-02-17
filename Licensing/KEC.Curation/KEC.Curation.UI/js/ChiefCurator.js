(function () {
    $(document).ready(function () {
        let subjectsUrl = apiBaseUrl.concat('/chiefcurator/subjects')
        $.ajax({
            url: subjectsUrl,
            crossDomain: true,
            statusCode: {
                404: function () {
                    alert("page not found");
                },
                403: function () {
                    alert("Access denied");
                }

            }
        }).done(function (data, textStatus, jqXHR){
            $('#subjects-list').typeahead({
                source: ["item 1", "item 2", "item 3"]
            }); 
            }).fail(function (jqXHR, textStatus, errorThrown) {
                $('#subjects-list').typeahead({
                    source: ["item 1", "item 2", "item 3"]
                }); 
        });
    });
})();