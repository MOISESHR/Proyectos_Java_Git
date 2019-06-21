(function () {
    'use strict'

    angular
    .module('app.Solicitud')
    .controller('RegistrarSolicitudController', RegistrarSolicitudController);
        
    RegistrarSolicitudController.$inject = ['SolicitudService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder', '$timeout', 'MensajesUI', 'URLS', '$scope', '$compile'];

    function RegistrarSolicitudController(SolicitudService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile) {
        var vmc = this;
        
        vmc.RestarFechas = RestarFechas;        
        vmc.LimpiarCampos = LimpiarCampos;
        vmc.RegistroSolicitud = RegistroSolicitud;

        vmc.ObtenerGoceFuturo = ObtenerGoceFuturo;
        vmc.ObtieneIndicadores = ObtieneIndicadores;
        
        vmc.DesaparecerEfectosDeValidacion = DesaparecerEfectosDeValidacion;
        vmc.ValidarCampos = ValidarCampos;

        vmc.CodigoUsuario = "";
        vmc.FechaInicioRegistro = "";
        vmc.FechaFinalRegistro = "";

        vmc.FechaDerecho = ""; 

        vmc.Pendientes = ""; 
        vmc.Vencidas = ""; 
        vmc.Truncos = ""; 
        vmc.Futuros = ""; 
        vmc.Disponibles = ""; 
        
        ObtieneIndicadores();
        ObtenerGoceFuturo();

        function LimpiarCampos() {            
            vmc.FechaInicioRegistro = "";
            vmc.FechaFinalRegistro = "";
            vmc.CalculaDias = "0";
        };

        function RestarFechas() {
            var sinfechafinal = "N";
            var sinfechainicio = "N";
            var calcularesta = true;
            vmc.CalculaDias = 0;
            if (vmc.FechaInicioRegistro != "") {
                if (vmc.FechaFinalRegistro == "") {
                    vmc.FechaFinalRegistro = vmc.FechaInicioRegistro;
                    sinfechafinal = "S";
                }
            };
            if (vmc.FechaFinalRegistro != "") {
                if (vmc.FechaInicioRegistro == "") {
                    vmc.FechaInicioRegistro = vmc.FechaFinalRegistro;
                    sinfechainicio = "S";
                }
            };
            if (vmc.FechaInicioRegistro == "-1" || vmc.FechaInicioRegistro == ""  || vmc.FechaInicioRegistro == undefined) {
                calcularesta = false;
            }
            if (vmc.FechaFinalRegistro == "-1" || vmc.FechaFinalRegistro == "" || vmc.FechaFinalRegistro == undefined) {
                calcularesta = false;
            }
            if (!UtilsFactory.ValidarFecha(vmc.FechaInicioRegistro)) {
                calcularesta = false;
            }
            if (!UtilsFactory.ValidarFecha(vmc.FechaFinalRegistro)) {
                calcularesta = false;
            }
            if (calcularesta==true) {
                var solicitud = {                    
                    FechaInicio: vmc.FechaInicioRegistro,
                    FechaFinal: vmc.FechaFinalRegistro,
                };               
                var promise = SolicitudService.FuncionRestarFechas(solicitud);                
                promise.then(function (response) {                    
                    var Respuesta = response.data;
                    vmc.CalculaDias = Respuesta;                    
                }, function (response) {                 
                });
            }
            if (sinfechafinal == "S") {
                vmc.FechaFinalRegistro = "";
            }
            if (sinfechainicio == "S") {
                vmc.FechaInicioRegistro = "";
            }
            ObtieneIndicadores();
        };

        function RegistroSolicitud() {
            var solicitud = {
                CodigoEmpleado: vmc.CodigoUsuario,
                FechaInicio: vmc.FechaInicioRegistro,
                FechaFinal: vmc.FechaFinalRegistro,
                NumeroDias: vmc.CalculaDias, 
                FlagAdelanto: null, 
                CodigoUsuario: null, 
                TipoSolicitud: null, 
                Responsable: null,   
            };

            var mensaje = ValidarCampos();
            if (mensaje == "") {
                if (confirm('¿Estas seguro de grabar el registro ?')) {
                blockUI.start();
                var promise = SolicitudService.RegistrarSolicitud(solicitud);
                promise.then(function (response) {
                    blockUI.stop();
                    var Respuesta = response.data;
                    if (Respuesta.Tipo != 0) {
                        UtilsFactory.Alerta('#divAlertRegistrar', 'danger', Respuesta.Errores[0], 20);
                    } else {
                        if (vmc.Id == "") {
                            LimpiarCampos();
                        }
                        UtilsFactory.Alerta('#divAlertRegistrar', 'success', MensajesUI.DatosOk, 5);
                    }                    
                }, function (response) {
                    blockUI.stop();
                    UtilsFactory.Alerta('#divAlertRegistrar', 'danger', MensajesUI.DatosError, 3);
                });
                };
            }
            else {
                UtilsFactory.Alerta('#divAlertRegistrar', 'danger', mensaje, 3);
                $timeout(function () {
                    DesaparecerEfectosDeValidacion();
                }, 3000);
            }
        };
           
        function ObtenerGoceFuturo() {
            var solicitud = {
                CodigoUsuario: vmc.CodigoUsuario,
            };
            blockUI.start();
            var promise = SolicitudService.ObtenerGoceFuturo(solicitud);
            promise.then(function (response) {
                var Respuesta = response.data;
                vmc.FechaDerecho = response.data.FechaGoceDto.FechadeDerecho;
            }, function (response) {

                blockUI.stop();
            });
            blockUI.stop();
        };

        function ObtieneIndicadores() {
            var vFechaInicio = "";
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (vmc.FechaInicioRegistro != "") {
                vFechaInicio = vmc.FechaInicioRegistro;
            }
            else {
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                var vFechaInicio = dd + '/' + mm + '/' + yyyy;
            }

            var solicitud = {
                CodigoUsuario: vmc.CodigoUsuario,
                FechaInicio: vFechaInicio,
            };            
            var promise = SolicitudService.ObtieneIndicadores(solicitud);
            promise.then(function (response) {                
                var Respuesta = response.data;
                vmc.Pendientes = Respuesta.IndicadorTrabajadorDto.Pendientes;
                vmc.Vencidas = response.data.IndicadorTrabajadorDto.Vencidos;
                vmc.Truncos = response.data.IndicadorTrabajadorDto.Truncos;
                vmc.Futuros = response.data.IndicadorTrabajadorDto.GoceFuturo;
                vmc.Disponibles = response.data.IndicadorTrabajadorDto.DiasDisponible;
            });            
        };

        function ValidarCampos() {
            var mensaje = "";
            if ($.trim(vmc.FechaInicioRegistro) == "") {
                mensaje = mensaje + "Ingrese Fecha Inicio" + '<br/>';
                UtilsFactory.InputBorderColor('#idFechaInicioRegistro', 'Rojo');
            }
            if ($.trim(vmc.FechaFinalRegistro) == "") {
                mensaje = mensaje + "Ingrese Fecha Final" + '<br/>';
                UtilsFactory.InputBorderColor('#idFechaFinalRegistro', 'Rojo');
            }
            if ($.trim(vmc.FechaInicioRegistro) > $.trim(vmc.FechaFinalRegistro)) {
                mensaje = mensaje + "La Fecha de Inicio debe ser menor que la Fecha Final " + '<br/>';
                UtilsFactory.InputBorderColor('#idFechaInicioRegistro', 'Rojo');
                UtilsFactory.InputBorderColor('#idFechaFinalRegistro', 'Rojo');
            }
            return mensaje;
        };

        function DesaparecerEfectosDeValidacion() {
            UtilsFactory.InputBorderColor('#idFechaInicioRegistro', 'Ninguno');
            UtilsFactory.InputBorderColor('#idFechaFinalRegistro', 'Ninguno');
        };
    }
})();