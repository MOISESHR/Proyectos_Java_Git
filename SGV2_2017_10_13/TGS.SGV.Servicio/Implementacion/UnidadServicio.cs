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
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/UnidadServicio", Name = "UnidadServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class UnidadServicio : IUnidadServicio
    {
        private readonly IUnidadNegocio _IUnidadNegocio;
        public UnidadServicio(IUnidadNegocio UnidadNegocio)
        {
            _IUnidadNegocio = UnidadNegocio;
        }
        public List<ListaDto> ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest)
        {
            return _IUnidadNegocio.ListarCCRPerfil(unidadDtoRequest);
        }
        public void Dispose()
        {
            _IUnidadNegocio.Dispose();
        }
    }
}
