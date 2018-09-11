<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMCategoriesUpdate.aspx.cs" Inherits="Blog.Admin.ADMCategoriesUpdate" ValidateRequest="false" %>

<%@ Register Src="~/Admin/UserControl/UCFooterJS.ascx" TagPrefix="uc1" TagName="UCFooterJS" %>
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
<body class="no-skin">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>

        <div class="main-container">
            <div class="main-content">
                <div class="main-content-inner">
                    <div class="page-content">
                        <div class="row">
                            <div class="widget-container-col">
                                <div class="filter">
                                    <div class="widget-body">
                                        <div class="widget-main">

                                            <div class="row">
                                                <table class="table table-striped table-condensed table-bordered">
                                                    <tr>
                                                        <td colspan="2" id="tdImagePreview" runat="server" visible="false">
                                                            <asp:Image ID="imgImage" runat="server" />
                                                        </td>
                                                    </tr>
                                              
                                                    <tr>
                                                        <th>Başlık</th>
                                                        <td>
                                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <th>Aktif</th>
                                                        <td>
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></td>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" style="text-align: right;">
                                                            <a href="javascript:void(0)" runat="server" id="btnCancel" onclick="CloseModal(false);" class="btn btn-sm btn-warning"><i class="ace-icon fa fa-minus-circle"></i>&nbsp;İptal</a>
                                                            <asp:LinkButton ID="btnDelete" Visible="false" OnClick="btnDelete_Click" runat="server" CssClass="btn btn-sm btn-danger btn-delete">
                                                                      <i class="ace-icon fa fa-trash bigger-130 "></i> &nbsp;&nbsp;Sil
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="SaveToHdn();" CssClass="btn btn-sm btn-success"><i class="fa fa-floppy-o"></i> Kaydet</asp:LinkButton></th>
                                                    </tr>
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
        </div>
        <uc1:UCFooterJS runat="server" ID="UCFooterJS" />
    </form>
    <script src="/assets/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InitializePage);
        function InitializePage() {
            document.onkeydown = function (e) {
                if (e == null) { // ie 
                    keycode = event.keyCode;
                } else { // mozilla 
                    keycode = e.which;
                }
                if (keycode == 27) { // escape, close box, esc 
                    CloseModal(false);
                }
            };

            $('#dvDescription').ace_wysiwyg({
                toolbar:
                [
                    'font',
                    null,
                    'fontSize',
                    null,
                    { name: 'bold', className: 'btn-info' },
                    { name: 'italic', className: 'btn-info' },
                    { name: 'strikethrough', className: 'btn-info' },
                    { name: 'underline', className: 'btn-info' },
                    null,
                    { name: 'insertunorderedlist', className: 'btn-success' },
                    { name: 'insertorderedlist', className: 'btn-success' },
                    { name: 'outdent', className: 'btn-purple' },
                    { name: 'indent', className: 'btn-purple' },
                    null,
                    { name: 'justifyleft', className: 'btn-primary' },
                    { name: 'justifycenter', className: 'btn-primary' },
                    { name: 'justifyright', className: 'btn-primary' },
                    { name: 'justifyfull', className: 'btn-inverse' },
                    null,
                    { name: 'createLink', className: 'btn-pink' },
                    { name: 'unlink', className: 'btn-pink' },
                    null,

                    'foreColor',
                    null,
                    'insertImage',
                    { name: 'undo', className: 'btn-grey' },
                    { name: 'redo', className: 'btn-grey' },
                    { name: 'viewSource', className: 'btn-grey' }

                ],
                hotKeys: {

                }

            }).prev().addClass('wysiwyg-style2');
            $('#dvDescription').html($('#hdnDescription').val());
            $('.wysiwyg-choose-file').css('display', 'none');
        }
        function SaveToHdn() {
            $('#hdnDescription').val($('#dvDescription').html());
        }
        InitializePage();
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
