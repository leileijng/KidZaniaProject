<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="summary.aspx.cs" Inherits="WebForm.summary" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>Summary</title>
    <!-- Onsite -->
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <link rel='stylesheet' href='/Scripts/css/style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='/Scripts/css/misc.css' type='text/css' media='all' />
    <link rel="stylesheet" href="/Scripts/css/jquery-ui.css" />

    <script type="text/javascript" src="/Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <!-- Font Awesome -->

    <link href="/Scripts/lib/font-awesome/css/all.min.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <link href="/Scripts/lib/twitter-bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Material Design Bootstrap -->
    <link href="/Scripts/lib/mdb/css/mdb.min.css" rel="stylesheet" />

    <link href="/Scripts/lib/noty/noty.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Scripts/css/noty_custom.css" />

    <!-- Bootsrap-table core CSS -->
    <link href="/Scripts/lib/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <!-- jqWidget core CSS -->
    <link href="/Scripts/lib/jqwidgets/styles/jqx.base.css" rel="stylesheet" />
    <link href="/Scripts/lib/jqwidgets/styles/jqx.flat.css" rel="stylesheet" />
    <!-- jQuery library-->
    <script src="/Scripts/lib/jquery/dist/jquery.js"></script>

    <!-- Bootstrap tooltips -->
    <script src="/Scripts/lib/popper.js/umd/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <script src="/Scripts/lib/twitter-bootstrap/js/bootstrap.min.js"></script>
    <!-- MDB core JavaScript -->
    <script src="/Scripts/lib/mdb/js/mdb.min.js"></script>
    <!-- jqWidgets core JavaScript for all widgets (large JS file size)-->
    <script src="/Scripts/lib/jqwidgets/jqx-all.js"></script>
    <!-- jQuery validate plugin-->
    <script src="/Scripts/lib/jquery-validation/dist/jquery.validate.js"></script>
    <script src="/Scripts/lib/jquery-validation/dist/additional-methods.js"></script>
    <!-- Boostrap table core JavaScript -->
    <script src="/Scripts/lib/bootstrap-table/bootstrap-table.min.js"></script>
    <script src="/Scripts/lib/noty/noty.min.js"></script>
    <script src="/Scripts/lib/moment/moment.min.js"></script>
    <script src="/Scripts/lib/store/store.min.js"></script>
    <script>

        jQuery(document).ready(function ($) {
            if (window.history && window.history.pushState) {

                $(window).on('popstate', function () {
                    var hashLocation = location.hash;
                    var hashSplit = hashLocation.split("#!/");
                    var hashName = hashSplit[1];

                    if (hashName !== '') {
                        var hash = window.location.hash;
                        if (hash === '') {
                            window.location.href = '/selection.aspx';
                        }

                        window.history.pushState(null, null, window.location.pathname);
                    }
                });

                window.history.pushState(null, null, window.location.pathname);
            }
        });



        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }

        function validateOrder() {
            if ($("#useremail").length) {
                console.log("digital copy here!");
                var useremail = $("#useremail").val();
                var pid = $('#pidSession').text();
                if (isEmail(useremail)) {
                    $("#email_status").html("");
                    var dataValue = JSON.stringify({ "email": useremail, "pid": pid});
                    console.log(dataValue);
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("summary.aspx/insertEmail") %>',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: dataValue,
                        success: function () {
                            console.log("Inserted Successfully");
                            return true;
                        },
                        error: function () {
                            console.log("not successfully");
                            return false;
                        }
                    });
                }
                else {
                    $("#email_status").html("Invalid email");
                    return false;
                }
            }
            else {
                return true;
            }
        }
        function mobileMenu() {
            document.getElementById("mobile-primary-menu").classList.toggle("show");
        }
    </script>
    
    <style>
		.btn_snap {
			-moz-box-shadow: 0px 10px 14px -7px #276873;
			-webkit-box-shadow: 0px 10px 14px -7px #276873;
			box-shadow: 0px 10px 14px -7px #276873;
			background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #599bb3), color-stop(1, #408c99));
			background:-moz-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-webkit-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-o-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-ms-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:linear-gradient(to bottom, #599bb3 5%, #408c99 100%);
			filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#599bb3', endColorstr='#408c99',GradientType=0);
			background-color:#599bb3;
			-moz-border-radius:8px;
			-webkit-border-radius:8px;
			border-radius:8px;
			display:inline-block;
			cursor:pointer;
			color:#ffffff;
			font-family:Arial;
			font-size:20px;
			font-weight:bold;
			padding:13px 32px;
			text-decoration:none;
			text-shadow:0px 1px 0px #3d768a;
		}
		.btn_snap:hover {
			background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #408c99), color-stop(1, #599bb3));
			background:-moz-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-webkit-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-o-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-ms-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:linear-gradient(to bottom, #408c99 5%, #599bb3 100%);
			filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#408c99', endColorstr='#599bb3',GradientType=0);
			background-color:#408c99;
		}

		.btn_default {
			-moz-box-shadow: 0px 10px 14px -7px #276873;
			-webkit-box-shadow: 0px 10px 14px -7px #276873;
			box-shadow: 0px 10px 14px -7px #276873;
			background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #599bb3), color-stop(1, #408c99));
			background:-moz-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-webkit-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-o-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:-ms-linear-gradient(top, #599bb3 5%, #408c99 100%);
			background:linear-gradient(to bottom, #599bb3 5%, #408c99 100%);
			filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#599bb3', endColorstr='#408c99',GradientType=0);
			background-color:#599bb3;
			-moz-border-radius:8px;
			-webkit-border-radius:8px;
			border-radius:8px;
			display:inline-block;
			cursor:pointer;
			color:#ffffff;
			font-family:Arial;
			font-size:20px;
			font-weight:bold;
			padding:13px 32px;
			text-decoration:none;
			text-shadow:0px 1px 0px #3d768a;
		}
		.btn_default:hover {
			background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #408c99), color-stop(1, #599bb3));
			background:-moz-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-webkit-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-o-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:-ms-linear-gradient(top, #408c99 5%, #599bb3 100%);
			background:linear-gradient(to bottom, #408c99 5%, #599bb3 100%);
			filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#408c99', endColorstr='#599bb3',GradientType=0);
			background-color:#408c99;
		}
    
        .heading1 { font-size: 30px; font-weight: bold;}

        .hr1 {
            height: 12px;
            border: 0;
            box-shadow: inset 0 12px 12px -12px rgba(0, 0, 0, 0.5);
        }

        div.gallery {
            margin: 5px;
            border: 1px solid #ccc;
            /*float: left;*/
            display: inline-block;
            width: 200px;
        }

        div.gallery:hover {
            border: 1px solid #777;
        }

        div.gallery img {
            width: 100%;
            height: auto;
        }

        div.desc {
            padding: 15px;
            text-align: center;
        }

				

		/* Style the Image Used to Trigger the Modal */
		.thumbnail:hover {opacity: 0.7;}

		/* The Modal (background) */
		.modal {
			display: none; /* Hidden by default */
			position: fixed; /* Stay in place */
			z-index: 9999; /* Sit on top */
			padding-top: 100px; /* Location of the box */
			left: 0;
			top: 0;
			width: 100%; /* Full width */
			height: 100%; /* Full height */
			overflow: auto; /* Enable scroll if needed */
			background-color: rgb(0,0,0); /* Fallback color */
			background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
		}

		/* Modal Content (Image) */
		.modal-content {
			margin: auto;
			display: block;
			width: auto;
			max-width: 700px;
		}

		/* Caption of Modal Image (Image Text) - Same Width as the Image */
		#caption {
			margin: auto;
			display: block;
			width: 80%;
			max-width: 700px;
			text-align: center;
			color: #ccc;
			padding: 10px 0;
			height: 150px;
		}

		/* Add Animation - Zoom in the Modal */
		.modal-content, #caption { 
			-webkit-animation-name: zoom;
			-webkit-animation-duration: 0.6s;
			animation-name: zoom;
			animation-duration: 0.6s;
		}

		@-webkit-keyframes zoom {
			from {-webkit-transform:scale(0)} 
			to {-webkit-transform:scale(1)}
		}

		@keyframes zoom {
			from {transform:scale(0)} 
			to {transform:scale(1)}
		}

		/* The Close Button */
		.close {
			position: absolute;
			top: 15px;
			right: 35px;
			color: #f1f1f1;
			font-size: 40px;
			font-weight: bold;
			transition: 0.3s;
		}

		.close:hover,
		.close:focus {
			color: #bbb;
			text-decoration: none;
			cursor: pointer;
		}

		/* 100% Image Width on Smaller Screens */
		@media only screen and (max-width: 700px){
			.modal-content {
				width: 100%;
			}
		}

		.fix_corner { 
			position:fixed; 
			top:2%; 
			right:2%; 
			background: #e0f538;
			width: 200px;
		   /* height: 80px;*/
			border-radius: 7px;
			-webkit-border-radius: 7px;
			-moz-border-radius: 7px;
			box-shadow: 2px 2px 3px #666666;
			-webkit-box-shadow: 2px 2px 3px #666666;
			-moz-box-shadow: 2px 2px 3px #666666;
		}
		.fix_corner_ctn {
			margin:  12px auto;
		}
		.fix_corner_price {
			text-align:left;
			margin-left:20px;
		}
		.fix_corner_price_title{
			font-weight: bold;
			text-align:left;
			margin-left:10px;
		}
		.fix_corner_price_annotation{
			text-align:left;
			font-size:12px;
		}
        .column{
            padding: 1px 5px;
        }
        .row{
            margin: 1px;
            text-align: center;
        }
    </style>


</head>
<body>
    <!-- HEADER -->
    <header id="masthead" class="site-header" role="banner">
    <div class="masthead">
        <div class="wrapper">
            <button onclick="mobileMenu()" class="mobile-menu-button"><span></span><span></span><span></span></button>
            <div class="menu-quicklinks-container">
                <ul id="menu-quicklinks" class="menu">
                    <li id="menu-item-452" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home current-menu-item page_item page-item-206 current_page_item menu-item-452"><a href="https://kidzania.com.sg/">Homepage</a></li>
                    <li id="menu-item-1282" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-1282"><a href="https://kidzania.com.sg/faq/">FAQs</a></li>
                    <li id="menu-item-1544" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-1544"><a href="https://kidzania.com.sg/our-partners/">Our Partners</a></li>
                    <li id="menu-item-2237" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2237"><a target="_blank" href="/M-Login/">Media</a></li>
                    <li id="menu-item-1760" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-1760"><a href="/contact-us/">Contact Us</a></li>
                </ul>
            </div>
            <div class="site-branding">
                <a href="https://kidzania.com.sg/" rel="home"><img src="/Content/img/kidzania.png" title="KidZania Singapore – A City Built for Kids!"/></a>
            </div>
            <!-- .site-branding -->
            <div class="header-right">
                <div class="btn-book-tickets">
                    <a href="https://ticketing.kidzania.com.sg" onclick="floodlightBookTickets();" class="navbar-brand" target="_blank"><img src="/Content/img/btn-book-tickets.png" /></a>
                </div>
            </div>
        </div>
        <!-- wrapper -->
    </div>
    <!-- masthead -->
    <!-- #site-navigation -->
         <div id="mobile-primary-menu" class="mobile-menu">
            <ul id="mobile-quicklink-menu" class="menu">
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home current-menu-item page_item page-item-206 current_page_item menu-item-452"><a href="https://kidzania.com.sg/">Homepage</a></li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-1282"><a href="https://kidzania.com.sg/faq/">FAQs</a></li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-1544"><a href="https://kidzania.com.sg/our-partners/">Our Partners</a></li>
                <li class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2237"><a target="_blank" href="/M-Login/">Media</a></li>
                <li class="menu-item menu-item-type-custom menu-item-object-custom menu-item-1760"><a href="/contact-us/">Contact Us</a></li>
            </ul>
            <div class="mobile-book-tickets">
                <a href="https://ticketing.kidzania.com.sg" onclick="floodlightBookTickets();" class="navbar-brand" target="_blank">
                    <img src="/Content/img/btn-book-tickets.png" /></a>
            </div>
        </div>
    </header>
    <!-- HEADER -->


    <form id="form1">
    <div id="page content" style="margin:0 auto; display: table;text-align:center;">
        <div style="width: 100%; height: 150px; text-align:center;margin:0 auto;" id="heading">
            <span class="heading1">Details</span>
        </div>
        <div style="text-align:center;margin-top: 10px;margin-bottom: 30px;"><img src="Content/img/process-details.png" /></div>
        <div style="text-align:center;margin:0 auto;">
            <table class="table table-hover table-bordered">
                <thead class="thead-light">
                    <tr>
                        <th>Product</th>
                        <th>Photos</th>
                        <th>Quantity</th>
                        <th>Cost</th>
                    </tr>
                </thead>
                <tbody id="summarydiv" runat="server">
                </tbody>
            </table>
        </div>
    </div>

    <div style="margin-top:-10px; width: 90%; text-align:center;margin:0 auto;" id="photo_gallery">
        <div style="text-align:center;" id="photo_gallery_ctn" runat="server"></div>
    </div>

    </form>
    <br />

    <table>
        <tr>
            <td>
                <form action="payment_onsite.aspx" method="post" runat="server" onsubmit = "return validateOrder();">
                    <div style="margin-top: 30px; width: 90%; text-align: center; margin: 0 auto;" id="payment_onsite_summary">
                        <input type="hidden" id="Hidden1" value="0" runat="server" />
                        <input type="hidden" id="sa" value="0" runat="server" />
                        <div id="email_ctn" style='margin-bottom: 15px;' runat="server">
                        </div>
                        <div>
                            <input style='margin-bottom: 10px;' class='btn_default' type="submit" value="Onsite Payment" />
                        </div>
                    </div>
                </form>
            </td>
        </tr>
    </table>
    
    <!-- FOOTER -->
    <footer id="colophon" class="site-footer" role="contentinfo">
        <div class="footer-content">
            <div class="wrapper">

                <div class="footer-widgets widget-area clear">
                    <div id="nav_menu-2" class="widget widget_nav_menu">
                        <h4>What Is KidZania?</h4>
                        <div class="menu-what-is-kidzania-container">
                            <ul id="menu-what-is-kidzania" class="menu">
                                <li id="menu-item-43" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-43"><a href="https://kidzania.com.sg/the-concept/">The Concept</a></li>
                                <li id="menu-item-78" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-78"><a href="https://kidzania.com.sg/our-story/">Our Story</a></li>
                                <li id="menu-item-103" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-103"><a href="https://kidzania.com.sg/kids-activities/">Kids Activities</a></li>
                                <li id="menu-item-70" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-70"><a href="https://kidzania.com.sg/our-economy/">Our Economy</a></li>
                                <li id="menu-item-71" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-71"><a href="https://kidzania.com.sg/security/">Security</a></li>
                                <li id="menu-item-66" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-66"><a href="https://kidzania.com.sg/accessibility/">Accessibility</a></li>
                                <li id="menu-item-72" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-72"><a target="_blank" href="http://www.kidzania.com">KidZania In The World</a></li>
                                <li id="menu-item-73" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-73"><a target="_blank" href="https://kzjournal.kidzania.com/">KZ Journal</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="nav_menu-4" class="widget widget_nav_menu">
                        <h4>About KidZania Singapore</h4>
                        <div class="menu-about-kidzania-container">
                            <ul id="menu-about-kidzania" class="menu">
                                <li id="menu-item-3812" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-3812"><a href="https://kidzania.com.sg/annual-pass/">Annual Pass</a></li>
                                <li id="menu-item-3973" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-3973"><a href="https://kidzania.com.sg/corporate-annual-pass/">Corporate Annual Pass</a></li>
                                <li id="menu-item-3371" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-3371"><a href="https://kidzania.com.sg/b-kidzanian/">B-KidZanian</a></li>
                                <li id="menu-item-498" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-498"><a href="https://kidzania.com.sg/our-partners/">Our Partners</a></li>
                                <li id="menu-item-2044" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2044"><a target="_blank" href="/J-Portal/">Careers</a></li>
                                <li id="menu-item-966" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-966"><a href="https://kidzania.com.sg/image-gallery/">Image Gallery</a></li>
                                <li id="menu-item-2141" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2141"><a target="_blank" href="/M-Login/">Media</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="nav_menu-3" class="widget widget_nav_menu">
                        <h4>Plan Your Visit</h4>
                        <div class="menu-plan-your-visit-container">
                            <ul id="menu-plan-your-visit" class="menu">
                                <li id="menu-item-201" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-201"><a href="https://kidzania.com.sg/opening-times-services/">Opening Times &amp; Services</a></li>
                                <li id="menu-item-200" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-200"><a href="https://kidzania.com.sg/ticket-prices/">Ticket Prices</a></li>
                                <li id="menu-item-252" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-252"><a href="https://kidzania.com.sg/how-to-get-here/">How To Get Here</a></li>
                                <li id="menu-item-3455" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-3455"><a href="/events/category/events/">Events</a></li>
                                <li id="menu-item-3456" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-3456"><a href="/events/category/promotions/">Promotions</a></li>
                                <li id="menu-item-2637" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-2637"><a href="https://kidzania.com.sg/visitors-guide-to-kidzania-singapore/">Visitors’ Guide to KidZania Singapore</a></li>
                                <li id="menu-item-2143" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2143"><a target="_blank" href="/s-login">School Visits</a></li>
                                <li id="menu-item-330" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-330"><a href="https://kidzania.com.sg/corporate-events/">Corporate Events</a></li>
                                <li id="menu-item-251" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-251"><a href="https://kidzania.com.sg/birthday-parties/">Birthday Parties</a></li>
                                <li id="menu-item-2147" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-2147"><a target="_blank" href="/t-login">Travel Trade</a></li>
                                <li id="menu-item-1954" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-1954"><a href="https://kidzania.com.sg/group-purchase/">Group Purchase</a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="nav_menu-5" class="widget widget_nav_menu">
                        <h4>Learn With KidZania</h4>
                        <div class="menu-learn-with-kidzania-container">
                            <ul id="menu-learn-with-kidzania" class="menu">
                                <li id="menu-item-3871" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-3871"><a href="https://kidzania.com.sg/learn-through-play/">Learn Through Play</a></li>
                                <li id="menu-item-270" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-270"><a href="https://kidzania.com.sg/role-play/">Role-Play</a></li>
                                <li id="menu-item-294" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-294"><a href="https://kidzania.com.sg/educational-value/">Educational Value</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- #footer-sidebar -->
                <div class="footer-contact">
                    <ul>
                        <li class="footer-logo"><a href="#"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/kidzania.png"></a></li>
                        <li class="footer-location">Palawan Kidz City. 31 Beach View #01-01/02 Singapore 098008.</li>
                        <li class="footer-phone">Local Hotline: 1800 653 6888</li>
                        <li class="footer-phone">International Hotline: +65 6653 6888</li>
                        <li class="footer-mail"><a href="mailto:share@kidzania.com.sg">share@kidzania.com.sg</a></li>
                    </ul>
                </div>
                <ul class="footer-social-media">
                    <li><a href="https://www.facebook.com/KidzaniaSingapore" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-fb.png" title="Facebook" alt="Facebook"></a></li>
                    <li><a href="https://www.tripadvisor.com.my/Attraction_Review-g294264-d7789437-Reviews-KidZania_Singapore-Sentosa_Island.html" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-tripadvisor.png" title="Trip Advisor" alt="Trip Advisor"></a></li>
                    <li><a href="https://instagram.com/KidzaniaSingapore" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-instagram.png" title="Instagram" alt="Instagram"></a></li>
                </ul>
            </div>
        </div>

        <div class="site-info wrapper">
            <div id="text-2" class="widget widget_text">
                <div class="textwidget">
                    <p>Rakan Riang Pte Ltd (UEN: 201022374H) is the Authorised Licensee of KidZania, S.A.P.I. de C.V. and a joint venture company between Themed Attractions and Resorts Sdn Bhd (860668-B) and Boustead Curve Sdn Bhd (103320-B), of Malaysia.</p>
                    <p><a href="/enewsletter-form/">Mailing List</a>|&nbsp;<a href="/M-Login/ " target="_blank" rel="noopener">Media</a>|&nbsp;<a href="/privacy-policy/">Privacy Policy</a>|&nbsp;<a href=" https://ticketing.kidzania.com.sg/terms-website/">Website Terms</a></p>
                </div>
            </div>
        </div>
        <!-- #site-info -->
        <!-- .site-info -->
    </footer>
   
    <!-- FOOTER -->
</body>
</html>
