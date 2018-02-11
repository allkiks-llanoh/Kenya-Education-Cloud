<script type="text/JavaScript">
    //get a reference to the select element
    $select = $('#countycode');
    //request the JSON data and parse into the select element
    $.ajax({
        type: 'GET',
        url: "http://localhost:60823/api/counties",
      dataType:'JSON',
      success:function(data){
        //clear the current content of the select
        $select.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val){
          $select.append(`<option id=${val.Id}  value='${val.CountyCode}'+ >${val.CountyName}</option>`);
        })
      },
      error:function(){
        //if there is an error append a 'none available' option
        $select.html('<option id="-1">none available</option>');
      }
    });
    
</script>
    
    
     <script type="text/JavaScript">
    
    //get a reference to the select element
    $selects = $('#schooltypeid');
    //request the JSON data and parse into the select element
    $.ajax({
        type: 'GET',
        url: "http://localhost:60823/api/schooltypes",
      dataType:'JSON',
      success:function(data){
        //clear the current content of the select
        $selects.html('');
        //iterate over the data and append a select option
        $.each(data, function (index, val){
          $selects.append(`<option id=${val.Id}  value='${val.Id}'+ >${val.Description}</option>`);
        })
      },
      error:function(){
        //if there is an error append a 'none available' option
        $selects.html('<option id="-1">none available</option>');
      }
    });
      </script>
       <script type="text/javascript">
        $(document).ready(function()
        {
          $('#newclient').click(function()
          {
            $('#newclient').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
      
            var countycode         =  $('#countycode').val();
            var schooltypeid         =  $('#schooltypeid').val();
            $.ajax({
              headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json'
              } ,
          url: "http://localhost:60823/api/Batches",
          type: "POST",
          data: JSON.stringify({CountyCode: countycode,SchoolTypeId: schooltypeid }),
          success: function(response,status,jxhr) {
             console.log(response);
             console.log(status);
             $('#alert').html(`Batch number ${response} created successfully.`)
             $('div.alert-success').toggleClass('hidden');
             $('#newclient').html('CREATE BATCH');
          
             }
          });
            
            
          });
        },
        
        );
      </script>
      