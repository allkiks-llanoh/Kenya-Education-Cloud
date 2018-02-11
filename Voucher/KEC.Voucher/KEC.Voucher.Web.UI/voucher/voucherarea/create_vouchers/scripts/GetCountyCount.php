<script type="text/JavaScript">
//getting voucheCounties and do a count
$(document).ready(function(){
    var dd = new Date();
    var years = dd.getFullYear();
   var Count= $('#batch');
        $.ajax({
        url: `http://localhost:60823/api/batches/${years}/withpendingvouchers/count`,
        type: "GET",
    
        dataType: "json",    
        success: function(data,status,jqhxr) {
            console.log(data);
            $('#batch').html(data) 
        }
    });
}) ;  
</script>
<script type="text/JavaScript">
//getting voucheCounties and do a count
$(document).ready(function(){
    var d = new Date();
    var year = d.getFullYear();
 
   var Count= $('#voucher');
        $.ajax({
        url:` http://localhost:60823/api/schools/approvedvouchers/${year}`,
        type: "GET",
        dataType: "json",    
        success: function(data,status,jqhxr) {
           
            console.log(year);
            $('#voucher').html(data) 
        }
    });
}) ;  
</script>
