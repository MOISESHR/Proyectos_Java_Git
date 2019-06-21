(function () {
    'use strict'

    angular
    .module('app.Programacion')
    .controller('GoceVacacionalController', GoceVacacionalController);

    GoceVacacionalController.$inject = ['ProgramacionService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile'];

    function GoceVacacionalController(ProgramacionService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile) {

        var vmx = this;

        vmx.RegistroGoce = RegistroGoce;
        vmx.FechaInicioRegistro = '';
        vmx.FechaFinalRegistro = '';

        LimpiarDatos();

        function LimpiarDatos() {
            vmx.FechaInicioRegistro = '';
            vmx.FechaFinalRegistro = '';
        };

        function RegistroGoce() {
            vmx.FechaInicioRegistro = '';
        };

    }

})();