// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    function alertDismissed() {
        // do something
    };

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

        $('#get-weather').click(getWeather);


        $('#get-vibrate').click(function () {
            navigator.vibrate(2000);
        });

        $('#get-snapping').click(function () {
            var options = {
                sourceType: Camera.PictureSourceType.CAMERA
            };

            navigator.camera.getPicture(function () {
                console.log("camera online...snap it!");
            }, function () {
                console.log("error getting camera instance...");
            }, options);
        });


        $('#get-dialog').click(function () {
            navigator.notification.alert(
                    'You are the winner!',  // message
                    alertDismissed,         // callback
                    'Game Over',            // title
                    'Done'                  // buttonName
            );
        });
        getLocation();

        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };
})();