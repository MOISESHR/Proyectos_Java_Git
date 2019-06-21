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
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/TablaGeneralServicio", Name = "TablaGeneralServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class TablaGeneralServicio : ITablaGeneralServicio    
    {
        private readonly ITablaGeneralNegocio _ITablaGeneralNegocio;
        public TablaGeneralServicio(ITablaGeneralNegocio tablageneralNegocio)
        {
            _ITablaGeneralNegocio = tablageneralNegocio;
        }
        public List<ListaDto> ListarTablaGeneral(string CodigoFiltro)
        {
            return _ITablaGeneralNegocio.ListarTablaGeneral(CodigoFiltro);
        }
        public void Dispose()
        {
            _ITablaGeneralNegocio.Dispose();
        }
    }
}
