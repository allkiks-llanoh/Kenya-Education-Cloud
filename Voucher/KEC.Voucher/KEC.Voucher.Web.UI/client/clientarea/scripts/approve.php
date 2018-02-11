

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
                'Access-Control-Allow-Origin': 'http://localhost:60823/api/'
            } ,
        url: "http://localhost:60823/api/vouchers/selected/accept",
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