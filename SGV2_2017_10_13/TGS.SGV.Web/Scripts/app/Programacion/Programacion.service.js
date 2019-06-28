(function () {
    'use strict',
    angular
    .module('app.Programacion')
    .service('ProgramacionService', ProgramacionService);

    ProgramacionService.$inject = ['$http', 'URLS'];

    function ProgramacionService($http, $urls) {

        var service = {
            ListarObtenerGoce: ListarObtenerGoce,
            ModalGoceVacacional: ModalGoceVacacional,
            ModalEditarGoce: ModalEditarGoce,
            ObtenerDatosTrabajador: ObtenerDatosTrabajador,
            ListarFechaDerecho: ListarFechaDerecho,
            ObtenerIndicadoresFuturo: ObtenerIndicadoresFuturo,
        }
        return service;

        function ListarObtenerGoce(programacion) {
            return $http({
                url: $urls.ApiProgramacion + "BandejaProgramacion/ListarObtenerGoce",
                method: "POST",
                data: JSON.stringify(programacion)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function ModalGoceVacacional() {
            return $http({
                url: $urls.ApiProgramacion + "GoceVacacional/ModalGoceVacacional",
                method: "GET" 
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

       function ModalEditarGoce() {
            return $http({
                url: $urls.ApiProgramacion + "EditarGoce/ModalEditarGoce",
                method: "GET"                
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
       };

       function ObtenerDatosTrabajador(programacion) {
           return $http({
               url: $urls.ApiProgramacion + "BandejaProgramacion/ObtenerDatosTrabajador",
               method: "POST",
               data: JSON.stringify(programacion)
           }).then(DatosCompletados);

           function DatosCompletados(response) {
               return response;
           }
       };

       function ObtenerIndicadoresFuturo(programacion) {
           return $http({
               url: $urls.ApiSolicitud + "BandejaProgramacion/ObtenerIndicadoresFuturo",
               method: "POST",
               data: JSON.stringify(programacion)
           }).then(DatosCompletados);

           function DatosCompletados(response) {
               return response;
           }
       };

       function ListarFechaDerecho(programacion) {
           return $http({
               url: $urls.ApiProgramacion + "BandejaProgramacion/ListarFechaDerecho",
               method: "POST",
               data: JSON.stringify(programacion)
           }).then(DatosCompletados);

           function DatosCompletados(response) {
               return response;
           }
       };

    }
})();