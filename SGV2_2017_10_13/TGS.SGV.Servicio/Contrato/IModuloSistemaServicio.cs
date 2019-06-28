using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface IModuloSistemaServicio : IDisposable
    {
        [OperationContract]
        ModuloSistemaResponse ListarModuloUsuario(UsuarioRequest usuarioRequest);

      
    }
}
