<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header5.ascx.cs" Inherits="Blog.Controler.Header5" %>
     <nav class="navbar navbar-expand-lg navbar-light navbar-transparent bg-faded nav-sticky">
 
            <div class="container">

                <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <a class="navbar-brand" href="/Anasayfa"><img src="../images/logo.png" alt="">
                    <img src="../images/logo-light.png" alt="">
                </a>
                <div  id="navbarNavDropdown" class="navbar-collapse collapse">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item dropdown ">
                            <a class="nav-link " aria-haspopup="true" style="color:black" aria-expanded="false" href="/Anasayfa">ANA SAYFA</a>
                        </li>
                          <li class="nav-item dropdown ">
                            <a class="nav-link " aria-haspopup="true" aria-expanded="false" style="color:black" href="/Hakkımda">HAKKIMDA</a>
                        </li>
                          <li class="nav-item dropdown active">
                            <a class="nav-link " aria-haspopup="true" aria-expanded="false" href="/Blog" style="color:darkblue">BLOG</a>
                        </li>
                          <li class="nav-item dropdown ">
                            <a class="nav-link " aria-haspopup="true" aria-expanded="false" href="/Iletisim" style="color:black">İLETİŞİM</a>
                        </li>
                         
                       
                  </ul>
                    </div>
            </div>
        </nav>
