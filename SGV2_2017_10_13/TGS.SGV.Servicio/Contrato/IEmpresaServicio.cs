using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface IEmpresaServicio : IDisposable    
    {
        [OperationContract]
        List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest);

        [OperationContract]
        EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado);

        [OperationContract]
        EmpresaResponse ObtenerFechaCorteEvaluacion(string CodigoEmpleado);
    }
}
