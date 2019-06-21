using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Strings;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Comun.Constantes;

namespace TGS.SGV.Negocio.Implementacion
{
    public class PlantillaNegocio : IPlantillaNegocio
    {
        private readonly IPlantillaAccesoDatos _IPlantillaAccesoDatos;
        public PlantillaNegocio(IPlantillaAccesoDatos plantillaAccesoDatos)
        {
            _IPlantillaAccesoDatos = plantillaAccesoDatos;
        }
        public PlantillaResponse ObtenerEstadoCorreo(string codigoCorreo, string codigoEmpresa)
        {
            return _IPlantillaAccesoDatos.ObtenerEstadoCorreo(codigoCorreo, codigoEmpresa);
        }
        public PlantillaResponse ObtenerDatosMail(PlantillaRequest request)
        {
            return _IPlantillaAccesoDatos.ObtenerDatosMail(request);
        }

        public Resultado<string> EnvioCorreoAprobacionSolictud(PlantillaRequest obReq)
        {
            var rsResul = ObtenerEstadoCorreo(obReq.CodigoCorreo, obReq.CodigoEmpresa);
            if (rsResul.PlantillaDto == null)
            {
                return new Resultado<string>(TipoResultado.Invalido, string.Empty, Constantes.TipoEstadoCorreo.Inactivo);
            }

            if (rsResul.PlantillaDto.TipoGeneracion == Constantes.TipoEstadoCorreo.Inactivo)
            {
                return new Resultado<string>(TipoResultado.Invalido, string.Empty, Constantes.TipoEstadoCorreo.Inactivo);
            }
            
            PlantillaDto plantilla = ObtenerDatosMail(obReq).PlantillaDto;        

            DatosCorreo oCorr = new DatosCorreo();
            oCorr.CorreoEnvia = plantilla.CorreoDe;
            oCorr.Destinatarios= plantilla.CorreoPara;
            oCorr.Asunto = plantilla.CorreoAsunto;
            oCorr.ConCopia = plantilla.CorreoCC;
            oCorr.Cuerpo = plantilla.CorreoCuerpo;
            
            if (Funciones.EnviarCorreo(oCorr).Equals(General.Fallo))
            {
                return new Resultado<string>(TipoResultado.Invalido, Errores.EnvioCorreo);
            }
            else
            {
                return new Resultado<string>(TipoResultado.Ok, string.Empty, Constantes.TipoEstadoCorreo.Activo);
            }
        }

        public void Dispose()
        {
            _IPlantillaAccesoDatos.Dispose();
        }

    }
}
