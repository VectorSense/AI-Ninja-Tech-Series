jQuery(function($){
    "use strict";

    var pasta = window.pasta || {};

    //  CONTACT MAIN FUNCTION // -------------------

    pasta.mainFunction = function(){

        /*Google map*/

        var customMapType = new google.maps.StyledMapType(
            [
                {
                    "featureType": "water",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#c1d2ff"
                        },
                        {
                            "lightness": 17
                        }
                    ]
                },
                {
                    "featureType": "landscape",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#F2F2F2"
                        },
                        {
                            "lightness": 20
                        }
                    ]
                },
                {
                    "featureType": "road.highway",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "color": "#E6E6E6"
                        },
                        {
                            "lightness": 17
                        }
                    ]
                },
                {
                    "featureType": "road.highway",
                    "elementType": "geometry.stroke",
                    "stylers": [
                        {
                            "color": "#ffffff"
                        },
                        {
                            "lightness": 29
                        },
                        {
                            "weight": 0.2
                        }
                    ]
                },
                {
                    "featureType": "road.arterial",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#ffffff"
                        },
                        {
                            "lightness": 18
                        }
                    ]
                },
                {
                    "featureType": "road.local",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#F2F2F2"
                        },
                        {
                            "lightness": 16
                        }
                    ]
                },
                {
                    "featureType": "poi",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#f5f5f5"
                        },
                        {
                            "lightness": 21
                        }
                    ]
                },
                {
                    "featureType": "poi.park",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#dedede"
                        },
                        {
                            "lightness": 21
                        }
                    ]
                },
                {
                    "elementType": "labels.text.stroke",
                    "stylers": [
                        {
                            "visibility": "on"
                        },
                        {
                            "color": "#ffffff"
                        },
                        {
                            "lightness": 16
                        }
                    ]
                },
                {
                    "elementType": "labels.text.fill",
                    "stylers": [
                        {
                            "saturation": 36
                        },
                        {
                            "color": "#1b1b1b"
                        },
                        {
                            "lightness": 40
                        }
                    ]
                },
                {
                    "elementType": "labels.icon",
                    "stylers": [
                        {
                            "visibility": "off"
                        }
                    ]
                },
                {
                    "featureType": "transit",
                    "elementType": "geometry",
                    "stylers": [
                        {
                            "color": "#f2f2f2"
                        },
                        {
                            "lightness": 19
                        }
                    ]
                },
                {
                    "featureType": "administrative",
                    "elementType": "geometry.fill",
                    "stylers": [
                        {
                            "color": "#fefefe"
                        },
                        {
                            "lightness": 20
                        }
                    ]
                },
                {
                    "featureType": "administrative",
                    "elementType": "geometry.stroke",
                    "stylers": [
                        {
                            "color": "#fefefe"
                        },
                        {
                            "lightness": 17
                        },
                        {
                            "weight": 1.2
                        }
                    ]
                }
            ],
            {
                name: 'Custom Style'
        });
        var customMapTypeId = 'custom_style';

        var properties = [
           { title:"Spacious 3 Bedroom Semi-Detached for sale", address:"116 Waverly Place", price:"$2,500", bed:"3", bath:"2", area:"350 sq ft", lat:40.714062, lng:-74.006876, thumb:"assets/images/content/property-4.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", } ,
           { title:"Spacious 3 Bedroom Semi-Detached for sale", address:"232 East 63rd Street", price:"$2,800", bed:"3", bath:"2", area:"350 sq ft",  lat:40.716664,  lng:-74.002455, thumb:"assets/images/content/property-1.jpg",  url:"property-single.html",  icon:"assets/images/icon-location.png", } ,
           { title:"Spacious 3 Bedroom Semi-Detached To Let", address:"55 Warren Street", price:"800", bed:"3", bath:"2", area:"350 sq ft",  lat:40.718746, lng:-74.008034, thumb:"assets/images/content/property-6.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", } ,
           { title:"Spacious 6 Bedroom Semi-Detached for sale", address:"459 West Broadway", price:"$850,000", bed:"3", bath:"2", area:"350 sq ft",  lat:40.724535, lng:-74.006489, thumb:"assets/images/content/property-6.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", } ,
           { title:"Spacious 3 Bedroom Semi-Detached for sale", address:"70 Greene Street", price:"$2,800", bed:"3", bath:"2", area:"350 sq ft",  lat:40.717835, lng:-74.011468, thumb:"assets/images/content/property-8.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", }  ,
           { title:"Spacious 3 Bedroom Semi-Detached for sale", address:"115 Allen Street", price:"$2,800", bed:"3", bath:"2", area:"350 sq ft",  lat:40.718616, lng:-73.987178, thumb:"assets/images/content/property-10.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", }  ,
           { title:"Spacious 3 Bedroom Semi-Detached To Let", address:"115 Allen Street", price:"$900", bed:"3", bath:"2", area:"350 sq ft",  lat:40.706636, lng:-74.015362, thumb:"assets/images/content/property-17.jpg", url:"property-single.html",  icon:"assets/images/icon-location.png", }
         ];

         window.onload = function () {
         PropertiesMap();
         }

        function PropertiesMap() {

          var mapOptions = {
              center: new google.maps.LatLng(properties[0].lat, properties[0].lng),
              zoom:14,
              scrollwheel: false,
              draggable: true,
              disableDefaultUI: true,
              mapTypeControlOptions: {
                  mapTypeIds: [google.maps.MapTypeId.ROADMAP, customMapTypeId]
              },
          };
          var map = new google.maps.Map(document.getElementById("location-map"), mapOptions);
          map.mapTypes.set(customMapTypeId, customMapType);
          map.setMapTypeId(customMapTypeId);

          //Create and open InfoWindow.
          var infoWindow = new google.maps.InfoWindow();
          var infoWindow = new google.maps.InfoWindow({
              maxWidth: 270,
            });


          for (var i = 0; i < properties.length; i++) {
              var data = properties[i];
              var myLatlng = new google.maps.LatLng(data.lat, data.lng);
              var marker = new google.maps.Marker({
                  position: myLatlng,
                  map: map,
                  icon: data.icon,
                  title: data.title,
                  animation: google.maps.Animation.DROP,
              });

              //Attach click event to the marker.
              (function (marker, data) {
                  google.maps.event.addListener(marker, "click", function (e) {

                      //Create and open InfoWindow.
                      infoWindow.setContent("<div class='map-property mapSlideDown'><div class='item-box image-hover'><label class='decor-label let-agreed'>"+data.price+"</label><a href='"+data.url+"'><div class='main-content'><div class='image-box'><img src='"+data.thumb+"' alt='"+data.title+"'><i class='fa fa-search'></i><div class='main-info-overlay'><div class='property-tilte'>"+data.title+"</div></div></div><div class='main-info'><div class='property-address'>"+data.address+"</div></div></div><div class='property-bottom-info'><div class='icons'><span><i class='fa fa-bed'></i> "+data.bed+"</span><span><i class='fa fa-bath' aria-hidden='true'></i> "+data.bath+"</span></div><div class='text'><span>"+data.area+"</span></div></div><div class='bottom-arrow'></div></a></div></div>");

                      if(!marker.open){
                          infoWindow.open(map,marker);
                          marker.open = true;
                      }
                      else{
                          infoWindow.close();
                          marker.open = false;
                      }
                      google.maps.event.addListener(map, 'click', function() {
                          infoWindow.close();
                          marker.open = false;
                      });

                  });

                  google.maps.event.addListener(marker, "click", function(){

                      var iwOuter = $('.gm-style-iw');
                      var iwBackground = iwOuter.prev();
                      iwBackground.children(':nth-child(6)').css({width: '270px'});
                      iwBackground.children(':nth-child(2)').css({'display' : 'none', width: '270px'});
                      iwBackground.children(':nth-child(4)').css({'display' : 'none', width: '270px'});

                      iwOuter.parent().parent().css({left: '8px', top: '20px', width: '270px'});
                      iwBackground.children(':nth-child(1)').attr('style', function(i,s){ return s + 'display: none!important;width: 270px!important;'});
                      iwBackground.children(':nth-child(3)').attr('style', function(i,s){ return s + 'display: none!important;width: 270px!important;'});
                      var iwCloseBtn = iwOuter.next();
                      iwCloseBtn.css({
                        display: 'none',
                        });

                        // Remove Close button div
                        $(".gm-style-iw").next("div").remove();
                        // Edit parent div
                        $(".gm-style-iw").parent().closest('div').css({width: '0px'});

                      iwCloseBtn.mouseout(function(){
                        $(this).css({opacity: '0'});
                      });

                  });

              })(marker, data);

          }

        }

    };

      // INIT FUNCTIONS //--------------------------

    $(document).ready(function(){
        pasta.mainFunction();
    });

    //   End of INIT FUNCTIONS  //

});
