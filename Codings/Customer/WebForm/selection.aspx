<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selection.aspx.cs" Inherits="WebForm.selection" %>


<!DOCTYPE html>
<meta content="width=device-width, initial-scale=1" name="viewport" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    
    <title>Selection</title>

    <style>
        .cartValidation{
            text-align: right;
            color: red;
            font-size: 0.8rem;
            position: absolute;
            right: 0px;  
            vertical-align: middle;
            padding-top:3px;
        }

        .btn_default {
            -moz-box-shadow: 0px 10px 14px -7px #276873;
            -webkit-box-shadow: 0px 10px 14px -7px #276873;
            box-shadow: 0px 10px 14px -7px #276873;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #599bb3), color-stop(1, #408c99));
            background: -moz-linear-gradient(top, #599bb3 5%, #408c99 100%);
            background: -webkit-linear-gradient(top, #599bb3 5%, #408c99 100%);
            background: -o-linear-gradient(top, #599bb3 5%, #408c99 100%);
            background: -ms-linear-gradient(top, #599bb3 5%, #408c99 100%);
            background: linear-gradient(to bottom, #599bb3 5%, #408c99 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#599bb3', endColorstr='#408c99',GradientType=0);
            background-color: #599bb3;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            border-radius: 8px;
            display: inline-block;
            cursor: pointer;
            color: #ffffff;
            font-family: Arial;
            font-size: 20px;
            font-weight: bold;
            padding: 13px 32px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #3d768a;
        }

            .btn_default:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #408c99), color-stop(1, #599bb3));
                background: -moz-linear-gradient(top, #408c99 5%, #599bb3 100%);
                background: -webkit-linear-gradient(top, #408c99 5%, #599bb3 100%);
                background: -o-linear-gradient(top, #408c99 5%, #599bb3 100%);
                background: -ms-linear-gradient(top, #408c99 5%, #599bb3 100%);
                background: linear-gradient(to bottom, #408c99 5%, #599bb3 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#408c99', endColorstr='#599bb3',GradientType=0);
                background-color: #408c99;
            }

        span {
            cursor: pointer;
        }

        .number {
            margin-top: 3px;
            text-align: center;
        }

        .minus, .plus {
            width: 22px;
            height: 23px;
            background: #f2f2f2;
            border-radius: 4px;
            border: 1px solid #ddd;
            display: inline-block;
            vertical-align: middle;
            text-align: center;
        }

        .numberInput {
            margin: 0 3px;
            height: 20px;
            width: 30px;
            color: black;
            text-align: center;
            border: 1px solid #ddd;
            border-radius: 4px;
            display: inline-block;
            vertical-align: middle;
            font-size: 0.9rem;
        }

        .deleteItem:hover {
            color: darkred;
            cursor: pointer;
        }

        .heading1 {
            font-size: 30px;
            font-weight: bold;
        }

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
        .thumbnail:hover {
            opacity: 0.7;
        }
        /* The Close Button */
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
            border: none;
            background-color: transparent;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }
        /* Add Animation */
        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
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
            from {
                -webkit-transform: scale(0)
            }

            to {
                -webkit-transform: scale(1)
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0)
            }

            to {
                transform: scale(1)
            }
        }
        /* The Close Button */
        .close {
            position: absolute;
            top: 15px;
            right: 35px;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover, .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        .cart-img {
            padding: 10px;
            padding-top: 0;
        }
        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }

        .fix_corner {
            position: fixed;
            top: 25%;
            right: 2%;
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
            margin: 12px auto;
        }

        .fix_corner_price {
            text-align: left;
            margin-left: 20px;
        }

        .fix_corner_price_title {
            font-weight: bold;
            text-align: left;
            margin-left: 10px;
        }

        .fix_corner_price_annotation {
            text-align: left;
            font-size: 12px;
        }

        .item_checkbox_grp {
            background-color: white;
            text-align: left !important;
            margin-left: 40%;
            margin-right: 35%;
        }

        .modal_window {
            cursor: pointer;
            height: 80px;
            width: 80px;
        }

        .pricing_heading {
            text-align: center;
            font-size: large;
        }

        .pricing_table {
            margin: 0 auto;
            width: 80%;
            background-color: #e0f538;
            border-radius: 7px;
            -webkit-border-radius: 7px;
            -moz-border-radius: 7px;
            box-shadow: 2px 2px 3px #666666;
            -webkit-box-shadow: 2px 2px 3px #666666;
            -moz-box-shadow: 2px 2px 3px #666666;
        }

        .pricing_subheader {
            margin-left: 10px;
            text-align: left !important;
            font-weight: bold;
        }

        .pricing_grp {
            margin-top: 0px;
            margin-left: 15px;
            text-align: left !important;
        }

        .pricing_item {
            display: inline-block;
            margin-left: 15px;
            vertical-align: top;
        }

        .pricing_header {
            font-weight: bold;
            margin-left: 5px;
        }

        .pricing {
            margin-left: 10px;
        }

        .pricing_header_sample {
            font-weight: bold;
            margin-left: 5px;
            cursor: pointer;
            color: blue;
            text-decoration: underline;
        }

        #prods {
            margin-top: 5%;
            margin-left: 20%;
        }

        #something {
            margin-bottom: 10%;
        }

        .sidebar {
            height: 76%;
            width: 0;
            position: fixed;
            z-index: 5;
            bottom: 0;
            right: 0;
            background: #f5f5f5 !important;
            overflow-x: hidden;
            overflow-y: hidden;
            transition: 0.5s;
            margin: 0 !important;
        }

            .sidebar .closebtn {
                position: absolute;
                top: 0;
                left: 25px;
                font-size: 36px;
                margin-right: 50px;
            }

        .openbtn {
            font-size: 20px;
            cursor: pointer;
            border: none;
        }

        .wrap {
            display: inline-block;
            position: relative;
            margin-left: -10px;
        }

        .overlap {
            display: none;
        }

        .poptooltip .overlap {
            display: block;
            position: absolute;
            height: 100%;
            width: 100%;
            z-index: 1000;
        }

        #photo_gallery {
            transition: margin-right .5s;
            padding: 16px;
        }
        /* On smaller screens, where height is less than 450px, change the style of the sidenav (less padding and a smaller font size) */
        @media screen and (max-height: 450px) {
            .sidebar {
                padding-top: 15px;
            }

           .sidebar a {
                font-size: 18px;
            }
        }
        
    </style>

    <script>
        //Checker for all digital
        var checker = "stage1";
        var CartItems = [];
        var productCostData = [];
        var AllCosts = [];

        $(window).on("load", function () {
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("selection.aspx/GetCartItems") %>',
                dataType: 'text',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var inCartItems = JSON.parse(msg.d);
                    if (inCartItems != null) {
                        $('.itemsChk').prop("checked", false);
                        console.log(inCartItems);
                        var i;
                        for (i = 0; i < inCartItems.length; i++) {
                            console.log(inCartItems[i]);
                            var newCartItem = new CartItem(inCartItems[i].photoId, inCartItems[i].photoSource, inCartItems[i].productId);
                            CartItems.push(newCartItem);
                            $(`#item${inCartItems[i].photoId}${inCartItems[i].productId}`).prop("checked", true);
                        }
                        console.log(CartItems);
                        openCart();
                    }
                }
            });
        });

        $(document).ready(function () {
            //get this product name
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("selection.aspx/GetProducts") %>',
                dataType: 'text',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    productDetails = JSON.parse(msg.d);
                    var i;
                    for (i = 0; i < productDetails.length; i++) {
                        if (productDetails[i].ProductVisibility) {
                            let $tablerow = null;
                            let $namecell = null;
                            let $descriptioncell = null;
                            let $originalcell = null;
                            let $pwpcell = null;
                            let $actioncell = null;
                            if (productDetails[i].PhotoProduct) {
                                let photoProduct = productDetails[i];
                                $tablerow = $(`<tr></tr>`);
                                $namecell = $(`<td><div class="m-r-10"><img src="${photoProduct.ProductImagePath}" class="rounded" width="100"/></div><b>${photoProduct.ProductName}</b></td>`);
                                $descriptioncell = $(`<td>${photoProduct.ProductDescription}</td>`);
                                $originalcell = $(`<td><b>$${photoProduct.OrginalPrice} </b>/ Photo</td>`);
                                $pwpcell = $(`<td><b>$${photoProduct.PwpPrice} </b>/ Photo</td>`);
                                $tablerow.append($namecell);
                                $tablerow.append($descriptioncell);
                                $tablerow.append($originalcell);
                                $tablerow.append($pwpcell);
                                $(`#photoProductTableBody`).append($tablerow);
                            }
                            else {
                                let nonPhotoProduct = productDetails[i];
                                $tablerow = $(`<tr></tr>`);
                                $namecell = $(`<td><div class="m-r-10"><img src="${nonPhotoProduct.ProductImagePath}" class="rounded" width="100"/></div><b>${nonPhotoProduct.ProductName}</b></td>`);
                                $descriptioncell = $(`<td>${nonPhotoProduct.ProductDescription}</td>`);
                                $originalcell = $(`<td><b>$${nonPhotoProduct.OrginalPrice} </b>/ ${nonPhotoProduct.ProductName}</td>`);
                                if (nonPhotoProduct.PwpPrice == 0) {
                                    $pwpcell = $(`<td><b>-</b></td>`);
                                }
                                else {
                                    $pwpcell = $(`<td><b>$${nonPhotoProduct.PwpPrice} </b>/ ${nonPhotoProduct.ProductName}</td>`);
                                }

                                if (nonPhotoProduct.ProductQuantityConstraint == "Max 1 Unit") {
                                    $actioncell = $(`<td><button type="button" class="btn btn-secondary" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 1)">Add</button></td>`);
                                }
                                else {
                                    $actioncell = $(`<td><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Add</button><div class="dropdown-menu" aria-labelledby="dropdownMenuButton"><a class="dropdown-item" value="${nonPhotoProduct.ProductId}" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 1)">1</a><a class="dropdown-item" value="${nonPhotoProduct.ProductId}" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 2)" >2</a><a class="dropdown-item" value="${nonPhotoProduct.ProductId}" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 3)" >3</a><a class="dropdown-item" href="#" value="${nonPhotoProduct.ProductId}" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 4)">4</a><a class="dropdown-item" href="#" value="${nonPhotoProduct.ProductId}" onclick="addNonPhotoToCart('${nonPhotoProduct.ProductId}', 5)">5</a></div></td>`);
                                }
                                $tablerow.append($namecell);
                                $tablerow.append($descriptioncell);
                                $tablerow.append($originalcell);
                                $tablerow.append($pwpcell);
                                $tablerow.append($actioncell);
                                $(`#nonPhotoProductTable`).append($tablerow);
                            }
                        }
                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });



            $("select").change(function () {
                refresh_values();
            });

            //server side
            //change of formulas e.g. Ajax call to server for calculation (no. of products), then get the return of total money
            function cal_amt(dclen, eclen, a5len, mglen, kclen) {
                var dc_amt = 0; var ec_amt = 0; var a5_amt = 0; var mg_amt = 0; var kc_amt = 0;
                var dc_result = "$0;"; var ec_result = "$0;"; var a5_result = "$0;"; var mg_result = "$0;"; var kc_result = "$0;";
                var rebate = false;
                var rebate_type = "";
                totalamt = 0;
                if (dclen > 0) {
                    rebate = true;
                    totalamt = 20;
                    rebate_type = ";dc";
                    dc_result = "$20;";
                }
                else if (eclen > 0) {
                    rebate = true;
                    totalamt = 25;
                    eclen = eclen - 1;
                    rebate_type = ";ec";
                    ec_result = "$25;";
                }
                else if (a5len > 0) {
                    rebate = true;
                    totalamt = 25;
                    //a5len = a5len - 1;
                    rebate_type = ";a5";
                    a5_result = "$25;";
                }
                else if (kclen > 0) {
                    rebate = true;
                    totalamt = 25;
                    kclen = kclen - 1;
                    rebate_type = ";kc";
                    kc_result = "$25;";
                }
                else if (mglen > 0) {
                    rebate = true;
                    totalamt = 30;
                    mglen = mglen - 1;
                    rebate_type = ";mg";
                    mg_result = "$30;";
                }
                /*Establishment card*/
                if (eclen > 0) {
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
                    if (a5len >= 3) {
                        var mul_val = Math.floor(a5len / 3);
                        var mod_val = a5len % 3;
                        if (rebate) {
                            a5_amt = 10 * mod_val;
                            a5_amt = a5_amt + (mul_val * 20);
                        }
                        if (rebate_type == ";a5")
                            a5_result = "$25 + <b>$" + a5_amt + "</b>;";
                        else {
                            a5_result = "<b>$" + a5_amt + "</b>;";
                        }
                    }
                    else {
                        if (rebate_type == ";a5") {
                            if (a5len == 1) {
                                //a5_amt = 10 * a5len;
                                a5_result = "$25;";
                            }
                            if (a5len == 2)      //total = 2
                            {
                                a5_amt = 10 * (a5len - 1);
                                a5_result = "$25 + <b>$" + a5_amt + "</b>;";
                            }
                        }
                        else {
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
                var output = dc_result + ec_result + a5_result + mg_result + kc_result + totalamt;
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
            if (dclen > 0) {
                rebate = true;
                totalamt = 20;
                rebate_type = ";dc";
                dc_result = "$20;";
            }
            else if (eclen > 0) {
                rebate = true;
                totalamt = 25;
                eclen = eclen - 1;
                rebate_type = ";ec";
                ec_result = "$25;";
            }
            else if (a5len > 0) {
                rebate = true;
                totalamt = 25;
                //a5len = a5len - 1;
                rebate_type = ";a5";
                a5_result = "$25;";
            }
            else if (kclen > 0) {
                rebate = true;
                totalamt = 25;
                kclen = kclen - 1;
                rebate_type = ";kc";
                kc_result = "$25;";
            }
            else if (mglen > 0) {
                rebate = true;
                totalamt = 30;
                mglen = mglen - 1;
                rebate_type = ";mg";
                mg_result = "$30;";
            }
            /*Establishment card*/
            if (eclen > 0) {
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
                if (a5len >= 3) {
                    var mul_val = Math.floor(a5len / 3);
                    var mod_val = a5len % 3;
                    if (rebate) {
                        a5_amt = 10 * mod_val;
                        a5_amt = a5_amt + (mul_val * 20);
                    }
                    if (rebate_type == ";a5")
                        a5_result = "$25 + <b>$" + a5_amt + "</b>;";
                    else {
                        a5_result = "<b>$" + a5_amt + "</b>;";
                    }
                }
                else {
                    if (rebate_type == ";a5") {
                        if (a5len == 1) {
                            //a5_amt = 10 * a5len;
                            a5_result = "$25;";
                        }
                        if (a5len == 2)      //total = 2
                        {
                            a5_amt = 10 * (a5len - 1);
                            a5_result = "$25 + <b>$" + a5_amt + "</b>;";
                        }
                    }
                    else {
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
            var output = dc_result + ec_result + a5_result + mg_result + kc_result + totalamt;
            return output;
        }

        function refresh_values() {
            var lrlen = "";
            $("select option:selected").each(function () {
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
            var modal = document.getElementById('myModala' + e);
            var modalImg = document.getElementById("img01");
            var captionText = document.getElementById("caption");
            modal.style.display = "block";
        }

        function delete_gl(id) {
            if (confirm('Are you confirmed to remove photo?')) {
                $("#gl_id_" + id).remove();
                refresh_values();
            } else {
            }
        }

        function modal_load(e) {
            var modal = document.getElementById('myModala' + e);
            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }
            //        $("#menu_select input:checkbox").on("change", function () {
            //            var dclen = $("#photo_gallery input[name='digital_cb']:checked").length;
            //            //Select one digital will select all digital
            //            if (dclen == 0) {
            //                $("#photo_gallery input[name='digital_cb']").each(function () {
            //                    $(this).prop('checked', true);
            //                })
            //            }
            //            else if (dclen > 1) {
            //                    $("#photo_gallery input[name='digital_cb']").each(function () {
            //                        $(this).prop('checked', false);
            //                })
            //            }
            //        });
            //        $("#photo_gallery input:checkbox").on("change", function () {
            //            var dclen = $("#photo_gallery input[name='digital_cb']:checked").length;
            //            var eclen = $("#photo_gallery input[name='ECcopy_cb']:checked").length;
            //            var a5len = $("#photo_gallery input[name='A5copy_cb']:checked").length;
            //            var mglen = $("#photo_gallery input[name='MGcopy_cb']:checked").length;
            //            var kclen = $("#photo_gallery input[name='KCcopy_cb']:checked").length;
            //            var lrlen = 0;
            //            $("select option:selected").each(function() {
            //                lrlen = $(this).text() + " ";
            //            });
            //var totalamt_a = cal_amt(dclen, eclen, a5len, mglen, kclen);
            //var totalamt_array = totalamt_a.split(';');
            //var totalamt = totalamt_array[5];
            //var dc_result = totalamt_array[0];
            //var ec_result = totalamt_array[1];
            //var a5_result = totalamt_array[2];
            //var mg_result = totalamt_array[3];
            //var kc_result = totalamt_array[4];
            //            if (lrlen > 0) {
            //                var lr_amt = lrlen * 15;
            //                totalamt = parseInt(lr_amt) + parseInt(totalamt);
            //            }
            //$("#digital_copy_amt").html("Digital Copy: " + dc_result);
            //            $("#ec_copy_amt").html("Establishment Card: " + ec_result);
            //            $("#a5_copy_amt").html("A5 hardcopy: " + a5_result);
            //            $("#mg_copy_amt").html("Magnet: " + mg_result);
            //            $("#kc_copy_amt").html("Keychain: " + kc_result);
            //            $("#Total_cost").html("$" + totalamt + " SGD");
            //$("#dc").attr('value', dclen);
            //$("#a5").attr('value', a5len);
            //            $("#ec").attr('value', eclen);
            //            $("#mg").attr('value', mglen);
            //            $("#kc").attr('value', kclen);
            //            $("#sa").attr('value', totalamt);
            //        });
            //            var dc_amt = 0; var ec_amt = 0; var a5_amt = 0; var mg_amt = 0; var kc_amt = 0;
            //var dc_result = "$0;"; var ec_result = "$0;"; var a5_result = "$0;"; var mg_result = "$0;"; var kc_result = "$0;";
            //var rebate = false;
            //var rebate_type = "";
            //totalamt = 0;
            //            if (dclen > 0) 
            //{
            //	rebate = true;
            //	totalamt = 20;
            //	rebate_type = ";dc";
            //	dc_result = "$20;";
            //}
            //else if (eclen > 0)
            //{
            //	rebate = true;
            //	totalamt = 25;
            //	eclen = eclen - 1;
            //	rebate_type = ";ec";
            //	ec_result = "$25;";
            //            }
            //            else if (a5len > 0)
            //{
            //	rebate = true;
            //	totalamt = 25;
            //	//a5len = a5len - 1;
            //	rebate_type = ";a5";
            //	a5_result = "$25;";
            //}
            //else if (kclen > 0)
            //{
            //	rebate = true;
            //	totalamt = 25;
            //	kclen = kclen - 1;
            //	rebate_type = ";kc";
            //	kc_result = "$25;";
            //}
            //else if (mglen > 0)
            //{
            //	rebate = true;
            //	totalamt = 30;
            //	mglen = mglen - 1;
            //	rebate_type = ";mg";
            //	mg_result = "$30;";
            //}
            //            /*Establishment card*/
            //            if (eclen > 0)
            //{
            //	if (rebate)
            //		ec_amt = 8 * eclen;
            //	else
            //		ec_amt = 25 * eclen;
            //	if (rebate_type == ";ec")
            //		ec_result = "$25 + <b>$" + ec_amt + "</b>;";
            //	else
            //		ec_result = "<b>$" + ec_amt + "</b>;";
            //}
            //            /*A5Photo card*/
            //            if (a5len > 0) {
            //	if (a5len >= 3)
            //	{
            //		var mul_val = Math.floor(a5len / 3);
            //		var mod_val = a5len % 3;
            //		if (rebate)
            //		{
            //			a5_amt = 10 * mod_val;
            //			a5_amt = a5_amt + (mul_val * 20);
            //		}
            //		if (rebate_type == ";a5")
            //			a5_result = "$25 + <b>$" + a5_amt + "</b>;";
            //		else
            //		{
            //			a5_result = "<b>$" + a5_amt + "</b>;";
            //		}
            //	}
            //                else {
            //		if (rebate_type == ";a5")
            //                    {
            //			if (a5len == 1)
            //			{
            //				//a5_amt = 10 * a5len;
            //				a5_result = "$25;";
            //			}
            //			if (a5len == 2)      //total = 2
            //                        {
            //				a5_amt = 10 * (a5len - 1);
            //				a5_result = "$25 + <b>$" + a5_amt + "</b>;";
            //			}
            //		}
            //		else
            //                    {
            //			a5_amt = a5len * 10;
            //                        if (rebate_type == ";a5") {
            //                            a5_result = "$25 + <b>$" + a5_amt + "</b>;";
            //                        }
            //                        else
            //                        {
            //                            a5_result = "<b>$" + a5_amt + "</b>;";	
            //                        }
            //		}
            //	}
            //            }
            //            /*Magnet card*/
            //            if (mglen > 0) {
            //	if (rebate)
            //		mg_amt = 8 * mglen;
            //	else
            //		mg_amt = 30 * mglen;
            //	if (rebate_type == ";mg")
            //		mg_result = "$30 + <b>$" + mg_amt + "</b>;";
            //	else
            //		mg_result = "<b>$" + mg_amt + "</b>;";
            //            }
            //            /*Keychain*/
            //            if (kclen > 0) {
            //	if (rebate)
            //		kc_amt = 8 * kclen;
            //	else
            //		kc_amt = 25 * kclen;
            //	if (rebate_type == ";kc")
            //		kc_result = "$25 + <b>$" + kc_amt + "</b>;";
            //	else
            //		kc_result = "<b>$" + kc_amt + "</b>;";	
            //            }
            //            var totalamt = totalamt + ec_amt + a5_amt + mg_amt + kc_amt;
            //var output = dc_result  + ec_result + a5_result + mg_result + kc_result + totalamt;
            //            return output;
        }

        function selectProduct(id, filename) {
            $('#productModal').val(id);
            $('.itemsChk').prop("checked", false);
            $('#photoImg').attr("src", '/Content/photos/' + filename);
            var i;
            console.log("Modal opened");
            console.log(CartItems);
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].photoId == id) {
                    $(`#item${CartItems[i].productId}`).prop("checked", true);
                }
            }
            //show the modal
            //document.getElementById("prods").innerHTML += "<input type='checkbox' id='check" +item+ "'/>" +item + "<br>"; 
            $('#productModal').modal("show");
        }

        //This will open the shopping cart
        function openCart() {
            load_Cart();
            $("#cart").css("width", "350px");
            $("#photo_gallery").css("width", "80%");
            $("#photo_gallery").css("marginRight", "350px");
        }

        //Closing the shopping cart
        function closeCart() {
            $("#cart").css("width", "0");
            $("#photo_gallery").css("width", "90%");
            $("#photo_gallery").css("margin", "0 auto");
        }

        function CartItem(inPhotoId, inPhotoSource, inProductId) {
            this.productId = inProductId;
            this.photoId = inPhotoId;
            this.photoSource = inPhotoSource;
        }
        //When the user submitted the product, this method will load the chosen product to shopping cart

        function addToCart(src, phoId, proId) {
            $("input:checkbox[name=product]:checked").each(function () {
                var exist = false;
                for (i = 0; i < CartItems.length; i++) {
                    if (CartItems[i].photoId == phoId && CartItems[i].productId == proId) {
                        exist = true;
                    }
                }
                if (!exist) {
                    var newCartItem = new CartItem(phoId, src, proId);
                    CartItems.push(newCartItem);
                }
            });
            openCart();
        }
        //When the select all digital is clicked the item will be inserted to the shopping cart

        function addNonPhotoToCart(proId, quantity) {
            var exist = false;
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].productId == proId) {
                    exist = true;
                    CartItems[i].photoSource = quantity;
                }
            }
            if (!exist) {
                var newCartItem = new CartItem("photoProduct", quantity, proId);
                CartItems.push(newCartItem);
            }
            console.log(CartItems);
            openCart();
        }

        function checkChange(src, phoId, proId) {
            if ($(`#item${phoId}${proId}`).prop("checked") == true) {
                addToCart(src, phoId, proId);
            } else {
                deleteItem(phoId, proId);
            }
        }

        //Delete a product in the shopping cart
        function deleteItem(phoId, proId) {
            var i;
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].photoId == phoId && CartItems[i].productId == proId) {
                    //if 1 photo only has 1 item selected
                    CartItems.splice(i, 1);
                    $(`#item${phoId}${proId}`).prop('checked', false);
                }
            }
            load_Cart();
        }

        //Tooltip
        $('[rel="tooltip"]').tooltip({
            animated: 'fade',
            placement: 'bottom'
        });


        function minusQuantity(proId) {
            var i;
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].productId == proId) {
                    if (CartItems[i].photoSource > 1) {
                        //update shopping cart
                        CartItems[i].photoSource = CartItems[i].photoSource - 1;
                        //change input
                        $(`#qty${proId}`).val(CartItems[i].photoSource);
                        //update productCostData
                        var x;
                        for (x = 0; x < productCostData.length; x++) {
                            if (productCostData[x].productId == proId) {
                                productCostData[x].quantity = CartItems[i].photoSource;
                            }
                        }
                        //update cost
                        calculateTotalCost();
                    }
                    else {
                        //error message
                    }
                }
            }
        }

        function addQuantity(proId) {
            var i;
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].productId == proId) {
                    //update shopping cart
                    CartItems[i].photoSource = CartItems[i].photoSource + 1;
                    //change input
                    $(`#qty${proId}`).val(CartItems[i].photoSource);
                    //update productCostData
                    var x;
                    for (x = 0; x < productCostData.length; x++) {
                        if (productCostData[x].productId == proId) {
                            productCostData[x].quantity = CartItems[i].photoSource;
                        }
                    }
                    //update cost
                    calculateTotalCost();
                }
            }
        }

        function Costs(inProduct, inCost) {
            this.ProductId = inProduct;
            this.Cost = inCost;
        }

        function calculateTotalCost() {
            AllCosts = [];
            if (productCostData.length != 0) {
                pcd = JSON.stringify(productCostData);
                var dataValues = JSON.stringify({ "cartItems": pcd });
                //console.log(productCostData);
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("selection.aspx/CalculateTotalCost") %>',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: dataValues,
                    success: function (msg) {
                        var allCosts = JSON.parse(msg.d);
                        var k;
                        for (k = 0; k < allCosts.length - 1; k++) {
                            var productCost = new Costs(allCosts[k].ProductId, allCosts[k].Cost.toFixed(2));
                            AllCosts.push(productCost);
                            $(`#totalFor${allCosts[k].ProductId}`).text('$ ' + allCosts[k].Cost.toFixed(2));
                        }
                        var totalCost = new Costs('total', allCosts[allCosts.length - 1].Cost.toFixed(2));
                        AllCosts.push(totalCost);
                        $(`#cartTotalCost`).text('$ ' + allCosts[allCosts.length - 1].Cost.toFixed(2));
                        updateCartForm();
                    },
                    error: function (response) {
                        console.log(response);
                    }
                });
            }
            else {
                $(`#cartTotalCost`).text('$ 0');
            }


        }

        function updateCartForm() {
            $(`#cartForm`).html("");
            var i;
            let item_photo;
            let cost_item;
            let item_nonPhoto;
            for (i = 0; i < CartItems.length; i++) {
                if (CartItems[i].photoId == "photoProduct") {
                    item_nonPhoto = $(`<input type="hidden" name="summary${CartItems[i].productId}" value="${CartItems[i].photoSource}"/>`);
                    $(`#cartForm`).append(item_nonPhoto);

                }
                else {
                    //create input chk box
                    item_photo = $(`<input type="checkbox" name="summary${CartItems[i].productId}" value="${CartItems[i].photoSource}" style="display: none;" checked/>`);
                    $(`#cartForm`).append(item_photo);
                }
            }
            //console.log(AllCosts);
            var x;
            for (x = 0; x < AllCosts.length; x++) {
                cost_item = $(`<input type="hidden" name="summary${AllCosts[x].ProductId}cost" value="${AllCosts[x].Cost}" />`);
                $(`#cartForm`).append(cost_item);
            }
        }

        //Loading item using ajax call
        function load_Cart() {
            $('#cart_items').html('');
            var categories = [];
            var nonPhotoProducts = [];
            productCostData = [];
            //load Photo Product
            for (i = 0; i < CartItems.length; i++) {
                var categoryExist = false;
                var n;
                //check whether this product has been selected / created
                for (n = 0; n < categories.length; n++) {
                    if (categories[n] == CartItems[i].productId) {
                        categoryExist = true;
                    }
                }
                if (CartItems[i].photoId != "photoProduct") {
                    //if the product (i.e. hardcopy, keychain) not created yet
                    if (!categoryExist) {
                        categories.push(CartItems[i].productId);
                        let $divProduct = $(`<div style="background-color: white" class="m-1 p-2 mb-2"></div>`);
                        let $divProductName = $(`<div class="pb-1 mb-0" style="border-bottom: 1px solid #c3c3c3; position: relative;" id="cartProduct${CartItems[i].productId}"><span id="cartProductName${CartItems[i].productId}" style="font-size: 0.85rem" class="ml-1 pb-0"></span></div>`);
                        let $divProductPhotos = $(`<div class="row p-2 pb-1" id="cartphotosFor${CartItems[i].productId}"></div>`);
                        let $divProductTotal = $(`<div class="pt-1 mt-0 text-right" style="border-top: 1px solid #c3c3c3; font-size: 0.95rem"><b id="totalFor${CartItems[i].productId}" class="pr-1">SGD: 18.00</b></div>`);
                        $divProduct.append($divProductName);
                        $divProduct.append($divProductPhotos);
                        $divProduct.append($divProductTotal);
                        $('#cart_items').append($divProduct);
                        dataValue = { "id": CartItems[i].productId };
                        //get this product name
                        $.ajax({
                            type: "POST",
                            url: '<%= ResolveUrl("selection.aspx/GetProductbyId") %>',
                            dataType: 'text',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(dataValue),
                            success: function (msg) {
                                productDetails = JSON.parse(msg.d);
                                $(`#cartProductName${productDetails.ProductId}`).text(productDetails.ProductName);
                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });
                    }
                    //if(CartItems)

                    //add in selected photo to the selected product
                    let $divSelectedPro = $(`#cartphotosFor${CartItems[i].productId}`);
                    let $divsOuter = $(`<div class="col-md-4 cart-img pb-0"><div class="thumbnail"></div></div>`);
                    let $imgPhoto = $(`<img src="${CartItems[i].photoSource}" style="width: 100%" />`);
                    let $divDelete = $(`<div class="caption text-center" style="background-color: #eee"><i class="far fa-trash-alt deleteItem" style="font-size: 0.9rem;" onclick="deleteItem('${CartItems[i].photoId}', '${CartItems[i].productId}');"></i></div>`);
                    //calculate price for this particular product e.g. hardcopy, softcopy
                    var noOfPhotos = 0;
                    var x;
                    for (x = 0; x < CartItems.length; x++) {
                        if (CartItems[x].productId == CartItems[i].productId) {
                            noOfPhotos++;
                        }
                    }
                    var m;
                    var costExist = false;
                    for (m = 0; m < productCostData.length; m++) {
                        if (productCostData[m].productId == CartItems[i].productId) {
                            costExist = true;
                        }
                    }
                    if (!costExist) {
                        var oneProduct = { "productId": CartItems[i].productId, "quantity": noOfPhotos };
                        productCostData.push(oneProduct);
                    }
                    $divsOuter.append($imgPhoto);
                    $divsOuter.append($divDelete);
                    $divSelectedPro.append($divsOuter);
                }
                //the item is nonphoto product
                else {
                    nonPhotoProducts.push(CartItems[i]);
                }
            }
            if (nonPhotoProducts.length != 0) {
                var x;
                let $nonPhotoProductDiv = $('<div style="background-color: white" class="m-1 p-2"></div>');
                let $productDiv = $(`<div class="row p-2 pb-0 pl-3"></div>`);
                let $productInnerDiv = null;
                let $productImgElement = null;
                let $divDelete = null;
                let $productNameP = null;
                let $productQtyDiv = null;
                let $productPriceP = null;
                for (x = 0; x < nonPhotoProducts.length; x++) {
                    var oneProduct = { "productId": nonPhotoProducts[x].productId, "quantity": nonPhotoProducts[x].photoSource };
                    productCostData.push(oneProduct);

                    let $captionDiv = $(`<div></div>`);
                    $productInnerDiv = $(`<div class="col-md-5 cart-img pb-0 pr-2 pl-3 mr-2"></div>`);
                    $productImgElement = $(`<img id="img${nonPhotoProducts[x].productId}" style="width: 100%"/>`);
                    $divDelete = $(`<div class="caption text-center" style="background-color: #eee"><i class="far fa-trash-alt deleteItem" style="font-size: 0.9rem;" onclick="deleteItem('photoProduct', '${nonPhotoProducts[x].productId}');"></i></div>`);
                
                    //product Info
                    $productNameP = $(`<p class="text-center" style="font-size: 0.85rem; margin-bottom:0.2rem" id="name${nonPhotoProducts[x].productId}"></p>`);
                    $productQtyDiv = $(`<div class="number" id="numberOfProduct${nonPhotoProducts[x].productId}" style="height: 25px;"><span class="minus" onclick="minusQuantity('${nonPhotoProducts[x].productId}')">-</span><input class="numberInput" id="qty${nonPhotoProducts[x].productId}" type="text" value="${nonPhotoProducts[x].photoSource}" /><span class="plus" onclick="addQuantity('${nonPhotoProducts[x].productId}')">+</span></div>`);
                    $productPriceP = $(`<p class="text-center mt-2" style="font-size: 0.95rem; margin-bottom:0.2rem"><b id="totalFor${nonPhotoProducts[x].productId}"></b></p>`);

                    $captionDiv.append($productNameP);
                    $captionDiv.append($productQtyDiv);
                    $captionDiv.append($productPriceP);

                    $productInnerDiv.append($productImgElement);
                    $productInnerDiv.append($divDelete);
                    $productInnerDiv.append($captionDiv);

                    $productDiv.append($productInnerDiv);
                    $nonPhotoProductDiv.append($productDiv);

                    var nonPhotoProductId = { "id": nonPhotoProducts[x].productId };
                    //get this product name
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("selection.aspx/GetProductbyId") %>',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(nonPhotoProductId),
                        success: function (msg) {
                            var productDetails = JSON.parse(msg.d);
                            var productImg = productDetails.ProductImagePath;
                            var productName = productDetails.ProductName;
                            var productQunatityConstraint = productDetails.ProductQuantityConstraint;
                            $(`#img${productDetails.ProductId}`).attr("src", productImg);
                            $(`#name${productDetails.ProductId}`).text(productName);
                            if (productQunatityConstraint == "Max 1 Unit") {
                                $(`#numberOfProduct${productDetails.ProductId}`).html("");
                                $(`#numberOfProduct${productDetails.ProductId}`).css("margin-top", "0");
                                $(`#numberOfProduct${productDetails.ProductId}`).css("font-size", "0.88rem");
                                $(`#numberOfProduct${productDetails.ProductId}`).css("vertical-align;", "middle");
                                $(`#numberOfProduct${productDetails.ProductId}`).text("x 1");
                            }
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
                $('#cart_items').append($nonPhotoProductDiv);
            }

            //validate cart
            var n;
            for (n = 0; n < productCostData.length; n++) {
                if (productCostData[n].quantity % 2 != 0) {
                    dataValue = { "id": productCostData[n].productId };
                    //get this product name
                    $.ajax({
                        type: "POST",
                        url: '<%= ResolveUrl("selection.aspx/GetProductbyId") %>',
                        dataType: 'text',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(dataValue),
                        success: function (msg) {
                            productDetails = JSON.parse(msg.d);
                            if (productDetails.ProductQuantityConstraint == "Even Numbered Units") {
                                $(`#cartProduct${productDetails.ProductId}`).append(`<b class="cartValidation">* Select even number photos for ${productDetails.ProductName}</b>`);
                            }

                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
            }
            calculateTotalCost();
        }


        function validateCartForm() {
            if (CartItems.length == 0) {
                alert("Please add items to your cart!");
                return false;
            }
            else if ($(".cartValidation")[0]) {
                alert("Please " + $(".cartValidation").text() + "!");
                return false;
            }
            else {
                return true;
            }
        }
        function mobileMenu() {
            document.getElementById("mobile-primary-menu").classList.toggle("show");
        }
    </script>
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
                    <a href="https://kidzania.com.sg/" rel="home">
                        <img src="/Content/img/kidzania.png" title="KidZania Singapore – A City Built for Kids!" /></a>
                </div>
                <!-- .site-branding -->
                <div class="header-right">
                    <div class="btn-book-tickets">
                        <a href="https://ticketing.kidzania.com.sg" onclick="floodlightBookTickets();" class="navbar-brand" target="_blank">
                            <img src="/Content/img/btn-book-tickets.png" /></a>
                    </div>
                </div>
                <div id="cd-cart-trigger"><a onclick="openCart()"><i class="fa fa-shopping-cart"></i></a></div>
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

    <!-- The Modal --Create Lesson Type Modal-->
    <div class="modal fade" id="productModal">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 80%">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header p-1 pt-2">
                    <h4 class="modal-title" style="margin-left: 20px">Choose Product</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card">
                        <div class="card-body p-1 pl-3">
                            <div style="margin-bottom: 5px; display: flex;">
                                <div style="height: auto; margin-right: -10px; -ms-flex: 0.8; flex: 0.8;">
                                    <img id="photoImg" style="width: 78%; vertical-align: central; margin-left: 30px" />
                                </div>

                                <form id="createTimeTableForm" style="-ms-flex: 1; flex: 1;">
                                    <div class="form-group col-md-12 ml-0">
                                        <div id="prods" runat="server">
                                        </div>
                                    </div>
                                    <!-- Modal footer -->
                                    <div class="modal-footer p-0 ">
                                        <button type="button" class="btn btn-primary" id="addProductBtn" onclick="addToCart()" runat="server">Submit</button>
                                        <a id="backBtn" class="goToManageTB btn btn-grey">Back</a>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div>
        <div style="width: 100%; height: 150px; text-align:center;margin:0 auto;" id="heading">
            <span class="heading1">Photo Selection</span>
        </div>

        <div style="text-align:center;margin-top: 10px;margin:0 auto;"><img src='Content/img/process-selection.png' /></div>
        
        <div style="width: 100%; text-align:center;margin-top: 10px;margin:0 auto;" id="user_profile">
            <div id="profile_photo" runat="server"></div>
            <div id="purchase_status" runat="server"></div>
        </div>
    
        <div><hr class="hr1"/></div> 

        <!-- PRICING TABLE -->
        <div style="text-align: center; width: 100%; display: none;">
            <!-- remove from display when it is onsite -->
            <div class="pricing_table">
                <div class="pricing_heading">Pricing</div>
                <div class="pricing_grp">
                    <div class="pricing_item">
                        <span class="pricing_header">Digital</span>
                        <br />
                        <span class="pricing"><b>$20</b></span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header">Hardcopy</span>
                        <br />
                        <span class="pricing"><b>$25</b>: 1 hardcopy</span>
                        <br />
                        <span class="pricing"><b>$45</b>: 3 hardcopies</span>
                        <br />
                        <span class="pricing"><b>$60</b>: 5 hardcopies<b><sup>*</sup></b></span>
                        <br />
                        <span class="pricing"><b><sup>*</sup></b>All digital frees</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Establishment_Card.jpg" title="sample">Establishment Card</span>
                        <br />
                        <span class="pricing"><b>$8</b>: 1 card</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\A5_Folder.jpg" title="sample">A5 w/Folder</span>
                        <br />
                        <span class="pricing"><b>$25</b>: 1 copy</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\A5_Folder.jpg" title="sample">A5 w/Folder x 3</span>
                        <br />
                        <span class="pricing"><b>$50</b>: 1 copy</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header">Magnet</span>
                        <br />
                        <span class="pricing"><b>$30</b>: 1 magnet</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Keychain.jpg" title="sample">Keychain</span>
                        <br />
                        <span class="pricing"><b>$25</b>: 1 keychain (2 photos)</span>
                    </div>
                    <div class="pricing_item">
                        <span class="pricing_header_sample" onclick="show_modal(this);" src="\img\products\Leatherette.jpg" title="sample">Leatherette (A5 x 2)</span>
                        <br />
                        <span class="pricing"><b>$50</b>: 1 leatherette (2 photos)</span>
                    </div>
                </div>
            </div>
        </div>


        <div>
            <div style="margin-top: -10px; width: 90%; text-align: center; margin: 0 auto;" id="photo_gallery">
                <!--<div id="menu_select">
                    <input name="all_digital_cb" onclick="disableCheck()" id="all_digital" type="checkbox" />&nbsp;<label>All Digital</label>
                </div> -->

                <div class="PT">
                    <div id="photoS">
                        <button class="accordion">Photo Souvenir Table</button>
                        <div class="panel">
                            <div class="card-body" id="photoProductTable">
                                <div class="table-responsive" style="overflow-x: unset;">
                                    <table class="table table-striped table-bordered first">
                                        <thead>
                                            <tr>
                                                <th colspan="2">Photo Souvenir</th>
                                                <th>Price</th>
                                                <th>PWP Price</th>
                                            </tr>
                                        </thead>
                                        <tbody id="photoProductTableBody"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="nonphotoS">
                        <button class="accordion">Other Souvenir Table</button>
                        <div class="panel">
                            <div class="card-body" id="otherProductTable">
                                <div class="table-responsive" style="overflow-x: unset;">
                                    <table class="table table-striped table-bordered first">
                                        <thead>
                                            <tr>
                                                <th colspan="2">Other Souvenir</th>
                                                <th>Price</th>
                                                <th>PWP Price</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="nonPhotoProductTable">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="text-align: center;" id="photo_gallery_ctn" runat="server">
                </div>
            <br />
            <!--
                Leatherette
                <select id="leatherette_select">
                    <option value="0">0</option>
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
                <br/>
                <br/>

                <input type="hidden" id="sa" value="0" runat="server" />
                <input type="hidden" id="dc" value="0" runat="server" />
                <input type="hidden" id="ec" value="0" runat="server" />
                <input type="hidden" id="a5" value="0" runat="server" />
                <input type="hidden" id="mg" value="0" runat="server" />
                <input type="hidden" id="kc" value="0" runat="server" />
                <input type="hidden" id="lr" value="0" runat="server" />
                <input type="hidden" id="js" value="0" runat="server" />
                    -->
            <div>
                <!--<input class='btn_default' type="submit" />-->
                <input style="margin-left: 10px;" onclick='location.reload();' class='btn_default' type="reset" />
            </div>
            <br />
            </div>
            <div id="cart" class="sidebar">
                <div class="p-2 mt-1 mb-2" style="text-align: center">
                    <i class="fas fa-shopping-cart"></i>
                    <span class="modal-title" style="margin-left: 10px">My Cart</span>
                    <button type="button" class="close" onclick="closeCart()">&times;</button>
                </div>
                <div id="cart_items" style="height: 76%; overflow-y: scroll; overflow-x: hidden;">
                </div>
                <div class="totalPrice p-2 pl-3 bg-white" style="border-top: 1px solid #c3c3c3; height: 60px; display: flex; bottom: 0; position: fixed;width: 350px;">
                    <p style="-ms-flex: 1; flex: 1; font-size: 1.02rem" class="mt-2">
                        Total: <b id="cartTotalCost"> SGD 72.00</b>
                    </p>

                    <form action="/summary.aspx" method="post" runat="server" onsubmit = "return validateCartForm();">
                        <div id="cartForm"></div>
                        <input class="btn btn-primary p-2" style="-ms-flex: 0.4; flex: 0.4;" type="submit" />
                    </form>
                </div>
            </div>



        </div>
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
                        <li class="footer-logo"><a href="#">
                            <img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/kidzania.png"></a></li>
                        <li class="footer-location">Palawan Kidz City. 31 Beach View #01-01/02 Singapore 098008.</li>
                        <li class="footer-phone">Local Hotline: 1800 653 6888</li>
                        <li class="footer-phone">International Hotline: +65 6653 6888</li>
                        <li class="footer-mail"><a href="mailto:share@kidzania.com.sg">share@kidzania.com.sg</a></li>
                    </ul>
                </div>
                <ul class="footer-social-media">
                    <li><a href="https://www.facebook.com/KidzaniaSingapore" target="_blank">
                        <img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-fb.png" title="Facebook" alt="Facebook"></a></li>
                    <li><a href="https://www.tripadvisor.com.my/Attraction_Review-g294264-d7789437-Reviews-KidZania_Singapore-Sentosa_Island.html" target="_blank">
                        <img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-tripadvisor.png" title="Trip Advisor" alt="Trip Advisor"></a></li>
                    <li><a href="https://instagram.com/KidzaniaSingapore" target="_blank">
                        <img src="https://kidzania.com.sg/kidzania/wp-content/themes/kidzania/images/icon-instagram.png" title="Instagram" alt="Instagram"></a></li>
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
    <script>
        var acc = document.getElementsByClassName("accordion");
        var acctab;

        for (acctab = 0; acctab < acc.length; acctab++) {
            acc[acctab].addEventListener("click", function () {
                this.classList.toggle("active");
                var panel = this.nextElementSibling;
                if (panel.style.maxHeight) {
                    panel.style.maxHeight = null;
                } else {
                    panel.style.maxHeight = panel.scrollHeight + "px";
                }
            });
        }
    </script>
</body>
</html>
