
<!-- Foo Pagination Starts Here -->

<!-- Foo Pagination Stops Here --> 


<script type="text/JavaScript">
//Global Table Rows Is Defined Here
 function tableRows(data) {
            var tableRows= [];    
            for (var i = 0; i < data.length; i++) {
                tableRows.push(drawRow(data[i]));
            }
            return tableRows;
        }
//Global Table Rows Is Definition Ends Here

//ajax GET  Transaction Information Starts Here
$(document).ready(function(){
        var id     =  $('#voucher').attr('data-fund');
        console.log(id);
        $.ajax({
        url: `http://localhost:60823/api/transactions/Voucher/${id}`,

        type: "GET",
        headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json',
                  'Access-Control-Expose-Headers':'*'
              },
        dataType: "json",    
        success: function(data,textStatus,jqXHR) {
           console.log(data); 
        }
        });
 //ajax GET  Transaction Infrmation Ends Here

//Ajax GET Voucher Information Starts Here
        $.ajax({
        url: `http://localhost:60823/api/vouchers/info/${id}`,

        type: "GET",
        headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json',
                  'Access-Control-Expose-Headers':'*'
              },
 //ajax GET Voucher Information Ends Here
        
//Append To Labels Starts Here
        dataType: "json",    
        success: function(data,textStatus,jqXHR) {
            
            // Convert UTC Time To Local Then To Simple    
            var Bdate = new Date(data.BalanceDate);
            var BDate= ((Bdate.getDate() + "-" + Bdate.getMonth() + 1) + "-" + Bdate.getFullYear());
            BDate.toString();
           
           //Convert Balance to Currency
            var bal= accounting.formatMoney(data.Balance, { symbol: "KES",  format: "%v %s" }); 
            bal.toString();
            console.log(bal);
            $('#SchoolCounty').html(data.County);
            $('#SchoolName').html(data.SchoolName);
            $('#SchoolType').html(data.SchoolType);
            $('#VoucherBalance').html(bal);
            $('#VoucherStatus').html(data.Status);
            $('#VoucherLastUse').html(BDate);                 
        }
        });
//Append To Labels Stops Here

 //Draw Table Rows
         function drawRow(rowData) {
                var row = $('<tr class="footable-sortable" />')
                row.append($("<td>" + rowData.TransactionDate + "</td>"));
                row.append($("<td>" + rowData.Pin + "</td>"));
                row.append($("<td>" + rowData.TransactionDescription + "</td>"));
                row.append($("<td>" + rowData.TransactionAmount + "</td>"));
                row.append($("<td>" + rowData.TransactedBy + "</td>"));
                return row[0];
               }
 //Draw Table Rows Stops Here

 //Script Ends / Stops Here
      });
</script>

<script type="text/javascript">
	// Library ready to use:
	accounting.formatMoney(5318008);
</script>
 
   