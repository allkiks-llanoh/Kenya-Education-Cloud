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
    <style>
            .loader {
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 120px;
            height: 120px;
            -webkit-animation: spin 2s linear infinite; /* Safari */
            animation: spin 2s linear infinite;
            }

            /* Safari */
            @-webkit-keyframes spin {
            0% { -webkit-transform: rotate(0deg); }
            100% { -webkit-transform: rotate(360deg); }
            }

            @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
            }
</style>

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
                <li class="active">
                    <a href="#"><i class="fa fa-th-large"></i> <span class="nav-label">Create Schools</span></a>
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
                           Upload CSV as Presented To Register School, If You get Any Probles Contact Administrator with the Specific Error
                        </small>
                        <br>
                        <br>
                        <h3>Select and Upload The CSV File</h3>
                        <div class="alert alert-success hidden">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <p id="message"></p>
                        </div> 
                        <div class="form-group"  align="center" id="spinner">
                        <div class="loader"></div>
                     
                        </div>

                         <!--Uploding csv code starts here-->
                        <form method="post" enctype="multipart/form-data" id="uploadform">
                            <div class="row">
                                <div class="col-sm-6">
                                 
                                    <input type="file" name="postedFile" id="postedFile" class="btn btn-w-m btn-info" ><br><br>
                                    </div>
                                <div class="col-sm-6">
                                    <div class="[ btn-group ]">
                                    <button type="submit" id="btn-postFile" class="btn btn-w-m btn-primary">UPLOAD FILE</button>                               
                                </div>
                             </div>    
                        </form>
                        <!--Uploding csv code ends here-->

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
</div

<!-- Mainly scripts -->
<script src="js/jquery-3.1.1.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
<script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

<!-- Custom and plugin javascript -->
<script src="js/inspinia.js"></script>


<?php include_once "scripts/Function_Scripts.php"?>
</body>

</html>
