﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUsMail.aspx.cs" Inherits="Blog.ContactUsMail" %>

<%@ Import Namespace="Blog.Code" %>


<%@ Register Src="~/Controler/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Controler/Header4.ascx" TagPrefix="uc1" TagName="Header4" %>
<%@ Register Src="~/Controler/SliderContactUs.ascx" TagPrefix="uc1" TagName="SliderContactUs" %>




<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>PsikoBlog | İletişim</title>    
    <!-- Plugins CSS -->
    <link href="css/plugins/plugins.css" rel="stylesheet">
    <link type="text/css" rel="stylesheet" href="smart-form/contact/css/smart-forms.css">
    <link href="css/style.css" rel="stylesheet">
      
        <!-- REVOLUTION STYLE SHEETS -->
        <link rel="stylesheet" type="text/css" href="revolution/css/settings.css">
        <!-- REVOLUTION LAYERS STYLES -->
        <link rel="stylesheet" type="text/css" href="revolution/css/layers.css">
        <!-- REVOLUTION NAVIGATION STYLES -->
        <link rel="stylesheet" type="text/css" href="revolution/css/navigation.css">
        <link rel='stylesheet' href='revolution/revolution-addons/particles/css/revolution.addon.particles.css?ver=1.0.3' type='text/css'>
        <!-- load css for cubeportfolio -->
        <link rel="stylesheet" type="text/css" href="cubeportfolio/css/cubeportfolio.min.css">
        <link href="css/style.css" rel="stylesheet">
</head>

