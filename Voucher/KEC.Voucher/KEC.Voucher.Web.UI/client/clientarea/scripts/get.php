
<script type="text/JavaScript">
$(document).ready(function(){
       
        $.ajax({
        url: "http://voucherapi-d.kec.ac.ke/api/vouchers",
        type: "GET",
        headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json',
                  'Access-Control-Expose-Headers':'*'
              },
        dataType: "json",    
        success: function(data,textStatus,jqXHR) {
            console.log(jqXHR.getResponseHeader('page-Headers'));
            //This code snipet prepares to append Json Data
            $("#DataTables_Table_0>tbody").html(tableRows(data)); 
        },
        complete: function(jqXHR, textStatus) {
            console.log(jqXHR.getResponseHeader('page-Headers'));
      }
    });   
});
</script>

   <!--SCripts Start Here--> 
   <script type="text/JavaScript">
//Start by getting voucher list based on batch Id

//This functionpopulates the tbody inner HTML with json data on call
function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
    }
    function drawRow(rowData) {
        var row = $('<tr class="odd" />')
        row.append($('<td class="hidden"> + rowData.Id + </td>'));
        row.append($("<td>" + rowData.VoucherCode + "</td>"));
        row.append($("<td>" + rowData.SchoolName + "</td>"));
        row.append($("<td>" + rowData.WalletAmount + "</td>"));
        row.append($("<td>" + rowData.Status + "</td>"));
        row.append($(`<td> <a href="../voucher/Index.php?batchNumber=${rowData.BatchNumber}&batchId=${rowData.Id}&county=${rowData.CountyName}&schooltype=${rowData.SchoolType}&schoolTypeId=${rowData.SchoolTypeId}" class="btn btn-w-m btn-info btn-xs" role="button">TRANSACTIONS</a>`));  
              
        return row[0];
       }
   //This function Sends selected rows for rejection then replaces existeng rows with new rows 
   function ajaxData(){
        var BatchId     =  $('#batchId').attr('data-batch');
        var voucherIdArray = []
        var checked = $('.reject:checked');
        $('.reject:checked').each((index,row)=>{
            voucherIdArray.push(row['id'])
        });
        return JSON.stringify({BatchId: BatchId,SelectedVouchers: voucherIdArray })
        };
        $('#reject').click(function(e) {
            e.preventDefault();
            console.log(ajaxData());
            $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': 'http://voucherapi-d.kec.ac.ke/api/'
            } ,
        url: "http://voucherapi-d.kec.ac.ke/api/Vouchers/selected/reject",
        type: "PATCH",
        data: ajaxData() ,
        success: function(data,status,jxhr) {
             var remainingRows = tableRows(data);
             var row = $("<tr/>");
             row.append("<th class='hidden'> ORDER </th>")
             row.append("<th> SCHOOL NAME </th>");
             row.append("<th> VOUCHER AMOUNT</th>");
             row.append("<th> APPROVE</th>");
             row.append("<th> REJECT</th>");
             remainingRows.unshift(row);
            $('#DataTables_Table_0>tbody').html(remainingRows); 
           }
        });
    });
</script> 
<!--This Script Checks the rejection checkbox-->

<!--HAPPY CODDING TO YOU LAZZY TESTERS-->