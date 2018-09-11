<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Blog.Admin.MainPage" %>

<%@ Register Src="~/Admin/UserControl/UCFooter.ascx" TagPrefix="uc1" TagName="UCFooter" %>
<%@ Register Src="~/Admin/UserControl/UCFooterJS.ascx" TagPrefix="uc1" TagName="UCFooterJS" %>
<%@ Register Src="~/Admin/UserControl/UCHeader.ascx" TagPrefix="uc1" TagName="UCHeader" %>
<%@ Register Src="~/Admin/UserControl/UCLeftMenu.ascx" TagPrefix="uc1" TagName="UCLeftMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="/Admin/assets/css/jquery-ui.css" />
    <link rel="stylesheet" href="/Admin/assets/css/bootstrap.css" />
    <link rel="stylesheet" href="/Admin/components/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace-fonts.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace.css" />
    <link rel="stylesheet" href="/Admin/assets/css/ace-rtl.css" />
    <link rel="stylesheet" href="/Admin/components/ModalEffect/modal-effect.css" />
    <link rel="stylesheet" href="/Admin/assets/css/datepicker.css" />
    <!--[if lte IE 9]>
		<link rel="stylesheet" href="/assets/css/ace-part2.css" />
        <link rel="stylesheet" href="/assets/css/ace-ie.css" />
	<![endif]-->
    <!--[if lte IE 8]>
	    <script src="/components/html5shiv/dist/html5shiv.min.js"></script>
	    <script src="/components/respond/dest/respond.min.js"></script>
	<![endif]-->
    <link rel="stylesheet" href="/Admin/assets/css/Custom.css" />
</head>
<body class="no-skin ">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <uc1:UCHeader runat="server" ID="UCHeader" />
                <div class="main-container" id="main-container">
                    <script type="text/javascript">
                        try { ace.settings.check('main-container', 'fixed') } catch (e) { }
                    </script>
                    <uc1:UCLeftMenu runat="server" ID="UCLeftMenu" />
                    <div class="main-content">
                        <div class="main-content-inner">
                            <div class="breadcrumbs" id="breadcrumbs">
                                <script type="text/javascript">
                                    try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                                </script>
                                <ul class="breadcrumb">
                                    <li>
                                        <a href="/AdminMainPage">
                                            <i class="ace-icon fa fa-home home-icon"></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="page-content">
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnRefreshList" runat="server" Style="display: none;" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <uc1:UCFooter runat="server" ID="UCFooter" />
        <uc1:UCFooterJS runat="server" ID="UCFooterJS" />
    </form>

</body>
</html>

