<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>KENYA EDUCATION CLOUD</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet">

</head>

<body>

<div id="wrapper">

    <nav class="navbar-default navbar-static-side" role="navigation">
        <div class="sidebar-collapse">
            <ul class="nav metismenu" id="side-menu">
                <li class="nav-header">
                    <div class="dropdown profile-element">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <span class="clear"> <span class="block m-t-xs"> <strong class="font-bold">Admin</strong>
                             </span> <span class="text-muted text-xs block">KEC Admin <b class="caret"></b></span> </span> </a>
                            <ul class="dropdown-menu animated fadeInRight m-t-xs">
                                <li><a href="#">Logout</a></li>
                            </ul>
                    </div>
                    <div class="logo-element">
                        IN+
                    </div>
                </li>
               
            </ul>

        </div>
    </nav>

    <div id="page-wrapper" class="gray-bg">
        <div class="row border-bottom">
            <nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
                <div class="navbar-header">
                    <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
                    
                </div>
                <ul class="nav navbar-top-links navbar-right">
                    <li>
                        <a href="#">
                            <i class="fa fa-sign-out"></i> Log out
                        </a>
                    </li>
                </ul>

            </nav>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="text-center m-t-lg">
                        <h1>
                           Kenya Education Cloud
                        </h1>
                        <small>
                          You Must Select County and Type of School to Enable Creation of a Batch For The County Selected
                      
                        </small>
                        <br>
                        <br>
                        <div class="alert alert-success hidden">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <p id="alert"></p>
                        </div>
                        <!--Submission Form starts here-->
                           
                        <div class="ibox float-e-margins">
                        <div class="row">
                            <div class="ibox-title">
                            <div col-md-6>
                            <h5>Search For a County To Create a Batch</h5>   
                            </div>  
                            <div col-md-6>
                            <a href="../"  style="float:right; margin-right:20px;"  class="btn btn-primary">BACK TO START</a>
                            </div>              
                        </div>
                        <div>
                        <div class="ibox-content">
                            <input type="text" class="form-control input-sm m-b-xs" id="filter"placeholder="Search For Batch / County">

                            <table id="countylist"  class="footable table table-stripped" data-page-size="20" data-filter=#filter>
                                <tr class="footable-sortable">
                                    <th class="hidden"> ID</th>
                                    <th> COUNTY NAME </th>
                                    <th> COUNTY CODE</th>
                                    <th> ACTIONS</th>
                                </tr>
                                <tfoot>
                                <tr>
                                    <td colspan="5">
                                        <ul class="pagination pull-right"></ul>
                                    </td>
                                </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                     </div>     
                        <!--Submission form ends here-->
                          
                        <!--BEGIN REFRESH-->
                                <script>
                            function myFunction() {
                                location.reload();
                            }
                            </script>

                            <!--END REFRESH-->
                    </div>
                </div>
            </div>
        </div>


  </div>
  <div class="footer">
    <div>
        <strong>Copyright</strong> KICD &copy; 2018
     </div>
</div>
</div>

<!-- Mainly scripts -->
<script src="js/jquery-3.1.1.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
<script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>


<!-- Custom and plugin javascript -->
<script src="js/inspinia.js"></script>
<script src="js/plugins/pace/pace.min.js"></script>

</body>
<!-- Call Ajax Functions To Create Batch -->
<?php require_once 'scripts/getbatch.php';?>
<?php require_once 'scripts/CountyToList.php';?>    
   CountyTo List
</html>
