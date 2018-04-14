

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
        <div class="col-lg-4">
                <a href="#">
                <div class="widget style1 navy-bg">
                    <div class="row">
                       
                        <div class="col-xs-12 text-right">
                            <span style="font-size:20px;"> Batches Available</span>
                            <h2 id="batch">Loading...</h2><br><br>

                        </div>
                    </div>
                </div>
                </a>
            </div>
            <div class="col-lg-4">
                <a href="#">
                <div class="widget style1 lazur-bg">
                    <div class="row">
                       
                        <div class="col-xs-12 text-right">
                            <span style="font-size:20px;"> Vouchers Approved in KEC </span>
                            <h2 id="voucher">Loading...</h2><br><br>
                        </div>
                    </div>
                </div>
                </a>
            </div>
            <div class="col-lg-4">
                <a href="#">
                    <div class="widget style1 yellow-bg">
                    <div class="row">
                        
                        <div class="col-xs-12 text-right">
                            <span style="font-size:20px;"> Batches with Missing Vouchers </span>
                            <h2 id="vouchers"> 
                            <button class="btn btn-info" onclick="show()" style="font-size: 14px;">View List</button>
                            </h2> 
                           

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
                      <a href="voucher/"  style="float:right; margin-right:20px;"  class="btn btn-primary">CREATE THESE VOUCHERS</a>
                      </div>
                    </div>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                <i class="fa fa-wrench"></i>
                            </a>
                           
                           
                        </div>
                    </div>
                    <div class="ibox-content">
                    <div class="alert alert-success hidden">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <p id="message"></p>
                        </div>
                        <div class="alert alert-danger hidden">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <p id="error"></p>
                        </div>
                        <div class="table-responsive">
                            <table id="createdbatches" style="text-align:left;" class="footable table table-stripped toggle-arrow-tiny">
                                <tr>
                                    <th class="hidden"> ID </th>
                                    <th> BATCH NUMBER </th>
                                    <th> COUNTY NAME </th>
                                    <th> COUNTY CODE</th>
                                    
                                <tr>
                              <tfoot>
                                <tr>
                                   
                                </tr>
                              </tfoot>
                            </table>  
                        </div>

                    </div>
                </div>
        
            </div>
      
        <div class="footer" style="">
            <strong>Copyright</strong> KEC  &copy; 2018
        </div>
        
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

<?php include_once 'scripts/GetCountyCount.php';?>
<?php include_once 'scripts/BatchToList.php';?>
<?php require_once 'scripts/Function_Scripts.php';?>
</body>
</html>
