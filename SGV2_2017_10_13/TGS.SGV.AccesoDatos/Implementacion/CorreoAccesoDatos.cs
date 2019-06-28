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
    public class CorreoAccesoDatos : BaseAcceso, ICorreoAccesoDatos
    {
        public CorreoAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public CorreoResponse ObtenerDatoCorreo(CorreoRequest request)
        {
            object[] parametros = {
                new OracleParameter("P_CORREOENVIO", OracleDbType.Varchar2, request.CorreoEnvio, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, request.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, request.Empresa, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, request.CodigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_TIPO_SOLICITUD", OracleDbType.Varchar2, request.TipoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_MANDO", OracleDbType.Varchar2, request.CodigoMando, ParameterDirection.Input),
                new OracleParameter("P_ADM_EMP", OracleDbType.Varchar2, request.AdministradorEmpresa, ParameterDirection.Input),
                new OracleParameter("P_USUARIO", OracleDbType.Varchar2, request.Usuario, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_FROM", OracleDbType.Varchar2, request.CodigoFrom, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_TO", OracleDbType.Varchar2, request.CodigoTo, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_CC", OracleDbType.Varchar2, request.CodigoCC, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<CorreoDto>("BEGIN SGVPG_COMUN_V2.OBTENER_DATOS_CORREO(:P_CORREOENVIO, :P_CODIGO_USUARIO, :P_EMPRESA, :P_CODIGO_SOLICITUD, :P_TIPO_SOLICITUD, :P_CODIGO_MANDO, :P_ADM_EMP, :P_USUARIO, :P_CODIGO_FROM, :P_CODIGO_TO, :P_CODIGO_CC, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new CorreoResponse
            {
                CorreoDto = consulta,
            };
            return datosconsulta;
        }
    }
}
