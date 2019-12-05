jQuery(window).load( function($) {

//
  // Flexslider
  //
  if( jQuery(".flexslider").length > 0) {
    jQuery(".flexslider").flexslider({
      'controlNav': true,
      'directionNav': true,
      start: function(slider) {
				slider.removeClass('loading');
			}
    });
  }

  jQuery('#entry-listing').isotope({
    animationOptions: {
      duration: 750,
      easing: 'linear',
      queue: false
    },
    itemSelector: 'article.entry',
    transformsEnabled: false
  });

});

jQuery(document).ready(function($) {


  var $menu = $("#navigation");
  $menu.superfish({
    animation: {
      opacity: "show",
      height: "show"
    },
    speed: "fast",
    delay: 250
  });

  $(".searchsubmit").bind("click", function() {
    $(this).parent().submit();
  });

  $(".fancybox").fancybox({
    fitToView: true
  });

  $("a[data-lightbox^=fancybox]").fancybox({
    fitToView: true
  });


  // Responsive menu
  $("<select />").appendTo("nav");

  // Create default option "Go to..."
  $("<option />", {
    "selected": "selected",
    "value": "",
    "text": "Go to..."
  }).appendTo("nav select");

  // Populate dropdown with menu items
  $("nav a").each(function () {
    var el = $(this);
    $("<option />", {
      "value": el.attr("href"),
      "text": el.text()
    }).appendTo("nav select");
  });

  $("nav select").change(function () {
    window.location = $(this).find("option:selected").val();
  });

  $(function(){
    $.fn.formLabels();
  });

  $("#jp500").jPlayer({
    ready: function (event) {
      $(this).jPlayer("setMedia", {
        m4a:"http://www.jplayer.org/audio/m4a/TSP-01-Cro_magnon_man.m4a",
        oga:"http://www.jplayer.org/audio/ogg/TSP-01-Cro_magnon_man.ogg"
      });
    },
    swfPath: "js",
    supplied: "m4a, oga",
    wmode: "window",
    cssSelectorAncestor: "#jp-203"
  });

  //fitVids
  $(".inner-container .format-video .entry-image").fitVids();


});

jQuery(window).load( function() {

  // Page width calculations

  jQuery(window).resize(setContainerWidth);
  var $box = jQuery(".box");

  function setContainerWidth() {
    var columnNumber = parseInt((jQuery(window).width()+15) / ($box.outerWidth(true))),
      containerWidth = (columnNumber * $box.outerWidth(true)) - 15;

    if ( columnNumber > 1 )  {
      jQuery("#box-container").css("width",containerWidth+'px');
    } else {
      jQuery("#box-container").css("width", "100%");
    }

  }

  setContainerWidth();
  loadAudioPlayer();

});

function loadAudioPlayer() {
  jQuery(".format-audio").each(function() {
    var $audio_id = jQuery(this).find(".audio-wrap").data("audio-id"),
      $media = jQuery(this).find(".audio-wrap").data("audio-file"),
      $play_id = '#jp-'+$audio_id,
      $play_ancestor = '#jp-play-'+$audio_id,
      $extension = $media.split('.').pop();

    if ( $extension.toLowerCase() =='mp3' ) {
      var $extension = 'mp3';
    } else if ( $extension.toLowerCase() =='mp4' ||  $extension.toLowerCase() =='m4a' ) {
      var $extension = 'm4a';
    } else if ( $extension.toLowerCase() =='ogg' || $extension.toLowerCase() =='oga' ) {
      var $extension = 'oga';
    } else {
      var $extension = '';
    }


    jQuery($play_id).jPlayer({
      ready: function (event) {
        var playerOptions = {
          $extension: $media
        };
        playerOptions[$extension] = $media;
        jQuery(this).jPlayer("setMedia", playerOptions);
      },
//      swfPath: '/js', uncomment this for swf support
      supplied: $extension,
      wmode: 'window',
      cssSelectorAncestor: $play_ancestor
    });

  });
}
