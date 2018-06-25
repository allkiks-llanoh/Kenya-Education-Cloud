<script type="text/JavaScript">
    function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
        };
    //Start by getting voucher list based on batch Id
        var year= (new Date()).getFullYear();
   
        $.ajax({
           
            url: `https://voucherapi.kec.ac.ke/api/batches/${year}/withpendingvouchers`,
            type: "GET",
            dataType: "json",   
            success: function(data,status,jqhxr){
                console.log(data);
              
                //This code snipet prepares to append Json Data
                $('#createdbatches').append(tableRows(data));  
                console.log(year);
            }
        });
          
//This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
        var row = $("<tr />")
        row.append($(`<td id="bId" data-bachid=${rowData.Id} class="hidden">${rowData.Id}</td>`));
        row.append($("<td>" + rowData.BatchNumber + "</td>"));
        row.append($("<td>" + rowData.CountyName + "</td>"));
        row.append($("<td>" + rowData.CountyCode + "</td>"));
        row.append($(`<td id="sId" class="hidden"> + rowData.SchoolTypeId + </td>`));
        return row[0];
       }

       function CreateVouchers(){
                $('#newvoucher').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
                var batchId=$('#bId').attr('data-bachid');
               
            
                console.log(`${batchId} `);
            $.ajax({
                   headers : {
                                'Accept' : 'application/json',
                                'Content-Type' : 'application/json'
              } ,
            url: "https://voucherapi.kec.ac.ke/api/Vouchers",
            type: "POST",
            data: JSON.stringify({BatchId:batchId}),
            success: function(response,status,jxhr) {
                    console.log(response);
                    console.log(status);
                  
                    $('#message').html(` ${response}.`)
                    $('div.alert-success').toggleClass('hidden');
                    $('#newvoucher').html('CREATE VOUCHERS');
                
                    },
                    error: function(request,status,error) {
                    console.log(request);
                    console.log(status);
                
                    console.log(request.responseText);
                    $('#error').html(request.responseText)
                    $('div.alert-danger').toggleClass('hidden');
                    $('#newvoucher').html('CREATE VOUCHERS');
                    }
            });
        }
</script> 

<!--HAPPY CODDING TO YOU LAZZY TESTERS-->