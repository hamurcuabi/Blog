<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Blog.Admin.Login" %>

<%@ Register Src="~/Admin/UserControl/UCFooterJS.ascx" TagPrefix="uc1" TagName="UCFooterJS" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="/Admin/assets/css/bootstrap.css" />
    <link rel="stylesheet" href="/Admin/components/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace-fonts.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace-rtl.css" />
   <link href="/Admin/assets/css/NotificationEffect.css" rel="stylesheet" />
    <!--[if lte IE 9]>
		<link rel="stylesheet" href="/assets/css/ace-part2.css" />
        <link rel="stylesheet" href="/assets/css/ace-ie.css" />
	<![endif]-->
    <!--[if lte IE 8]>
	    <script src="/components/html5shiv/dist/html5shiv.min.js"></script>
	    <script src="/components/respond/dest/respond.min.js"></script>
	<![endif]-->
</head>
<body class="login-layout light-login">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <div class="main-container">
            <div class="main-content">
                <div class="row">
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="login-container">
                            <div class="center">
                                <h1>
                                    <span class="text-primary font700">Mehmet Saban<br /></span>
                                    <span class="red">Admin Paneli</span>
                                </h1>
                            </div>
                            <div class="space-6"></div>
                            <div class="position-relative">
                                <div id="login-box" class="login-box visible widget-box no-border">
                                    <div class="widget-body">
                                        <asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="widget-main">
                                                    <h4 class="header blue lighter bigger">Kullanıcı bilgilerinizi giriniz
                                                    </h4>

                                                    <div class="space-6"></div>

                                                    <fieldset>
                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtLoginName" runat="server" CssClass="form-control" placeholder="Mail Adresi"></asp:TextBox>
                                                                <i class="ace-icon fa fa-user"></i>
                                                            </span>
                                                        </label>

                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre"></asp:TextBox>
                                                                <i class="ace-icon fa fa-lock"></i>
                                                            </span>
                                                        </label>

                                                        <div class="space"></div>

                                                        <div class="clearfix">
                                                            <label class="inline">
                                                                <input name="form-field-radio" type="checkbox" class="ace input-lg" id="chkRemember" runat="server" />
                                                                <span class="lbl">Beni Hatırla </span>
                                                            </label>

                                                            <asp:LinkButton ID="btnLogin" runat="server" OnClick="btnLogin_Click"  CssClass="width-35 pull-right btn btn-sm btn-primary">
                                                            <i class="ace-icon fa fa-key"></i>
															<span class="bigger-110">
                                                               Giriş
															</span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="space-4"></div>
                                                    </fieldset>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="toolbar clearfix">
                                            <div>
                                                <a href="#" data-target="#forgot-box" class="forgot-password-link">
                                                    <i class="ace-icon fa fa-arrow-left"></i>
                                                    Şifremi Unuttum
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.login-box -->

                                <div id="forgot-box" class="forgot-box widget-box no-border">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <h4 class="header red lighter bigger">Şifre Yenileme
                                            </h4>

                                            <div class="space-6"></div>


                                            <fieldset>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <asp:TextBox ID="txtForgotPassworLoginName" runat="server" CssClass="form-control" AutoCompleteType="Email" placeholder="Mail Adresi"></asp:TextBox>
                                                        <i class="ace-icon fa fa-envelope"></i>
                                                    </span>
                                                </label>

                                                <div class="clearfix">
                                                    <asp:LinkButton ID="btnForget" runat="server" OnClick="btnForget_Click" CssClass="width-35 pull-right btn btn-sm btn-danger">
                                                            <i class="ace-icon fa fa-send icon-on-right"></i>
															<span class="bigger-110">
                                                              Gönder
															</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <!-- /.widget-main -->

                                        <div class="toolbar center">
                                            <a href="#" data-target="#login-box" class="back-to-login-link">Giriş Yap
												<i class="ace-icon fa fa-arrow-right"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.forgot-box -->
                            </div>


                        </div>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.main-content -->
        </div>
        <uc1:UCFooterJS runat="server" id="UCFooterJS" />
    </form>
    
    <script type="text/javascript">
        jQuery(function ($) {
            $('#txtPhone').mask('(999) 999-9999');
            $(document).on('click', '.toolbar a[data-target]', function (e) {
                e.preventDefault();
                var target = $(this).data('target');
                $('.widget-box.visible').removeClass('visible');//hide others
                $(target).addClass('visible');//show target
            });
        });
    </script>
     <script>
         function button_click(objTextBox, objBtnID) {
             if (window.event.keyCode == 13) {
                 document.getElementById(objBtnID).focus();
                 document.getElementById(objBtnID).click();
             }
         }
    </script>
</body>
</html>
