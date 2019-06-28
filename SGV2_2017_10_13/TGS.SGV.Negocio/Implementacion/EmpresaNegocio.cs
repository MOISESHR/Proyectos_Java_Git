using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Comun.Constantes;

namespace TGS.SGV.Negocio.Implementacion
{
    public class EmpresaNegocio : IEmpresaNegocio
    {
        private readonly IEmpresaAccesoDatos _IEmpresaAccesoDatos;
        public EmpresaNegocio(IEmpresaAccesoDatos EmpresaAccesoDatos)
        {
            _IEmpresaAccesoDatos = EmpresaAccesoDatos;
        }
        public List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest)
        {
            return _IEmpresaAccesoDatos.ListarEmpresa(empresaRequest);
        }
        public EmpresaResponse ObtenerFechaCorte(EmpresaRequest request)
        {
            return _IEmpresaAccesoDatos.ObtenerFechaCorte(request);
        }
        public EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado)
        {
            return _IEmpresaAccesoDatos.ObtenerFechaCorteCip(CodigoEmpleado);
        }
        public EmpresaResponse ObtenerFechaCorteEvaluacion(string CodigoEmpleado)
        {
            EmpresaResponse respuesta = new EmpresaResponse();
            var listaFechaCorte = _IEmpresaAccesoDatos.ObtenerFechaCorteCip(CodigoEmpleado);
            //Mensual, Quicenal, SinFechaCorte 
            var diaCorte1 = string.Empty;
            var diaCorte2 = string.Empty;
            var sinFechaCorte = false;
            var fechaCorte1 = string.Empty;
            if (listaFechaCorte.EmpresaDtoLista.Count > 0)
            {
                foreach (var item in listaFechaCorte.EmpresaDtoLista)
                {
                    switch (item.Dato)
                    {
                        case Constantes.Empresas.CorteMensual:
                            diaCorte1 = item.Valor;
                            break;
                        case Constantes.Empresas.CorteQuicenal:
                            diaCorte2 = item.Valor;
                            break;
                        case Constantes.Empresas.SinFechaCorte:
                            sinFechaCorte = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                sinFechaCorte = true;
            }
            fechaCorte1 = diaCorte1 + "/" + DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year.ToString();
            respuesta.EmpresaDto = new EmpresaDto();
            respuesta.EmpresaDto.DiaCorte1 = diaCorte1.PadLeft(2, '0');
            respuesta.EmpresaDto.FechaCorte = fechaCorte1;
            respuesta.EmpresaDto.SinFechaCorte = sinFechaCorte;

            return respuesta;
        }
        public EmpresaResponse ObtenerFechaPagoCalculo(string cipEmpleado)
        {
            return _IEmpresaAccesoDatos.ObtenerFechaPagoCalculo(cipEmpleado);
        }
        public void Dispose()
        {
            _IEmpresaAccesoDatos.Dispose();
        }

    }
}
