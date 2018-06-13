
<script type="text/JavaScript">
   
$('#btn-postFile').click(function(e){
  e.preventDefault();
  var form_data = new FormData($('#uploadform')[0]);
  $.ajax({
      type:'POST',
      url:'https://voucherapi.kec.ac.ke/api/schools',
      processData: false,
      contentType: false,
      async: true,
      cache: false,
      data : form_data,
      beforeSend:function() {
        $('#spinner').show();
      },
      success: function(response,status,jxhr){
      console.log(response);
      $('#message').html(response);
        $('div.alert-success').toggleClass('hidden');
      },

      complete: function(xhr, status) {
        $('#spinner').hide();
        $('#postedFile').val(''); 
    }
  });
});  

$( document ).ready(function() {
   
        $('#spinner').hide();
    
});
</script> 
     