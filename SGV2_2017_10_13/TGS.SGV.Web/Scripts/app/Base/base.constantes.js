(function () {
    'use strict';

    angular
        .module('app.Base')
        .constant('URLS', { 
            ApiLogin: "http://localhost:16525/Seguridad/",
            ApiEvaluacion: "http://localhost:16525/Solicitud/",
            ApiSolicitud: "http://localhost:16525/Solicitud/",
            ApiProgramacion: "http://localhost:16525/Programacion/",
            ApiTablaGeneral: "http://localhost:16525/TablaGeneral/",
            ApiTrabajador: "http://localhost:16525/Trabajador/",
            ApiUnidad: "http://localhost:16525/Unidad/",
            ApiReportes: "/Reportes/frmContenedor.aspx?"
        })
        .constant('MensajesUI', {
            DatosOk: "Los datos fueron grabados correctamente",
            DatosOkCorreo: "El correo fue enviado correctamente",
            DatosDeleteOk: "Los datos fueron Eliminados correctamente",
            DatosError: "No se pudo realizar la acción, consulte con el administrador"
        });
})();
