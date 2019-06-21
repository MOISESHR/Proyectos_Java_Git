(function () {
    'use strict' 

    angular
    .module('app.Base')
    .directive('dateTimePicker', dateTimePicker);

    function dateTimePicker() {
        var directive = {
            restrict: "A",
            require: "ngModel",
            link: function (scope, element, attrs, ngModelCtrl) {
                var parent = $(element).parent();
                var dtp = parent.datetimepicker({
                    format: "DD/MM/YYYY",
                    showTodayButton: true
                });
                dtp.on("dp.change", function (e) {
                    ngModelCtrl.$setViewValue(moment(e.date).format("DD/MM/YYYY"));
                    scope.$apply();
                });
            }
        };

        return directive; 
    }

})();

 