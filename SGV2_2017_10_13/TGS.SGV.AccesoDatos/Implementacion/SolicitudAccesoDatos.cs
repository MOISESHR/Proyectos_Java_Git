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
    public class SolicitudAccesoDatos : BaseAcceso, ISolicitudAccesoDatos
    {
        public SolicitudAccesoDatos(DbContext context)
            : base(context)
        {

        }

        #region Solicitud
        public SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest)
        {            
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitudRequest.CodUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_ESTADO_SOLICITUD", OracleDbType.Varchar2, solicitudRequest.EstadoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, solicitudRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, solicitudRequest.Tamanio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudDto>("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.LISTAR_SOLICITUD_TRABAJADOR(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_ESTADO_SOLICITUD, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new SolicitudResponse
            {
                SolicitudDtoLista = consulta,
                Total =(consulta!=null && consulta.Count>0 ? consulta[0].Total :0)                
            };            
            return datosconsulta;
        }

        public ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, comboFeriadoRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, comboFeriadoRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, comboFeriadoRequest.TipoEmpresa, ParameterDirection.Input),                
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<ComboFeriadoDto>("BEGIN SGVPG_COMUN_V2.LISTAR_COMBO_FERIADO(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_EMPRESA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new ComboFeriadoResponse
            {
                ComboFeriadoDtoLista = consulta,
            };
            return datosconsulta;
        }
        
        public ComboFeriadoResponse ObtenerComboFeriado(ComboFeriadoRequest comboFeriadoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, comboFeriadoRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, comboFeriadoRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, comboFeriadoRequest.TipoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<ComboFeriadoDto>("BEGIN SGVPG_COMUN_V2.OBTENER_COMBO_FERIADO(:P_FECHA_INICIO, :P_FECHA_FINAL, :P_EMPRESA, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new ComboFeriadoResponse
            {
                ComboFeriadoDto = consulta,
            };
            return datosconsulta;
        }
        
        public SolicitudResponse ObtenerSolicitudPorCodigo(string codigoSolicitud)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, codigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudDto>("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.OBTENER_SOLICITUD_TRABAJADOR(:P_CODIGO_SOLICITUD, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new SolicitudResponse
            {
                SolicitudDtoLista = consulta,
            };
            return datosconsulta;
        }
        public Solicitud ActualizaSolicitudTrabajador(string codigoSolicitud, string codigoUsuario)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, codigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, codigoUsuario, ParameterDirection.Input)
            };
            return ExecuteQuery<Solicitud>("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.ACTUALIZA_SOLICITUD_TRABAJADOR(:P_CODIGO_SOLICITUD, :P_CODIGO_USUARIO); end;", parametros).FirstOrDefault();
        }
        public int RegistraSolicitudTrabajador(Solicitud solicitud)
        {
            object[] parametros = {
                new OracleParameter("P_FECHA_INICIAL", OracleDbType.Varchar2, solicitud.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitud.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_NUMERO_DIAS", OracleDbType.Varchar2, solicitud.CalculoDias, ParameterDirection.Input),
                new OracleParameter("P_FLAG_ADELANTO", OracleDbType.Varchar2, solicitud.FlagAdelantoVacaciones, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitud.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_TIPO_SOLICITUD", OracleDbType.Varchar2, solicitud.TipoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RESPONSABLE", OracleDbType.Varchar2, solicitud.CodigoAprobador, ParameterDirection.Input)                
            };
            int resultado =  ExecuteCommand("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.INSERTAR_SOLICITUD_TRABAJADOR(:P_FECHA_INICIAL, :P_FECHA_FINAL, :P_NUMERO_DIAS, :P_FLAG_ADELANTO, :P_CODIGO_USUARIO, :P_TIPO_SOLICITUD, :P_RESPONSABLE); end;", parametros);            
            return resultado;
        }

        public SolicitudResponse ObtenerSolicitudRegistrada(SolicitudRequest solicitudRequest)
        {            
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitudRequest.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudDto>("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.OBTENER_SOLICITUD(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudResponse
            {
                SolicitudDto = consulta,
            };
            return datosconsulta;            
        }

        public int InsertaSolicitudIndividual(Solicitud solicitud)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, solicitud.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, solicitud.TipoSolicitud, ParameterDirection.Input)
            };
            return ExecuteCommand("BEGIN SGVPG_SOLICITUD_REGISTRO_V2.INSERTAR_SOLICITUD_TRABAJADOR(:P_CODIGO_USUARIO, :P_CODIGO_SOLICITUD); end;", parametros);            
        }
                             
              
        public SolicitudParametroResponse ValidarCruceFechaSolicitud(SolicitudParametroRequest solicitudParametroRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO",OracleDbType.Varchar2,solicitudParametroRequest.CodigoUsuario,ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, solicitudParametroRequest.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, solicitudParametroRequest.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudParametroDto>("BEGIN SGVPG_VALIDACIONES_V2.VALIDAR_CRUCE_SOLICITUD(:P_CODIGO_USUARIO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_RECORDSET); end; ", parametros).FirstOrDefault();
            var datosconsulta = new SolicitudParametroResponse
            {
                SolicitudParametroDto = consulta,
            };
            return datosconsulta;
        }
        
       
        
      


        #endregion

        #region Evaluacion de Solicitud

        public SolicitudResponse ObtenerSolicitud(string codigoSolicitud)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, codigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<SolicitudDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.OBTENER_SOLICITUD(:P_CODIGO_SOLICITUD, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new SolicitudResponse
            {
                SolicitudDto = consulta.Count>0? consulta[0] : new SolicitudDto(),
            };

            return datosconsulta;
        }
        public EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_EMPRESA", OracleDbType.Varchar2, ESRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_CCR", OracleDbType.Varchar2, ESRequest.CodigoCCR, ParameterDirection.Input),
                new OracleParameter("P_HIJOS", OracleDbType.Varchar2, ESRequest.Hijos, ParameterDirection.Input),
                new OracleParameter("P_CIP", OracleDbType.Varchar2, ESRequest.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_NOMBRES", OracleDbType.Varchar2, ESRequest.Nombres, ParameterDirection.Input),
                new OracleParameter("P_APATERNO", OracleDbType.Varchar2, ESRequest.APaterno, ParameterDirection.Input),
                new OracleParameter("P_AMATERNO", OracleDbType.Varchar2, ESRequest.AMaterno, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_ESTADO", OracleDbType.Varchar2, ESRequest.CodigoEstado, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_TIPO", OracleDbType.Varchar2, ESRequest.CodigoTipo, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, ESRequest.FechaSolicitudInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, ESRequest.FechaSolicitudFin, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, ESRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_PERFIL", OracleDbType.Varchar2, ESRequest.CodigoPerfil, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, ESRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, ESRequest.Tamanio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<EvaluacionSolicitudDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.LISTAR_EVALUACION_SOLICITUD(:P_CODIGO_EMPRESA, :P_CODIGO_CCR, :P_HIJOS, :P_CIP, :P_NOMBRES, :P_APATERNO, :P_AMATERNO, :P_CODIGO_ESTADO, :P_CODIGO_TIPO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_CODIGO_USUARIO, :P_CODIGO_PERFIL, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new EvaluacionSolicitudResponse
            {
                ListarEvaluacionSolicitudDto = consulta,
                Total = (consulta != null && consulta.Count > 0 ? consulta[0].Total : 0)
                //Total = consulta.Count
            };
            return datosconsulta;
        }
        /// <summary>
        /// Listar Evaluacion Solicitud Desplazado
        /// </summary>
        /// <param name="ESRequest"></param>
        /// <returns></returns>
        public EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest ESRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_EMPRESA", OracleDbType.Varchar2, ESRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_CCR", OracleDbType.Varchar2, ESRequest.CodigoCCR, ParameterDirection.Input),
                new OracleParameter("P_HIJOS", OracleDbType.Varchar2, ESRequest.Hijos, ParameterDirection.Input),
                new OracleParameter("P_CIP", OracleDbType.Varchar2, ESRequest.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_NOMBRES", OracleDbType.Varchar2, ESRequest.Nombres, ParameterDirection.Input),
                new OracleParameter("P_APATERNO", OracleDbType.Varchar2, ESRequest.APaterno, ParameterDirection.Input),
                new OracleParameter("P_AMATERNO", OracleDbType.Varchar2, ESRequest.AMaterno, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_ESTADO", OracleDbType.Varchar2, ESRequest.CodigoEstado, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_TIPO", OracleDbType.Varchar2, ESRequest.CodigoTipo, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2, ESRequest.FechaSolicitudInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FINAL", OracleDbType.Varchar2, ESRequest.FechaSolicitudFin, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, ESRequest.CodigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_PERFIL", OracleDbType.Varchar2, ESRequest.CodigoPerfil, ParameterDirection.Input),
                new OracleParameter("P_REG_X_PAGINA", OracleDbType.Int16, ESRequest.Indice, ParameterDirection.Input),
                new OracleParameter("P_NUM_PAGINA", OracleDbType.Int16, ESRequest.Tamanio, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<EvaluacionSolicitudDto>("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.LISTAR_EVALUAC_SOLICITUD_DPZ(:P_CODIGO_EMPRESA, :P_CODIGO_CCR, :P_HIJOS, :P_CIP, :P_NOMBRES, :P_APATERNO, :P_AMATERNO, :P_CODIGO_ESTADO, :P_CODIGO_TIPO, :P_FECHA_INICIO, :P_FECHA_FINAL, :P_CODIGO_USUARIO, :P_CODIGO_PERFIL, :P_REG_X_PAGINA, :P_NUM_PAGINA, :P_RECORDSET); end;", parametros).ToList();
            var datosconsulta = new EvaluacionSolicitudResponse
            {
                ListarEvaluacionSolicitudDto = consulta,
                Total = (consulta != null && consulta.Count > 0 ? consulta[0].Total : 0)
            };
            return datosconsulta;
        }
        
        public int CreaGocePrev(EvaluacionSolicitudRequest oRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, oRequest.CipEmpleado, ParameterDirection.Input),
                new OracleParameter("P_FECHA_DERECHO", OracleDbType.Varchar2, oRequest.FechaDerecho, ParameterDirection.Input),
                new OracleParameter("P_COMPRA_DISFRUTE", OracleDbType.Varchar2, oRequest.CompraDisfrute, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_SOL", OracleDbType.Varchar2, oRequest.FechaSolicitudInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FIN_SOL", OracleDbType.Varchar2, oRequest.FechaSolicitudFin, ParameterDirection.Input),
                new OracleParameter("P_REQUIERE_BONO", OracleDbType.Varchar2, oRequest.RequiereBonos, ParameterDirection.Input),
                new OracleParameter("P_COMENTARIO_SOL", OracleDbType.Varchar2, oRequest.ComentarioSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CIP_USUARIO", OracleDbType.Varchar2, oRequest.CipUsuario, ParameterDirection.Input),
                new OracleParameter("P_PERFIL_USUARIO", OracleDbType.Varchar2, oRequest.CodigoPerfil, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PAGO", OracleDbType.Varchar2, oRequest.TipoPago, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_PADRE", OracleDbType.Varchar2, oRequest.CodigoPadre, ParameterDirection.Input),
                new OracleParameter("P_DESCANSO_FISICO", OracleDbType.Varchar2, oRequest.DescansoFisico, ParameterDirection.Input),
                new OracleParameter("P_REPROGRAMADO", OracleDbType.Varchar2, oRequest.Reprogramado, ParameterDirection.Input),
                new OracleParameter("P_ADELANTO_VAC", OracleDbType.Varchar2, oRequest.Adelanto, ParameterDirection.Input),
                new OracleParameter("P_FECHA_PAGO", OracleDbType.Varchar2, oRequest.FechaPago, ParameterDirection.Input),
                new OracleParameter("P_CODIGO_SOLICITUD", OracleDbType.Varchar2, oRequest.CodigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_TIPO_SOLICITUD", OracleDbType.Varchar2, oRequest.TipoSolicitud, ParameterDirection.Input)
            };
            return ExecuteCommand("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.CREA_GOCE_PREVENTIVO(:P_CIP_EMPLEADO, :P_FECHA_DERECHO, :P_COMPRA_DISFRUTE, :P_FECHA_INICIO_SOL, :P_FECHA_FIN_SOL, :P_REQUIERE_BONO, :P_COMENTARIO_SOL, :P_CIP_USUARIO, :P_PERFIL_USUARIO, :P_TIPO_PAGO, :P_CODIGO_PADRE, :P_DESCANSO_FISICO, :P_REPROGRAMADO, :P_ADELANTO_VAC, :P_FECHA_PAGO, :P_CODIGO_SOLICITUD, :P_TIPO_SOLICITUD); end;", parametros);
        }
        
        public int ActualizarAprobado(SolicitudRequest solicitud)
        {
            object[] parametros = {
                new OracleParameter("P_COD_SOLICITUD", OracleDbType.Varchar2, solicitud.CodigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_ESTADO_SOLICITUD", OracleDbType.Varchar2, solicitud.EstadoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CIP_USUARIO", OracleDbType.Varchar2, solicitud.CodUsuario, ParameterDirection.Input),
                new OracleParameter("P_PERFIL_USUARIO", OracleDbType.Varchar2, solicitud.CodigoPerfil, ParameterDirection.Input),
            };
            return ExecuteCommand("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.ACTUALIZAR_SOLICITUD_APROBADO(:P_COD_SOLICITUD, :P_ESTADO_SOLICITUD, :P_CIP_USUARIO, :P_PERFIL_USUARIO); end;", parametros);
        }
        public int ActualizarAprobadoRegula(SolicitudRequest solicitud)
        {
            object[] parametros = {
                new OracleParameter("P_COD_SOLICITUD", OracleDbType.Varchar2, solicitud.CodigoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_TIPO_SOLICITUD", OracleDbType.Varchar2, solicitud.TipoSolicitud, ParameterDirection.Input),
                new OracleParameter("P_CIP_USUARIO", OracleDbType.Varchar2, solicitud.CodUsuario, ParameterDirection.Input),
                new OracleParameter("P_PERFIL_USUARIO", OracleDbType.Varchar2, solicitud.CodigoPerfil, ParameterDirection.Input),
            };
            return ExecuteCommand("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.ACTUALIZAR_SOLICITUD_REGULA(:P_COD_SOLICITUD, :P_TIPO_SOLICITUD, :P_CIP_USUARIO, :P_PERFIL_USUARIO); end;", parametros);
        }
        public int ActualizarSolicitudMasiva(SolicitudRequest solicitud)
        {
            object[] parametros = {
                new OracleParameter("P_CIP_EMPLEADO", OracleDbType.Varchar2, solicitud.CodigoEmpleado, ParameterDirection.Input),
                new OracleParameter("P_FECHA_INICIO_SOL", OracleDbType.Varchar2, solicitud.FechaInicio, ParameterDirection.Input),
                new OracleParameter("P_FECHA_FIN_SOL", OracleDbType.Varchar2, solicitud.FechaFinal, ParameterDirection.Input),
                new OracleParameter("P_CIP_USUARIO", OracleDbType.Varchar2, solicitud.CodUsuario, ParameterDirection.Input),
            };
            return ExecuteCommand("BEGIN SGVPG_SOLICITUD_EVALUACION_V2.ACTUALIZAR_SOLICITUD_MASIVA(:P_CIP_EMPLEADO, :P_FECHA_INICIO_SOL, :P_FECHA_FIN_SOL, :P_CIP_USUARIO); end;", parametros);
        }
        
        #endregion

    }
}
