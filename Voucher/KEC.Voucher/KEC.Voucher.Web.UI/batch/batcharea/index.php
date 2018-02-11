

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>KEC | Dashboard</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

 

    <!-- Gritter -->
    <link href="js/plugins/gritter/jquery.gritter.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
   

    

</head>

<body>
    <div id="wrapper">

        <?php require_once 'nav.php';?>
        <br>
        <div class="row">
        <div class="col-lg-3">
                <a href="#">
                <div class="widget style1 navy-bg">
                    <div class="row">
                        
                        <div class="col-xs-12 text-right">
                            <span style="font-size:18px;">Counties Registerd in <br>KEC</span>
                            <h2 id="count">Loading...</h2>
                        </div>
                    </div>
                </div>
                </a>
            </div>
            <div class="col-lg-3">
                <a href="#">
                <div class="widget style1 lazur-bg">
                    <div class="row">
                        
                        <div class="col-xs-12 text-right">
                            <span style="font-size:18px;"> School Types Registerd in KEC</span>
                            <h2 id="types">Loading...</h2>
                        </div>
                    </div>
                </div>
                </a>
            </div>
            <div class="col-lg-3">
                <a href="#">
                <div class="widget style1 red-bg">
                    <div class="row">
                       
                        <div class="col-xs-12 text-right">
                            <span style="font-size:18px;">Number of Batches Created</span>
                            <h2 id="batches">Loading...</h2>
                        </div>
                    </div>
                </div>
                </a>
            </div>
            <div class="col-lg-3">
                <a href="#">
                    <div class="widget style1 yellow-bg">
                       <div class="row">
                            <div class="col-xs-12 text-right">
                            <span style="margin-left:25; font-size:18px; margin-top:20px;"> Counties with Missing Batches </span>
                            <h2> <button onclick="show()" style="float:right;" class="btn btn-info">View List</button></h2> 
                            </div>
                        </div>
                    </div>
                </a> 
            </div>
            <div class="col-lg-12" id="list">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <div class="row" class="form-group">
                        <div col-md-6 >
                        <h5 style="margin-left: 25px; margin-top:10px;">List Of Batches Available for Voucher Creation</h5>
                        </div>
                        <div col-md-6>
                        <a href="batch/"  style="float:right; margin-right:20px;"  class="btn btn-primary">CREATE THESE BATCHES</a>
                        </div>
                       </div>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                    </div>
                    </div>
               <div class="ibox-content">
                    <div class="table-responsive">
                        <table id="createdvouchers" style="text-align:left;" class="footable table table-stripped toggle-arrow-tiny">
                                 <tr>
                                    <th class="hidden"> ID </th>
                                    <th> COUNTY NAME </th>
                                    <th> COUNTY CODE</th>
                                 <tr>
                            <tfoot>
                               <td colspan="5">
                               <ul class="pagination pull-right"></ul>
                              </td>
                            </tfoot>
                        </table>
                    </div>
                </div>
        
            
      
        
        
    </div>
    <div class="footer" style="">
            <strong>Copyright</strong> KEC  &copy; 2018
        </div>
    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Flot -->
    <script src="js/plugins/flot/jquery.flot.js"></script>
    <script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="js/plugins/flot/jquery.flot.spline.js"></script>
    <script src="js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="js/plugins/flot/jquery.flot.pie.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- GITTER -->
    <script src="js/plugins/gritter/jquery.gritter.min.js"></script>

    <!-- Sparkline -->
    <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- Toastr -->
 
    <script type="text/javascript">

            $("#list").hide();

        function show() {
            var x = document.getElementById("list");
            if (x.style.display === "block") {
                x.style.display = "none";
            } else {
                x.style.display = "block";
            }
        }
    </script>
<?php require_once 'scripts/GetCountyCount.php';?>
<?php require_once 'scripts/BatchToList.php';?>

</body>
</html>
