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
    public class GoceVacacionalAccesoDatos : BaseAcceso, IGoceVacacionalAccesoDatos
    {
        public GoceVacacionalAccesoDatos(DbContext context)
            : base(context)
        {
        }

        #region GoceVacacional
        public GoceVacacionalResponse ListarObtenerGoce(GoceVacacionalRequest goceVacacionalRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, goceVacacionalRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_DERECHO", OracleDbType.Varchar2, goceVacacionalRequest.FechaDerecho, ParameterDirection.Input),
                new OracleParameter("P_FECHA_ROL", OracleDbType.Varchar2, goceVacacionalRequest.FechaRol, ParameterDirection.Input),
                new OracleParameter("P_PERIODO", OracleDbType.Int16, goceVacacionalRequest.Periodo, ParameterDirection.Input),
                new OracleParameter("P_REPROGRAMADO", OracleDbType.Int16, goceVacacionalRequest.Reprogramado, ParameterDirection.Input),
                new OracleParameter("P_TIPO_EMPLEADO", OracleDbType.Varchar2, goceVacacionalRequest.TipoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Tamanio, ParameterDirection.Input),                
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<GoceVacacionalDto>("BEGIN SGVPG_GOCE_VACACIONAL_V2.OBTENER_GOCES(:P_CODIGO_USUARIO, :P_FECHA_DERECHO, :P_FECHA_ROL, :P_PERIODO, :P_REPROGRAMADO, :P_TIPO_EMPLEADO, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new GoceVacacionalResponse
            {
                GoceVacacionalDtoLista = consulta,
                Total = (consulta != null && consulta.Count > 0 ? consulta[0].Total : 0)
            };
            return datosconsulta;
        }

        public GoceVacacionalResponse ListarObtenerGoceDirectivo(GoceVacacionalRequest goceVacacionalRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, goceVacacionalRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_DERECHO", OracleDbType.Varchar2, goceVacacionalRequest.FechaDerecho, ParameterDirection.Input),
                new OracleParameter("P_FECHA_ROL", OracleDbType.Varchar2, goceVacacionalRequest.FechaRol, ParameterDirection.Input),
                new OracleParameter("P_PERIODO", OracleDbType.Int16, goceVacacionalRequest.Periodo, ParameterDirection.Input),
                new OracleParameter("P_REPROGRAMADO", OracleDbType.Int16, goceVacacionalRequest.Reprogramado, ParameterDirection.Input),
                new OracleParameter("P_TIPO_EMPLEADO", OracleDbType.Varchar2, goceVacacionalRequest.TipoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Tamanio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<GoceVacacionalDto>("BEGIN SGVPG_GOCE_VACACIONAL_V2.OBTENER_GOCES_DIRECTORES(:P_CODIGO_USUARIO, :P_FECHA_DERECHO, :P_FECHA_ROL, :P_PERIODO, :P_REPROGRAMADO, :P_TIPO_EMPLEADO, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new GoceVacacionalResponse
            {
                GoceVacacionalDtoLista = consulta,
                Total = (consulta != null && consulta.Count > 0 ? consulta[0].Total : 0)
            };
            return datosconsulta;
        }

        public GoceVacacionalResponse ListarObtenerGoceReprogramado(GoceVacacionalRequest goceVacacionalRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, goceVacacionalRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_DERECHO", OracleDbType.Varchar2, goceVacacionalRequest.FechaDerecho, ParameterDirection.Input),
                new OracleParameter("P_FECHA_ROL", OracleDbType.Varchar2, goceVacacionalRequest.FechaRol, ParameterDirection.Input),
                new OracleParameter("P_PERIODO", OracleDbType.Int16, goceVacacionalRequest.Periodo, ParameterDirection.Input),
                new OracleParameter("P_REPROGRAMADO", OracleDbType.Int16, goceVacacionalRequest.Reprogramado, ParameterDirection.Input),
                new OracleParameter("P_TIPO_EMPLEADO", OracleDbType.Varchar2, goceVacacionalRequest.TipoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, goceVacacionalRequest.Tamanio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<GoceVacacionalDto>("BEGIN SGVPG_GOCE_VACACIONAL_V2.OBTENER_GOCES_REPROGRAMADO(:P_CODIGO_USUARIO, :P_FECHA_DERECHO, :P_FECHA_ROL, :P_PERIODO, :P_REPROGRAMADO, :P_TIPO_EMPLEADO, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new GoceVacacionalResponse
            {
                GoceVacacionalDtoLista = consulta,
                Total = (consulta != null && consulta.Count > 0 ? consulta[0].Total : 0)
            };
            return datosconsulta;
        }

        #endregion

    }
}
