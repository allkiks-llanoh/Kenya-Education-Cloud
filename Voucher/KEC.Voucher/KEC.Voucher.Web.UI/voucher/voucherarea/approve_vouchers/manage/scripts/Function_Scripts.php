<?php
require '.../config.php';
?>
<script>
        $(document).ready(function() {

            $('.footable').footable();
            $('.footable').footable();

        });

    </script>

<script type="text/JavaScript">
$(document).ready(function(){
        var batchId     =  $('#batchId').attr('data-batch');
        $.ajax({
        url: `https://voucherapi.kec.ac.ke/api/vouchers/created?batchId=${batchId}`,
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
            $("#createdvouchers").append(tableRows(data)); 
        },
        complete: function(jqXHR, textStatus) {
            console.log(jqXHR.getResponseHeader('page-Headers'));
      }
    });   
});
</script>
<!--  script to load counties stops here  -->

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
        var row = $("<tr />")
        row.append($("<td class='hidden'>" + rowData.Id + "</td>"));
        row.append($("<td>" + rowData.SchoolName + "</td>"));
    
        row.append($("<td>" + rowData.WalletAmount + "</td>"));
        row.append($(`<td> <input type='checkbox' id=${rowData.Id} data-voucher=${rowData.Id} name=${rowData.Id} 'approve' class='approve'/> </td>`));  
        row.append($(`<td> <input type='checkbox' id=${rowData.Id} data-voucher=${rowData.Id} name=${rowData.Id} 'reject' class='reject'/> </td>`)); 
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
                'Access-Control-Allow-Origin': 'https://voucherapi.kec.ac.ke/api/'
            } ,
        url: "https://voucherapi.kec.ac.ke/api/Vouchers/selected/reject",
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
            $('#createdvouchers>tbody').html(remainingRows); 
           }
        });
    });
</script> 
<!--This Script Checks the rejection checkbox-->

<!--HAPPY CODDING TO YOU LAZZY TESTERS-->


             <!--SCripts Start Here--> 

<!--Start by getting voucher list based on batch Id-->
<script type="text/JavaScript">
    function ajaxDatas(){
        var BatchId     =  $('#batchId').attr('data-batch');
        var voucherIdArray = []
        var checked = $('.approve:checked');
        $('.approve:checked').each((index,row)=>{
            voucherIdArray.push(row['id'])
        });
        return JSON.stringify({BatchId: BatchId,SelectedVouchers: voucherIdArray })
        };
        $('#approve').click(function(e) {
            e.preventDefault();
            console.log(ajaxDatas());
            $.ajax({
            headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': 'https://voucherapi.kec.ac.ke/api/'
            } ,
        url: "https://voucherapi.kec.ac.ke/api/vouchers/selected/accept",
        type: "PATCH",
        data: ajaxDatas() ,
        success: function(data,status,jxhr) {
             var remainingRows = tableRows(data);
             var row = $("<tr/>");
             row.append("<th> ORDER </th>")
             row.append("<th> SCHOOL NAME </th>");
             row.append("<th> VOUCHER AMOUNT</th>");
             row.append("<th> APPROVE</th>");
             row.append("<th> REJECT</th>");
             remainingRows.unshift(row);
            $('#createdvouchers>tbody').html(remainingRows); 
           }
        });
    });  
</script>
<!--This Script Checks the Approve checkbox-->

<script type="text/JavaScript">
      $(function() {
        $('.chk_approve').click(function() {
        $('.approve').prop('checked', this.checked);
      });
  });
</script> 
<!--HAPPY CODDING TO YOU LAZZY TESTER-->