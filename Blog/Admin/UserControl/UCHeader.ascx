<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCHeader.ascx.cs" Inherits="Blog.Admin.UserControl.UCHeader" %>

<div id="navbar" class="navbar navbar-default ace-save-state ">
	<div class="navbar-container" id="navbar-container">
		<button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler" data-target="#sidebar">
			<span class="sr-only">Toggle sidebar</span>

			<span class="icon-bar"></span>

			<span class="icon-bar"></span>

			<span class="icon-bar"></span>
		</button>

		<div class="navbar-header pull-left>">
			<a href="/AdminMainPage" class="navbar-brand" style="padding-top:2px;padding-bottom:0px;">
                
				<small>
                    Blog 
                        <span class="menu-text" style="color:black">
                            | Admin Panel
                    </span>
				</small>
			</a>
            
		</div>
		<div class="navbar-buttons navbar-header pull-right" role="navigation">
			<ul class="nav ace-nav">
				<li id="liOnlineUser" runat="server" visible="true" class="light-blue dropdown-modal" >
					<a data-toggle="dropdown" href="#" class="dropdown-toggle">
						
						<span class="user-info">
							<small>Hoşgeldiniz</small>
							<asp:Literal ID="lblMemberName" runat="server"></asp:Literal>
						</span>
						<i class="ace-icon fa fa-caret-down"></i>
					</a>
					<ul class="user-menu dropdown-menu-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
						<li>
                            <asp:LinkButton ID="btnLogout" OnClick="btnLogout_Click" runat="server">Güvenli Çıkış</asp:LinkButton>
						</li>
					</ul>
				</li>
               
			</ul>
		</div>
	</div>
</div>
