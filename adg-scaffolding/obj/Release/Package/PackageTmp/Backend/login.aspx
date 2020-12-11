<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="adg_scaffolding.Backend.loginXc" %>

<!DOCTYPE html>

<html lang="en" class="default-style">
<head>
    <title>ADG Scaffolding</title>
    <link rel="shortcut icon" href="http://xcite.co.th//img/logo/logo.png" />
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge,chrome=1">
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
    <link rel="icon" type="image/x-icon" href="favicon.ico">
    <link href="https://fonts.googleapis.com/css?family=Muli:300,400,600,700,800" rel="stylesheet">
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
    <script src="/assets/vendor/js/material-ripple.js"></script>
    <script src="/assets/vendor/js/layout-helpers.js"></script>
    <!-- Core scripts -->
    <script src="/assets/vendor/js/pace.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!-- Page -->
    <link rel="stylesheet" href="/assets/css/blog.css">
    <!-- Libs -->
    <link rel="stylesheet" href="/assets/vendor/libs/swiper/swiper.css">
    <link rel="stylesheet" href="/assets/vendor/css/pages/authentication.css">
    <link rel="stylesheet" href="/assets/css/custom.css">

    <script>
        function Login() {
            $('button[id*=btnSigninServer]').trigger('click');
        }

        function WarningAlertWithFocus(msg, id) {
            bootbox.alert({
                message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
                className: 'bootbox-sm text-center',
                callback: function () {
                    setTimeout(function () {
                        if ($(id).val() != '') {
                            id = 'input[id*=txtPassword]';
                        }

                        $(id).focus();

                    }, 10);
                },
            });
        }
    </script>
</head>
<body>
    <div class="page-loader">
        <div class="bg-primary"></div>
    </div>
    <!-- Nav -->
    <nav class="navbar navbar-expand-lg bg-white">
        <div class="container">
            <a href="javascript:void(0);" class="navbar-brand text-large font-weight-bolder line-height-1 py-2">
                <i class="ion ion-md-trending mr-2"></i>ADG Scaffolding
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#header-3">
                <span class="navbar-toggler-icon"></span>
            </button>
        </div>
    </nav>
    <!-- / Nav -->
    <!-- Article's primary image -->
    <div class="ui-rect ui-rect-5 ui-bg-cover mb-5" style="background-color: darkgrey;">
        <div class="row no-gutters">
            <div class="col-md-3 ml-md-auto mr-md-auto">
                <div class="authentication-inner py-5">
                    <div class="card bg-white-95 ">
                        <div class="p-4">
                            <!-- Form -->
                            <form runat="server">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div class="modal modal-fill-in fade show" style="display: block; padding-right: 17px;">
                                            <div class="modal-dialog modal">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div class="progress m-3">
                                                            <div class="progress-bar" style="width: 200%">กำลังดำเนินการ ...</div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-backdrop fade show"></div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label class="form-label">Username</label>
                                            <asp:TextBox ID="txtUsername" class="form-control" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label d-flex justify-content-between align-items-end">
                                                <div>Password</div>
                                            </label>
                                            <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server" />
                                        </div>

                                        <div class="d-flex justify-content-between align-items-center m-0">
                                            <%--<label class="custom-control custom-checkbox m-0">
                                        <input type="checkbox" class="custom-control-input">
                                        <span class="custom-control-label">Remember me</span>
                                    </label>--%>
                                            <button type="button" class="btn btn-primary" onclick="Login();">Sign In</button>
                                            <button id="btnSigninServer" runat="server" class="invisible" onserverclick="btnSignin_Click"></button>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </form>
                            <!-- / Form -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Footer -->
    <nav class="footer bg-white">
        <div class="container text-center py-3">
            <div class="pb-3">
                <a href="javascript:void(0)" class="footer-text font-weight-bolder">ADG Scaffolding</a> &copy; &nbsp;All rights reserved
            </div>
        </div>
    </nav>
    <!-- / Footer -->
    <!-- Core scripts -->
    <script src="/assets/vendor/libs/popper/popper.js"></script>
    <script src="/assets/vendor/js/bootstrap.js"></script>
    <!-- Libs -->
    <script src="/assets/vendor/libs/swiper/swiper.js"></script>
    <!-- Page -->
    <script src="/assets/js/blog.js"></script>
    <script src="/assets/vendor/libs/bootbox/bootbox.js"></script>
</body>

</html>
