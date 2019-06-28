(function () {
    'use strict'

    angular
    .module('app.Base')
    .directive('uploadFiles', uploadFiles);

    uploadFiles.$inject = ['$parse'];

    function uploadFiles($parse) {
        var directive = {
            scope: true,
            restrict: 'A', 
            link: function (scope, element, attrs) { 
                element.bind('change', function () {
                    $parse(attrs.uploadFiles).assign(scope, element[0].files)
                    scope.$apply();
                });                 
            }
        };

        
        return directive;
    }

})();

 