(function () {
    'use strict'

    angular
    .module('app.Solicitud')
    .controller('BusquedaSolicitudController', BusquedaSolicitudController);

    BusquedaSolicitudController.$inject = ['SolicitudService', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout'];

    function BusquedaSolicitudController(SolicitudService, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout) {
        var vm = this;
        vm.BuscarSolicitud = BuscarSolicitud;
        vm.LimpiarFiltros = LimpiarFiltros;
        vm.CodUsuario = '';
        vm.FechaInicio = '';
        vm.FechaFinal = '';
        vm.dtInstance = {};

        vm.dtColumns = [
           DTColumnBuilder.newColumn('TipoSolicitud').withTitle('Tipo Solicitud').notSortable(),
           DTColumnBuilder.newColumn('CodigoSolicitud').withTitle('Código Solicitud').notSortable(),
           DTColumnBuilder.newColumn('FechaInicio').withTitle('Fecha Inicio').notSortable(),
           DTColumnBuilder.newColumn('FechaFinal').withTitle('Fecha Final').notSortable(),
           DTColumnBuilder.newColumn('CalculoDias').withTitle('Calculo días').notSortable(),
           DTColumnBuilder.newColumn('EstadoSolicitud').withTitle('Estado Solicitud').notSortable(),
           DTColumnBuilder.newColumn(null).withTitle('Acciones').notSortable().renderWith(AccionesBusquedaSolicitud)
        ];

        LimpiarGrilla();

        function BuscarSolicitud() {
            vm.dtOptions = DTOptionsBuilder
              .newOptions().withOption('bFilter', false)
              .withOption('responsive', true)
              .withOption('order', [])
              .withFnServerData(BuscarSolicitudPaginado)
              .withDataProp('data')
              .withOption('serverSide', true)
              .withOption('paging', true)
              .withOption('destroy', true)
              .withPaginationType('full_numbers')
              .withDisplayLength(2);
        };

        function BuscarSolicitudPaginado(sSource, aoData, fnCallback, oSettings) {

            var draw = aoData[0].value;
            var start = aoData[3].value;

            var solicitud = {
                CodUsuario: vm.CodUsuario,
                FechaInicio: vm.FechaInicio,
                FechaFinal: vm.FechaFinal,
                EstadoSolicitud: vm.IdListEstados,
                Indice: start,
                Tamanio: length
            };

            var promise = SolicitudService.BuscarSolicitud(solicitud);

            promise.then(function (response) {
                var records = {
                    'draw': draw,
                    'recordsTotal': response.data.Total,
                    'recordsFiltered': response.data.Total,
                    'data': response.data.SolicitudDto
                };

                fnCallback(records);

            }, function (response) {
                LimpiarGrilla();
            });
        };

        function LimpiarGrilla() {
            vm.dtOptions = DTOptionsBuilder
            .newOptions()
            .withOption('data', [])
            .withOption('bFilter', false)
            .withOption('responsive', true)
            .withOption('destroy', true)
            .withOption('order', [])
            .withDisplayLength(0)
            .withOption('paging', false);
        };

        function AccionesBusquedaSolicitud(data, type, full, meta) {
            return '<button title="Editar.." type="button" class="btn btn-primary btn-sm" ng-click="vm.EditarSolicitud(' + data.CodigoSolicitud + ')">' +
                '  <span class="glyphicon glyphicon-edit"></span> ' +
                '</button>';
        };

        function LimpiarFiltros() {
            vm.CodUsuario = '';
            vm.FechaInicio = '';
            vm.FechaFinal = '';
            LimpiarGrilla();
        };
    }

})();