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
    public class PlantillaAccesoDatos : BaseAcceso, IPlantillaAccesoDatos
    {
        public PlantillaAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public PlantillaResponse ObtenerEstadoCorreo(string codigoCorreo, string codigoEmpresa)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_CORREO", OracleDbType.Varchar2, codigoCorreo, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_EMPRESA", OracleDbType.Varchar2, codigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<PlantillaDto>("BEGIN SGVPG_COMUN_V2.OBTENER_ESTADO_CORREO(:P_CODIGO_CORREO, :P_CODIGO_EMPRESA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new PlantillaResponse
            {
                PlantillaDto = consulta.Count > 0 ? consulta[0] : new PlantillaDto()
            };
            return datosconsulta;
        }
        public PlantillaResponse ObtenerDatosMail(PlantillaRequest request)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_CORREO", OracleDbType.Varchar2, request.CodigoCorreo, ParameterDirection.Input),
                new OracleParameter("P_EMPLEADO", OracleDbType.Varchar2, request.Empleado, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_EMPRESA", OracleDbType.Varchar2, request.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_ESOLICITUD", OracleDbType.Varchar2, request.CodigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_TIPO_SOLICITUD", OracleDbType.Varchar2, request.TipoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_MANDO", OracleDbType.Varchar2, request.Mando, ParameterDirection.Input),
                new OracleParameter("P_ADM_EMP", OracleDbType.Varchar2, request.AdmEmp, ParameterDirection.Input),
                new OracleParameter("P_USUARIO", OracleDbType.Varchar2, request.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_CIP_FROM", OracleDbType.Varchar2, request.CipFrom, ParameterDirection.Input),
                new OracleParameter("P_CIP_TO", OracleDbType.Varchar2, request.CipTo, ParameterDirection.Input),
                new OracleParameter("P_CIP_CC", OracleDbType.Varchar2, request.CipCC, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<PlantillaDto>("BEGIN SGVPG_COMUN_V2.OBTENER_DATOS_MAIL(:P_CODIGO_CORREO, :P_EMPLEADO, :P_CODIGO_EMPRESA, :P_CODIGO_ESOLICITUD, :P_TIPO_SOLICITUD, :P_MANDO, :P_ADM_EMP, :P_USUARIO, :P_CIP_FROM, :P_CIP_TO, :P_CIP_CC, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new PlantillaResponse
            {
                PlantillaDto = consulta
            };
            return datosconsulta;
        }
    }
}