<body data-spy="scroll" data-darget=".navbar-seconday">
    <div id="preloader">
        <div id="preloader-inner"></div>
    </div>
    <!--/preloader-->

    <!-- Site Overlay -->
    <div class="site-overlay"></div>
    <uc1:Header4 runat="server" ID="Header4" />
   <uc1:SliderContactUs runat="server" id="SliderContactUs" />
    

    <div class="container">
         <div class="container mb30">
            <div class="row">
                <div class="col-md-9" >
                    <br />
                    <br />
                    
                    <div class="smart-wrap ">
                        <div class="smart-forms smart-container">
                               <div class="col-sm-6 ml-auto mr-auto">
                            <div class="media white-card">
                               <i class="fa fa-check fa-4x text-success mb0 align-self-center d-flex mr-3"></i>
                                <div class="media-body">
                                    <h4 class="mb20 mb-1 text-white">
                                    Mesajınız İletildi! En Kısa Sürede Size Geri Dönüş Yapacağım. 
                                    </h4>
                                    <a href='/Anasayfa' class='btn btn-white-outline'>Ana Sayfa</a>
                                </div>
                            
                        </div>
                        
                    </div>
                           
                        </div>
                        <!-- end .smart-forms section -->
                    </div>
                    <!-- end .smart-wrap section -->
                </div>
               
                 <div class="col-md-3 mb40">
                    <div class="mb40">
                        <form >
                            <div class="input-group">
                                <input type="text" class="form-control" placeholder="Arama..." aria-describedby="basic-addon2">
                                <button class="input-group-addon" id="basic-addon2"><i class="ti-search"></i></button>
                            </div>
                        </form>
                         
                    </div><!--/col-->
                    <div class="mb40">
                        <h4 class="sidebar-title">Kategoriler</h4>
                        <ul class="list-unstyled categories">
                            <asp:Repeater ID="rptrCategories" runat="server">
                             <ItemTemplate>
                                 <li>
                                   <a href="<%#string.Format("kategoriler/{0}",Helper.SEOUrl(Eval("TITLE").ToString()))%>">  <%# Eval("TITLE")%>  (<%# (GetCountBlog(Eval("ID").ToString()))%>)</a>
                               </li> 
                             </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div><!--/col-->
                    <div>
                        <h4 class="sidebar-title">Son Paylaşımlar</h4> 
                        <ul class="list-unstyled categories">
                            <asp:Repeater runat="server" ID="rptLastPost"><ItemTemplate>
                            <li class="list-unstyled categories">
                                    <h6 class="mt-0 mb-1"><a href='<%#string.Format("/blog/{0}",Helper.SEOUrl(Eval("TITLE").ToString()))%>'"><%# Eval("TITLE") %></a></h6>
                            </li>
                       </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        </div>

        <uc1:Footer runat="server" ID="Footer" />
        <!--Footer-->
        <!--back to top-->
        <a href="#" class="back-to-top hidden-xs-down" id="back-to-top"><i class="ti-angle-up"></i></a>
        <!-- jQuery first, then Tether, then Bootstrap JS. -->
        <script src="js/plugins/plugins.js"></script>
        <script src="js/assan.custom.js"></script>

        <!--smart-form script-->
        <script src="smart-form/contact/js/jquery.form.min.js" type="text/javascript"></script>
        <script src="smart-form/contact/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="smart-form/contact/js/additional-methods.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="smart-form/contact/js/smart-form.js"></script>
          <!-- REVOLUTION JS FILES -->
        <script type="text/javascript" src="revolution/js/jquery.themepunch.tools.min.js"></script>
        <script type="text/javascript" src="revolution/js/jquery.themepunch.revolution.min.js"></script>
        <!-- PARTICLES ADD-ON FILES -->
        <script type='text/javascript' src='revolution/revolution-addons/particles/js/revolution.addon.particles.min.js?ver=1.0.3'></script>
        <!-- SLIDER REVOLUTION 5.0 EXTENSIONS  (Load Extensions only on Local File Systems !  The following part can be removed on Server for On Demand Loading) -->	
    <script src="/js/SomeeAdsRemover.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.actions.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.carousel.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.kenburn.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.layeranimation.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.migration.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.navigation.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.parallax.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.slideanims.min.js"></script>
        <script type="text/javascript" src="revolution/js/extensions/revolution.extension.video.min.js"></script>

        <script>
            /**Hero particle script**/
            var tpj = jQuery;
            var revapi8;
            tpj(document).ready(function () {
                if (tpj("#rev_slider_8_1").revolution == undefined) {
                    revslider_showDoubleJqueryError("#rev_slider_8_1");
                } else {
                    revapi8 = tpj("#rev_slider_8_1").show().revolution({
                        sliderType: "hero",
                        jsFileLocation: "../revolution/js/",
                        sliderLayout: "fullscreen",
                        dottedOverlay: "none",
                        delay: 9000,
                        particles: {
                            startSlide: "first", endSlide: "last", zIndex: "1",
                            particles: {
                                number: { value: 300 }, color: { value: "#000000" },
                                shape: {
                                    type: "circle", stroke: { width: 0, color: "#ffffff", opacity: 1 },
                                    image: { src: "" }
                                },
                                opacity: { value: 0.1, random: false, min: 0.25, anim: { enable: false, speed: 1, opacity_min: 0, sync: false } },
                                size: { value: 1, random: true, min: 0.5, anim: { enable: false, speed: 40, size_min: 1, sync: false } },
                                line_linked: { enable: true, distance: 80, color: "#000000", opacity: 0.35, width: 1 },
                                move: { enable: true, speed: 1, direction: "right", random: true, min_speed: 3, straight: false, out_mode: "out" }
                            },
                            interactivity: {
                                events: { onhover: { enable: true, mode: "repulse" }, onclick: { enable: true, mode: "bubble" } },
                                modes: { grab: { distance: 400, line_linked: { opacity: 0.5 } }, bubble: { distance: 400, size: 100, opacity: 1 }, repulse: { distance: 75 } }
                            }
                        },
                        navigation: {
                        },
                        responsiveLevels: [1240, 1024, 778, 480],
                        visibilityLevels: [1240, 1024, 778, 480],
                        gridwidth: [1240, 1024, 778, 480],
                        gridheight: [868, 768, 960, 720],
                        lazyType: "none",
                        parallax: {
                            type: "scroll",
                            origo: "slidercenter",
                            speed: 400,
                            levels: [5, 10, 15, 20, 25, 30, 35, 40, 45, 46, 47, 48, 49, 50, 0, 55]
                        },
                        shadow: 0,
                        spinner: "spinner0",
                        autoHeight: "off",
                        fullScreenAutoWidth: "off",
                        fullScreenAlignForce: "off",
                        fullScreenOffsetContainer: "",
                        fullScreenOffset: "60px",
                        disableProgressBar: "on",
                        hideThumbsOnMobile: "off",
                        hideSliderAtLimit: 0,
                        hideCaptionAtLimit: 0,
                        hideAllCaptionAtLilmit: 0,
                        debugMode: false,
                        fallbacks: {
                            simplifyAll: "off",
                            disableFocusListener: false
                        }
                    });
                }

                RsParticlesAddOn(revapi8);
            });	/*ready*/
        </script> 
        <!-- load cubeportfolio -->
        <script type="text/javascript" src="cubeportfolio/js/jquery.cubeportfolio.min.js"></script>
        <script>
            //testimonials
            (function ($, window, document, undefined) {
                'use strict';

                // init cubeportfolio
                $('#js-grid-slider-testimonials').cubeportfolio({
                    layoutMode: 'slider',
                    drag: true,
                    auto: false,
                    autoTimeout: 5000,
                    autoPauseOnHover: true,
                    showNavigation: true,
                    showPagination: true,
                    rewindNav: true,
                    scrollByPage: false,
                    gridAdjustment: 'responsive',
                    mediaQueries: [{
                        width: 0,
                        cols: 1
                    }],
                    gapHorizontal: 0,
                    gapVertical: 0,
                    caption: '',
                    displayType: 'default'
                });
            })(jQuery, window, document);
            //thumbnail slider
            (function ($, window, document, undefined) {
                'use strict';

                // init cubeportfolio
                $('#js-grid-slider-thumbnail').cubeportfolio({
                    layoutMode: 'slider',
                    drag: true,
                    auto: false,
                    autoTimeout: 5000,
                    autoPauseOnHover: true,
                    showNavigation: false,
                    showPagination: false,
                    rewindNav: true,
                    scrollByPage: true,
                    gridAdjustment: 'responsive',
                    mediaQueries: [{
                        width: 0,
                        cols: 1
                    }],
                    gapHorizontal: 0,
                    gapVertical: 0,
                    caption: '',
                    displayType: 'fadeIn',
                    displayTypeSpeed: 400,
                    plugins: {
                        slider: {
                            pagination: '#js-pagination-slider',
                            paginationClass: 'cbp-pagination-active'
                        }
                    }
                });
            })(jQuery, window, document);
        </script>
</body>
</html>
