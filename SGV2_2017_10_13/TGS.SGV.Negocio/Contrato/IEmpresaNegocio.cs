using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
namespace TGS.SGV.Negocio.Contrato
{
   public interface IEmpresaNegocio : IDisposable
   {
        List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest);

        EmpresaResponse ObtenerFechaCorte(EmpresaRequest request);
        EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado);
        EmpresaResponse ObtenerFechaPagoCalculo(string cipEmpleado);

        EmpresaResponse ObtenerFechaCorteEvaluacion(string CodigoEmpleado);
    }
    
}
