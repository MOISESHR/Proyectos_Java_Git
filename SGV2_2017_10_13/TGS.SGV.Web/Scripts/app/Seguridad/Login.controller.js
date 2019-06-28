(function () {
    'use strict',

    angular
    .module('app.Seguridad')
    .controller('LoginController', LoginController);

    LoginController.$inject = ['LoginService', 'blockUI', '$timeout', 'UtilsFactory', 'MensajesUI'];

    function LoginController(LoginService, blockUI, $timeout, UtilsFactory,$MensajesUI) {
        var vm = this;
        vm.Login = Login;

        vm.TxtUsuario = '';
        vm.TxtClave = '';

        function Login() {
             
            var UsuarioDto = {
                Login: vm.TxtUsuario,
                Password: vm.TxtClave
            };

            if (UsuarioDto.Login == '') {
                UtilsFactory.Alerta('#divAlert', 'danger', "Ingrese su usuario", 5);
                blockUI.stop();
                return;
            }

            if (UsuarioDto.Password == '') {
                UtilsFactory.Alerta('#divAlert', 'danger', "Ingrese su clave", 5);
                blockUI.stop();
                return;
            }

            blockUI.start();

            var promise = LoginService.LoginDatos(UsuarioDto);

            promise.then(function (response) {

                var respuesta = response.data.Respuesta;

                if (respuesta.Tipo == "Ok") {
                    window.location.href = respuesta.Mensaje;
                } else {
                    UtilsFactory.Alerta('#divAlert', 'danger', respuesta.Mensaje, 5);                    
                }

                $timeout(function () {
                    blockUI.stop();
                }, 3000);

            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', $MensajesUI.DatosError, 6);
            });
        };
    }

})();

