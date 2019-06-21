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
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/EmpresaServicio", Name = "EmpresaServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class EmpresaServicio : IEmpresaServicio
    {
        private readonly IEmpresaNegocio _IEmpresaNegocio;
        public EmpresaServicio(IEmpresaNegocio EmpresaNegocio)
        {
            _IEmpresaNegocio = EmpresaNegocio;
        }

        public List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest)
        {
            return _IEmpresaNegocio.ListarEmpresa(empresaRequest);
        }
 
        public EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado)
        {
            return _IEmpresaNegocio.ObtenerFechaCorteCip(CodigoEmpleado);
        }

        public EmpresaResponse ObtenerFechaCorteEvaluacion(string CodigoEmpleado)
        {
            return _IEmpresaNegocio.ObtenerFechaCorteEvaluacion(CodigoEmpleado);
        }

        public void Dispose()
        {
            _IEmpresaNegocio.Dispose();
        }
    }
}
