using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Strings;
using TGS.SGV.Comun.Constantes;

namespace TGS.SGV.Negocio.Implementacion
{
    public class PerfilUsuarioNegocio : IPerfilUsuarioNegocio
    {
        private readonly IPerfilUsuarioAccesoDatos _IPerfilUsuarioAccesoDatos; 
        private readonly IParametroNegocio _IParametroNegocio;

        public PerfilUsuarioNegocio(IPerfilUsuarioAccesoDatos PerfilUsuarioAccesoDatos , IParametroAccesoDatos parametroAccesoDatos, IParametroNegocio parametroNegocio)
        {
            _IPerfilUsuarioAccesoDatos = PerfilUsuarioAccesoDatos; 
            _IParametroNegocio = parametroNegocio;
        }
        
        public List<PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa)
        {
            return _IPerfilUsuarioAccesoDatos.ListarAdministradorEmpresa(empresa);
        }

        public List<PerfilUsuarioDto> ListarAdministradorCCR(string unidad)
        {
            return _IPerfilUsuarioAccesoDatos.ListarAdministradorCCR(unidad);
        }
        
        public PerfilDtoResponse ListarPerfilUsuario(UsuarioRequest usuarioRequest)
        { 
            var consultaAcceso = _IParametroNegocio.ObtenerParametroAcceso(usuarioRequest).ParametroAccesoDto;
            
            var perfilDtoRequest = new PerfilDtoRequest
            {
               Cip = usuarioRequest.Cip,
               EmpresaSuccessFactor = consultaAcceso.EmpresaSuccessFactor
            };

            if (consultaAcceso.GoceFuturo >0 && (usuarioRequest.TipoEmpleado.Equals(Constantes.TipoEmpleado.Directivo) ||
                usuarioRequest.TipoEmpleado.Equals(Constantes.TipoEmpleado.Desplazado)) )
            {
                return _IPerfilUsuarioAccesoDatos.ListarPerfilesDireccion(perfilDtoRequest);
            }
            else
            {
                return _IPerfilUsuarioAccesoDatos.ListarPerfilesPersona(perfilDtoRequest);                
            }       
        }

        /// <summary>
        /// se obtiene un perfil adicional en caso el usuario es directivo
        /// </summary>
        /// <param name="usuarioRequest">parametro de usuario logueado</param>
        /// <returns></returns>
        public string ObtenerPerfilAdicional(UsuarioRequest usuarioRequest)
        {
            string tipoPerfil = string.Empty;

            var parametroGoce = new ParametroRequest
            {
                Codigo = Constantes.ParametrosInternos.GoceFuturo,
                Empresa = usuarioRequest.CodigoEmpresa
            };

            var goceFuturo = _IParametroNegocio.ObtenerParametroInterno(parametroGoce).ParametroDto;

            if (!string.IsNullOrEmpty(goceFuturo.ValorParametro) &&
              usuarioRequest.TipoPerfil.Equals(Constantes.TipoPerfil.Usuario) &&
              (usuarioRequest.TipoEmpleado.Equals(Constantes.TipoEmpleado.Directivo) ||
              usuarioRequest.TipoEmpleado.Equals(Constantes.TipoEmpleado.Desplazado)))
            {
                tipoPerfil = Constantes.TipoPerfil.Directivo;
            }
            else
            {
                tipoPerfil = usuarioRequest.TipoPerfil;
            }

            return tipoPerfil;
        }

        public void Dispose()
        {
            _IPerfilUsuarioAccesoDatos.Dispose();
            _IParametroNegocio.Dispose();
        }

    }
}
