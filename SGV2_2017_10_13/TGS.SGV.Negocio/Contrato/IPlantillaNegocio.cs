using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Negocio.Contrato
{
   public interface IPlantillaNegocio : IDisposable
   {
        PlantillaResponse ObtenerEstadoCorreo(string codigoCorreo, string codigoEmpresa);
        PlantillaResponse ObtenerDatosMail(PlantillaRequest request);
        Resultado<string> EnvioCorreoAprobacionSolictud(PlantillaRequest obReq);

    }    
}
