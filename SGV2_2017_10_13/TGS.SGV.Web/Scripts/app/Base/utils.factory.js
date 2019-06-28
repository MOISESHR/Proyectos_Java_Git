(function () {
    'use strict';
    angular
    .module('app.Base')
    .factory('UtilsFactory', UtilsFactory);
     
    UtilsFactory.$inject = ['$alert', '$timeout'];

    function UtilsFactory($alert, $timeout, Wizard, WizardPage) {
        var vm = this;

        vm.alert = ""; 

        var factory = {
            AgregarItemSelect: AgregarItemSelect,
            Alerta: Alerta,
            CerrarAlerta: CerrarAlerta,
            InputBorderColor: InputBorderColor,
            HabilitarElemento: HabilitarElemento,
            DeshabilitarElemento: DeshabilitarElemento,
            LimpiarLista: LimpiarLista,
            ValidarFecha: ValidarFecha,
            AgregarItemSelectTodos: AgregarItemSelectTodos,
            ObtenerExtensionArchivo: ObtenerExtensionArchivo,
            ValidarNumero: ValidarNumero,
            ValidarEmail: ValidarEmail,
            ValidarVacio: ValidarVacio,
            ValidarSoloLetras: ValidarSoloLetras,
            OcultarElemento: OcultarElemento,
            MostrarElemento: MostrarElemento
        }; 

        return factory;

        function AgregarItemSelect(data) {
            var lista = data;            
            var item = { "Codigo": "-1", "Descripcion": "--Seleccione--" };
           lista.splice(0, 0, item);
            return lista;
        };

        function AgregarItemSelectTodos(data) {
            var lista = data;
            var item = { "Codigo": "0", "Descripcion": "--Todos--" };
            lista.splice(0, 0, item);
            return lista;
        };

        function Alerta(elemento, tipoMsj, mensaje, duracion) { 
             
            var titulo = "";
            if (tipoMsj == "danger") {
                titulo = "Error:";
            } else {
                titulo = "Correcto:";
            }
              
            vm.alert= $alert({
                title: titulo,
                content: mensaje,
                container: elemento ,  
                type: tipoMsj ,       
                dismissable: true,
                duration: duracion,
                show: false,
                animation: 'am-fade'
            });
             
            $timeout(function () {
                vm.alert.$promise.then(function () {
                    vm.alert.show();
                });
            }, 1000);            
        };

    
        function CerrarAlerta() {
            if (vm.alert!= "")
            {
                vm.alert.$promise.then(function () {
                    vm.alert.hide();
                });
            } 
        }

        function InputBorderColor(InputId,Color){
            var FailedCss = { "border": '2px solid red' };
            var SuccessCss = { "border": '2px solid green' };
            var nothingCss = { "border": '' };
            if (Color == 'Rojo') {
                $(InputId).css(FailedCss);
            }
            if (Color == 'Verde') {
                $(InputId).css(SuccessCss);
            }
            if (Color == 'Ninguno') {
                $(InputId).css(nothingCss);
            }
            if (Color == 'combo') {
                $(InputId).css(nothingCss);
            }
        }

        function LimpiarLista(objetoLista) {
            return objetoLista.filter(function (item) {
                return !!item;
            });
        }
         

        function HabilitarElemento(IdItem) {
           $(IdItem).prop("disabled", "");
        }

        function DeshabilitarElemento(IdItem) {
           $(IdItem).prop("disabled", "disabled");
        }

        function ValidarFecha(fecha) {
            var expReg = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/g;
            return expReg.test(fecha);
        } 

        function ObtenerExtensionArchivo(filename) {
            var ext = /^.+\.([^.]+)$/.exec(filename);
            return ext == null ? "" : ext[1];
        }

        function ValidarNumero(Value) {
            var expReg = new RegExp('^[0-9]+$');
            return expReg.test(Value);
        }

        function ValidarEmail(Value) {
            var expReg = new RegExp('^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$');
            return expReg.test(Value);
        }

        function ValidarVacio(Value) {
            var expReg = new RegExp('\S+');
            return expReg.test(Value);
        }

        function ValidarSoloLetras(Value) {
            var expReg = new RegExp("^[A-Za-z ]+$");
            var bol = expReg.test(Value);
            return expReg.test(Value);
        }

        function ValidarSoloLetras(Value) {
            var expReg = new RegExp("^[A-Za-z ]+$");
            var bol = expReg.test(Value);
            return expReg.test(Value);
        }

        function OcultarElemento(item) {
            $(item).hide();
        }

        function MostrarElemento(item) {
            $(item).show();
        }
    }

})();
