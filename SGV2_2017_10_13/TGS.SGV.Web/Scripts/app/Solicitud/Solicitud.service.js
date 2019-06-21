(function () {
    'use strict',
    angular
    .module('app.Solicitud')
    .service('SolicitudService', SolicitudService);

    SolicitudService.$inject = ['$http', 'URLS'];
    
    function SolicitudService($http, $urls) {

        var service = {
            ListarSolicitud: ListarSolicitud,
            RegistrarSolicitud: RegistrarSolicitud,
            EliminarSolicitud: EliminarSolicitud,
            FuncionRestarFechas: FuncionRestarFechas,
            ObtieneIndicadores: ObtieneIndicadores,
            ObtenerGoceFuturo: ObtenerGoceFuturo,
            ListarComboFeriado: ListarComboFeriado,
            RegistrarCombo: RegistrarCombo,
            ModalComboVacacional: ModalComboVacacional,
            ModalRegistrarSolicitud: ModalRegistrarSolicitud,
            ListarEvaluaciones: ListarEvaluaciones,
            ListarCCRPorIdEmpresa: ListarCCRPorIdEmpresa,
            AprobarEvaluacion: AprobarEvaluacion,
            AprobarEvaluacionFuturo: AprobarEvaluacionFuturo,
            ModalEvaluacionModificar: ModalEvaluacionModificar,
            ModalEvaluacionRechazar: ModalEvaluacionRechazar
        }
        return service;
        
        function ListarSolicitud(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "Bandeja/ListarSolicitud",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function RegistrarSolicitud(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "RegistrarSolicitud/RegistrarSolicitud",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function EliminarSolicitud(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "Bandeja/EliminarSolicitud",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function FuncionRestarFechas(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "RegistrarSolicitud/FuncionRestarFechas",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function ObtieneIndicadores(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "RegistrarSolicitud/ObtieneIndicadores",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function ObtenerGoceFuturo(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "RegistrarSolicitud/ObtenerGoceFuturo",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function ListarComboFeriado(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "ComboVacacional/ListarComboFeriado",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function RegistrarCombo(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "ComboVacacional/RegistrarCombo",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function ModalComboVacacional() {
            return $http({
                url: $urls.ApiSolicitud + "ComboVacacional/ModalComboVacacional",
                method: "GET" 
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
    
        function ModalRegistrarSolicitud() {
            return $http({
                url: $urls.ApiSolicitud + "RegistrarSolicitud/ModalRegistrarSolicitud",
                method: "GET"
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };


        //Módulo Evaluación de Solicitud
        function ListarEvaluaciones(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/ListarEvaluaciones",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
        function ListarCCRPorIdEmpresa(IdEmpresa) {
            console.log("Servicio 1");
            console.log(IdEmpresa);
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/ListarCCRPerfil",
                method: "POST",
                data: { IdEmpresa: IdEmpresa }
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
        function AprobarEvaluacion(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/AprobarEvaluacion",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);
            function DatosCompletados(response) {
                return response;
            }
        };
        function AprobarEvaluacionFuturo(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/AprobarEvaluacionFuturo",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);
            function DatosCompletados(response) {
                return response;
            }
        };
        function ModificarEvaluacion(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/ModificarEvaluacion",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
        function RechazarEvaluacion(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "BandejaEvaluacion/RechazarEvaluacion",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
        
        function ModalEvaluacionModificar(solicitud) {
            return $http({
                url: $urls.ApiSolicitud + "EvaluacionModificar/ModalEvaluacionModificar",
                method: "POST",
                data: JSON.stringify(solicitud)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
        function ModalEvaluacionRechazar() {
            return $http({
                url: $urls.ApiSolicitud + "EvaluacionRechazar/ModalEvaluacionRechazar",
                method: "GET"
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
    
    }
})();