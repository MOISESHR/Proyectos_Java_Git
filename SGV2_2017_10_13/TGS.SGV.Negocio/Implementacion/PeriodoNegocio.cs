using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
namespace TGS.SGV.Negocio.Implementacion
{
    public class PeriodoNegocio : IPeriodoNegocio    
    {
        private readonly IPeriodoAccesoDatos _IPeriodoAccesoDatos;        
        public PeriodoNegocio(IPeriodoAccesoDatos PeriodoAccesoDatos)
        {
            _IPeriodoAccesoDatos = PeriodoAccesoDatos;
        }
        public PeriodoResponse ObtenerPeriodo(string cipEmpleado)
        {
            return _IPeriodoAccesoDatos.ObtenerPeriodo(cipEmpleado);
        }
        public void Dispose()
        {
            _IPeriodoAccesoDatos.Dispose();
        }
    }
}
