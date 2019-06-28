(function () {
    'use strict'
    angular
    .module('app.Solicitud')
    .controller('BandejaSolicitudController', BandejaSolicitudController);
     
    BandejaSolicitudController.$inject = ['SolicitudService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile', '$modal', '$injector'];

    function BandejaSolicitudController(SolicitudService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile, $modal, $injector) {

        var vm = this;
        vm.BuscarSolicitud = BuscarSolicitud;
        vm.LimpiarFiltros = LimpiarFiltros;
        vm.ValidarCampos = ValidarCampos;
        vm.CargarComboVacacional = CargarComboVacacional;
        vm.CargaRegistroSolicitud = CargaRegistroSolicitud;

        vm.MensajeDeAlerta = '';
        vm.EliminarSolicitud = EliminarSolicitud;
        vm.CodUsuario = '';
        
        vm.FechaInicio = '';
        vm.FechaFinal = '';
         
        vm.IdListEstados = "-1";
        vm.ListEstados = [];
        
        vm.IdSolicitud = '';

        vm.ListEstados = UtilsFactory.AgregarItemSelect([]);
        vm.NombreLider = jsonDatosTrabajador.TrabajadorDto.NombreLider; 
        vm.Unidad = jsonDatosTrabajador.TrabajadorDto.Unidad;      

        vm.dtColumns = [ 
          DTColumnBuilder.newColumn('TipoSolicitud').withTitle('Tipo Solicitud').notSortable(),
          DTColumnBuilder.newColumn('CodigoSolicitud').withTitle('Código Solicitud').notSortable(),
          DTColumnBuilder.newColumn('FechaInicio').withTitle('Fecha Inicio').notSortable(),
          DTColumnBuilder.newColumn('FechaFinal').withTitle('Fecha Final').notSortable(),
          DTColumnBuilder.newColumn('CalculoDias').withTitle('Calculo días').notSortable(),
          DTColumnBuilder.newColumn('EstadoSolicitud').withTitle('Estado Solicitud').notSortable()     
          //DTColumnBuilder.newColumn(null).withTitle('Acciones').notSortable().renderWith(AccionesBusquedaSolicitud)
        ];
        
        LimpiarGrilla();
        CargarDatos();
          
        function CargarDatos() {
            if (jsonListEstados != null) {
                vm.ListEstados = UtilsFactory.AgregarItemSelect(jsonListEstados);
                vm.IdListEstados = "-1";
            }
         };     

        function RegistrarCombo() {
            vm.FechaIni = vm.RecordCombo.FeriadoDiaInicio;
            vm.FechaFin = vm.RecordCombo.FeriadoDiaFinal;
            var solicitud = {
                CodigoEmpleado: vm.CodUsuario,
                FechaInicio: vm.FechaIni,
                FechaFinal: vm.FechaFin,
            };
            blockUI.start();
            var promise = SolicitudService.RegistrarCombo(solicitud);
            promise.then(function (response) {
                blockUI.stop();
                var Respuesta = response.data;
                if (Respuesta.Tipo != 0) {
                    UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 5);
                } else {
                    if (vm.Id == "") {
                        LimpiarCampos();
                    }
                    UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosOk, 5);
                }
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
            });
        };
        
        function BuscarSolicitud() {
            blockUI.start();
            LimpiarGrilla();
            $timeout(function () {
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
                .withDisplayLength(10);
            }, 500);
        };

        function BuscarSolicitudPaginado(sSource, aoData, fnCallback, oSettings) {
            var draw = aoData[0].value;
            var start = aoData[3].value;
            var length = aoData[4].value;
            var solicitud = {                    
                    FechaInicio: vm.FechaInicio,
                    FechaFinal: vm.FechaFinal,
                    EstadoSolicitud: vm.IdListEstados,
                    Indice: start,
                    Tamanio: start+length
            };
            var num = ValidarCampos();
            if (num == 0) {
                var promise = SolicitudService.ListarSolicitud(solicitud);
                promise.then(function (response) {
                    var records = {
                        'draw': draw,
                        'recordsTotal': response.data.Total,
                        'recordsFiltered': response.data.Total,
                        'data': response.data.SolicitudDtoLista
                    };
                    blockUI.stop();
                    fnCallback(records);

                }, function (response) {
                    blockUI.stop();
                    LimpiarGrilla();
                });
            } else {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', vm.MensajeDeAlerta, 5);
                LimpiarGrilla();
                vm.MensajeDeAlerta = "";
                $timeout(function () {
                    DesaparecerEfectosDeValidacion();
                }, 3000);
            }            
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
            return "<div class='col-xs-6 col-sm-6'><a title='Editar' class='btn btn-primary'  id='tab-button' class='btn ;' >" + "<span class='glyphicon glyphicon-edit'></span>" + "</a></div> " +
            "<div class='col-xs-6 col-sm-6'> <a title='Eliminar' class='btn btn-primary'   id='tab-button' class='btn '  onclick='EliminarSolicitud(\"" + data.CodigoSolicitud + "\");'>" + "<span class='glyphicon glyphicon-trash'></span>" + "</a></div> ";
        };

        function LimpiarFiltros() {            
            vm.FechaInicio = '';
            vm.FechaFinal = '';            
            vm.IdListEstados = "-1";
            vm.IdSolicitud = '';
            LimpiarGrilla();
        };

        function ValidarCampos() {
            var num = 0;
            if ($.trim(vm.IdListEstados) == "-1" || $.trim(vm.IdListEstados) == "") {
                vm.MensajeDeAlerta = vm.MensajeDeAlerta + "Por favor seleccione un estado" + '<br/>';
                UtilsFactory.InputBorderColor('#CmbEstados', 'Rojo');
                num = 1;
            }
            return num;
        };

        function EliminarSolicitud(IdSolicitud) {                                   
            var solicitud = {
                CodigoSolicitud: IdSolicitud,
                CodigoUsuario: vm.CodUsuario,
            };
            blockUI.start();
            if (confirm('¿Estas seguro de eliminar la Solicitud ' + IdSolicitud + ' ?')) {
                var promise = SolicitudService.EliminarSolicitud(solicitud);
                UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosDeleteOk, 5);
                LimpiarGrilla();
                blockUI.stop();
                BuscarSolicitud();
            }
            else {
                blockUI.stop();                
            };
        };

        function DesaparecerEfectosDeValidacion() {
            UtilsFactory.InputBorderColor('#CmbEstados', 'Ninguno');
        };

        function CargaRegistroSolicitud() {
            var promise = SolicitudService.ModalRegistrarSolicitud();
            promise.then(function (response) {
                var respuesta = $(response.data);

                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope);
                    $("#ContenidoModalDatosRegistro").html(content);
                });

                $('#ModalDatosRegistro').modal({
                    keyboard: false,
                    backdrop: 'static'
                });
            }, function (response) {
                blockUI.stop();
            });
        };
                  
        function CargarComboVacacional() { 
            var promise = SolicitudService.ModalComboVacacional();
            promise.then(function (response) {
                var respuesta = $(response.data);  

                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope); 
                    $("#ContenidoModalDatosCombo").html(content);
                });

                $('#ModalDatosCombo').modal({
                    keyboard: false,
                    backdrop: 'static'
                });
            }, function (response) {
                blockUI.stop();
            });  
        };
        
    }   

})();
 