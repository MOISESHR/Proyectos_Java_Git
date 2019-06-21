(function () {
    'use strict',
    angular
    .module('app.Trabajador')
    .service('TrabajadorService', TrabajadorService);

    TrabajadorService.$inject = ['$http', 'URLS'];

    function TrabajadorService($http, $urls) {
        var service = {
            ObtenerDatosTrabajador: ObtenerDatosTrabajador,
        };
        return service;

        function ObtenerDatosTrabajador() {
            return $http({
                url: $urls.ApiTrabajador + "Trabajador/ObtenerDatosTrabajador",
                method: "POST",
                data: {}
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
    }
})();