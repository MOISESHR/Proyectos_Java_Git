(function () {
    'use strict',
    angular
    .module('app.TablaGeneral')
    .service('TablaGeneralService', TablaGeneralService);

    TablaGeneralService.$inject = ['$http', 'URLS'];

    function TablaGeneralService($http, $urls) {
        var service = {
            ListarTablaGeneral: ListarTablaGeneral,
        };
        return service;

        function ListarTablaGeneral() {
            return $http({
                url: $urls.ApiTablaGeneral + "TablaGeneral/ListarTablaGeneral",
                method: "POST",
                data: {}
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };
    }
})();