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
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.0/footable.paging.js">
</head>

<body>

<div id="wrapper">
<!--Left Column Navigation Starts Here -->

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

<!--Left Column Navigation Stops Here -->

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
                        Please Confirm The Allocated Amount Before Updating
                        </small>
                        <br>
                        <br>
                        <div class="alert alert-success hidden">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <p id="message"></p>
                        </div>                        <div class="form-group">
                                <div class="panel panel-info text-justify" id="fundallocation" data-fund=<?php echo $_GET["Id"] ?>>
                                    <div class="panel-heading">FUND ALLOCATION INFORMATION</div>
                                    <div class="panel-body" id="FundPanel">
                                   
                                    
                                    <a class="button btn btn-w-m btn-primary pull-right" href="../">BACK TO LIST</a>
                                    </div>
                                     
                                   <div class="panel-footer" id="FundPanelFooter">

                                      
                                   </div>
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
</div>

<!-- Mainly scripts -->
<script src="js/jquery-3.1.1.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
<script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

<!-- Custom and plugin javascript for Inspinia -->
<script src="js/inspinia.js"></script>

<script src="js/plugins/pace/pace.min.js"></script>
</body>
<?php include_once 'scripts/get.php';?>

</html>



