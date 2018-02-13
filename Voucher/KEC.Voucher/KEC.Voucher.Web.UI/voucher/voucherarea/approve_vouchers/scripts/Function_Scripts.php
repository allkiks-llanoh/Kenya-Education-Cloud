<script>
        $(document).ready(function() {

            $('.footable').footable();
            $('.footable').footable();

        });

    </script>
<script type="text/JavaScript">
    //get a reference to the select element
    $select = $('#BatchId');
    //request the JSON data and parse into the select element
    $.ajax({
        type: 'GET',
        url: "http://voucherapi-d.kec.ac.ke/api/batches",
      dataType:'JSON',
      success:function(data){
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val){
          $select.append('<option id="' + val.BatchNumber+ '">' + val.Id+ '</option>');
        })
      },
      error:function(){
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
      }
    });
</script>
<script type="text/JavaScript">
   
    //This function creates the Rows where data will be appended
    
       function tableRows(data) {
            var tableRows= [];    
            for (var i = 0; i < data.length; i++) {
                tableRows.push(drawRow(data[i]));
            }
            return tableRows;
        }
    //This functionpopulates the tbody inner HTML with json data on call
       
    //This function Sends selected rows for approval then replaces existeng rows with new rows 
       function ajaxData(){
            var BatchId  = $('#BatchId').val();
            var voucherIdArray = []
            var checked = $('.approve:checked');
            $('.approve:checked').each((index,row)=>{
                voucherIdArray.push(row['id'])
            });
            return JSON.stringify({BatchId: BatchId,SelectedVouchers: voucherIdArray })
            };
            $('#approved').click(function(e) {
                e.preventDefault();
                console.log(ajaxData());
                $.ajax({
                headers : {
                    'Accept' : 'application/json',
                    'Content-Type' : 'application/json'
                } ,
            url: "http://localhost:60823/api/vouchers/approve",
            type: "PATCH",
            data: ajaxData() ,
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
    <script type="text/JavaScript">
        //Start by getting voucher list based on batch Id
        
            var BatchId  = $('#BatchId').val();
                $.ajax({
                url: "http://localhost:60823/api/Batches",
                type: "GET",
                dataType: "json",    
                success: function(data) {
                    //This code snipet prepares to append Json Data
                    $("#createdvouchers").append(tableRows(data)); 
                }
            });   
        
        //This functionpopulates the tbody inner HTML with json data on call
        function drawRow(rowData) {
                var row = $('<tr class="footable-sortable" />')
                row.append($("<td>" + rowData.BatchNumber + "</td>"));
                row.append($("<td>" + rowData.CountyCode + "</td>"));
                row.append($("<td>" + rowData.CountyName + "</td>"));
                
                row.append($("<td>" + rowData.SchoolType + "</td>"));
                row.append($(`<td><a href="Manage/Index.php?batchNumber=${rowData.BatchNumber}&batchId=${rowData.Id}&county=${rowData.CountyName}&schooltype=${rowData.SchoolType}" class="btn btn-w-m btn-info btn-xs" role="button">APPROVE VOUCHERS</a></td>`));  
              
                return row[0];
               }
           //This function Sends selected rows for rejection then replaces existeng rows with new rows 
           function ajaxData(){
                var BatchId  = $('#BatchId').val();
                var voucherIdArray = []
                var checked = $('.reject:checked');
                $('.reject:checked').each((index,row)=>{
                    voucherIdArray.push(row['id'])
                });
                return JSON.stringify({BatchId: BatchId,SelectedVouchers: voucherIdArray })
                };
                $('#BatchId').click(function(e) {
                    e.preventDefault();
                    console.log(ajaxData());
                    $.ajax({
                    headers : {
                        'Accept' : 'application/json',
                        'Content-Type' : 'application/json'
                    } ,
                url: "http://localhost:60823/api/Vouchers/selected/reject",
                type: "PATCH",
                data: ajaxData() ,
                success: function(data,status,jxhr) {
                     var remainingRows = tableRows(data);
                     var row = $('<tr  class="footable-sortable" />');
                     row.append("<th> ORDER </th>")
                     row.append("<th> SCHOOL NAME </th>");
                     row.append("<th> VOUCHER AMOUNT</th>");
                     row.append("<th> APPROVE</th>");
                     row.append("<th> REJECT</th>");
                     remainingRows.unshift(row);
                    $("#createdvouchers").html(remainingRows); 
                   }
                });
         });
        </script> 
        <!--This Script Checks the rejection checkbox-->
        <script type="text/JavaScript"> 
            $(function() {
                $('.reject').click(function() {
                $('.reject').prop('checked', this.checked);
              });
          });
          </script>