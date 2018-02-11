
     <!-- this script creates the vouchers -->
<script type="text/javascript"> 
    function CreateVouchers()
        {
            $('#newvoucher').click(function()
             {
                $('#newvoucher').html('<i class="fa fa-refresh fa-spin"></i> Please wait');
                var batchId=$('#bId');
                var schoolTypeId= $('#sId');
            
            
                console.log(`${schoolTypeId} - ${batchId} `);
            $.ajax({
              headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json'
              } ,
            url: "http://localhost:60823/api/Vouchers",
            type: "POST",
            data: JSON.stringify({BatchId:batchId,SchoolTypeId:schoolTypeId}),
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
        });
        }
      
      </script>
      