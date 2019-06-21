(function () {
    'use strict'

    angular
    .module('app.Programacion')
    .controller('EditarGoceController', EditarGoceController);

    EditarGoceController.$inject = ['ProgramacionService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile'];

    function EditarGoceController(ProgramacionService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile) {

        var vmx = this;
        
        vmx.LimpiaDatos = LimpiaDatos;
        vmx.FechaInicioRegistro = $scope.$parent.vm.FechaInicio;
        vmx.FechaFinalRegistro = $scope.$parent.vm.FechaFinal;
        
        function LimpiaDatos() {
            vmx.FechaInicioRegistro = '';
            vmx.FechaFinalRegistro = '';
        };

    }

})();