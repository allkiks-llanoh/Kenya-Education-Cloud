
<script type="text/JavaScript">
    function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
        };
        //Start by getting voucher list based on batch Id
       
        reloadSchoolTypes();
      

        
//This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
       
        var row = $("<tr />")
        row.append($('<td class="hidden"> + rowData.Id + </td>'));
        row.append($("<td>" + rowData.BatchNumber + "</td>"));
        row.append($("<td>" + rowData.CountyCode + "</td>"));
        row.append($("<td>" + rowData.CountyName + "</td>"));
        row.append($("<td>" + rowData.SchoolType + "</td>"));
        row.append($(`<td class="pull-right"> <button type="button" data-vid=${rowData.Id} class="btn btn-w-m btn-info btn-md NewBatch" role="button">CREATE VOUCHERS</button>`));  
        return row[0];
       }
       function reloadSchoolTypes(){
        var d = new Date();
       var n = d.getFullYear();
        $.ajax({
            url:`https://voucherapi.kec.ac.ke/api/batches/${n}/withpendingvouchers`,
            type: "GET",
            dataType: "json",   
            success: function(data,status,jqhxr){
                console.log(data);
              
                //This code snipet prepares to append Json Data
                $('#createdvouchers>tbody').html(tableRows(data)); 
                hookCreateBatchButtons(); 
            },
             
        });
       }
       function hookCreateBatchButtons(){
           $('.NewBatch').click(function()
          {
            $(this).html('<i class="fa fa-refresh fa-spin"></i> Please wait');
      
            var bId        =  $(this).attr('data-vid');
          
            console.log($(this).attr('data-county'));
            $.ajax({
              headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json'
              } ,
          beforeSend: function(){
              if(!$('div.alert-success').attr('class').includes('hidden')){
                $('div.alert-success').toggleClass('hidden');
              }
          },
          url: "https://voucherapi.kec.ac.ke/api/Vouchers",
          type: "POST",
          data: JSON.stringify({BatchId: bId}),
          success: function(response,status,jxhr) {
             console.log(response);
             console.log(status);
             $('#alert').html(`${response}`)
             $('div.alert-success').toggleClass('hidden');
             reloadSchoolTypes();
            
             }
          });
          });
        }
</script> 
