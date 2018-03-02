

let principalCuratorGetUrl = apiBaseUrl.concat(`/PrincipalCurator/PrincipalCurator`)
var prGuid = $('#dataGUID').attr('data-pGUID');

    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };

    //Start by getting voucher list based on batch Id
    $.ajax({
        url: principalCuratorGetUrl,
        type: "GET",
        dataType: 'json',
        success: function (data, status, jqhxr) {
            console.log(data);

            //This code snipet prepares to append Json Data
            $('#unassigned-publications').append(tableRows(data));
        }
    });

    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.id + "</td>"));
        row.append($("<td>" + rowData.title + "</td>"));
        row.append($("<td>" + rowData.description + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($(`<td> <a href="/PrincipalCurator/PrincipalCuratorReview/?Title=${rowData.title}&PGUID=${prGuid}&Identity=${rowData.id}&KICDN=${rowData.kicdNumber}&Publication=${rowData.url}&Stage=PrincipalCurator" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button">Review</a>`));

        return row[0];
    } 
