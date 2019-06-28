"use strict";
angular.module('app.Base', ["blockUI", "mgcrea.ngStrap", "ngAnimate", "ngSanitize","summernote"]).config(config);

function config(blockUIConfig, $httpProvider, $modalProvider) {
    blockUIConfig.autoInjectBodyBlock = false;
    blockUIConfig.autoBlock = false;
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    $httpProvider.defaults.headers.post['__RequestVerificationToken'] = antiForgeryToken;

    angular.extend($modalProvider.defaults, {
        html: true
    });
     
}

 
 