using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IPerfilUsuarioAccesoDatos : IDisposable
    {        
        List<PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa);
        List<PerfilUsuarioDto> ListarAdministradorCCR(string unidad);

        PerfilDtoResponse ListarPerfilesPersona(PerfilDtoRequest perfilDtoRequest);
        PerfilDtoResponse ListarPerfilesDireccion(PerfilDtoRequest perfilDtoRequest);
    }
}
