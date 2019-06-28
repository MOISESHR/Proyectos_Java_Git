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
    public class TablaGeneralNegocio : ITablaGeneralNegocio    
    {
        private readonly ITablaGeneralAccesoDatos _ITablaGeneralAccesoDatos;        
        public TablaGeneralNegocio(ITablaGeneralAccesoDatos TablaGeneralAccesoDatos)
        {
            _ITablaGeneralAccesoDatos = TablaGeneralAccesoDatos;
        }
        public List<ListaDto> ListarTablaGeneral(string codigoFiltro)
        {
            return _ITablaGeneralAccesoDatos.ListarTablaGeneral(codigoFiltro);
        }
        public void Dispose()
        {
            _ITablaGeneralAccesoDatos.Dispose();
        }
    }
}
