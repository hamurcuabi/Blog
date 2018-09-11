<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMBlogList.aspx.cs" Inherits="Blog.Admin.ADMBlogList" %>

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
    <link href="/Admin/assets/css/NotificationEffect.css" rel="stylesheet" />
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
                                    <li>Blog
                                    </li>
                                </ul>
                            </div>
                            <div class="page-content">
                                <div class="row">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title smaller">Blog
                                                 <asp:Literal ID="lblCountImages" runat="server"> ( 0 )</asp:Literal>
                                            </h4>
                                            <div id="dvToolboxImages" runat="server" class="widget-toolbox pull-right">
                                                <a href="javascript:void(0)" onclick="ShowModal('/Admin/ADMBlogUpdate.aspx','lg',540,'Yeni Ekle','btnRefreshList')" class="btn btn-sm btn-success"><i class="ace-icon fa fa-plus"></i>Yeni Ekle</a>
                                            </div>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <div class="table-responsive">
                                                    <table class="table table-bordered table-striped table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Resim</th>
                                                                <th>Başlık</th>
                                                                <th>Kategori</th>
                                                                <th class="col-xs-1" colspan="2" style="text-align: center">İşlem</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptrBlog" runat="server" OnItemCommand="rptrBlog_ItemCommand" OnItemDataBound="rptrBlog_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Image ID="imgImage" runat="server" />
                                                                            <asp:HiddenField ID="hdnImage" runat="server" Value='<%#Eval("IMAGE")%>' />
                                                                        </td>
                                                                        <td><%#Eval("TITLE")%></td>
                                                                        <td><%# GetCategorieName(Int32.Parse(Eval("CATEGORIESID").ToString()))%></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnUp" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Up" CssClass="green"><i data-rel="tooltip" title="Yukarı" class="ace-icon fa fa-angle-double-up bigger-200 tooltip-info"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnDown" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="Down" CssClass="green"><i data-rel="tooltip" title="Aşağı" class="ace-icon fa fa-angle-double-down bigger-200 tooltip-info"></i></asp:LinkButton></td>
                                                                        <td>
                                                                            <a href="javascript:void(0)" onclick="ShowModal('/Admin/ADMBlogUpdate.aspx?ID=<%#Eval("ID")%>','lg',540,'Güncelle','btnRefreshList')" class="tooltip-info" data-rel="tooltip" data-original-title="Güncelle"><i class="ace-icon fa fa-pencil green bigger-150"></i></a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnRefreshList" runat="server" Style="display: none;" OnClick="btnRefreshList_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <uc1:UCFooter runat="server" ID="UCFooter" />
        <uc1:UCFooterJS runat="server" ID="UCFooterJS" />
    </form>

</body>
</html>


