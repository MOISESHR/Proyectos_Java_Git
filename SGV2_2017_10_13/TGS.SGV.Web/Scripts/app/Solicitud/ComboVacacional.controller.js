(function () {
    'use strict'
   
    angular
    .module('app.Solicitud')
    .controller('ComboVacacionalController', ComboVacacionalController);

    ComboVacacionalController.$inject = ['SolicitudService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder','$timeout', 'MensajesUI', 'URLS', '$scope', '$compile'];

    function ComboVacacionalController(SolicitudService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile ) {

        var vmx = this; 

        vmx.RegistrarCombo = RegistrarCombo;
        vmx.RegistraSolicitudCombo = RegistraSolicitudCombo;
        vmx.BuscarCombos = BuscarCombos;
        vmx.RecorrerCombo = RecorrerCombo;
        vmx.MensajeDeAlerta = ""; 
          
        vmx.FechaIniCombo = '';
        vmx.FechaFinCombo = '';
        vmx.RecordCombo = [];
        vmx.ListaCombo = [];
     
        vmx.selected = {};
        vmx.selectAll = false; 

        var titleHtml = '<input type="checkbox" ng-model="vmx.selectAll" ng-click="vmx.toggleAll(vmx.selectAll, vmx.selected)">';

        vmx.dtColumnsCombo = [
            DTColumnBuilder.newColumn(null).withTitle('').notSortable().withClass('no-click').renderWith(AccionesComboSolicitud),
            DTColumnBuilder.newColumn('FeriadoDiaInicio').withTitle('Dia Inicio').notSortable(),
            DTColumnBuilder.newColumn('FeriadoDiaFinal').withTitle('Dia Final').notSortable(),
            DTColumnBuilder.newColumn('FeriadoComentario').withTitle('Descripción').notSortable()
        ];

        LimpiarGrillaCombo();

        BuscarCombos();
        
        function BuscarCombos() {
            blockUI.start();
            LimpiarGrillaCombo();
            $timeout(function () {
                vmx.dtOptionsCombo = DTOptionsBuilder
                .newOptions().withOption('bFilter', false)
                .withOption('responsive', true)
                .withOption('order', [])
                .withFnServerData(BuscarComboVacacional)
                .withDataProp('data')
                .withOption('serverSide', true)
                .withOption('paging', false)

                .withOption('headerCallback', function (header) {
                    if (!vmx.headerCompiled) {
                        vmx.headerCompiled = true;
                        $compile(angular.element(header).contents())($scope);
                    }
                })
                .withPaginationType('full_numbers')
                .withDisplayLength(10)
                ;
            }, 500);
        };

        function BuscarComboVacacional(sSource, aoData, fnCallback, oSettings) {
            var draw = aoData[0].value;
            var start = aoData[3].value;
            var length = aoData[4].value;
            var solicitud = {
                CodigoUsuario: '',
                FechaInicio: '',
                TipoEmpresa: ''
            };
            var promise = SolicitudService.ListarComboFeriado(solicitud);
            promise.then(function (response) {
                var records = {
                    'draw': draw,
                    'recordsTotal': response.data.ComboFeriadoDtoLista.length,
                    'recordsFiltered': response.data.ComboFeriadoDtoLista.length,
                    'data': response.data.ComboFeriadoDtoLista
                };
                blockUI.stop();
                fnCallback(records);
                vmx.RecordCombo = records.data;
            }, function (response) {
                blockUI.stop();
                LimpiarGrillaCombo();
            });
        };

        function LimpiarGrillaCombo() {
            vmx.dtOptionsCombo = DTOptionsBuilder
            .newOptions()
            .withOption('data', [])
            .withOption('bFilter', false)
            .withOption('responsive', true)
            .withOption('destroy', true)
            .withOption('order', [])
            .withDisplayLength(0)
            .withOption('paging', false);
        };

        function RegistrarCombo() {
            var idx = 0;
            var id = 0;
            var vMensajeSalida = "";                        
            for ( id in vmx.ListaCombo)
            {                
                idx = 0
                for (idx in vmx.RecordCombo) {
                    if (vmx.ListaCombo[id] == vmx.RecordCombo[idx].FeriadoDiaInicio) {
                        vmx.FechaIniCombo = vmx.RecordCombo[idx].FeriadoDiaInicio;
                        vmx.FechaFinCombo = vmx.RecordCombo[idx].FeriadoDiaFinal;
                        RegistraSolicitudCombo();
                    };
                };               
            };
            if (vmx.ListaCombo.length==0)
            {
                UtilsFactory.Alerta('#divAlertCombo', 'danger', 'Seleccione al menos un combo vacacional', 3);
            };
        };

        function RegistraSolicitudCombo() {
            var solicitud = {
                CodigoEmpleado: '',
                FechaInicio: vmx.FechaIniCombo,
                FechaFinal: vmx.FechaFinCombo,
            };
            blockUI.start();
            var promise = SolicitudService.RegistrarCombo(solicitud);
            promise.then(function (response) {
                blockUI.stop();
                var Respuesta = response.data;
                if (Respuesta.Tipo != 0) {
                    UtilsFactory.Alerta('#divAlertCombo', 'danger', Respuesta.Errores[0], 20);
                } else {
                    if (vmx.Id == "") {
                        LimpiarCampos();
                    }
                    UtilsFactory.Alerta('#divAlertCombo', 'success', MensajesUI.DatosOk, 5);
                }
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlertCombo', 'danger', mensaje, 3);
            });
        };

        function AccionesComboSolicitud(data, type, full, meta) {            
            vmx.selected[full.FeriadoDiaInicio] = false;
            return '<input type="checkbox" ng-model="vmx.selected[' + data.FeriadoDiaInicio + ']" onclick="RecorreCombo(\'' + data.FeriadoDiaInicio + '\'); ">';
        };

        function RecorrerCombo(codigo) {
            var tmpLista = [];
            if (vmx.ListaCombo.indexOf(codigo) !== -1) {
                for (var id in vmx.ListaCombo) {
                    if (vmx.ListaCombo[id] == codigo) {
                    }
                    else {
                        tmpLista.push(vmx.ListaCombo[id]);
                    }
                }
                vmx.ListaCombo = tmpLista;
            }
            else {
                vmx.ListaCombo.push(codigo);
            }           
        };

    }

})();