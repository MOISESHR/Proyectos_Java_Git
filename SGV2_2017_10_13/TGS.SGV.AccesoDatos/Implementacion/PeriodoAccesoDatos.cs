using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Strings;

namespace TGS.SGV.AccesoDatos.Implementacion
{
    public class PeriodoAccesoDatos : BaseAcceso, IPeriodoAccesoDatos
    {
        public PeriodoAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public PeriodoResponse ObtenerPeriodo(string cipEmpleado)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, cipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<PeriodoDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_PERIODO(:P_CIP_EMPLEADO, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new PeriodoResponse 
            {
                PeriodoDto = consulta,
            };
            return datosconsulta;
        }
    }
}
