﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebForm.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <link rel='stylesheet' href='/Scripts/css/style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='/Scripts/css/misc.css' type='text/css' media='all' />
    <link rel="stylesheet" href="/Scripts/css/jquery-ui.css" />
    <link href="/Scripts/lib/datepicker/datepicker.css" rel="stylesheet" />
    <link href="/Scripts/lib/datepicker/datepicker.min.css" rel="stylesheet" />

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
    <!-- MDB core JavaScript-->
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
    <script src="/Scripts/lib/datepicker/datepicker.js"></script>
    <script src="/Scripts/lib/datepicker/datepicker.min.js"></script>
    <script>
        $(function () {
            $("#datepicker").datepicker();
        });

        $.getJSON('https://api.ipify.org?format=jsonp&callback=?', function (data) {
            $('#ip_addr').val(data.ip);
        });
    </script>
    

</head>
<body>
        <!-- HEADER -->
        <header id="masthead" class="site-header" role="banner">
        <div class="masthead">
            <div class="wrapper">
                <button class="mobile-menu-button"><span></span><span></span><span></span></button>
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
                        <a href="https://ticketing.kidzania.com.sg" onclick="floodlightBookTickets();" class="navbar-brand" target="_blank"><img src="/Content/img/btn-book-tickets.png"/></a>
                    </div>
                </div>
            </div>
            <!-- wrapper -->
        </div>
        <!-- masthead -->
        <!-- #site-navigation -->
        </header>
        <!-- HEADER -->

    

    <form id="form" runat="server">
    <div style="width:100%;">
        <div id="page content" style="margin: 0 auto; display: table;text-align:center;">
            <div style="height:160px;"><h1></h1></div>    
            
            <div style="margin-top: 10px;width: 100% /*780px*/; text-align:center;margin:0 auto;" id="control_area">
                <div class="btnmain" style="display: inline-block;">
                    
                    <asp:LinkButton ID="btnPhotoScan" runat="server"  class="btn_default" onclick="PhotoScan">
                        <div style="width:100%;">
                            <div><img src="/Content/img/webcam.png" border="0" style="background-color:transparent;"/></div>
                            <div>Photo Scan</div>
                        </div>
                    </asp:LinkButton> 

                </div>
                <div class="btnmain" style="display: inline-block;">

                    <asp:LinkButton ID="btnPhotoUpload" runat="server"  class="btn_default" onclick="PhotoUpload">
                        <div style="width:100%;">
                            <div><img src="/Content/img/photoupload.png" border="0" style="background-color:transparent;"/></div>
                            <div>Photo Upload</div>
                        </div>
                    </asp:LinkButton> 

                </div>
                <div style="display: none;">

                    <asp:LinkButton ID="btnProfileSearch" runat="server"  class="btn_default" onclick="ProfileSearch">
                        <div style="width:100%;">
                            <div><img src="/Content/img/profile_search.png" border="0" style="background-color:transparent;"/></div>
                            <div>Profile Search</div>
                        </div>
                    </asp:LinkButton> 

                </div>
                <br /><br />
                
                <p>Date of Visit: <input type="text" id="datepicker" runat="server"/></p>
                <p>Accuracy (<span id='accuracy_val'>50%</span>): <br />1%&nbsp;&nbsp;<input oninput="document.getElementById('accuracy_val').innerHTML = this.value+'%';" runat="server" id="Range1" type="range" min="1" max="100" step="1" value="50" data-orientation="horizontal" />&nbsp;&nbsp;100%</p>
            </div>

        </div>
    </div>
    <input id="ip_addr" style="display:none;" runat="server" />
    </form>





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
                        <li class="footer-logo"><a href="#"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/kidzania.png"/></a></li>
                        <li class="footer-location">Palawan Kidz City. 31 Beach View #01-01/02 Singapore 098008.</li>
                        <li class="footer-phone">Local Hotline: 1800 653 6888</li>
                        <li class="footer-phone">International Hotline: +65 6653 6888</li>
                        <li class="footer-mail"><a href="mailto:share@kidzania.com.sg">share@kidzania.com.sg</a></li>
                    </ul>
                </div>
                <ul class="footer-social-media">
                    <li><a href="https://www.facebook.com/KidzaniaSingapore" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-fb.png" title="Facebook" alt="Facebook"/></a></li>
                    <li><a href="https://www.tripadvisor.com.my/Attraction_Review-g294264-d7789437-Reviews-KidZania_Singapore-Sentosa_Island.html" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-tripadvisor.png" title="Trip Advisor" alt="Trip Advisor"/></a></li>
                    <li><a href="https://instagram.com/KidzaniaSingapore" target="_blank"><img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-instagram.png" title="Instagram" alt="Instagram"/></a></li>
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