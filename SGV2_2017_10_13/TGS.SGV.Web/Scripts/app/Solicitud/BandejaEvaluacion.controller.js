(function () {
    'use strict'

    angular
    .module('app.Solicitud')
    .controller('BandejaEvaluacionController', BandejaEvaluacionController);

    BandejaEvaluacionController.$inject = ['SolicitudService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile', '$modal', '$injector', 'UnidadService', '$templateCache'];

    function BandejaEvaluacionController(SolicitudService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile, $modal, $injector, UnidadService, $templateCache) {
        var vm = this;
        vm.EnlaceRegistrar = $urls.ApiEvaluacion + "Registrar/Index/";
        vm.BuscarEvaluacion = BuscarEvaluacion;
        //vm.ListarCCRPerfil = ListarCCRPerfil;
        vm.ListarCCRPorIdEmpresa = ListarCCRPorIdEmpresa;
        vm.LimpiarFiltros = LimpiarFiltros;
        vm.ValidarCampos = ValidarCampos;

        vm.MensajeDeAlerta = ""; 
        vm.AdicionarSolicitud = AdicionarSolicitud;
        vm.AprobarSolicitud = AprobarSolicitud;
        vm.ModificarSolicitud = ModificarSolicitud;
        vm.RechazarSolicitud = RechazarSolicitud;
        vm.ConfirmaAprobar = ConfirmaAprobar; 
        vm.ModalModificar = ModalModificar;
 
        vm.BuscarUnidad = BuscarUnidad;
 
        vm.ModalRechazar = ModalRechazar;
 
        vm.CodigoEmpresa = '';
        vm.CodigoCCR = '';
        vm.IncluirSubUnidad = '';
        vm.Hijos = '';
        vm.CipEmpleado = "";
        vm.Nombres = "";
        vm.APaterno = "";
        vm.AMaterno = "";
        vm.CodigoTipo = "";
        vm.FechaSolicitudInicio = "";
        vm.FechaSolicitudFin = "";

        vm.EstGoceFut = "";
        vm.DiaCorte1 = "";
        vm.FechaCorte1 = "";
        vm.DiaHoy = "";
        vm.SinFechaCorte = false;

        vm.CodigoUsuario = '';
        vm.CodigoPerfil = '';
        vm.CodigoSolicitud = '';

        vm.ListEmpresas = [];
        vm.ListCCR = [];
        vm.ListTipos = [];

        vm.ListEstados = [];
        vm.ListEstados = UtilsFactory.AgregarItemSelect([]);
        vm.IdSolicitud = '';
        vm.CodigoEmpresa = '-1';

        //Carga Datos Iniciales
        vm.CodigoTipoPago == "";

        vm.ListaCodigoSolicitud = [];

        //Datos de Modificar
        vm.MCipEmpleado = "";
        vm.MFechaInicio = "";
        vm.MFechaFin = "";
        vm.MDiaCalendario = "";
        vm.MAdelantoVacacional = "";


        //Seleccion de Checkboxes
        vm.selected = {};
        vm.selectAll = false;
        vm.toggleAll = toggleAll;
        vm.toggleOne = toggleOne;
        vm.dtInstance = {};
        var titleHtml = '<input type="checkbox" ng-model="vm.selectAll" ng-click="vm.toggleAll(vm.selectAll, vm.selected)">';
        var titleHtmlP = '<input type="checkbox" ng-model="vm.selectAll" ng-change="vm.toggleAll(vm.selectAll, vm.checkedSymbol)">';

        vm.dtColumns = [
            DTColumnBuilder.newColumn(null).withTitle('<input type="checkbox" ng-model="selectAll" icheck>')
                .notSortable().withOption('width', '3%')
                .renderWith(function (data, type, full, meta) {
                    vm.selected[full.CodigoSolicitud] = false; 
                    var DataSeleccion = data.CodigoSolicitud + '|' + data.CipEmpleado + '|' + data.CodigoTipo + '|' + data.FechaInicio + '|' + data.FechaFin + '|' + data.CodigoEstado + '|' + data.Dias + '|' + data.Adelanto + '|' + data.TipConfig;
                    return "<div class='col-xs-6 col-sm-6'><input id='idIncluirSubUnidad0' type='checkbox' onclick='AdicionaEvaluacion(\"" + DataSeleccion + "\");'/></div> ";
                }),
          DTColumnBuilder.newColumn('CipEmpleado').withTitle('CIP').notSortable().withOption('width', '6%'),
          DTColumnBuilder.newColumn('Empleado').withTitle('Nombres').notSortable().withOption('width', '20%'),
          DTColumnBuilder.newColumn('TipEmpleado').withTitle('Tipo Empleado').notSortable().withOption('width', '6%'),
          //DTColumnBuilder.newColumn('CodigoSolicitud').withTitle('Solicitud').notSortable().withOption('width', '20%'),
          DTColumnBuilder.newColumn('FechaSolicitud').withTitle('Fecha Solicitud').notSortable().withOption('width', '6%'),
          DTColumnBuilder.newColumn('FechaInicio').withTitle('Fecha Inicio').notSortable().withOption('width', '6%'),
          DTColumnBuilder.newColumn('FechaFin').withTitle('Fecha Fin').notSortable().withOption('width', '6%'),
          DTColumnBuilder.newColumn('EstadoSolicitud').withTitle('Estado').notSortable().withOption('width', '6%'),
          DTColumnBuilder.newColumn('Gerencia').withTitle('Gerencia').notSortable().withOption('width', '20%'),
          DTColumnBuilder.newColumn('Unidad').withTitle('CCR').notSortable().withOption('width', '20%'),
          DTColumnBuilder.newColumn('MandoResponsable').withTitle('MandoResponsable').notSortable().withOption('width', '20%'),
          DTColumnBuilder.newColumn('Tipo').withTitle('Tipo').notSortable().withOption('width', '10%')
        ];
        
        LimpiarGrilla();
        CargarDatos();
        FijarPrivilegios();
        
        function CargarDatos() {
            
            if (jsonListEstados != null) {
 
                //vm.ListEmpresas = UtilsFactory.AgregarItemSelect(jsonListaEmpresas);
                vm.CodigoTipoPago = jsonDatosTrabajador.TrabajadorDto.CodigoTipoPago;
                if (vm.CodigoTipoPago == "") vm.CodigoTipoPago = "0";

                vm.ListEmpresas = jsonListaEmpresas;
 
                //vm.ListCCR = jsonListaCCR;
                vm.ListTipos = UtilsFactory.AgregarItemSelectTodos(jsonListaTipos);

                vm.CodigoEmpresa = "TDP";
                vm.CodigoTipo = "0";

                vm.ListEstados = UtilsFactory.AgregarItemSelect(jsonListEstados);

                vm.EstGoceFut = jsonEstGoceFut;
                vm.DiaCorte1 = jsonDiaCorte1;
                vm.FechaCorte1 = jsonFechaCorte1;
                vm.SinFechaCorte = jsonSinFechaCorte;
                vm.DiaHoy = jsonDiaHoy;

                console.log(vm.DiaCorte1);
                console.log(vm.FechaCorte1);
                console.log(vm.DiaHoy);
            }
        };
        function FijarPrivilegios() {

            var MostrarBotones = true
            vm.MostrarAprobar = MostrarBotones;
            vm.MostrarModificar = MostrarBotones;
            vm.MostrarRechazar = MostrarBotones;

        };
        
        function toggleAll(selectAll, selectedItems) {
            alert("Select Todos");
            for (var id in selectedItems) {
                if (selectedItems.hasOwnProperty(id)) {
                    selectedItems[id] = selectAll;
                }
            }
        };
        function toggleOne(selectedItems) {
            alert("Select Rows");
            for (var id in selectedItems) {
                if (selectedItems.hasOwnProperty(id)) {
                    if (!selectedItems[id]) {
                        vm.selectAll = false;
                        return;
                    }
                }
            }
            vm.selectAll = true;
        };
        
        function BuscarEvaluacion() {
            blockUI.start();
            LimpiarGrilla();
            $timeout(function () {
                vm.dtOptions = DTOptionsBuilder
                .newOptions().withOption('bFilter', false)
                .withOption('responsive', true)
                .withOption('order', [])
                .withFnServerData(BuscarEvaluacionPaginado)
                .withDataProp('data')
                .withOption('serverSide', true)
                .withOption('paging', true)
                .withOption('destroy', true)
                .withPaginationType('full_numbers')
                .withDisplayLength(10);
            }, 500);
        };

        function BuscarEvaluacionPaginado(sSource, aoData, fnCallback, oSettings) {
            console.log('BuscarEvaluacionPaginado');
            var draw = aoData[0].value;
            var start = aoData[3].value;
            var length = aoData[4].value;
            var evaluacion = {
                CodigoEmpresa: vm.CodigoEmpresa,
                CodigoCCR: vm.CodigoCCR,
                Hijos: vm.Hijos,
                CipEmpleado: vm.CipEmpleado,
                Nombres: vm.Nombres,
                APaterno: vm.APaterno,
                AMaterno: vm.AMaterno,
                CodigoTipo: vm.CodigoTipo,
                FechaSolicitudInicio: vm.FechaSolicitudInicio,
                FechaSolicitudFin: vm.FechaSolicitudFin,
                Indice: start,
                Tamanio: start + length
            };
            var num = ValidarCampos();
            if (num == 0) {
                vm.ListaCodigoSolicitud = [];
                var promise = SolicitudService.ListarEvaluaciones(evaluacion);
                promise.then(function (response) {
                    console.log('Lista Grilla');
                    var records = {
                        'draw': draw,
                        'recordsTotal': response.data.Total,
                        'recordsFiltered': response.data.Total,
                        'data': response.data.ListarEvaluacionSolicitudDto
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

        function AccionarSeleccion(data, type, full, meta) {
            return "<div class='col-xs-6 col-sm-6'><input id='idIncluirSubUnidad0' type='checkbox' onclick='AdicionarEvaluacion(\"" + data.CodigoSolicitud + "\");'/></div> ";
        };
        function LimpiarFiltros() {
            vm.CodigoEmpresa = '';
            vm.CodigoCCR = '';
            vm.Hijos = '';
            vm.CIP = "";
            vm.Nombres = '';
            vm.APaterno = '';
            vm.AMaterno = '';
            vm.CodigoEstado = '';
            vm.CodigoTipo = '';
            vm.FechaSolicitudInicio = '';
            vm.FechaSolicitudFin = '';
            vm.CodigoUsuario = '';
            vm.CodigoPerfil = '';
            vm.CodigoSolicitud = '';
            LimpiarGrilla();
        };

        function ListarCCRPorIdEmpresa() {
            vm.Unidad = '';
        };

        function Pad(n, length) {
            var n = n.toString();
            while (n.length < length)
                n = "0" + n;
            return n;
        }


        function ValidarCampos() {
            var num = 0;
            if ($.trim(vm.CodigoEmpresa) == "-1" || $.trim(vm.CodigoEmpresa) == "") {
                vm.MensajeDeAlerta = vm.MensajeDeAlerta + "Por favor seleccione un Tipo" + '<br/>';
                UtilsFactory.InputBorderColor('#IdListEmpresas', 'Rojo');
                num = 1;
            }
            return num;
        }; 

        function ConfirmaAprobar() {
            //AQUI AGREGAR MODIFACACIONES
            var strCodSolicitud = "";
            var strCipSolicitud = "";
            var strTipoSolicitud = "";
            var strFechaInicio = "";
            var intNumSel = 0;
            var strEstado = "";
            var blnAprobar = true;
            var strTipo = "";
            var strTipoI = "";
            var blnTipo = true;
            var strAprobacionFueraFecha = "";
            var bAprobacionFueraFecha = false;
            var strFechaMaxima = "";
            var strFechaCorte = "";
            var strTipConfig = "";
            var FechaMinima = "";
            var blnOrden = true;

            var strMensaje = "";

            var CadenaSeleccion = "";
            var arrayCol = [];
            for (var id in vm.ListaCodigoSolicitud) {
                intNumSel++;
                console.log(vm.ListaCodigoSolicitud[id]);
                //CadenaSeleccion += vm.ListaCodigoSolicitud[id] + ',';
                arrayCol = vm.ListaCodigoSolicitud[id].split('|');
                strCodSolicitud = arrayCol[0];
                strCipSolicitud = arrayCol[1];
                strTipoSolicitud = arrayCol[2];

                strTipo = arrayCol[2];
                strFechaInicio = arrayCol[3].split(' ').join('');
                strEstado = arrayCol[5];

                if (intNumSel <= 1) {
                    CadenaSeleccion = strCodSolicitud + '|' + strCipSolicitud + '|' + strTipoSolicitud;
                }                
                else {
                    CadenaSeleccion += ',' + strCodSolicitud + '|' + strCipSolicitud + '|' + strTipoSolicitud;
                }

                console.log(strTipoSolicitud);
                if (vm.SinFechaCorte == false && strTipoSolicitud != "E" && strTipoSolicitud != "R")
                {
                    var resY = strFechaInicio.substring(6, 10);
                    var resM = strFechaInicio.substring(3, 5);
                    var resD = strFechaInicio.substring(0, 2);
                    console.log(resY);
                    console.log(resM);
                    console.log(resD);
                    //vm.DiaHoy
                    //vm.DiaCorte1 = jsonDiaCorte1;
                    //vm.SinFechaCorte = jsonSinFechaCorte;

                    var corte_finmes = resY + resM + vm.DiaCorte1;
                    var fecha_hoy = vm.DiaHoy;
                    console.log(vm.DiaHoy);
                    if (corte_finmes <= fecha_hoy) {
                        bAprobacionFueraFecha = true;

                    }
                    strFechaMaxima = vm.DiaCorte1 + "/" + fecha_hoy.substring(4, 6) + "/" + fecha_hoy.substring(0, 4);
                    console.log(strFechaMaxima);
                }

                if (strTipoI == '') strTipoI = strTipo;
                if (strTipoI != strTipo) {
                    blnTipo = false;
                }
                if (strEstado != '02') {
                    blnAprobar = false;
                }

            }

            if (blnOrden == false) {
                strMensaje = 'Debe aprobar las solicitudes de fecha de inicio más antigua a más reciente para:\nUsuario: ';
            }
            if (intNumSel == 0) {
                strMensaje = 'Seleccione el registro a aprobar';
            }
            if (blnAprobar == false) {
                strMensaje = 'Algunas de la(s) solicitud(es) seleccionada(s) no puede(n) ser APROBADA(S) porque no se encuentra(n) en estado Pendiente.';
            }
            if (blnTipo == false) {
                strMensaje = 'Solo puede aprobar masivamente solicitudes del mismo tipo. Por favor verifique.';
            }

            if (strMensaje != "")
            {
                UtilsFactory.Alerta('#divAlert', 'danger', strMensaje, 5);
                return false;
            }

            if (vm.CodigoTipoPago == "1")
	        {	
                if (vm.CodigoTipo == "S" || vm.CodigoTipo == "I") {
			        if (bAprobacionFueraFecha == true) {
			            strMensaje = "Como está aprobando una solicitud en un fecha posterior a la fecha de corte " + vm.FechaCorte1 + ", el pago del concepto de vacaciones se traslada a la nómina siguiente, y por lo tanto se pierde la oportunidad del adelanto.\n\n¿Está seguro de aprobar la solicitud seleccionada.?";
			        }
			        else {
			            strMensaje = '¿Está seguro de aprobar la solicitud seleccionada.?';
			        }
		        }
		        else {
                    strMensaje = '¿Está seguro de aprobar la solicitud seleccionada.?';
		        }
	        }
	        else
	        {
                strMensaje = '¿Está seguro de aprobar la solicitud seleccionada.?';
	        }

            if (confirm(strMensaje))
            {
                var prefixDir = '';
		        if(strTipConfig=='01' && strTipo == 'R')
		        {
		            UtilsFactory.Alerta('#divAlert', 'danger', 'Entro a directivo', 5);
		            return;
			        
		        }
		        if (strTipConfig == '01') {
		            UtilsFactory.Alerta('#divAlert', 'danger', 'Entro a directivo tipo config', 5);
		            return;

		        }
		        else {
		            
		            console.log('EstGoceFut: ' + vm.EstGoceFut);
		            console.log('evaluacion = {CodigoEmpresa: ' + vm.CodigoEmpresa + ',TipoSolicitud: ' + strTipo + ',CodigoSolicitud: ' + CadenaSeleccion + '};');
		            var evaluacion = {
		                CodigoEmpresa: vm.CodigoEmpresa,
		                TipoSolicitud: strTipo,
		                CodigoSolicitud: CadenaSeleccion
		            };
		            if (vm.EstGoceFut != 'S') {
		                //Reprog
		                blockUI.start();
		                console.log("AprobarEvaluacion");
		                var promise = SolicitudService.AprobarEvaluacion(evaluacion);
		                promise.then(function (response) {
		                    var Respuesta = response.data;
		                    console.log(Respuesta.TipoRespuesta);
		                    if (Respuesta.TipoRespuesta != 'Ok') {
		                        blockUI.stop();
		                        for (var id in Respuesta.Errores) {
		                            UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[id], 15);

		                        }
		                        //UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 15);
		                    } else {
		                        UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosOk, 5);
		                        LimpiarGrilla();
		                        blockUI.stop();
		                        $timeout(function () {
		                            BuscarEvaluacion();
		                        }, 1000);
		                    }
		                }, function (response) {
		                    blockUI.stop();
		                    UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
		                });

		            }
		            else {
		                blockUI.start();
		                console.log("AprobarEvaluacionFuturo");
		                var promise = SolicitudService.AprobarEvaluacionFuturo(evaluacion);
		                promise.then(function (response) {
		                    var Respuesta = response.data;
		                    console.log(Respuesta.TipoRespuesta);
		                    if (Respuesta.TipoRespuesta != 'Ok') {
		                        blockUI.stop();
		                        UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 15);
		                    } else {
		                        UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosOk, 5);
		                        LimpiarGrilla();
		                        blockUI.stop();
		                        $timeout(function () {
		                            BuscarEvaluacion();
		                        }, 1000);
		                    }
		                }, function (response) {
		                    blockUI.stop();
		                    UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
		                });
		            }
		        }
                
		        return;
                
            }
        };

        function ModalModificar() {
            var intNumSel = 0;
            var strCodSolicitud = "";
            var strCipEmpleado = "";
            var strTipoSolicitud = "";
            var strTipo = "";
            var strFechaInicio = "";
            var strFechaFin = "";
            var strEstado = "";
            var strDias = "";
            var strAdelanto = "";
            var strTipConfig = "";
            var strMensaje = "";
            var arrayCol = [];
            for (var id in vm.ListaCodigoSolicitud) {
                intNumSel++;
                console.log(vm.ListaCodigoSolicitud[id]);
                arrayCol = vm.ListaCodigoSolicitud[id].split('|');
                strCodSolicitud = arrayCol[0];
                strCipEmpleado = arrayCol[1];
                strTipoSolicitud = arrayCol[2];

                strTipo = arrayCol[2];
                strFechaInicio = arrayCol[3].split(' ').join('');
                strFechaFin = arrayCol[4].split(' ').join('');
                strEstado = arrayCol[5];
                strDias = arrayCol[6];
                strAdelanto = arrayCol[7];
                strTipConfig = arrayCol[8];

            }

            if (intNumSel == 0) {
                strMensaje = 'Seleccione el registro a Modificar';
            }
            if (intNumSel > 1) {
                strMensaje = 'Solo seleccione un registro';
            }
            console.log(strEstado);
            if (strEstado != '' && strEstado != '02') {
                strMensaje = 'Solo puede MODIFICAR una solicitud en estado Pendiente.';
            }
            if (strMensaje != "") {
                UtilsFactory.Alerta('#divAlert', 'danger', strMensaje, 10);
                return false;
            }

            var prefixUrl = ""
            if (strTipConfig == '01') {
                prefixUrl = "_dir";
            }

            console.log('evaluacion = {CodigoEmpresa: ' + vm.CodigoEmpresa + ', TipoSolicitud: ' + strTipo + ', CodigoSolicitud: ' + strCodSolicitud + ', FechaSolicitudInicio: ' + strFechaInicio + ', FechaSolicitudFin: ' + strFechaFin);
            console.log('Dias: ' + strDias + ', Adelanto: ' + strAdelanto + ', CipEmpleado: ' + strCipEmpleado + ', Tipo: ' + strTipo + '};');
                        
            vm.MCipEmpleado = strCipEmpleado;
            vm.MFechaInicio = strFechaInicio;
            vm.MFechaFin = strFechaFin;
            vm.MDiaCalendario = strDias;
            vm.MAdelantoVacacional = strAdelanto;
            
            var evaluacion = {
                CodigoEmpresa: vm.CodigoEmpresa,
                TipoSolicitud: strTipo,
                CodigoSolicitud: strCodSolicitud,
                FechaInicio: strFechaInicio,
                FechaFin: strFechaFin,
                Dias: strDias,
                Adelanto: strAdelanto,
                CipEmpleado: strCipEmpleado,
                Tipo: strTipo

            };

            if (strTipo == 'R') {
                
            }
            else {
                if (vm.EstGoceFut != 'S') {
                    
                }
                else {
                    
                }
            }

            var promise = SolicitudService.ModalEvaluacionModificar(evaluacion);
            promise.then(function (response) {
                var respuesta = $(response.data);
                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope);
                    $("#ContenidoModalDatos").html(content);
                });

                $('#ModalDatos').modal({
                    keyboard: false,
                    backdrop: 'static'
                });
            }, function (response) {
                blockUI.stop();
            });
        }; 

        function ModalRechazar() {
            var intNumSel = 0;
            var strMensaje = "";
            for (var id in vm.ListaCodigoSolicitud) {
                intNumSel++;
                console.log(vm.ListaCodigoSolicitud[id]);

            }

            if (intNumSel == 0) {
                strMensaje = 'Seleccione el registro a Rechazar';
            }
            if (strMensaje != "") {
                UtilsFactory.Alerta('#divAlert', 'danger', strMensaje, 10);
                return false;
            }

            var promise = SolicitudService.ModalEvaluacionRechazar();
            promise.then(function (response) {
                var respuesta = $(response.data);
                $injector.invoke(function ($compile) {
                    var div = $compile(respuesta);
                    var content = div($scope);
                    $("#ContenidoModalDatos").html(content);
                });

                $('#ModalDatos').modal({
                    keyboard: false,
                    backdrop: 'static'
                });
            }, function (response) {
                blockUI.stop();
            });
        };

        function AdicionarSolicitud(CodigoSolicitud) {
            var tmpLista = [];
            if (vm.ListaCodigoSolicitud.indexOf(CodigoSolicitud) !== -1) {
                console.log("Pas 1");
                for (var id in vm.ListaCodigoSolicitud) {
                    if (vm.ListaCodigoSolicitud[id] == CodigoSolicitud) {

                    }
                    else {
                        tmpLista.push(vm.ListaCodigoSolicitud[id]);
                    }
                }
                vm.ListaCodigoSolicitud = tmpLista;
            }
            else {
                console.log("Pas 2");
                vm.ListaCodigoSolicitud.push(CodigoSolicitud);
            }
            
            //vm.ListaCodigoSolicitud = tmpLista;
            //alert(vm.ListaCodigoSolicitud.length);
            for (var id in vm.ListaCodigoSolicitud) {
                console.log(vm.ListaCodigoSolicitud[id]);
            }

        };

        function AprobarSolicitud(CodigoSolicitud) {
            return;
            var Solicitu = Pad(CodigoSolicitud, 10);

            var evaluacion = {
                CodigoSolicitud: vm.CodigoSolicitud,
                CodigoUsuario: vm.CodigoUsuario,
            };
            blockUI.start();
            var promise = SolicitudService.AprobarEvaluacion(evaluacion);
            promise.then(function (response) {
                var Respuesta = response.data;
                if (Respuesta.Tipo != 0) {
                    blockUI.stop();
                    UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 5);
                } else {
                    UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosDeleteOk, 5);
                    LimpiarGrilla();
                    blockUI.stop();
                    $timeout(function () {
                        BuscarEvaluacion();
                    }, 1000);
                }
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
            });
        };
        function ModificarSolicitud(IdSolicitud) {

            var Solicitu = Pad(IdSolicitud, 10);

            var evaluacion = {
                CodigoSolicitud: vm.CodigoSolicitud,
                MFechaDesde: vm.MFechaDesde,
                MFechaHasta: vm.MFechaHasta,
                MDiaCalendario: vm.MDiaCalendario,
                MAdelantoVacacional: vm.MAdelantoVacacional,
                CodigoUsuario: vm.CodigoUsuario,
            };
            blockUI.start();
            var promise = SolicitudService.ModificarEvaluacion(evaluacion);
            promise.then(function (response) {
                var Respuesta = response.data;
                if (Respuesta.Tipo != 0) {
                    blockUI.stop();
                    UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 5);
                } else {
                    UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosDeleteOk, 5);
                    LimpiarGrilla();
                    blockUI.stop();
                    $timeout(function () {
                        BuscarEvaluacion();
                    }, 1000);
                }
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
            });
        };
        function RechazarSolicitud(IdSolicitud) {

            var Solicitu = Pad(IdSolicitud, 10);

            var evaluacion = {
                CodigoSolicitud: vm.CodigoSolicitud,
                CodigoUsuario: vm.CodigoUsuario,
            };
            blockUI.start();
            var promise = SolicitudService.RechazarEvaluacion(evaluacion);
            promise.then(function (response) {
                var Respuesta = response.data;
                if (Respuesta.Tipo != 0) {
                    blockUI.stop();
                    UtilsFactory.Alerta('#divAlert', 'danger', Respuesta.Errores[0], 5);
                } else {
                    UtilsFactory.Alerta('#divAlert', 'success', MensajesUI.DatosDeleteOk, 5);
                    LimpiarGrilla();
                    blockUI.stop();
                    $timeout(function () {
                        BuscarEvaluacion();
                    }, 1000);
                }
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', MensajesUI.DatosError, 5);
            });
        };

        function DesaparecerEfectosDeValidacion() {
            UtilsFactory.InputBorderColor('#IdListEmpresas', 'combo');
            UtilsFactory.InputBorderColor('#idCCR', 'Ninguno');
            UtilsFactory.InputBorderColor('#idIncluirSubUnidad', 'Ninguno');
            UtilsFactory.InputBorderColor('#idCIP', 'Ninguno');
            UtilsFactory.InputBorderColor('#idNombres', 'Ninguno');
            UtilsFactory.InputBorderColor('#idAPaterno', 'Ninguno');
            UtilsFactory.InputBorderColor('#idAMaterno', 'Ninguno');
            UtilsFactory.InputBorderColor('#idTipo', 'combo');
            UtilsFactory.InputBorderColor('#idFechaInicio', 'Ninguno');
            UtilsFactory.InputBorderColor('#idFechaFinal', 'Ninguno');
        };

        vm.Unidad = ''; 

        function BuscarUnidad(unidad) {
            
            if (parseInt(unidad.length) < 2)
            {
                return false;
            }

            var UnidadDto = {
                NombreUnidad:unidad,
                CodigoEmpresa:vm.CodigoEmpresa
            };

            return UnidadService.ListarCCRPerfil(UnidadDto).then(function (response) {
                return response.data;
            }); 
        };


       
    }

})();