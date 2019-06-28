using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;

namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/ParametroServicio", Name = "ParametroServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class ParametroServicio : IParametroServicio
    {
        private readonly IParametroNegocio _IParametroNegocio;

        public ParametroServicio(IParametroNegocio parametroNegocio)
        {
            _IParametroNegocio = parametroNegocio;
        }

        public ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest)
        {
            return _IParametroNegocio.ObtenerParametroInterno(parametroRequest);
        }

        public ParametroResponse ObtenerParametro(ParametroRequest parametroRequest)
        {
            return _IParametroNegocio.ObtenerParametro(parametroRequest);
        }

        public ParametroAccesoResponse ObtenerParametroAcceso(UsuarioRequest usuarioRequest)
        {
            return _IParametroNegocio.ObtenerParametroAcceso(usuarioRequest);
        }

        public IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio)
        {
            return _IParametroNegocio.ObtenerIndicadoresFuturo(codigoUsuario, fechaInicio);
        }

        public FechaGoceResponse ObtenerGoceFuturo(string codigoUsuario)
        {
            return _IParametroNegocio.ObtenerGoceFuturo(codigoUsuario);
        }
        public IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario)
        {
            return _IParametroNegocio.ObtenerIndicadores(codigoUsuario);
        }

        public void Dispose()
        {
            _IParametroNegocio.Dispose();
        }
    }
}
