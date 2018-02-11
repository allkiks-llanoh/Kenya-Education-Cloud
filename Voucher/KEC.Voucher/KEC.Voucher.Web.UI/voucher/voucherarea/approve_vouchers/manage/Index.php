<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>KEC | Voucher Management</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">

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
        <nav class="navbar navbar-static-top" role="navigation" style="margin-bottom: 0">
        <div class="navbar-header">
            <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i> </a>
            
        </div>
            <ul class="nav navbar-top-links navbar-right">
                


                <li>
                    <a href="login.html">
                        <i class="fa fa-sign-out"></i> Log out
                    </a>
                </li>
            </ul>

        </nav>
        </div>
           
        <div class="wrapper wrapper-content animated fadeInRight">
        <div class="form-group">
                                <div class="panel panel-info text-justify"  >
                                    <div class="panel-heading">BATCH INFORMATION</div>
                                    <div class="panel-body">
                                        <?php echo "<h4 id='batchId' data-batch=" . $_GET["batchId"] . "><span><strong>Batch Number:</strong></span> " . $_GET["batchNumber"] . "</h4>"; ?>                                       
                                        <?php echo "<h4 id='schoolTypeId' data-schooltype=". $_GET["schoolTypeId"] . "><span><strong>Shool Type:</strong></span> " . $_GET["schooltype"] . "</h4>"; ?>                                    
                                        <?php echo "<h4><span><strong>County Name:</strong></span> " . $_GET["county"] . "</h4>"; ?>
                                        <a class="button btn btn-w-m btn-primary pull-right" href="../">BACK TO BATCH LIST</a>
                                    </div>
                                    
                                   <div class="panel-footer">

                                      
                                   </div>
                               </div>

        <div class="row">
                            
                            <div class="col-sm-6">
                                <input type="checkbox" class="chk_approve"  name="fancy-checkbox-default" id="fancy-checkbox-default" autocomplete="off" /> 
                                <label for="fancy-checkbox-default" class="[ btn btn-default active ]">SELECT ALL (APPROVAL) </label>
                            </div>
                            <div class="col-sm-6">
                                <div class="[ btn-group ] pull-right" >
                                <button type="button"  name="reject" id="reject" class="btn btn-w-m btn-danger btn-md">REJECT SELECTED</button> 
                                </div>
                                <div class="[ btn-group ] pull-right" >
                               
                                 <button type="button" name="approve"  id="approve" class="btn btn-w-m btn-primary btn-md">APPROVE SELECTED</button> 
                               
                                </div>
                            </div> 
                          
                           
                        </div>
           
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Serch For A School / Voucher</h5>

                           
                        </div>
                        <div class="ibox-content">
                            <input type="text" class="form-control input-sm m-b-xs" id="filter"
                                   placeholder="Search For School / Voucher">

                            <table id="createdvouchers"  class="footable table table-stripped" data-page-size="8" data-filter=#filter>
                               
                                <tr class="footable-sortable">

                                
                               <th style="display:none;"> ORDER </th>
                               <th> SCHOOL NAME </th>
                               <th> VOUCHER AMOUNT</th>
                               <th> APPROVE</th>
                               <th> REJECT</th>
                            
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

    <!-- FooTable -->
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    
</body>
<?php require_once 'scripts/Function_Scripts.php';?>
</html>
