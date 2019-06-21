using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;

namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/PerfilServicio", Name = "PerfilServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class PerfilUsuarioServicio : IPerfilUsuarioServicio
    {
        private readonly IPerfilUsuarioNegocio _IPerfilUsuarioNegocio;
        public PerfilUsuarioServicio(IPerfilUsuarioNegocio perfilUsuarioNegocio)
        {
            _IPerfilUsuarioNegocio = perfilUsuarioNegocio;
        }
        
        public List<PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa)
        {
            return _IPerfilUsuarioNegocio.ListarAdministradorEmpresa(empresa);
        }
        public List<PerfilUsuarioDto> ListarAdministradorCCR(string unidad)
        {
            return _IPerfilUsuarioNegocio.ListarAdministradorCCR(unidad);
        }

        public PerfilDtoResponse ListarPerfilUsuario(UsuarioRequest usuarioRequest)
        {
            return _IPerfilUsuarioNegocio.ListarPerfilUsuario(usuarioRequest);
        }

        public string ObtenerPerfilAdicional(UsuarioRequest usuarioRequest)
        {
            return _IPerfilUsuarioNegocio.ObtenerPerfilAdicional(usuarioRequest);
        }

        public void Dispose()
        {
            _IPerfilUsuarioNegocio.Dispose();
        }

    }
}
