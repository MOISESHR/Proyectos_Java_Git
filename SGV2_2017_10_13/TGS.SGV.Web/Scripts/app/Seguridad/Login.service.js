(function () {
    'use strict',
    angular
    .module('app.Seguridad')
    .service('LoginService', LoginService);

    LoginService.$inject = ['$http', 'URLS'];

    function LoginService($http, $urls) {

        var service = {
            LoginDatos: LoginDatos,
            EnviarClave: EnviarClave,
            CambiarClave: CambiarClave,
            IngresarSistema: IngresarSistema
        };

        return service;

        function LoginDatos(UsuarioDto) {
            return $http({
                url: $urls.ApiLogin + "Login/Ingresar",
                method: "POST",
                data: JSON.stringify(UsuarioDto)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function EnviarClave(UsuarioDto) {
            return $http({
                url: $urls.ApiLogin + "Login/EnviarClave",
                method: "POST",
                data: JSON.stringify(UsuarioDto)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function CambiarClave(UsuarioDto) {
            return $http({
                url: $urls.ApiLogin + "Login/CambiarClave",
                method: "POST",
                data: JSON.stringify(UsuarioDto)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

        function IngresarSistema(UsuarioDto) {
            return $http({
                url: $urls.ApiLogin + "Login/IngresarSistema",
                method: "POST",
                data: JSON.stringify(UsuarioDto)
            }).then(DatosCompletados);

            function DatosCompletados(response) {
                return response;
            }
        };

         
    }

})();

