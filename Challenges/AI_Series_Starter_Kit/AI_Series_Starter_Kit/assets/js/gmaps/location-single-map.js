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

         window.onload = function () {
         PropertyMap();
         }
        // **********************************//
       //  Property Single Map Configration //
       // *********************************//
        var property = [
           { lat:40.73221,  lng:-73.99948,  icon:"assets/images/icon-location.png",}
         ];

        function PropertyMap() {

          var mapOptions = {
              center: new google.maps.LatLng(property[0].lat, property[0].lng),
              zoom:14,
              scrollwheel: false,
              draggable: true,
              disableDefaultUI: true,
              mapTypeControlOptions: {
                  mapTypeIds: [google.maps.MapTypeId.ROADMAP, customMapTypeId]
              },
          };
          var map = new google.maps.Map(document.getElementById("locationSingle-map"), mapOptions);
          map.mapTypes.set(customMapTypeId, customMapType);
          map.setMapTypeId(customMapTypeId);

          for (var i = 0; i < property.length; i++) {
              var data = property[i];
              var myLatlng = new google.maps.LatLng(data.lat, data.lng);
              var marker = new google.maps.Marker({
                  position: myLatlng,
                  map: map,
                  icon: data.icon,
                  title: data.title,
                  animation: google.maps.Animation.DROP,
              });

          }

        }

    };

      // INIT FUNCTIONS //--------------------------

    $(document).ready(function(){
        pasta.mainFunction();
    });

    //   End of INIT FUNCTIONS  //

});
