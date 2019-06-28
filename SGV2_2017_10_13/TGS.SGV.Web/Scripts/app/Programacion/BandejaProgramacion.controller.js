(function () {
    'use strict'
    angular
    .module('app.Programacion')
    .controller('BandejaProgramacionController', BandejaProgramacionController);

    BandejaProgramacionController.$inject = ['ProgramacionService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile', '$modal', '$injector' ];

    function BandejaProgramacionController(ProgramacionService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile, $modal, $injector ) {

        var vm = this;
        vm.BuscarProgramacion = BuscarProgramacion;
        vm.LimpiarFiltros = LimpiarFiltros;
        vm.ValidarCampos = ValidarCampos;
        vm.CargaGoceVacacional = CargaGoceVacacional;
        vm.EditarGoce = EditarGoce;
        vm.SeleccionaFechaDerecho = SeleccionaFechaDerecho;
        vm.ObtenerDatosTrabajador = ObtenerDatosTrabajador;
        vm.ObtenerIndicadoresFuturo = ObtenerIndicadoresFuturo;
              

        vm.Busqueda = '';
        vm.Empresa = '',
        vm.Tipo = '',
        vm.Vencidos = '',
        vm.Truncos = '',

        vm.Pagados = '',
        vm.Gozados = '',
        vm.PendienteGoce = '',

        vm.FechaRol = '',
        vm.vFechaDerecho = '',

        vm.MensajeDeAlerta = "";        
        vm.CodUsuario = '';
        vm.FechaInicio = '';
        vm.FechaFinal = '';

        vm.IdSolicitud = '';
 
        vm.ListaFechaDerecho = '-1';
        vm.FechaDerecho = [];

        vm.dtColumns = [          
          DTColumnBuilder.newColumn('FechaInicio').withTitle('Inicio').notSortable(),
          DTColumnBuilder.newColumn('FechaFinal').withTitle('Final').notSortable(),
          DTColumnBuilder.newColumn('Situacion').withTitle('Estado').notSortable(),
          DTColumnBuilder.newColumn('DiasPagados').withTitle('N° Pagina').notSortable(),
          DTColumnBuilder.newColumn('Dias').withTitle('N° Días').notSortable(),
          DTColumnBuilder.newColumn('Activo').withTitle('Activo').notSortable(),
          DTColumnBuilder.newColumn('GoceReprogramado').withTitle('Reprog').notSortable(),
          DTColumnBuilder.newColumn('UsuarioCreacion').withTitle('Usuario Creado').notSortable(),
          DTColumnBuilder.newColumn('FechaCreacion').withTitle('Fecha Creado').notSortable(),
          DTColumnBuilder.newColumn('Comentario').withTitle('Comentario').notSortable(),
          DTColumnBuilder.newColumn(null).withTitle('Acciones').notSortable().renderWith(AccionesBusquedaProgramacion)
        ];
        
        LimpiarGrilla();
 
        function CargarDatos() {
            vm.FechaDerecho = UtilsFactory.AgregarItemSelectTodos(jsonListaFechaDerecho.FechaDerechoDtoLista)

            if (jsonListaFechaDerecho.FechaDerechoDtoLista.length > 0) {
                vm.Gozados = jsonListaFechaDerecho.FechaDerechoDtoLista[1].DiasGozados,
                vm.Pagados = jsonListaFechaDerecho.FechaDerechoDtoLista[1].DiasPagados,
                vm.PendienteGoce = jsonListaFechaDerecho.FechaDerechoDtoLista[1].DiasPendientes             
             };
        };

        function BuscarProgramacion() {
            blockUI.start();
            LimpiarGrilla();            
            $timeout(function () {
                vm.dtOptions = DTOptionsBuilder
                .newOptions().withOption('bFilter', false)
                .withOption('responsive', true)
                .withOption('order', [])
                .withFnServerData(BuscarProgramacionPaginado)
                .withDataProp('data')
                .withOption('serverSide', true)
                .withOption('paging', true)
                .withOption('destroy', true)
                .withPaginationType('full_numbers')
                .withDisplayLength(10);
            }, 500);
        };

        function BuscarProgramacionPaginado(sSource, aoData, fnCallback, oSettings) {
            var draw = aoData[0].value;
            var start = aoData[3].value;
            var length = aoData[4].value;
            vm.vFechaDerecho = "";
            vm.FechaRol = "";
            if (vm.ListaFechaDerecho.FechaDerecho == undefined) {
                vm.vFechaDerecho = '01/01/2017';
                vm.FechaRol = '01/01/2017';
            }
            else {
                vm.vFechaDerecho = vm.ListaFechaDerecho.FechaDerecho;
                vm.FechaRol = vm.ListaFechaDerecho.FechaInicio;
            }


            var programacion = {
                CodUsuario: vm.CodUsuario,
                FechaDerecho: vm.vFechaDerecho,
                FechaRol: vm.FechaRol,
                Indice: start,
                Tamanio: start + length
            };
            var promise = ProgramacionService.ListarObtenerGoce(programacion);
                promise.then(function (response) {
                    var records = {
                        'draw': draw,
                        'recordsTotal': response.data.Total,
                        'recordsFiltered': response.data.Total,
                        'data': response.data.GoceVacacionalDtoLista
                    };
                    blockUI.stop();
                    fnCallback(records);

                }, function (response) {
                    blockUI.stop();
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

        function AccionesBusquedaProgramacion(data, type, full, meta) {
            return "<div class='col-xs-6 col-sm-6'> <a title='Editar' class='btn btn-primary'   id='tab-button' class='btn '  onclick='EditarGoce(\"" + data.FechaInicio + "\",\"" + data.FechaFinal + "\");'>" + "<span class='glyphicon glyphicon-edit'></span>" + "</a></div> " +
                   "<div class='col-xs-6 col-sm-6'><a title='Cancelar Vacaciones' class='btn btn-primary'  id='tab-button' class='btn ;' >" + "<span class='glyphicon glyphicon-trash'></span>" + "</a></div> ";          
        };       

        function LimpiarFiltros() {
            vm.FechaInicio = '';
            vm.FechaFinal = '';            
            vm.IdSolicitud = '';
            LimpiarGrilla();
            vm.Truncos = '0';
            vm.Vencidos = '0';
            vm.Gozados = '0';
            vm.Pagados = '0';
            vm.PendienteGoce = '0';
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

        function SeleccionaFechaDerecho() {
            if (vm.ListaFechaDerecho.DiasGozados != undefined) {
               vm.Gozados = vm.ListaFechaDerecho.DiasGozados,
               vm.Pagados = vm.ListaFechaDerecho.DiasPagados,
               vm.PendienteGoce = vm.ListaFechaDerecho.DiasPendientes
               BuscarProgramacion();
            };
        };

        function ObtenerDatosTrabajador() {
            LimpiarFiltros();
            var trabajador = {
                CodigoUsuario: vm.CodUsuario,
            };
            blockUI.start();
            var promise = ProgramacionService.ObtenerDatosTrabajador(trabajador);
            promise.then(function (response) {
                var Respuesta = response.data;
                vm.Empresa = response.data.TrabajadorDto.CodigoEmpresa;
                vm.Tipo = response.data.TrabajadorDto.TipoEmpleado;                
            }, function (response) {
                blockUI.stop();
            });            
            blockUI.stop();
            ListarFechaDerecho();
            ObtenerIndicadoresFuturo();
            BuscarProgramacion();
        };
        
        function ListarFechaDerecho() {
            var fechaderecho = {
                CodigoUsuario: vm.CodUsuario,
            };
            blockUI.start();
            var promise = ProgramacionService.ListarFechaDerecho(fechaderecho);
            promise.then(function (response) {
                var Respuesta = response.data;
                vm.FechaDerecho = UtilsFactory.AgregarItemSelectTodos(response.data.FechaDerechoDtoLista);                
            }, function (response) {
                blockUI.stop();
            });            
            blockUI.stop();
        };

        function ObtenerIndicadoresFuturo() {
            var vFechaInicio = "";
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
             }
            if (mm < 10) {
                mm = '0' + mm;
             }
            var vFechaInicio = dd + '/' + mm + '/' + yyyy;

            var programacion = {
                CodigoUsuario: vm.CodUsuario,
                FechaInicio: vFechaInicio,
            };
            var promise = ProgramacionService.ObtenerIndicadoresFuturo(programacion);
            promise.then(function (response) {
                var Respuesta = response.data;
                vm.Vencidos = response.data.IndicadorTrabajadorDto.Vencidos;
                vm.Truncos = response.data.IndicadorTrabajadorDto.Truncos;
            });
        };

        function CargaGoceVacacional() {
            var promise = ProgramacionService.ModalGoceVacacional();
            promise.then(function (response) {
                var respuesta = $(response.data);

                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope);
                    $("#ContenidoModalGoce").html(content);
                });

                $('#ModalDatosGoce').modal({
                    keyboard: false,
                    backdrop: 'static'
                });
            }, function (response) {
                blockUI.stop();
            });
        };

        function EditarGoce(fechainicio,fechafinal) {
            var promise = ProgramacionService.ModalEditarGoce();

            vm.FechaInicio = fechainicio;
            vm.FechaFinal = fechafinal;

            promise.then(function (response) {
                var respuesta = $(response.data);

                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope);
                    $("#ContenidoEditarGoce").html(content);
                });

                $('#ModalEditarGoce').modal({
                    keyboard: false,
                    backdrop: 'static'                    
                });
            }, function (response) {
                blockUI.stop();
            });
        };

      
    }
})();
