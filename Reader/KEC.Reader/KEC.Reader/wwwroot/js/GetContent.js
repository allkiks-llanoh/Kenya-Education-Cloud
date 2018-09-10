$(document).ready(function () {
    const apiBaseUrl = "https://store-d.kec.ac.ke/Publications";
    let userGuid = $('#userEmail').val();
    let url = apiBaseUrl.concat(`/GetBooksToRead?email=${userGuid}`);
    function tableRows(data) {
        var tableRows = [];
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    };
    //Start by getting publication list based on Payment Verification Stage
    $.ajax({
        url: url,
        type: "GET",
        crossDomain: true,
        success: function (data, status, jqhxr) {
            //This code snipet prepares to append Json Data
            $('#unassigned-publications').append(tableRows(data));
        }
    });

    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var s = rowData.thumbNailImage;
        var q = "";
        while (s.charAt(0) === '~') {
            s = s.substr(1);
            q = s;
        }
        var imageLocation = `https://store-d.kec.ac.ke${q}`;
        console.log(imageLocation);
        var row = $("<tr />")
        row.append($(`<td> <a href="/Home/About/${rowData.id}" class="btn btn-w-m btn-info" style="background-color:#00B95F;" role="button"><img src=${imageLocation} alt="Thumbnail" height="80" width="80"></a>`));
        row.append($("<td>" + rowData.description + "</td>"));
       
        return row[0];
    }
});

