using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;

namespace TGS.SGV.Negocio.Implementacion
{
    public class ModuloSistemaNegocio : IModuloSistemaNegocio
    {
        private readonly IModuloSistemaAccesoDatos _IModuloSistemaAccesoDatos;

        public ModuloSistemaNegocio(IModuloSistemaAccesoDatos moduloSistemaAccesoDatos)
        {
            _IModuloSistemaAccesoDatos = moduloSistemaAccesoDatos;
        }

        public ModuloSistemaResponse ListarModuloUsuario(UsuarioRequest usuarioRequest)
        {
            if (usuarioRequest.TipoPerfil.Equals(Constantes.TipoPerfil.Usuario) ||
                usuarioRequest.TipoPerfil.Equals(Constantes.TipoPerfil.Directivo) ||
                usuarioRequest.TipoPerfil.Equals(Constantes.TipoPerfil.MandoResponsable))
            {
                return _IModuloSistemaAccesoDatos.ListarModuloNoAsignado(usuarioRequest);
            }
            else if (!usuarioRequest.TipoPerfil.Equals(Constantes.TipoPerfil.AdministradorDirectivos))
            {
                return _IModuloSistemaAccesoDatos.ListarModuloAsignado(usuarioRequest);
            }
            else
            {
                return _IModuloSistemaAccesoDatos.ListarModuloDirectivo(usuarioRequest);
            } 
        }         

        public void Dispose()
        {
            _IModuloSistemaAccesoDatos.Dispose();
        }

    }
}
