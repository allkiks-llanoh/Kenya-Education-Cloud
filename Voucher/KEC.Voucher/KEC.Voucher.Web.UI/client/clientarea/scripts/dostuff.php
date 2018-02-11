   <!--SCripts Start Here--> 
   <script type="text/JavaScript">
   
        function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    }
    //Start by getting voucher list based on batch Id
    $('#search').click(function(){
    var countyName  = $('#county').val();
    var schoolId  = $('#schooltype').val();
        $.ajax({
        url: "http://voucherapi-d.kec.ac.ke/api/vouchers",
        type: "GET",
        dataType: "json",    
        success: function(data) {
            //This code snipet prepares to append Json Data
            $("#createdvouchers").append(tableRows(data)); 
                                }
    //This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($("<td>" + rowData.SchoolName + "</td>"));
        row.append($("<td>" + rowData.WalletAmount + "</td>"));
        row.append($("<td>" + rowData.Status + "</td>"));
        return row[0];
                             }

            });
    });
</script> 

<!--HAPPY CODDING TO YOU LAZZY TESTERS-->