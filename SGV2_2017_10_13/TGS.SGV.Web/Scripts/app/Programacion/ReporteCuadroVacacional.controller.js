(function () {
    'use strict',

    angular
    .module('app.Programacion')
    .controller('ReporteCuadroVacacionalController', ReporteCuadroVacacionalController);

    ReporteCuadroVacacionalController.$inject = ['blockUI', '$timeout', 'interval', 'UtilsFactory', 'MensajesUI', 'URLS', 'TrabajadorService'];

    function ReporteCuadroVacacionalController(blockUI, $timeout,$interval, UtilsFactory, $MensajesUI, $urls ,TrabajadorService) {
        var vm = this;
        vm.Buscar = Buscar;
        vm.LimpiarFiltros = LimpiarFiltros;

        vm.Usuario = '';

        function LimpiarFiltros() {
            vm.Usuario = '';
        };

        function Buscar() {

            blockUI.start();

            var parametros =
            "reporte=CuadroVacaciones" +
            "&CodigoUsuario=" + vm.Usuario.Codigo;

            $("[id$='FReporte']").attr('src', $urls.ApiReportes + parametros);

            var promise = $interval(function () {
                if (document.getElementById("rvVisor_Toolbar") != 'null') {
                    blockUI.stop();
                    $interval.cancel(promise);
                }
            }, 8000);

        };
    }

})();

