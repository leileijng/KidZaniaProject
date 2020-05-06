<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selection.aspx.cs" Inherits="PhotoBoothPortal.Views.Test.selection" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
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
    </style>
    <style>
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
            top:25%; 
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

        .item_checkbox_grp { text-align: left !important; margin-left: 15px; margin-top: -10px;}
        .modal_window { cursor: pointer; }
        .pricing_heading { text-align: center; font-size: large; }
        .pricing_table { margin: 0 auto; width: 80%; background-color: #e0f538; border-radius: 7px; -webkit-border-radius 7px; -moz-border-radius: 7px; box-shadow: 2px 2px 3px #666666; -webkit-box-shadow: 2px 2px 3px #666666; -moz-box-shadow: 2px 2px 3px #666666;}
        .pricing_subheader { margin-left: 10px; text-align: left !important; font-weight: bold;}
        
        .pricing_grp { margin-top: 0px; margin-left: 15px; text-align: left !important;}
        .pricing_item { display: inline-block; margin-left: 15px; vertical-align: top;}
        .pricing_header { font-weight: bold; margin-left: 5px; }
        .pricing { margin-left: 10px; }
        .pricing_header_sample { font-weight: bold; margin-left: 5px; cursor: pointer; color: blue; text-decoration: underline;}
        
    </style>


    <link rel='stylesheet'  href='/css/style.css' type='text/css' media='all' />
    <link rel='stylesheet'  href='/css/misc.css' type='text/css' media='all' />
    <link rel="stylesheet" href="/css/jquery-ui.css"/>
    <script type="text/javascript" src="/js/jquery.min.js"></script>
    <script>
		$(window).on("load", function () {
            refresh_values();
        });

        $(document).ready(function () {
            $("form").submit(function (e) {
                var total = $("#photo_gallery input[name='digital_cb']:checked").length;
                total += $("#photo_gallery input[name='ECcopy_cb']:checked").length;
                total += $("#photo_gallery input[name='A5copy_cb']:checked").length;
                total += $("#photo_gallery input[name='MGcopy_cb']:checked").length;
                total += $("#photo_gallery input[name='KCcopy_cb']:checked").length;
                if (total == 0)
                    e.preventDefault(); //prevent from submitting
            });

            $("select").change(function () {
                refresh_values();
            });

            function cal_amt(dclen, eclen, a5len, mglen, kclen) {
                var dc_amt = 0; var ec_amt = 0; var a5_amt = 0; var mg_amt = 0; var kc_amt = 0;
				var dc_result = "$0;"; var ec_result = "$0;"; var a5_result = "$0;"; var mg_result = "$0;"; var kc_result = "$0;";
				
				
				var rebate = false;
				var rebate_type = "";
				totalamt = 0;
                if (dclen > 0) 
				{
					rebate = true;
					totalamt = 20;
					rebate_type = ";dc";
					dc_result = "$20;";
				}
				else if (eclen > 0)
				{
					rebate = true;
					totalamt = 25;
					eclen = eclen - 1;
					rebate_type = ";ec";
					ec_result = "$25;";
                }
				else if (a5len > 0)
				{
					rebate = true;
					totalamt = 25;
					//a5len = a5len - 1;
					rebate_type = ";a5";
					a5_result = "$25;";
				}
				else if (kclen > 0)
				{
					rebate = true;
					totalamt = 25;
					kclen = kclen - 1;
					rebate_type = ";kc";
					kc_result = "$25;";
				}
				else if (mglen > 0)
				{
					rebate = true;
					totalamt = 30;
					mglen = mglen - 1;
					rebate_type = ";mg";
					mg_result = "$30;";
				}
				
				
                /*Establishment card*/
                if (eclen > 0)
				{
					if (rebate)
						ec_amt = 8 * eclen;
					else
						ec_amt = 25 * eclen;
						
					if (rebate_type == ";ec")
						ec_result = "$25 + <b>$" + ec_amt + "</b>;";
					else
						ec_result = "<b>$" + ec_amt + "</b>;";
				}
                    
                /*A5Photo card*/
                if (a5len > 0) {
					if (a5len >= 3)
					{
						var mul_val = Math.floor(a5len / 3);
						var mod_val = a5len % 3;
						if (rebate)
						{
							a5_amt = 10 * mod_val;
							a5_amt = a5_amt + (mul_val * 20);
						}
						if (rebate_type == ";a5")
							a5_result = "$25 + <b>$" + a5_amt + "</b>;";
						else
						{
							a5_result = "<b>$" + a5_amt + "</b>;";
						}
						
					}
					else {
						if (rebate_type == ";a5")
						{
							if (a5len == 1)
							{
								//a5_amt = 10 * a5len;
								a5_result = "$25;";
							}
							if (a5len == 2)      //total = 2
							{
								a5_amt = 10 * (a5len - 1);
								a5_result = "$25 + <b>$" + a5_amt + "</b>;";
							}
						}
						else
						{
								a5_amt = a5len * 10;
								if (rebate_type == ";a5")
									a5_result = "$25 + <b>$" + a5_amt + "</b>;";
								else
									a5_result = "<b>$" + a5_amt + "</b>;";	
						}
													
						
					}
					
                }

                /*Magnet card*/
                if (mglen > 0) {
					if (rebate)
						mg_amt = 8 * mglen;
					else
						mg_amt = 30 * mglen;

					if (rebate_type == ";mg")
						mg_result = "$30 + <b>$" + mg_amt + "</b>;";
					else
						mg_result = "<b>$" + mg_amt + "</b>;";
                }

                /*Keychain*/
                if (kclen > 0) {
					if (rebate)
						kc_amt = 8 * kclen;
					else
						kc_amt = 25 * kclen;
						
					if (rebate_type == ";kc")
						kc_result = "$25 + <b>$" + kc_amt + "</b>;";
					else
						kc_result = "<b>$" + kc_amt + "</b>;";	
                }

                
                var totalamt = totalamt + ec_amt + a5_amt + mg_amt + kc_amt;
                
				var output = dc_result  + ec_result + a5_result + mg_result + kc_result + totalamt;
				
                return output;
            }
            
			
			
            refresh_values();    
        });

        function cal_amt(dclen, eclen, a5len, mglen, kclen) {
                var dc_amt = 0; var ec_amt = 0; var a5_amt = 0; var mg_amt = 0; var kc_amt = 0;
				var dc_result = "$0;"; var ec_result = "$0;"; var a5_result = "$0;"; var mg_result = "$0;"; var kc_result = "$0;";
				
				
				var rebate = false;
				var rebate_type = "";
				totalamt = 0;
                if (dclen > 0) 
				{
					rebate = true;
					totalamt = 20;
					rebate_type = ";dc";
					dc_result = "$20;";
				}
				else if (eclen > 0)
				{
					rebate = true;
					totalamt = 25;
					eclen = eclen - 1;
					rebate_type = ";ec";
					ec_result = "$25;";
                }
   				else if (a5len > 0)
				{
					rebate = true;
					totalamt = 25;
					//a5len = a5len - 1;
					rebate_type = ";a5";
					a5_result = "$25;";
				}
				else if (kclen > 0)
				{
					rebate = true;
					totalamt = 25;
					kclen = kclen - 1;
					rebate_type = ";kc";
					kc_result = "$25;";
				}
				else if (mglen > 0)
				{
					rebate = true;
					totalamt = 30;
					mglen = mglen - 1;
					rebate_type = ";mg";
					mg_result = "$30;";
				}
				
				
                /*Establishment card*/
                if (eclen > 0)
				{
					if (rebate)
						ec_amt = 8 * eclen;
					else
						ec_amt = 25 * eclen;
						
					if (rebate_type == ";ec")
						ec_result = "$25 + <b>$" + ec_amt + "</b>;";
					else
						ec_result = "<b>$" + ec_amt + "</b>;";
				}
                    
                /*A5Photo card*/
                if (a5len > 0) {
					if (a5len >= 3)
					{
						var mul_val = Math.floor(a5len / 3);
						var mod_val = a5len % 3;
						if (rebate)
						{
							a5_amt = 10 * mod_val;
							a5_amt = a5_amt + (mul_val * 20);
						}
						if (rebate_type == ";a5")
							a5_result = "$25 + <b>$" + a5_amt + "</b>;";
						else
						{
							a5_result = "<b>$" + a5_amt + "</b>;";
						}
						
					}
					else {
						if (rebate_type == ";a5")
						{
							if (a5len == 1)
							{
								//a5_amt = 10 * a5len;
								a5_result = "$25;";
							}
							if (a5len == 2)      //total = 2
							{
								a5_amt = 10 * (a5len - 1);
								a5_result = "$25 + <b>$" + a5_amt + "</b>;";
							}
						}
						else
						{
								a5_amt = a5len * 10;
								if (rebate_type == ";a5")
									a5_result = "$25 + <b>$" + a5_amt + "</b>;";
								else
									a5_result = "<b>$" + a5_amt + "</b>;";	
						}
													
						
					}
					
                }

                /*Magnet card*/
                if (mglen > 0) {
					if (rebate)
						mg_amt = 8 * mglen;
					else
						mg_amt = 30 * mglen;

					if (rebate_type == ";mg")
						mg_result = "$30 + <b>$" + mg_amt + "</b>;";
					else
						mg_result = "<b>$" + mg_amt + "</b>;";
                }

                /*Keychain*/
                if (kclen > 0) {
					if (rebate)
						kc_amt = 8 * kclen;
					else
						kc_amt = 25 * kclen;
						
					if (rebate_type == ";kc")
						kc_result = "$25 + <b>$" + kc_amt + "</b>;";
					else
						kc_result = "<b>$" + kc_amt + "</b>;";	
                }

                
                var totalamt = totalamt + ec_amt + a5_amt + mg_amt + kc_amt;
				
				var output = dc_result  + ec_result + a5_result + mg_result + kc_result + totalamt;
				
                return output;
            }
        function refresh_values() {
            var lrlen = "";
            $("select option:selected").each(function() {
                lrlen += $(this).text() + " ";
            });
            var dclen = $("#photo_gallery input[name='digital_cb']:checked").length;
            var eclen = $("#photo_gallery input[name='ECcopy_cb']:checked").length;
            var a5len = $("#photo_gallery input[name='A5copy_cb']:checked").length;
            var mglen = $("#photo_gallery input[name='MGcopy_cb']:checked").length;
            var kclen = $("#photo_gallery input[name='KCcopy_cb']:checked").length;

			var totalamt_a = cal_amt(dclen, eclen, a5len, mglen, kclen);
			var totalamt_array = totalamt_a.split(';');
			var totalamt = totalamt_array[5];
			var dc_result = totalamt_array[0];
			var ec_result = totalamt_array[1];
			var a5_result = totalamt_array[2];
			var mg_result = totalamt_array[3];
			var kc_result = totalamt_array[4];


			$("#digital_copy_amt").html("Digital Copy: " + dc_result);
			$("#ec_copy_amt").html("Establishment Card: " + ec_result);
			$("#a5_copy_amt").html("A5 hardcopy: " + a5_result);
			$("#mg_copy_amt").html("Magnet: " + mg_result);
			$("#kc_copy_amt").html("Keychain: " + kc_result);
			
			var calculated_total = totalamt_array[5];
            if (lrlen > 0) {
				var lr_amt = lrlen * 15;
				total = parseInt(lr_amt) + parseInt(calculated_total);
                $("#lr_copy_amt").html("Leatherette: $" + lr_amt);
                $("#lr").attr('value', lrlen);
                $("#sa").attr('value', total);
                $("#Total_cost").html("$" + total + " SGD");
            }
            else {
                total = calculated_total;
                $("#lr_copy_amt").html("Leatherette: $0");
                $("#lr").attr('value', lrlen);
                $("#sa").attr('value', total);
                $("#Total_cost").html("$" + total + " SGD");
            }
			
			$("#dc").attr('value', dclen);
			$("#a5").attr('value', a5len);
			$("#ec").attr('value', eclen);
			$("#mg").attr('value', mglen);
			$("#kc").attr('value', kclen);
			$("#sa").attr('value', total);
        }

        function show_modal(e) {
            var modal = document.getElementById('myModal');
            var modalImg = document.getElementById("img01");
            var captionText = document.getElementById("caption");
            modal.style.display = "block";
            modalImg.src = $(e).attr("src");
        }

        function delete_gl(id)
	    {
            if (confirm('Are you confirmed to remove photo?')) {
                $("#gl_id_" + id).remove();
                refresh_values();
            } else {
                
            }
        }

        function modal_load() {
            var modal = document.getElementById('myModal');
            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }

            
            $("#menu_select input:checkbox").on("change", function () {
                var dclen = $("#photo_gallery input[name='digital_cb']:checked").length;

                //Select one digital will select all digital
                if (dclen == 0) {
                    $("#photo_gallery input[name='digital_cb']").each(function () {
                        $(this).prop('checked', true);
                    })
                }
                else if (dclen > 1) {
                        $("#photo_gallery input[name='digital_cb']").each(function () {
                            $(this).prop('checked', false);
                    })
                }
               
            });

            $("#photo_gallery input:checkbox").on("change", function () {
                var dclen = $("#photo_gallery input[name='digital_cb']:checked").length;
                var eclen = $("#photo_gallery input[name='ECcopy_cb']:checked").length;
                var a5len = $("#photo_gallery input[name='A5copy_cb']:checked").length;
                var mglen = $("#photo_gallery input[name='MGcopy_cb']:checked").length;
                var kclen = $("#photo_gallery input[name='KCcopy_cb']:checked").length;

                var lrlen = 0;
                $("select option:selected").each(function() {
                    lrlen = $(this).text() + " ";
                });

				var totalamt_a = cal_amt(dclen, eclen, a5len, mglen, kclen);
				var totalamt_array = totalamt_a.split(';');
				var totalamt = totalamt_array[5];
				var dc_result = totalamt_array[0];
				var ec_result = totalamt_array[1];
				var a5_result = totalamt_array[2];
				var mg_result = totalamt_array[3];
				var kc_result = totalamt_array[4];
				
                if (lrlen > 0) {
                    var lr_amt = lrlen * 15;
                    totalamt = parseInt(lr_amt) + parseInt(totalamt);
                }

				$("#digital_copy_amt").html("Digital Copy: " + dc_result);
                $("#ec_copy_amt").html("Establishment Card: " + ec_result);
                $("#a5_copy_amt").html("A5 hardcopy: " + a5_result);
                $("#mg_copy_amt").html("Magnet: " + mg_result);
                $("#kc_copy_amt").html("Keychain: " + kc_result);
				
                $("#Total_cost").html("$" + totalamt + " SGD");
				$("#dc").attr('value', dclen);
				$("#a5").attr('value', a5len);
                $("#ec").attr('value', eclen);
                $("#mg").attr('value', mglen);
                $("#kc").attr('value', kclen);
                $("#sa").attr('value', totalamt);
            });
                var dc_amt = 0; var ec_amt = 0; var a5_amt = 0; var mg_amt = 0; var kc_amt = 0;
				var dc_result = "$0;"; var ec_result = "$0;"; var a5_result = "$0;"; var mg_result = "$0;"; var kc_result = "$0;";
				
				
				var rebate = false;
				var rebate_type = "";
				totalamt = 0;
                if (dclen > 0) 
				{
					rebate = true;
					totalamt = 20;
					rebate_type = ";dc";
					dc_result = "$20;";
				}
				else if (eclen > 0)
				{
					rebate = true;
					totalamt = 25;
					eclen = eclen - 1;
					rebate_type = ";ec";
					ec_result = "$25;";
                }
                else if (a5len > 0)
				{
					rebate = true;
					totalamt = 25;
					//a5len = a5len - 1;
					rebate_type = ";a5";
					a5_result = "$25;";
				}
				else if (kclen > 0)
				{
					rebate = true;
					totalamt = 25;
					kclen = kclen - 1;
					rebate_type = ";kc";
					kc_result = "$25;";
				}
				else if (mglen > 0)
				{
					rebate = true;
					totalamt = 30;
					mglen = mglen - 1;
					rebate_type = ";mg";
					mg_result = "$30;";
				}
				
				
                /*Establishment card*/
                if (eclen > 0)
				{
					if (rebate)
						ec_amt = 8 * eclen;
					else
						ec_amt = 25 * eclen;
						
					if (rebate_type == ";ec")
						ec_result = "$25 + <b>$" + ec_amt + "</b>;";
					else
						ec_result = "<b>$" + ec_amt + "</b>;";
				}
                    
                /*A5Photo card*/
                if (a5len > 0) {
					if (a5len >= 3)
					{
						var mul_val = Math.floor(a5len / 3);
						var mod_val = a5len % 3;
						if (rebate)
						{
							a5_amt = 10 * mod_val;
							a5_amt = a5_amt + (mul_val * 20);
						}
						if (rebate_type == ";a5")
							a5_result = "$25 + <b>$" + a5_amt + "</b>;";
						else
						{
							a5_result = "<b>$" + a5_amt + "</b>;";
						}
						
					}
                    else {
						if (rebate_type == ";a5")
                        {
							if (a5len == 1)
							{
								//a5_amt = 10 * a5len;
								a5_result = "$25;";
							}
							if (a5len == 2)      //total = 2
                            {
								a5_amt = 10 * (a5len - 1);
								a5_result = "$25 + <b>$" + a5_amt + "</b>;";
							}
						}
						else
                        {
							a5_amt = a5len * 10;
                            if (rebate_type == ";a5") {
                                a5_result = "$25 + <b>$" + a5_amt + "</b>;";
                            }
                            else
                            {
                                a5_result = "<b>$" + a5_amt + "</b>;";	
                            }
						}
													
						
					}
                }
                /*Magnet card*/
                if (mglen > 0) {
					if (rebate)
						mg_amt = 8 * mglen;
					else
						mg_amt = 30 * mglen;

					if (rebate_type == ";mg")
						mg_result = "$30 + <b>$" + mg_amt + "</b>;";
					else
						mg_result = "<b>$" + mg_amt + "</b>;";
                }

                /*Keychain*/
                if (kclen > 0) {
					if (rebate)
						kc_amt = 8 * kclen;
					else
						kc_amt = 25 * kclen;
						
					if (rebate_type == ";kc")
						kc_result = "$25 + <b>$" + kc_amt + "</b>;";
					else
						kc_result = "<b>$" + kc_amt + "</b>;";	
                }

                var totalamt = totalamt + ec_amt + a5_amt + mg_amt + kc_amt;
				
				var output = dc_result  + ec_result + a5_result + mg_result + kc_result + totalamt;
				
                return output;
            }
        

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
                <a href="https://kidzania.com.sg/" rel="home"><img src="/img/kidzania.png" title="KidZania Singapore – A City Built for Kids!"></a>
            </div>
            <!-- .site-branding -->
            <div class="header-right">
                <div class="btn-book-tickets">
                    <a href="https://ticketing.kidzania.com.sg" onclick="floodlightBookTickets();" class="navbar-brand" target="_blank"><img src="/img/btn-book-tickets.png"></a>
                </div>
            </div>
        </div>
        <!-- wrapper -->
    </div>
    <!-- masthead -->
    <!-- #site-navigation -->
    </header>
    <!-- HEADER -->

    <div>
        <div style="width: 100%; height: 150px; text-align:center;margin:0 auto;" id="heading">
            <span class="heading1">Photo Selection</span>
        </div>

        <div style="text-align:center;margin-top: 10px;margin:0 auto;"><img src='/img/process-selection.png' /></div>
        
        <div style="width: 100%; text-align:center;margin-top: 10px;margin:0 auto;" id="user_profile">
            <div id="profile_photo" runat="server"></div>
            <div id="purchase_status" runat="server"></div>
        </div>
    
        <div><hr class="hr1"/></div> 
        
        <!-- PRICING TABLE -->
        <div style="text-align: center; width:100%; display:none; ">  <!-- remove from display when it is onsite -->
            <div class="pricing_table">
                <div class="pricing_heading">Pricing</div>
                <div class="pricing_grp">
                    <div class="pricing_item">
                        <span class="pricing_header">Digital</span>
                        <br /><span class="pricing"><b>$20</b>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header">Hardcopy</span>
                        <br /><span class="pricing"><b>$25</b>: 1 hardcopy</span>
                        <br /><span class="pricing"><b>$45</b>: 3 hardcopies</span>
                        <br /><span class="pricing"><b>$60</b>: 5 hardcopies<b><sup>*</sup></b></span>
                        <br /><span class="pricing"><b><sup>*</sup></b>All digital frees</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Establishment_Card.jpg" title="sample">Establishment Card</span>
                        <br /><span class="pricing"><b>$8</b>: 1 card</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\A5_Folder.jpg" title="sample">A5 w/Folder</span>
                        <br /><span class="pricing"><b>$25</b>: 1 copy</span>
                    </div>
					<div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\A5_Folder.jpg" title="sample">A5 w/Folder x 3</span>
                        <br /><span class="pricing"><b>$50</b>: 1 copy</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header">Magnet</span>
                        <br /><span class="pricing"><b>$30</b>: 1 magnet</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Keychain.jpg" title="sample">Keychain</span>
                        <br /><span class="pricing"><b>$25</b>: 1 keychain (2 photos)</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Leatherette.jpg" title="sample">Leatherette (A5 x 2)</span>
                        <br /><span class="pricing"><b>$50</b>: 1 leatherette (2 photos)</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- PRICING TABLE -->

        <form action="summary.aspx" method="post">
            <div style="margin-top:-10px; width: 90%; text-align:center;margin:0 auto;" id="photo_gallery">
                <div id="menu_select"><input name="all_digital_cb" type="checkbox" checked="checked">&nbsp;<label>All Digital</label></div>
                <div style="text-align:center;" id="photo_gallery_ctn" runat="server"></div>
                <br />

                Leatherette
                <select id="leatherette_select">
                    <option value="0">0</option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
                <br /><br />

                <input type="hidden" id="sa" value="0" runat="server"/>
                <input type="hidden" id="dc" value="0" runat="server"/>
                <input type="hidden" id="ec" value="0" runat="server"/>
                <input type="hidden" id="a5" value="0" runat="server"/>
                <input type="hidden" id="mg" value="0" runat="server"/>
                <input type="hidden" id="kc" value="0" runat="server"/>
                <input type="hidden" id="lr" value="0" runat="server"/>
                <input type="hidden" id="js" value="0" runat="server" />
                <div><input class='btn_default' type="submit" /><input style="margin-left:10px;" onclick='location.reload();' class='btn_default' type="reset" /></div>
                <br />
            </div>
        </form>
    </div>


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
