using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Strings;
using TGS.SGV.Web.Utilitarios;
using TGS.SGV.Web.WsSolicitudServicio;

namespace TGS.SGV.Web.Areas.Programacion.Controllers
{
    public class GoceVacacionalController : BaseController
    {
        private readonly ISolicitudServicio _ISolicitudServicio;

        public GoceVacacionalController(ISolicitudServicio ISolicitudServicio)
        {
            this._ISolicitudServicio = ISolicitudServicio;
        }

        public ActionResult ModalGoceVacacional()
        {
            return PartialView("_RegistroGoceVacacional");
        }

    }
}
