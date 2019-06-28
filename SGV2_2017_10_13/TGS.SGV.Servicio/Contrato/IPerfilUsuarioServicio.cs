using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface IPerfilUsuarioServicio : IDisposable    
    {
        [OperationContract]
        List<PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa);

        [OperationContract]
        List<PerfilUsuarioDto> ListarAdministradorCCR(string unidad);

        [OperationContract]
        PerfilDtoResponse ListarPerfilUsuario(UsuarioRequest usuarioRequest);

        [OperationContract]
        string ObtenerPerfilAdicional(UsuarioRequest usuarioRequest);        
    }
}
