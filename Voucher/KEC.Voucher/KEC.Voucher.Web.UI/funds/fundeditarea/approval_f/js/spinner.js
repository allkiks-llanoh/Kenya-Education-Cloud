$(document).ready(function(){
    $('.loading').hide();
    $(document).on('ajax:before',function(){
$('.loading').show();
});
$(document).on('ajax:complete',function(xhr,status){
    $('.loading').hide();}); 
});
