(function () {
    'use strict',

    angular
    .module('app.Seguridad')
    .controller('PerfilController', PerfilController);

    PerfilController.$inject = ['LoginService', 'blockUI', '$timeout', 'UtilsFactory', 'MensajesUI'];

    function PerfilController(LoginService, blockUI, $timeout, UtilsFactory, $MensajesUI) {
        var vm = this;

        vm.IngresarSistema = IngresarSistema;
        vm.ListarPerfiles = ListarPerfiles;

        vm.CmbCodigoPerfil = '-1'; 
        vm.Perfiles = [];

        ListarPerfiles();

        function ListarPerfiles() { 
            vm.Perfiles = UtilsFactory.AgregarItemSelect(ListaPerfilesJson);
        };

        function IngresarSistema() {

            blockUI.start();

            var UsuarioDto = {
                TipoPerfil: vm.CmbCodigoPerfil
            };           
            
            if (UsuarioDto.TipoPerfil == '-1') {
                UtilsFactory.Alerta('#divAlert', 'danger', "Seleccione un perfil", 5);
                blockUI.stop();
                return;
            }

            var promise = LoginService.IngresarSistema(UsuarioDto);

            promise.then(function (response) {   
                window.location.href = response.data.Respuesta.Mensaje;
            }, function (response) {
                blockUI.stop();
                UtilsFactory.Alerta('#divAlert', 'danger', $MensajesUI.DatosError, 6);
            }); 
       };
    }

})();

