using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IEmpresaAccesoDatos : IDisposable
    {
        List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest);

        EmpresaResponse ObtenerFechaCorte(EmpresaRequest request);
        EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado);
        EmpresaResponse ObtenerFechaPagoCalculo(string cipEmpleado);
    }
}
