using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface IUsuarioServicio : IDisposable
    {
        [OperationContract]
        Resultado<string> ValidarIngreso(string usuarioLogin, string claveUsuario);

        [OperationContract]
        Resultado<string> CambiarCLave(string idUsuario, string claveAnterior, string claveNueva);

        [OperationContract]
        Resultado<string> EnvioClaveUsuario(string nroDocumento);
    }
       
}
