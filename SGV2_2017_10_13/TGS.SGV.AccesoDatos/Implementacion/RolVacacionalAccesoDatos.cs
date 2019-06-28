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
    public class RolVacacionalAccesoDatos : BaseAcceso, IRolVacacionalAccesoDatos
    {
        public RolVacacionalAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public RolVacacionalResponse ObtenerFechaDerecho(RolVacacionalRequest parametro)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, parametro.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_PERIODO", OracleDbType.Int32, parametro.Periodo, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<RolVacacionalDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_FECHA_DERECHO(:P_CIP_EMPLEADO, :P_PERIODO, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new RolVacacionalResponse 
            {
                RolVacacionalDtoLista = consulta,
            };
            return datosconsulta;
        }
        public RolVacacionalResponse ObtenerFechaDerechoRegulariza(string cipEmpleado)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, cipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<RolVacacionalDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_FECHA_DERECHO_REG(:P_CIP_EMPLEADO, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new RolVacacionalResponse
            {
                RolVacacionalDtoLista = consulta,
            };
            return datosconsulta;
        }
        public RolVacacionalResponse ObtenerDiasDisponiblesPagos(RolVacacionalRequest parametro)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, parametro.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_SOL", OracleDbType.Varchar2, parametro.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FIN_SOL", OracleDbType.Varchar2, parametro.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<RolVacacionalDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_DIAS_DISPONIBLE_PAGO(:P_CIP_EMPLEADO, :P_FECHA_INICIO_SOL, :P_FECHA_FIN_SOL, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new RolVacacionalResponse
            {
                RolVacacionalDto = consulta,
            };
            return datosconsulta;
        }
        public int ValidarModificarFechaPago(RolVacacionalRequest parametro)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, parametro.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_SOL", OracleDbType.Varchar2, parametro.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL_SOL", OracleDbType.Varchar2, parametro.FechaFinal, ParameterDirection.Input)
                //new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            return ExecuteCommand("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_MODIFICAR_FECHA_PAGO(:P_CIP_EMPLEADO, :P_FECHA_INICIO_SOL, :P_FECHA_FIN_SOL, :P_RECORDSET); end;", parametros);
        }

        public FechaDerechoResponse ListarRolesFechaDerecho(FechaDerechoRequest fechaDerechoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, fechaDerechoRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<FechaDerechoDto>("BEGIN SGVPG_COMUN_V2.ROLES_FECHA_DERECHO(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new FechaDerechoResponse
            {
                FechaDerechoDtoLista = consulta,
            };
            return datosconsulta;
        }

        public FechaDerechoResponse ListarRolesFechaDerechoVacio(FechaDerechoRequest fechaDerechoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, fechaDerechoRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<FechaDerechoDto>("BEGIN SGVPG_COMUN_V2.ROLES_FECHA_DERECHO_NULL(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new FechaDerechoResponse
            {
                FechaDerechoDtoLista = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarRolVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_ROL(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarCruceGoceVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_CRUCE_GOCE(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarRolActivoVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_ROL_ACTIVO(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

    }
}
