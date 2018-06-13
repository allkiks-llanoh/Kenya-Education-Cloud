<script>
        $(document).ready(function() {

            $('.footable').footable();
            $('.footable').footable();

        });

    </script>

<script type="text/JavaScript">
$(document).ready(function(){
       var year = (new Date()).getFullYear();
        $.ajax({
        url: `https://voucherapi.kec.ac.ke/api/fundallocations/${year}`,
        type: "GET",
        headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json',
                  'Access-Control-Expose-Headers':'*'
              },
        dataType: "json",    
        success: function(data,textStatus,jqXHR) {
            console.log(year);
            //This code snipet prepares to append Json Data
            $("#createdvouchers").append(tableRows(data)); 
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
        var row = $("<tr/>")
        row.append($("<td class='hidden'>" + rowData.Id + "</td>"));
        row.append($("<td>" + rowData.SchoolCode + "</td>"));
        row.append($("<td>" + rowData.School + "</td>"));
        row.append($("<td>" + rowData.Amount + "</td>"));
        row.append($("<td>" + rowData.Year + "</td>"));
        row.append($(`<td> <a href="approval_f/Index.php?Id=${rowData.Id}" class="btn btn-w-m btn-info btn-xs" role="button">EDIT AMOUNT</a>`));  
              
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