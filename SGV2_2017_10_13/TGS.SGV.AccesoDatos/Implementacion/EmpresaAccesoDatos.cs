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
    public class EmpresaAccesoDatos : BaseAcceso, IEmpresaAccesoDatos
    {
        public EmpresaAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public List<ListaDto> ListarEmpresa(EmpresaRequest empresaRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, empresaRequest.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_PERFIL", OracleDbType.Varchar2, empresaRequest.TipoPerfil, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, empresaRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            return ExecuteQuery<ListaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_EMPRESA_PERFIL(:P_CIP, :P_PERFIL, :P_EMPRESA, :P_RECORDSET); end;", parametros).ToList();            
        }

        public EmpresaResponse ObtenerFechaCorte(EmpresaRequest request)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, request.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_SOL", OracleDbType.Varchar2, request.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<EmpresaDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_FECHA_CORTE(:P_CIP_EMPLEADO, :P_FECHA_INICIO_SOL, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new EmpresaResponse
            {
                EmpresaDtoLista = consulta,
            };
            return datosconsulta;
        }

        public EmpresaResponse ObtenerFechaCorteCip(string CodigoEmpleado)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<EmpresaDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_FECHA_REPROG_CIP(:P_CIP_EMPLEADO, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new EmpresaResponse
            {
                EmpresaDtoLista = consulta,
            };
            return datosconsulta;
        }

        public EmpresaResponse ObtenerFechaPagoCalculo(string cipEmpleado)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, cipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<EmpresaDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_FECHA_PAGO_CALCULO(:P_CIP_EMPLEADO, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new EmpresaResponse
            {
                EmpresaDto = consulta,
            };
            return datosconsulta;
        }
    }
}
