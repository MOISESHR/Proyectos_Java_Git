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
    public class PerfilUsuarioAccesoDatos : BaseAcceso, IPerfilUsuarioAccesoDatos
    {
        public PerfilUsuarioAccesoDatos(DbContext context)
            : base(context)
        {
        }
        
        public List<PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa)
        {
            object[] parametros = {
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, empresa, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            //var valorx = ExecuteQuery<PerfilUsuarioDto>("BEGIN SGVPG_COMUN_V2.LISTAR_ADM_EMPRESA(:P_EMPRESA, :P_RECORDSET); end;", parametros).ToList();
            var valorx = ExecuteQuery<PerfilUsuarioDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PERFILES_ADM_EMP(:P_EMPRESA, :P_RECORDSET); end;", parametros).ToList();
            return valorx;
        }
        public List<PerfilUsuarioDto> ListarAdministradorCCR(string unidad)
        {
            object[] parametros = {
                new OracleParameter("P_UNIDAD", OracleDbType.Varchar2, unidad, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var valorx = ExecuteQuery<PerfilUsuarioDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PERFILES_ADM_CCR(:P_EMPRESA, :P_RECORDSET); end;", parametros).ToList();
            return valorx;
        }


        public PerfilDtoResponse ListarPerfilesPersona(PerfilDtoRequest perfilDtoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, perfilDtoRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_SSFF", OracleDbType.Varchar2, perfilDtoRequest.EmpresaSuccessFactor, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };
                       
            var resultado = ExecuteQuery<PerfilDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PERFILES_PER(:P_CIP, :P_SSFF, :P_RESULTADO); end;", parametros).ToList();

            var respuesta = new PerfilDtoResponse
            {
                PerfilDtoLista = resultado
            };

            return respuesta;
        }

        public PerfilDtoResponse ListarPerfilesDireccion(PerfilDtoRequest perfilDtoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, perfilDtoRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_SSFF", OracleDbType.Varchar2, perfilDtoRequest.EmpresaSuccessFactor, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var resultado = ExecuteQuery<PerfilDto>("BEGIN SGVPG_COMUN_V2.OBTENER_PERFILES_DIR(:P_CIP, :P_SSFF, :P_RESULTADO); end;", parametros).ToList();

            var respuesta = new PerfilDtoResponse
            {
                PerfilDtoLista = resultado
            };

            return respuesta;
        }

    }    
}
