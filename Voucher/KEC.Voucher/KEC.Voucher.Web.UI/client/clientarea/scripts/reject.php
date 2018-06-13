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