<script type="text/JavaScript">
    function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
        };
        var d = new Date();
        var n = d.getFullYear();
    //Start by getting voucher list based on batch Id
        $.ajax({
            url:  `https://voucherapi.kec.ac.ke/api/counties/batch/pending/${n}`,
            type: "GET",
            dataType: "json",   
            success: function(data,status,jqhxr){
                console.log(data);
              
                //This code snipet prepares to append Json Data
                $('#countylist').append(tableRows(data));  
            }
        });
          
//This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($('<td class="hidden"> + rowData.Id + </td>'));
        row.append($("<td>" + rowData.CountyName + "</td>"));
        row.append($("<td>" + rowData.CountyCode + "</td>"));
        row.append($(`<td> <a href="new_create_batch/Index.php?Id=${rowData.Id}&countyCode=${rowData.CountyCode.toString()}" class="btn btn-w-m btn-info btn-xs" role="button">CREATE BATCH</a>`)); 
        console.log(rowData.CountyCode);
        return row[0];
       }

 
</script> 

<!--HAPPY CODDING TO YOU LAZZY TESTERS-->