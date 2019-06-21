(function () {
    'use strict',

    angular
    .module('app.Seguridad')
    .controller('OlvidoClaveController', OlvidoClaveController);

    OlvidoClaveController.$inject = ['LoginService', 'blockUI', '$timeout', 'UtilsFactory', 'MensajesUI'];

    function OlvidoClaveController(LoginService, blockUI, $timeout, UtilsFactory, $MensajesUI) {
        var vm = this;

        vm.EnvioClave = EnvioClave;

        vm.TxtUsuario = '';
          
        function EnvioClave() {
             
            var UsuarioDto = {
                Login: vm.TxtUsuario
            }; 
            
            if (UsuarioDto.Login == '') {
                UtilsFactory.Alerta('#divAlert', 'danger', "Ingrese su doc. de identidad", 5);
                blockUI.stop();
                return;
            }

            blockUI.start();

            var promise = LoginService.EnviarClave(UsuarioDto);

            promise.then(function (response) {   
                 
                var respuesta = response.data;
                
                if (respuesta.Tipo != 0) {
                    UtilsFactory.Alerta('#divAlert', 'danger', respuesta.Errores[0], 5);
                } else {
                    UtilsFactory.Alerta('#divAlert', 'success', 'Su clave de acceso fue enviada correctamente', 6);
                    vm.TxtUsuario = "";
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

