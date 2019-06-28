(function () {
    'use strict'
   
    angular
    .module('app.Solicitud')
    .controller('EvaluacionModificarController', EvaluacionModificarController);

    EvaluacionModificarController.$inject = ['SolicitudService', 'blockUI', 'UtilsFactory', 'DTOptionsBuilder', 'DTColumnBuilder','$timeout', 'MensajesUI', 'URLS', '$scope', '$compile'];

    function EvaluacionModificarController(SolicitudService, blockUI, UtilsFactory, DTOptionsBuilder, DTColumnBuilder, $timeout, MensajesUI, $urls, $scope, $compile ) {
        var vmx = this;
        vmx.ModificarSolicitud = ModificarSolicitud;
        vmx.MCipEmpleado = '';
        vmx.SigFechaDerecho = '';
        vmx.Pendientes = '';
        vmx.Vencidos = '';
        vmx.Truncos = '';
        vmx.Futuros = '';

        vmx.MFechaInicio = '';
        vmx.MFechaFin = '';

        vmx.MDiaCalendario = '';
        vmx.MAdelantoVacacional = '';
        CargarDatos();
        
        function CargarDatos() {
            //alert(jsonCipEmpleado)
            //vmx.MCipEmpleado = vm.MCipEmpleado;
            //alert(vmx.MCipEmpleado);
            /*
            if (jsonFechaGoceDto != null) {
                alert("Paso 1");
                vmx.SigFechaDerecho = jsonFechaGoceDto.FechaDerecho;
            }
            if (jsonDatosModifica != null) {
                alert("Paso 2");
                //vm.SigFechaDerecho = jsonDatosModifica.FechadeDerecho;
                vmx.Pendientes = jsonDatosModifica.Pendientes;
                vmx.Vencidos = jsonDatosModifica.Vencidos;
                vmx.Truncos = jsonDatosModifica.Truncos;
                vmx.Futuros = jsonDatosModifica.GoceFuturo;

            }
            if (jsonDatosEvaluacion != null) {
                alert("Paso 3");
                vmx.MFechaDesde = jsonDatosEvaluacion.FechaSolicitudInicio;
                vmx.MFechaHasta = jsonDatosEvaluacion.FechaSolicitudFin;

                vmx.MDiaCalendario = jsonDatosEvaluacion.Dias;
                vmx.MAdelantoVacacional = jsonDatosEvaluacion.Adelanto;

            }
            */
        };
        
        function ModificarSolicitud() {
            var num = 0;
            
            return num;
        };

    }

})();