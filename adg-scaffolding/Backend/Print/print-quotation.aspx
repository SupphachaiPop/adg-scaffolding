<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-quotation.aspx.cs" Inherits="adg_scaffolding.Backend.Print.print_quotation" %>

<!DOCTYPE html>

<html lang="en" class="default-style">

<head>
    <title>ADG SCAFFOLDING</title>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge,chrome=1">
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
    <link rel="icon" type="image/x-icon" href="favicon.ico">

    <link href="https://fonts.googleapis.com/css?family=Roboto:300,300i,400,400i,500,500i,700,700i,900" rel="stylesheet">
    <link rel="icon" type="image/x-icon" href="http://www.comseven.com/wp-content/uploads/2016/11/fav-Logo-com7-01.png">

    <!-- Icon fonts -->
    <link rel="stylesheet" href="/assets/vendor/fonts/fontawesome.css">
    <link rel="stylesheet" href="/assets/vendor/fonts/ionicons.css">
    <link rel="stylesheet" href="/assets/vendor/fonts/linearicons.css">
    <link rel="stylesheet" href="/assets/vendor/fonts/open-iconic.css">
    <link rel="stylesheet" href="/assets/vendor/fonts/pe-icon-7-stroke.css">

    <!-- Core stylesheets -->
    <link rel="stylesheet" href="/assets/vendor/css/bootstrap-material.css" class="theme-settings-bootstrap-css">
    <link rel="stylesheet" href="/assets/vendor/css/appwork-material.css" class="theme-settings-appwork-css">
    <link rel="stylesheet" href="/assets/vendor/css/theme-corporate-material.css" class="theme-settings-theme-css">
    <link rel="stylesheet" href="/assets/vendor/css/colors-material.css" class="theme-settings-colors-css">
    <link rel="stylesheet" href="/assets/vendor/css/uikit.css">
    <link rel="stylesheet" href="/assets/css/demo.css">

    <script src="/assets/vendor/js/material-ripple.js"></script>
    <script src="/assets/vendor/js/layout-helpers.js"></script>

    <!-- Core scripts -->
    <script src="/assets/vendor/js/pace.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Libs -->
    <link rel="stylesheet" href="/assets/css/custom.css">

    <script type="text/javascript">
        function displayMessage(printContent) {
            $(document).ready(function () { window.print(); });
        }
    </script>

    <style type="text/css">
        @media print {
            #btnPrintPreview {
                display: none;
            }

            .card.mb-3 {
                margin-bottom: 0px !important;
            }

            .card-body {
                padding: 5px !important;
            }

            .headers-group {
                display: table-header-group;
            }

            html, body {
                width: 210mm;
                height: 297mm;
            }

            .page-load {
                display: none;
            }

            .container, .container-fluid {
                padding: 5px !important;
            }

            .container-p-y, .container-p-y:not([class^="pb-"]):not([class*=" pb-"]), .container-p-y:not([class^="pt-"]):not([class*=" pt-"]) {
                padding-top: 3px !important;
                padding-bottom: 3px !important;
            }

            .custom-control.custom-checkbox .custom-control-input:checked ~ .custom-control-label::before {
                content: '\2713';
                color: black;
                font-size: 16px;
                font-weight: bold;
            }
        }

        @page {
        }

        .bd-grey {
            border: 1px solid rgba(24,28,33,0.06) !important
        }

        .bd-l-dark {
            border-left: 1px solid rgba(24,28,33,0.06) !important
        }

        div.titlepage {
            page: blank;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="page-loader">
            <div class="bg-primary"></div>
        </div>

        <!-- Layout wrapper -->
        <div class="layout-wrapper layout-2">
            <div class="layout-inner">
                <!-- Layout container -->
                <div class="layout-container">

                    <!-- Layout content -->
                    <div class="layout-content">

                        <!-- Content -->
                        <div class="container-fluid flex-grow-1 container-p-y">
                            <!-- Statistics -->
                            <div class="row">
                                <div class="col-lg-11">
                                </div>
                                <div class="col-lg-1">
                                    <a id="btnPrintPreview" href="javascript:void(0);" onclick="displayMessage()" class="btn btn-success mb-3 mr-2">Preview</a>
                                </div>
                            </div>
                            <div class="card mb-3">
                                <div class="row no-gutters">
                                    <div class="col-md-12 col-lg-12 col-xl-12">
                                        <div class="card-body">
                                            <div class="container">
                                                <div class="row mb-2 ">
                                                    <div class="col-sm-2 col-lg-2 col-xl-2 mb-1">
                                                        <img src="/assets/img/adg_logo.png" class="img-fluid mt-1" alt="">
                                                        <asp:HiddenField ID="hddfquotationID" runat="server" />
                                                        <asp:HiddenField ID="hdfStoreID" runat="server" />
                                                        <asp:HiddenField ID="hdfUserID" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 col-lg-2 col-xl-2 mb-2 text-left">
                                                        <span class="badge p-2 ">
                                                            <h5 class="mb-0 text-left">บริษัท เอ ดี จี สแคฟโฟลดิ้ง(ประเทศไทย) จำกัด</h5>
                                                            <br />
                                                            <h5 class="mb-0 text-left">ADG SCAFFODING(THAILAND) Co.,Ltd.</h5>
                                                        </span>
                                                    </div>
                                                    <div class="col-sm-8 col-lg-8 col-xl-8 mb-2 text-right">
                                                        <span class="badge p-2 ">
                                                            <h5 class="mb-0">ใบเสนอราคา</h5>
                                                            <br />
                                                            <h5 class="mb-0">Quotation</h5>
                                                        </span>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 หมู่ที่ 1 ตำบลท่าทองหลาง อำเภอบางคล้า จังหวัดฉะเชิงเทรา 24110
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                        <asp:Label ID="Label2" runat="server" CssClass="pl-1"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 MOO 1 Tha Thong Lang,Bang Khla, Chachoengsao, 24110
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                        <asp:Label ID="Label3" runat="server" CssClass="pl-1"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                        <asp:Label ID="Label1" runat="server" CssClass="pl-1"></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row bd-grey mb-3 p-2 ">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6">
                                                                <div class="pt-2">
                                                                    <strong>quotation For</strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">
                                                                    <strong>TAX ID: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtTaxID" runat="server" CssClass=""></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">
                                                                    <strong>Customer name : </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtName" runat="server" Text="Label" CssClass=""></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">
                                                                    <strong>Address : </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtCustomerAddress" runat="server" CssClass=""></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">
                                                                    <strong>E-Mail : </strong>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtEmail" runat="server" CssClass="small"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">
                                                                    <strong>เบอร์มือถือ (Mobile): </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtMobile" runat="server" CssClass=""></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>

                                                    <!-- Side Right -->
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 bd-l-dark">
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">

                                                                <div class="pt-2">
                                                                    <strong>quotation # :</strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtquotationNo" runat="server" CssClass=""></asp:Label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">

                                                                <div class="pt-2">
                                                                    <strong>DATE  :</strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">
                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtquotationDate" runat="server" CssClass=""></asp:Label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- End Customer Information -->

                                                <!-- start description time for rent part-->
                                                <div class="row bd-grey p-2 mb-3">

                                                    <div class="col-12">
                                                        <h4>Service Detail</h4>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table">

                                                            <thead class="thead-dark">
                                                                <tr>
                                                                    <th scope="col">QUANTITY</th>
                                                                    <th scope="col">PART NUMBER</th>
                                                                    <th scope="col">DESCRIPTION</th>
                                                                    <th scope="col">UNIT PRICE</th>
                                                                    <th scope="col">TAXABLE</th>
                                                                    <th scope="col">AMOUNT</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--OnItemDataBound="repeaterParts_ItemDataBound"--%>
                                                                <asp:Repeater ID="repeater1" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblQuantity" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProductCode" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProductName" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblUnitPrice" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblTaxable" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblAmount" runat="server" Text="Label"></asp:Label></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <!-- end description time for rent part -->

                                                <!-- start service part-->
                                                <div class="row bd-grey p-2 mb-3">

                                                    <div class="col-12">
                                                        <h4>Service Detail</h4>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table class="table">

                                                            <thead class="thead-dark">
                                                                <tr>
                                                                    <th scope="col">QUANTITY</th>
                                                                    <th scope="col">PART NUMBER</th>
                                                                    <th scope="col">DESCRIPTION</th>
                                                                    <th scope="col">UNIT PRICE</th>
                                                                    <th scope="col">TAXABLE</th>
                                                                    <th scope="col">AMOUNT</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--OnItemDataBound="repeaterParts_ItemDataBound"--%>
                                                                <asp:Repeater ID="repeaterParts" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblQuantity" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProductCode" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProductName" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblUnitPrice" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblTaxable" runat="server" Text="Label"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblAmount" runat="server" Text="Label"></asp:Label></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <!-- end service part -->
                                                <!-- start service charge-->

                                                <div class="row bd-grey p-2 mb-3">
                                                    <div class="col-12">
                                                    </div>


                                                    <div class="col-5 offset-7">
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">

                                                                    <strong>SUBTOTAL: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtSubTotal" runat="server" CssClass="pl-1"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">

                                                                    <strong>TAX RATE: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtVat" runat="server" CssClass="pl-1" Text="0.07"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">

                                                                    <strong>SALES TAX: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtSaleTax" runat="server" CssClass="pl-1"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">

                                                                    <strong>SERVICE CHARGE: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtServiceCharge" runat="server" CssClass="pl-1"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <%-- <div class="row">
                                                        <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                            <div class="pt-2">

                                                                <strong>Discount: </strong>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                            <div class="pt-2">
                                                                <asp:Label ID="lblDiscount" runat="server" CssClass="pl-1"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 text-right">
                                                                <div class="pt-2">

                                                                    <strong>TOTAL: </strong>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6 col-xl-6 ">

                                                                <div class="pt-2">
                                                                    <asp:Label ID="txtTotal" runat="server" CssClass="pl-1"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                                <!-- end service charge -->

                                                <!-- start Model Information -->
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-center">
                                                        ผู้เช่า
                                                        <br />
                                                        ผู้เช่าตกลงเช่าสินค้าตามรายการและยินยอมในเงื่อไนต่างๆตามใบเสนอราคานี้
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-center">
                                                        <br />
                                                        ADG SCAFFOLDING(THAILAND)
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-center">
                                                        _______________________________________________
                                                        <br />
                                                        ผู้เสนอ / Request
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-center">
                                                        _______________________________________________
                                                         <br />
                                                        ผู้มีอำนาจอนุมัติ / Authorized Signature
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        วันที่ 
                                                    </div>
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-center">
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 หมู่ที่ 1 ตำบลท่าทองหลาง อำเภอบางคล้า จังหวัดฉะเชิงเทรา 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 MOO 1 Tha Thong Lang,Bang Khla, Chachoengsao, 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                </div>
                                                  <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 หมู่ที่ 1 ตำบลท่าทองหลาง อำเภอบางคล้า จังหวัดฉะเชิงเทรา 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 MOO 1 Tha Thong Lang,Bang Khla, Chachoengsao, 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                </div>
                                                  <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 หมู่ที่ 1 ตำบลท่าทองหลาง อำเภอบางคล้า จังหวัดฉะเชิงเทรา 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 MOO 1 Tha Thong Lang,Bang Khla, Chachoengsao, 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                </div>
                                                  <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 หมู่ที่ 1 ตำบลท่าทองหลาง อำเภอบางคล้า จังหวัดฉะเชิงเทรา 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        116/18 MOO 1 Tha Thong Lang,Bang Khla, Chachoengsao, 24110
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                </div>
                                                  <div class="row">
                                                    <div class="col-sm-6 col-lg-6 col-xl-6 text-left">
                                                        โทร 065-2414978 เลขประจำตัวผู้เสียภาษีอากร 0245563002398
                                                    </div>
                                                </div>
                                                <!-- End Model Information -->


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- / Statistics -->
                        </div>
                        <!-- / Content -->

                    </div>
                    <!-- Layout content -->

                </div>
                <!-- / Layout container -->

            </div>
            <!-- Overlay -->
            <div class="layout-overlay layout-sidenav-toggle"></div>
        </div>
        <!-- / Layout wrapper -->


        <!-- Core scripts -->
        <script src="/assets/vendor/libs/popper/popper.js"></script>
        <script src="/assets/vendor/js/bootstrap.js"></script>
        <script src="/assets/vendor/js/sidenav.js"></script>

        <!-- Libs -->
        <script src="/assets/js/ui_modals.js"></script>
    </form>
</body>

</html>
