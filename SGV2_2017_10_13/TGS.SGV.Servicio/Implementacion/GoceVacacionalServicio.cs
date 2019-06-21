using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;


namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/GoceVacacionalServicio", Name = "GoceVacacionalServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class GoceVacacionalServicio : IGoceVacacionalServicio    
    {
        private readonly IGoceVacacionalNegocio _IGoceVacacionalNegocio;
        public GoceVacacionalServicio(IGoceVacacionalNegocio goceVacacionalNegocio)
        {
            _IGoceVacacionalNegocio = goceVacacionalNegocio;
        }
        public GoceVacacionalResponse ListarObtenerGoce(GoceVacacionalRequest goceVacacionalRequest)
        {
            return _IGoceVacacionalNegocio.ListarObtenerGoce(goceVacacionalRequest);
        }

        public void Dispose()
        {
            _IGoceVacacionalNegocio.Dispose();
        }
    }
}
