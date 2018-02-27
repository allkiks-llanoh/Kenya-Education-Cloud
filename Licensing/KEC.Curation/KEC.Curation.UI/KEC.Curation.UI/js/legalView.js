
var _stage = "LegalVerification";
var publicationID = $('#identity').attr('data-identity');
let legalGETUrl = apiBaseUrl.concat(`/Publications/1/${_stage}`)
        function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };

    //Start by getting voucher list based on batch Id
    $.ajax({
        url: legalGETUrl,
        type: "GET",
        dataType: 'json',
        success: function (data, status, jqhxr) {
            console.log(data);

        //This code snipet prepares to append Json Data
        $('#LegalList').append(tableRows(data));
        }
    });

    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.id + "</td>"));
        row.append($("<td>" + rowData.title + "</td>"));
        row.append($("<td>" + rowData.description + "</td>"));
        row.append($("<td>" + rowData.kicdNumber + "</td>"));
        row.append($(`<td> <a href="/Stages/LegalVerify/?Title=${rowData.title}&KICDN=${rowData.kicdNumber}&Publication=${rowData.url}&Stage=LegalVerification" class="btn btn-w-m btn-info" role="button">Review</a>`));

        return row[0];
    }

