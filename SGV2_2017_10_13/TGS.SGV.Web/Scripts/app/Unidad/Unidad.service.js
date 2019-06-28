(function () {
    'use strict',
    angular
    .module('app.Unidad')
    .service('UnidadService', UnidadService);

    UnidadService.$inject = ['$http', 'URLS'];

    function UnidadService($http, $urls) {
        var service = {
            ListarCCRPerfil: ListarCCRPerfil,
        };
        return service;

        function ListarCCRPerfil(UnidadDto) {
            return $http({
                url: $urls.ApiUnidad + "Unidad/ListarCCRPerfil",
                method: "POST",
                data: JSON.stringify(UnidadDto)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
    }
})();