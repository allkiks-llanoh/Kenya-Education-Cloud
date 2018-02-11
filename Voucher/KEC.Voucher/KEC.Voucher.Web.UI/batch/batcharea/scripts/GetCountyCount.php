<script type="text/JavaScript">
//getting voucheCounties and do a count
$(document).ready(function(){
   var Count= $('#count');
        $.ajax({
        url: "http://localhost:60823/api/counties/count",
        type: "GET",
        dataType: "json",    
        success: function(data,status,jqhxr) {
            console.log(data);
            $('#count').html(data) 
        }
    });
}) ;  
</script>
<script type="text/JavaScript">
//getting voucheCounties and do a count
$(document).ready(function(){
   var Count= $('#types');
        $.ajax({
        url: "http://localhost:60823/api/schooltypes/count",
        type: "GET",
        dataType: "json",    
        success: function(data,status,jqhxr) {
            console.log(data);
            $('#types').html(data) 
        }
    });
}) ;  
</script>
<script type="text/JavaScript">
//getting voucheCounties and do a count
$(document).ready(function(){
   var Count= $('#batches');
        $.ajax({
        url: "http://localhost:60823/api/batches/count",
        type: "GET",
        dataType: "json",    
        success: function(data,status,jqhxr) {
            console.log(data);
            $('#batches').html(data) 
        }
    });
}) ;  
</script>