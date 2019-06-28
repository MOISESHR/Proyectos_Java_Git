using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Implementacion
{
    public class ParametroAccesoDatos : BaseAcceso, IParametroAccesoDatos
    {
        public ParametroAccesoDatos(DbContext context)
            : base(context)
        {
        }

        public ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_PARAMETRO", OracleDbType.Varchar2, parametroRequest.Codigo, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, parametroRequest.Empresa, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var consulta = ExecuteQuery<ParametroDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PARAMETRO_INTERNO(:P_CODIGO_PARAMETRO, :P_EMPRESA, :P_RESULTADO); end;", parametros).FirstOrDefault();

            var datosconsulta = new ParametroResponse
            {
                ParametroDto = consulta,
            }; 

            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarFeriadoComboParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitudParametroRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_FERIADOS_COMBO(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }


        public SolicitudParametroResponse ValidarComboContinuadoParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitudParametroRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_COMBO_CONTINUADO(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarComboSigueParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_COMBO_SIGUE(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public ParametroResponse ParametroPorOpcion(ParametroRequest parametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_TIPO_EMPRESA", OracleDbType.Varchar2, parametroRequest.Empresa, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERFIL", OracleDbType.Varchar2, parametroRequest.Codigo, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_OPCION", OracleDbType.Varchar2, parametroRequest.CodigoOpcion, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<ParametroDto>("BEGIN SGVPG_COMUN_V2.PARAMETRO_POR_OPCION(:P_TIPO_EMPRESA, :P_TIPO_PERFIL, :P_CODIGO_OPCION, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new ParametroResponse
            {
                ParametroDto = consulta,
            };
            return datosconsulta;
        }

        public ParametroResponse ObtenerParametro(ParametroRequest parametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_PARAMETRO", OracleDbType.Varchar2, parametroRequest.Codigo, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, parametroRequest.Empresa, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var consulta = ExecuteQuery<ParametroDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PARAMETRO(:P_CODIGO_PARAMETRO,:P_EMPRESA, :P_RESULTADO); end;", parametros).FirstOrDefault();

            var datosconsulta = new ParametroResponse
            {
                ParametroDto = consulta,
            };

            return datosconsulta;
        }

        public ParametroResponse ParametroTipoPago(ParametroRequest parametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, parametroRequest.Codigo, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<ParametroDto>("BEGIN SGVPG_COMUN_V2.TIPO_PAGO(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new ParametroResponse
            {
                ParametroDto = consulta,
            };
            return datosconsulta;
        }

        public IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2, codigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<IndicadorTrabajadorDto>("BEGIN SGVPG_COMUN_V2.OBTENER_CABECERASOLICITUDIND(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new IndicadorTrabajadorResponse
            {
                IndicadorTrabajadorDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarMandoParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_MANDO(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarDiasDisponibleParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_DIAS_DISPONIBLE(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarDiasMayorParametros(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_DIAS_MAYOR(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2, codigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, fechaInicio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<IndicadorTrabajadorDto>("BEGIN SGVPG_COMUN_V2.OBTENER_CABECERASOLICITUDFUT(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new IndicadorTrabajadorResponse
            {
                IndicadorTrabajadorDto = consulta,
            };
            return datosconsulta;
        }
        public FechaGoceResponse ObtenerGoceFuturo(string codigoUsuario)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,codigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<FechaGoceDto>("BEGIN SGVPG_COMUN_V2.OBTENER_FECFINCALENDAR_GOCEFUT(:P_CODIGO_USUARIO, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new FechaGoceResponse
            {
                FechaGoceDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarFeriadoFestivo(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_DIAS_FEST", OracleDbType.Int16, solicitudParametroRequest.DiasFest, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_FERIADOS_FEST(:P_FECHA_INICIO, :P_FECHA_FINAL, :P_DIAS_FEST, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }

        public SolicitudParametroResponse ValidarCruceFechaLicencia(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_LICENCIA(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }


    }
}
