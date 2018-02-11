<script type="text/JavaScript">
function createPanel(data){
    
       
        console.log( $('#FundPanel'));
        $('div#FundPanel').html(`<h4><span><strong>School Code:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ${data.SchoolCode} </h4>                                     
            <h4><span><strong>School:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;${data.School}</h4>                                    
            <h4><span><strong>Year:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;${data.Year} </h4>
            <h4><span><strong>Current Amount:</strong></span>&nbsp;${data.Amount} </h4>`);
            $('div#FundPanelFooter').html(  `<h4 id='fundallocationid' data-fundallocation=${data.Id}><span><strong>New Amount:</strong>&nbsp;&nbsp;</span><input type="number" class='form-control' id='NewAmount'></input></h4>
            <div> <button type="button" class="btn btn-primary" id='btn-update'>UPDATE AMOUNT</button> 
            <a href="../" class="btn btn-info pull-right" id='btn-back'>BACK TO LIST</a></div>`)
  
    
};
$(document).ready(function(){
        var id     =  $('#fundallocation').attr('data-fund');
        console.log(id);
        $.ajax({
        url: `http://voucherapi-d.kec.ac.ke/api/fundallocation/${id}`,

        type: "GET",
        headers : {
                  'Accept' : 'application/json',
                  'Content-Type' : 'application/json',
                  'Access-Control-Expose-Headers':'*'
              },
        dataType: "json",    
        success: function(data,textStatus,jqXHR) {
           console.log(data);
            createPanel(data);
            $('#btn-update').click(function(e) {
          
          console.log(ajaxData());
          $.ajax({
          headers : {
                'Accept' : 'application/json',
                'Content-Type' : 'application/json',
                'Access-Control-Allow-Origin': 'http://voucherapi-d.kec.ac.ke/api/'
            } ,
      url: "http://voucherapi-d.kec.ac.ke/api/fundallocations",
      type: "PATCH",
      data: ajaxData() ,
      success: function(response,status,jxhr) {
             console.log(response);
             console.log(status);
             $('#message').html(` ${response}.`)
             $('div.alert-success').toggleClass('hidden');
           
          
             }
     
      });
  });
        }
   });
   function ajaxData(){
        var Id     =  $('#fundallocation').attr('data-fund');
       
        var Amount     = $('#NewAmount').val();
      
        return JSON.stringify({Id: Id,Amount: Amount })
        };
       
});

</script>
   <!--SCripts Start Here--> 
   