/*
  javascript file.
* Author URI: https://themeforest.net/user/colorfuldesign
*/

(function ($) { "use strict";

    $(document).ready(function () {

      // Page Loader // --------------------------
      setTimeout(function(){
      		$('body').addClass('loaded');
    	}, 3000);

      // Mobile Menu Title // --------------------------

      var trigger = $('.nav-mobile-btn'),
        overlay = $('.overlay'),
        $navMobile = $("#nav-mobile"),
         isClosed = false;

        trigger.click(function () {
          navmobilebtn_cross();
        });

          function navmobilebtn_cross() {

            if (isClosed == true) {
              overlay.hide();
              trigger.removeClass('is-open');
              trigger.addClass('is-closed');
              isClosed = false;
            } else {
              overlay.show();
              trigger.removeClass('is-closed');
              trigger.addClass('is-open');
              isClosed = true;
            }

        }

        $('[data-toggle="offcanvas"]').click(function () {
              $('#wrapper').toggleClass('toggled');
        });

        // Search Mobil Section //  -----------------

        var s_trigger = $('.search-mobile-btn'),
        overlay = $('.overlay'),
        $searchMobile = $(".search-module-inside"),
         s_isClosed = false;
        s_trigger.click(function () {
          s_mobilebtn_cross();
        });

          function s_mobilebtn_cross() {

            if (s_isClosed == true) {
              overlay.hide();
              s_trigger.removeClass('s-is-open');
              s_trigger.addClass('s-is-closed');
              s_isClosed = false;
            } else {
              overlay.show();
              s_trigger.removeClass('s-is-closed');
              s_trigger.addClass('s-is-open');
              s_isClosed = true;
            }

        }

        $('[data-toggle="s-offcanvas"]').click(function () {
              $('#wrapper').toggleClass('search-toggled');
        });

          var $NavSection = $('#nav-section');
          var $NavButtons = $('body');
          $NavSection.waypoint('sticky');
          $('.home-page').waypoint(function (dir) {
              if (dir === "down") {
                  $NavSection.addClass('navshrink');
              } else {
                  $NavSection.removeClass('navshrink');
              }
          }, { offset: -450 });
          $('.default-page').waypoint(function (dir) {
              if (dir === "down") {
                  $NavSection.addClass('navshrink');
              } else {
                  $NavSection.removeClass('navshrink');
              }
          }, { offset: -450 });


          // Property items Section //  -----------------

        var $gridItems = $("#p-showTiles");
        var $listItems = $("#p-showList");
        var $showProperty = $("#property-listing");

        if($gridItems && $listItems){
            $gridItems.on("click", function(e){
                e.preventDefault();

                if(!$gridItems.hasClass("active"))
                {
                    var $existListClass = $(".list-style");

                    if($existListClass){
                        $showProperty.removeClass("list-style");
                        $showProperty.addClass("grid-style");
                        $showProperty.css("display", "none");
                        $showProperty.fadeIn();

                        $(this).addClass("active");
                        $listItems.removeClass("active");
                    }
                }
            });

            $listItems.on("click", function(e){
                e.preventDefault();

                if(!$listItems.hasClass("active")){
                    var $existDefaultClass = $(".glist-style");

                    if($existDefaultClass){
                        $showProperty.removeClass("grid-style");
                        $showProperty.addClass("list-style");
                        $showProperty.css("display", "none");
                        $showProperty.fadeIn();

                        $(this).addClass("active");
                        $gridItems.removeClass("active");
                    }
                }
            });
        }


    // Single Property Item Slide  // --------------------------
    //     START   //
      var sync1 = $("#item-images");
      var sync2 = $("#item-thumbs");
      var slidesPerPage = 4; //globaly define number of elements per page
      var syncedSecondary = true;

      sync1.owlCarousel({
        items:1,
        loop:true,
        smartSpeed: 1000,
        nav:true,
        dots:false,
        responsiveRefreshRate : 200,
        responsive:{
          0:{items:1},
         },
        navText : ["<i class='owl-prev-icon owl-button-icons'></i>","<i class='owl-next-icon owl-button-icons'></i>"],
      }).on('changed.owl.carousel', syncPosition);

      sync2
      .on('initialized.owl.carousel', function () {
           sync2.find(".owl-item").eq(0).addClass("current");
       })

      .owlCarousel({
        items : slidesPerPage,
        nav:false,
        dots:true,
        smartSpeed: 600,
        slideSpeed : 500,
        responsive:{
           0:{items:4},
           479:{items:4},
           768:{items:4},
           979:{items:5},
           1199:{items:5},
         },
        responsiveRefreshRate : 100,

      }).on('changed.owl.carousel', syncPosition2);

      function syncPosition(el) {
          //if you set loop to false, you have to restore this next line
          //var current = el.item.index;

          //if you disable loop you have to comment this block
          var count = el.item.count-1;
          var current = Math.round(el.item.index - (el.item.count/2) - .5);

          if(current <= 0) {
            current = count;
          }
          if(current >= count) {
            current = 0;
          }

          //end block

            sync2
              .find(".owl-item")
              .removeClass("current")
              .eq(current)
              .addClass("current");
            var onscreen = sync2.find('.owl-item.active').length - 1;
            var start = sync2.find('.owl-item.active').first().index();
            var end = sync2.find('.owl-item.active').last().index();

            if (current >= end) {
              sync2.data('owl.carousel').to(current, 300, true);
            }
            if (current <= start) {
              sync2.data('owl.carousel').to(current - onscreen, 300, true);
            }
          }

      function syncPosition2(el) {
        if(syncedSecondary) {
          var number = el.item.index;
          sync1.data('owl.carousel').to(number, 400, true);
        }
      }

      sync2.on("click", ".owl-item", function(e){
        e.preventDefault();
        var number = $(this).index();
        sync1.data('owl.carousel').to(number, 600, true);
      });
      //  END  //

        // Show More // ---------------------
        $('.show-more-button').on('click', function (e) {
    			e.preventDefault();
    			$('.show-more').toggleClass('visible');
    		});

         //  TABS // --------------------------
        $('.tabs .tab-links a').on('click', function(e)  {
            var currentAttrValue = $(this).attr('href');

            // Show/Hide Tabs
            $('.tabs ' + currentAttrValue).show().siblings().hide();

            // Change/remove current tab to active
            $(this).parent('li').addClass('active').siblings().removeClass('active');

            e.preventDefault();
        });


        // Adds ios class to html tag // --------------------------
          var deviceAgent = navigator.userAgent.toLowerCase();
          var agentID = deviceAgent.match(/(iphone|ipod|ipad)/);
              if (agentID) {

            $('.video-background').addClass('ios');
            $('body').addClass('nav-ios');

          };


          // Progress bars // --------------------------
           $('.progress .progress-bar').progressbar({display_text: 'fill'});


         // Datepiker: Format Date Time
         ///  https://eonasdan.github.io/bootstrap-datetimepicker/
            $('#dp-time').datetimepicker({
                format: 'LT'
            });
            $('#dp-date').datetimepicker( {

              format: 'DD/MM/YYYY'
            });

          // Footer Button to Top // --------------------------
          //
           $('.scrollTopButton').on("click", function () {
               $("body,html").animate({scrollTop: 0}, 1200);
               return false;
           });

           // Property Expert// --------------------------
           $(".owl-property-expert").owlCarousel({
               loop:true,
               nav:true,
               dots:false,
               autoplay:true,
               autoplayHoverPause: true, // Stops autoplay
               smartSpeed: 800,
               autoplayTimeout:8000,
               navText : ["<i class='owl-prev-icon fa fa-chevron-left'></i>","<i class='owl-next-icon fa fa-chevron-right'></i>"],
               responsive:{
                   0:{items:1},
                },
           });

           // Thef Team // --------------------------
           $(".owl-featured").owlCarousel({
             loop:true,
             nav:true,
             dots:false,
             autoplay:true,
             autoplayHoverPause: true, // Stops autoplay
             smartSpeed: 800,
             autoplayTimeout:8000,
             navText : ["<i class='owl-prev-icon fa fa-chevron-left'></i>","<i class='owl-next-icon fa fa-chevron-right'></i>"],
             responsive:{
                 0:{items:1},
                 479:{items:2},
                 768:{items:2},
                 979:{items:3},
                 1199:{items:4}
              },
           });

            // Persons Team // --------------------------
            $(".owl-persons").owlCarousel({
              loop:true,
              nav:true,
              dots:false,
              autoplay:true,
              autoplayHoverPause: true, // Stops autoplay
              smartSpeed: 800,
              autoplayTimeout:8000,
              navText : ["<i class='owl-prev-icon fa fa-chevron-left'></i>","<i class='owl-next-icon fa fa-chevron-right'></i>"],
              responsive:{
                  0:{items:1},
                  479:{items:2},
                  768:{items:2},
                  979:{items:3},
                  1199:{items:4}
               },
            });

           // Testimonials // --------------------------
           $(".owl-testimonials").owlCarousel({
             loop:true,
             nav:false,
             dots:true,
             autoplay:true,
             autoplayHoverPause: true, // Stops autoplay
             smartSpeed: 800,
             autoplayTimeout:8000,
             navText : ["<i class='owl-prev-icon owl-button-icons'></i>","<i class='owl-next-icon owl-button-icons'></i>"],
             responsive:{
                 0:{items:1},
              },
           });

           // Our Partners // --------------------------
           $(".owl-ourPartners").owlCarousel({
             loop:true,
             nav:false,
             dots:true,
             autoplay:true,
             smartSpeed: 800,
             slideSpeed : 800,
             navText : ["<i class='owl-prev-icon fa fa-chevron-left'></i>","<i class='owl-next-icon fa fa-chevron-right'></i>"],
             responsive:{
                 0:{items:1},
                 479:{items:2},
                 768:{items:3},
                 979:{items:4},
                 1199:{items:5}
              },
            });

           // ISOTOPE//--------------------------

          if($('.menu-items-list').length){
              var defaultFilter=$('.tagsort-active')
              .attr('data-filter');

              var $grid=$('.menu-items-list')

              .isotope({itemSelector:'.menu-item',layoutMode:'fitRows',filter:defaultFilter});

              $('.menu-button-filter').on('click','li',function(){

              $('.menu-button-filter li').removeClass('tagsort-active');

              $(this).toggleClass('tagsort-active');

              var filterValue=$(this).attr('data-filter');

                $grid.isotope({filter:filterValue})
              ;}
            );
          };

          // XPRO Slide Background //

          $( ".xpro-slider" ).each(function() {
            var attr = $(this).attr('data-minHeight');
            var $height = $(this);
            if (typeof attr !== typeof undefined && attr !== false) {
              $(this).css('min-height', ' '+attr+' ');
              $height.css({'height': '100%'
              });
            }
          });

          // Header background image //
          $( ".header-background" ).each(function() {
            var attr = $(this).attr('data-image-src');
            var $item = $(this);

            if (typeof attr !== typeof undefined && attr !== false) {
                $(this).css('background', 'url('+attr+') no-repeat');
            }
            $item.css({'background-position': 'center', 'background-size': 'cover'});
          });

          // Adverst image background //
          $( ".advert-image" ).each(function() {
            var attr = $(this).attr('data-image-src');
            var $item = $(this);

            if (typeof attr !== typeof undefined && attr !== false) {
                $(this).css('background', 'url('+attr+') no-repeat');
            }
            $item.css({'background-position': 'center', 'background-size': 'cover'});
          });

          // Item image //
          $( ".view-image" ).each(function() {
            var attr = $(this).attr('data-image-src');
            var $item = $(this);

            if (typeof attr !== typeof undefined && attr !== false) {
                $(this).css('background', 'url('+attr+') no-repeat');
            }
              $item.css({'background-position': 'center', 'background-size': 'cover'});
          });

          // Control Background Images //
          $( ".ct-parallax-bg-img" ).each(function() {
            var attr = $(this).attr('data-image-src');
            var $item = $(this);

            if (typeof attr !== typeof undefined && attr !== false) {
                $(this).css('background', 'url('+attr+') no-repeat');
            }
            $item.css({'background-position': 'center', 'background-size': 'cover', 'background-attachment': 'fixed'});
          });

          // Control Background color and Border //
          $( ".ct-background-color" ).each(function() {
            var attr = $(this).attr('data-bgcolor');
            var bTop = $(this).attr('data-borderTop');
            var bBottom = $(this).attr('data-borderBottom');
            // var bColor = $(this).attr('borderColor');

            if (typeof attr !== typeof undefined && attr !== false) {
              $(this).css('background', ' '+attr+' ');
              $(this).css('border-top', ' '+bTop+' ');
              $(this).css('border-bottom', ' '+bBottom+' ');
              // $(this).css('border-color', ' '+bColor+' ');
            }
          });


      //Form Sellect //--------------------------
        var trigger_plus = $('.ads-trigger'),
         isClosed = false;

         trigger_plus.click(function () {
           form_box_cross();
         });

         function form_box_cross() {

           if (isClosed == true) {
             // hide();
             $('.form-icon-b').removeClass('fa-minus');
             $('.form-icon-b').addClass('fa-plus');
             isClosed = false;
           } else {
             // show();
             $('.form-icon-b').removeClass('fa-plus');
             $('.form-icon-b').addClass('fa-minus');
             isClosed = true;
           }
         }
         $('[data-form-toogled="offcanvas-form-b"]').click(function () {
             $('body').toggleClass('form-toggled-box');
          });

            // Accordion //--------------------------
            function toggleIcon(e) {
               $(e.target)
                   .prev('.panel-heading')
                   .find(".more-less")
                   .toggleClass('glyphicon-plus glyphicon-minus');
              }
             $('.panel-group').on('hidden.bs.collapse', toggleIcon);
             $('.panel-group').on('shown.bs.collapse', toggleIcon);


    });
}(jQuery));
