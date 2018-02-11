
<script type="text/JavaScript">
    function tableRows(data) {
        var tableRows= [];    
        for (var i = 0; i < data.length; i++) {
            tableRows.push(drawRow(data[i]));
        }
        return tableRows;
        };
        //Start by getting voucher list based on batch Id
        var id= <?php echo $_GET["Id"]; ?>;
        var code= "<?php echo $_GET["countyCode"]; ?>";
        console.log( id);
        console.log(code);
        reloadSchoolTypes();
      

        
//This functionpopulates the tbody inner HTML with json data on call
    function drawRow(rowData) {
       
        var row = $("<tr />")
        row.append($('<td class="hidden"> + rowData.Id + </td>'));
        row.append($("<td>" + rowData.Description + "</td>"));
        row.append($(`<td class="pull-right"> <button type="button" data-schooltype=${rowData.Id} data-county='${code}' class="btn btn-w-m btn-info btn-md NewBatch" role="button">CREATE BATCH</button>`));  
        return row[0];
       }
       function reloadSchoolTypes(){
        $.ajax({
            url: `http://voucherapi-d.kec.ac.ke/api/batches/${id}/pendingschooltypes`,
            type: "GET",
            dataType: "json",   
            success: function(data,status,jqhxr){
                console.log(data);
              
                //This code snipet prepares to append Json Data
                $('#SchoolTypesTable').html(tableRows(data)); 
                hookCreateBatchButtons(); 
            },
             
        });
       }
       function hookCreateBatchButtons(){
           $('.NewBatch').click(function()
          {
            $(this).html('<i class="fa fa-refresh fa-spin"></i> Please wait');
      
            var countycodes        =  $(this).attr('data-county');
            var schooltypeid         =  $(this).attr('data-schooltype');
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
          url: "http://voucherapi-d.kec.ac.ke/api/Batches",
          type: "POST",
          data: JSON.stringify({CountyCode: countycodes,SchoolTypeId: schooltypeid }),
          success: function(response,status,jxhr) {
             console.log(response);
             console.log(status);
             $('#alert').html(`Batch number ${response} created successfully.`)
             $('div.alert-success').toggleClass('hidden');
             reloadSchoolTypes();
            
             }
          });
          });
        }
</script> 
