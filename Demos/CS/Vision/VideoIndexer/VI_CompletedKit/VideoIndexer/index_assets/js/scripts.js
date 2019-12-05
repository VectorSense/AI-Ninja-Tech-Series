/*
* ----------------------------------------------------------------------------------------
Author       : Onepageboss
Template Name: Lompot - Personal Portfolio/Resume Website Template for Professionals
Version      : 1.0
* ----------------------------------------------------------------------------------------
*/


(function ($) {
    'use strict';

    jQuery(document).ready(function () {

        /*
         * ----------------------------------------------------------------------------------------
         *  PRELOADER JS
         * ----------------------------------------------------------------------------------------
         */

        var prealoaderOption = $(window);
        prealoaderOption.on("load", function () {
            var preloader = jQuery('.preloader');
            var preloaderArea = jQuery('.preloader-area');
            preloader.fadeOut();
            preloaderArea.delay(350).fadeOut('slow');
        });




        /*
         * ----------------------------------------------------------------------------------------
         *  CHANGE MENU BACKGROUND JS
         * ----------------------------------------------------------------------------------------
         */

        var headertopoption = $(window);
        var headTop = $('.header-top-area');

        headertopoption.on('scroll', function () {
            if (headertopoption.scrollTop() > 400) {
                headTop.addClass('menu-bg');
            } else {
                headTop.removeClass('menu-bg');
            }
        });



        /*
         * ----------------------------------------------------------------------------------------
         *  SMOTH SCROOL JS
         * ----------------------------------------------------------------------------------------
         */

        $('a.smoth-scroll').on("click", function (e) {
            var anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $(anchor.attr('href')).offset().top - 50
            }, 1000);
            e.preventDefault();
        });


        /*
         * ----------------------------------------------------------------------------------------
         *  PARALLAX JS
         * ----------------------------------------------------------------------------------------
         */

        var parallaxeffect = $(window);
        parallaxeffect.stellar({
            responsive: true,
            positionProperty: 'position',
            horizontalScrolling: false
        });



        /*
         * ----------------------------------------------------------------------------------------
         *  WORK JS
         * ----------------------------------------------------------------------------------------
         */

        $('.work-inner').mixItUp();



        /*
         * ----------------------------------------------------------------------------------------
         *  MAGNIFIC POPUP JS
         * ----------------------------------------------------------------------------------------
         */

        var magnifPopup = function () {
            $('.work-popup').magnificPopup({
                type: 'image',
                removalDelay: 300,
                mainClass: 'mfp-with-zoom',
                gallery: {
                    enabled: true
                },
                zoom: {
                    enabled: true, // By default it's false, so don't forget to enable it

                    duration: 300, // duration of the effect, in milliseconds
                    easing: 'ease-in-out', // CSS transition easing function

                    // The "opener" function should return the element from which popup will be zoomed in
                    // and to which popup will be scaled down
                    // By defailt it looks for an image tag:
                    opener: function (openerElement) {
                        // openerElement is the element on which popup was initialized, in this case its <a> tag
                        // you don't need to add "opener" option if this code matches your needs, it's defailt one.
                        return openerElement.is('img') ? openerElement : openerElement.find('img');
                    }
                }
            });
        };
        // Call the functions 
        magnifPopup();





        /*
         * ----------------------------------------------------------------------------------------
         *  TESTIMONIAL JS
         * ----------------------------------------------------------------------------------------
         */

        $(".testimonial-list").owlCarousel({
            items: 1,
            autoPlay: true,
            navigation: false,
            itemsDesktop: [1199, 1],
            itemsDesktopSmall: [980, 1],
            itemsTablet: [768, 1],
            itemsTabletSmall: false,
            itemsMobile: [479, 1],
            pagination: true,
            autoHeight: true,
        });


        /*
         * ----------------------------------------------------------------------------------------
         *  BLOG JS
         * ----------------------------------------------------------------------------------------
         */

        $(".blog-list").owlCarousel({
            items: 2,
            autoPlay: true,
            navigation: false,
            itemsDesktop: [1199, 1],
            itemsDesktopSmall: [980, 1],
            itemsTablet: [768, 1],
            itemsTabletSmall: false,
            itemsMobile: [479, 1],
            pagination: true,
            autoHeight: true,
        });



        /*
         * ----------------------------------------------------------------------------------------
         *  EXTRA JS
         * ----------------------------------------------------------------------------------------
         */
        $(document).on('click', '.navbar-collapse.in', function (e) {
            if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
                $(this).collapse('hide');
            }
        });
        $('body').scrollspy({
            target: '.navbar-collapse',
            offset: 195
        });



        /*
         * ----------------------------------------------------------------------------------------
         *  AJAX JS
         * ----------------------------------------------------------------------------------------
         */

        // Function for email address validation
        function isValidEmail(emailAddress) {
            var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);

            return pattern.test(emailAddress);

        }
        $("#contactForm").on('submit', function (e) {
            e.preventDefault();
            var data = {
                name: $("#name").val(),
                email: $("#email").val(),
                subject: $("#subject").val(),
                message: $("#message").val()
            };

            if (isValidEmail(data['email']) && (data['message'].length > 1) && (data['name'].length > 1) && (data['subject'].length > 1)) {
                $.ajax({
                    type: "POST",
                    url: "sendmail.php",
                    data: data,
                    success: function () {
                        $('#contactForm .input-success').delay(500).fadeIn(1000);
                        $('#contactForm .input-error').fadeOut(500);
                    }
                });
            } else {
                $('#contactForm .input-error').delay(500).fadeIn(1000);
                $('#contactForm .input-success').fadeOut(500);
            }

            return false;
        });


        /*
         * ----------------------------------------------------------------------------------------
         *  WOW JS
         * ----------------------------------------------------------------------------------------
         */
        new WOW().init();




        /*
         * ----------------------------------------------------------------------------------------
         *  TYPE EFFECT JS
         * ----------------------------------------------------------------------------------------
         */

        var TxtType = function (el, toRotate, period) {
            this.toRotate = toRotate;
            this.el = el;
            this.loopNum = 0;
            this.period = parseInt(period, 10) || 1000;
            this.txt = '';
            this.tick();
            this.isDeleting = false;
        };

        TxtType.prototype.tick = function () {
            var i = this.loopNum % this.toRotate.length;
            var fullTxt = this.toRotate[i];

            if (this.isDeleting) {
                this.txt = fullTxt.substring(0, this.txt.length - 1);
            } else {
                this.txt = fullTxt.substring(0, this.txt.length + 1);
            }

            this.el.innerHTML = '<span class="wrap">' + this.txt + '</span>';

            var that = this;
            var delta = 150 - Math.random() * 100;

            if (this.isDeleting) {
                delta /= 2;
            }

            if (!this.isDeleting && this.txt === fullTxt) {
                delta = this.period;
                this.isDeleting = true;
            } else if (this.isDeleting && this.txt === '') {
                this.isDeleting = false;
                this.loopNum++;
                delta = 500;
            }

            setTimeout(function () {
                that.tick();
            }, delta);
        };

        window.onload = function () {
            var elements = document.getElementsByClassName('typewrite');
            for (var i = 0; i < elements.length; i++) {
                var toRotate = elements[i].getAttribute('data-type');
                var period = elements[i].getAttribute('data-period');
                if (toRotate) {
                    new TxtType(elements[i], JSON.parse(toRotate), period);
                }
            }
            // INJECT CSS
            var css = document.createElement("style");
            css.type = "text/css";
            css.innerHTML = ".typewrite > .wrap { border-right: 0.02em solid #fff}";
            document.body.appendChild(css);
        };


        /*
         * ----------------------------------------------------------------------------------------
         *  SKILLS CHART JS
         * ----------------------------------------------------------------------------------------
         */

        var index = 0;
        $(document).scroll(function () {
                var top = $('#skills').height() - $(window).scrollTop();
                console.log(top)
                if (top < -300) {
                    if (index == 0) {

                        $('.chart').easyPieChart({
                            easing: 'easeOutBounce',
                            onStep: function (from, to, percent) {
                                $(this.el).find('.percent').text(Math.round(percent));
                            }
                        });

                    }
                    index++;
                }
            })
            //console.log(nagativeValue)




    });

})(jQuery);