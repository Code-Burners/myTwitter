﻿requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions'
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);




define(['durandal/system', 'durandal/app', 'durandal/viewLocator'], function (system, app, viewLocator) {
  
    app.title = 'myTwitter';
    system.debug(true);
   
    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function () {
        
        viewLocator.useConvention();
        app.setRoot('viewmodels/shell');
    });

});