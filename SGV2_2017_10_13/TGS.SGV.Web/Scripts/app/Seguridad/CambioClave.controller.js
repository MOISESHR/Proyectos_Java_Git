(function () {
    'use strict',

    angular
    .module('app.Seguridad')
    .controller('CambioClaveController', CambioClaveController);

    CambioClaveController.$inject = ['LoginService', 'blockUI', '$timeout', 'UtilsFactory', 'MensajesUI'];

    function CambioClaveController(LoginService, blockUI, $timeout, UtilsFactory, $MensajesUI) {
        var vm = this;
        vm.CambioClave = CambioClave; 
          
        vm.TxtUsuario = '';
        vm.TxtClave = '';
        vm.TxtNuevaClave = '';
        vm.TxtConfirmarClave = '';

        function LimpiarModelo() {
            vm.TxtUsuario = '';
            vm.TxtClave = '';
            vm.TxtNuevaClave = '';
            vm.TxtConfirmarClave = '';
        };

        function CambioClave() {
             
            var UsuarioDto = {
                Login: vm.TxtUsuario,
                Password: vm.TxtClave,
                NuevoPassword: vm.TxtNuevaClave
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

            if (UsuarioDto.NuevoPassword.length < 7) {
                UtilsFactory.Alerta('#divAlert', 'danger', "Ingrese una clave mayor a 6 digitos", 5);
                blockUI.stop();
                return;
            }

            if (UsuarioDto.NuevoPassword == '') {
                UtilsFactory.Alerta('#divAlert', 'danger', "Ingrese su nueva clave", 5);
                blockUI.stop();
                return;
            }

            if (UsuarioDto.NuevoPassword != vm.TxtConfirmarClave) {
                UtilsFactory.Alerta('#divAlert', 'danger', "La nueva contraseña y la confirmación no coinciden", 5);
                blockUI.stop();
                return;
            }

            blockUI.start();

            var promise = LoginService.CambiarClave(UsuarioDto);

            promise.then(function (response) {                
                 
                var respuesta = response.data;

                if (respuesta.Tipo == 0)
                { 
                    UtilsFactory.Alerta('#divAlert', 'success', $MensajesUI.DatosOk, 5);
                    LimpiarModelo();
                } else { 
                    UtilsFactory.Alerta('#divAlert', 'danger', respuesta.Errores[0], 5);
                }

                $timeout(function () {
                    blockUI.stop();
                }, 1000);

            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', $MensajesUI.DatosError, 6);
            }); 
       };
    }

})();

