(function () {
    'use strict'

    angular
    .module('app.Base')
    .directive('tituloDatosObligatorios', tituloDatosObligatorios);
    
    function tituloDatosObligatorios() {
        var directive = {
            scope: true,
            restrict: 'E',
            template: 'Los campos con asterisco (*) son obligatorios.'
        };
         
        return directive;
    }

})();

