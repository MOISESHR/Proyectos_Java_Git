using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;

namespace TGS.SGV.Negocio.Implementacion
{
    public class TrabajadorNegocio : ITrabajadorNegocio
    {
        private readonly ITrabajadorAccesoDatos _ITrabajadorAccesoDatos;
        public TrabajadorNegocio(ITrabajadorAccesoDatos TrabajadorAccesoDatos)
        {
            _ITrabajadorAccesoDatos = TrabajadorAccesoDatos;
        }
        public TrabajadorResponse ObtenerDatosTrabajador(string codigoUsuario)
        {
            return _ITrabajadorAccesoDatos.ObtenerDatosTrabajador(codigoUsuario);
        }

        public List<ListaDto> ListarTrabajadorPorMando(TrabajadorRequest trabajadorRequest)
        {
            return _ITrabajadorAccesoDatos.ListarTrabajadorPorMando(trabajadorRequest);
        }


        public void Dispose()
        {
            _ITrabajadorAccesoDatos.Dispose();
        }
    }
}
