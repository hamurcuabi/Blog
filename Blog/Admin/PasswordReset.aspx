<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="Blog.Admin.PasswordReset" %>

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
    <link rel="stylesheet" href="/Admin/components/ModalEffect/modal-effect.css" />
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

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <div class="main-container">
                    <div class="main-content">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1">
                                <div class="login-container">
                                    <div class="center">
                                        <h1>
                                            <span class="grey">Soner Kadıköylü<br />
                                            </span>
                                            <span class="red">Admin Paneli</span>
                                        </h1>
                                    </div>
                                    <div class="space-6"></div>
                                    <div class="position-relative">
                                        <div id="login-box" class="login-box visible widget-box no-border">
                                            <div class="widget-body">

                                                <div class="widget-main">
                                                    <h4 class="header blue lighter bigger">Yeni Şifrenizi Giriniz
                                                    </h4>

                                                    <div class="space-6"></div>

                                                    <fieldset>
                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtPasword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Şifre"></asp:TextBox>
                                                                <i class="ace-icon fa fa-lock"></i>
                                                            </span>
                                                        </label>

                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox ID="txtPasswordAgain" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre Tekrarı"></asp:TextBox>
                                                                <i class="ace-icon fa fa-lock"></i>
                                                            </span>
                                                        </label>

                                                        <div class="space"></div>

                                                        <div class="clearfix">

                                                            <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="width-35 pull-right btn btn-sm btn-primary">
                                                            <i class="ace-icon fa fa-save"></i>
															<span class="bigger-110">
                                                               Kaydet
															</span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="space-4"></div>
                                                    </fieldset>


                                                </div>

                                                <!-- /.widget-main -->


                                            </div>
                                            <!-- /.widget-body -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <uc1:UCFooterJS runat="server" ID="UCFooterJS" />
    </form>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InitializePage);
        function InitializePage() {

        }
        function Redirect(url) {
            setTimeout(function () { window.location = url; }, 10000);
        }

        InitializePage();

    </script>
</body>
</html>

