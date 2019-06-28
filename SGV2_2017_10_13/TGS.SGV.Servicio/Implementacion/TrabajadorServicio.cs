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
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/TrabajadorServicio", Name = "TrabajadorServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class TrabajadorServicio : ITrabajadorServicio
    {
        private readonly ITrabajadorNegocio _ITrabajadorNegocio;
        public TrabajadorServicio(ITrabajadorNegocio trabajadorNegocio)
        {
            _ITrabajadorNegocio = trabajadorNegocio;
        }

        public TrabajadorResponse ObtenerDatosTrabajador(string CodigoUsuario)
        {
            return _ITrabajadorNegocio.ObtenerDatosTrabajador(CodigoUsuario);
        }

        public List<ListaDto> ListarTrabajadorPorMando(TrabajadorRequest trabajadorRequest)
        {
            return _ITrabajadorNegocio.ListarTrabajadorPorMando(trabajadorRequest);
        }

        public void Dispose()
        {
            _ITrabajadorNegocio.Dispose();
        }
    }
}
