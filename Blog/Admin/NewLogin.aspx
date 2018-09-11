<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewLogin.aspx.cs" Inherits="Blog.Admin.NewLogin" %>

<%@ Register Src="~/Admin/UserControl/UCFooterJS.ascx" TagPrefix="uc1" TagName="UCFooterJS" %>


<!DOCTYPE html>
<html lang="en">
    <head>
        <!-- Required meta tags -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <title>PsikoBlog | Admin</title>    
        <!-- Plugins CSS -->
        <link href="Admin/newlogin/plugins/plugins.css" rel="stylesheet"> 
        <link href="Admin/newlogin/smart-forms.css" rel="stylesheet">
        <link href="Admin/newlogin/style.css" rel="stylesheet">
    </head>

    <body class="layout-accounts">

         
        <div class="account-wrap">
            <div class="account-card">
                <div class="account-content smart-forms">
                    <div class="text-center">
                        <a href="#"><img src="../images/logo.png" /></a>
                    </div>
                    <form runat="server" id="form1">
                        <div class="form-body">

                            <div class="spacer-b30">
                                <div class="tagline"><span><b>M.Saban PsİkoBlog</b></span></div><!-- .tagline -->
                            </div>                 

                            <div class="spacer-t30 spacer-b30">
                                <div class="tagline"><span> AdmİN GİRİŞİ</span></div><!-- .tagline -->
                            </div>
                           
                            <div class="section">
                                <label class="field prepend-icon">
                                     <asp:TextBox ID="txtLoginName" runat="server" CssClass="gui-input" TextMode="Email" placeholder="Emailniz"></asp:TextBox>
                                    <span class="field-icon"><i class="fa fa-user"></i></span>  
                                </label>
                            </div><!-- end section -->                    

                            <div class="section">
                                <label class="field prepend-icon">
                                     <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="gui-input" placeholder="Şifre"></asp:TextBox>
                                    <span class="field-icon"><i class="fa fa-lock"></i></span>  
                                </label>
                            </div><!-- end section -->  

                            <div class="section">
                                <label class="switch block">
                                    <input type="checkbox" name="remember" id="chkRemember" checked runat="server" />
                                    <span class="switch-label" for="remember" data-on="YES" data-off="NO"></span> 
                                   
                                    <span>Beni Hatırla</span>
                                </label>
                            </div><!-- end section -->                                                           

                        </div><!-- end .form-body section -->
                        <div class="form-footer">
                            <asp:LinkButton ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="btn btn-primary btn-block">
															<span class="bigger-110">
                                                               Giriş
															</span>
                                                            </asp:LinkButton>
                        </div><!-- end .form-footer section -->
                                               
                          
                        <div class="spacer-t30 spacer-b30 pt20">
                            <div class="tagline"><span>EmrHmrc</span></div><!-- .tagline -->
                        </div>
                       
                    </form>

                </div>
            </div>
        </div>
<uc1:UCFooterJS runat="server" ID="UCFooterJS" />
        <!-- jQuery first, then Tether, then Bootstrap JS. -->
        <script type="text/javascript" src="Admin/newlogin/plugins.js"></script> 
        <script type="text/javascript" src="Admin/newlogin/assan.custom.js"></script> 
    </body>
</html>


