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
    public class UnidadAccesoDatos : BaseAcceso, IUnidadAccesoDatos
    {
        public UnidadAccesoDatos(DbContext context)
            : base(context)
        {
        }

        public List<ListaDto> ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_CCR", OracleDbType.Varchar2, unidadDtoRequest.CodigoUnidad, ParameterDirection.Input),
                new OracleParameter("P_NOMBRE_CCR", OracleDbType.Varchar2, unidadDtoRequest.NombreUnidad, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, unidadDtoRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_USUARIO", OracleDbType.Varchar2, unidadDtoRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_PERFIL", OracleDbType.Varchar2, unidadDtoRequest.TipoPerfil, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            return ExecuteQuery<ListaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_CCR_PERFIL(:P_CODIGO_CCR, :P_NOMBRE_CCR, :P_EMPRESA, :P_USUARIO, :P_PERFIL, :P_RECORDSET); end;", parametros).ToList();            
        }

    }
}
