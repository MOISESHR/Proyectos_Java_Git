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
    public class TrabajadorAccesoDatos : BaseAcceso, ITrabajadorAccesoDatos
    {
        public TrabajadorAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public TrabajadorResponse ObtenerDatosTrabajador(string codigoUsuario)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_USUARIO", OracleDbType.Varchar2, codigoUsuario, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<TrabajadorDto>("BEGIN SGVPG_COMUN_V2.OBTENER_DATOS_TRABAJADOR(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).FirstOrDefault();
            var datosConsulta = new TrabajadorResponse 
            {
                TrabajadorDto = consulta,
            };
            return datosConsulta;
        }

        public List<ListaDto> ListarTrabajadorPorMando(TrabajadorRequest trabajadorRequest)
        {
            object[] parametros = {
                new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, trabajadorRequest.Nombre, ParameterDirection.Input),
                new OracleParameter("P_CIP_MANDO", OracleDbType.Varchar2, trabajadorRequest.CipMando, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            return ExecuteQuery<ListaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_DATOS_TRABAJADOR(:P_CODIGO_USUARIO, :P_RECORDSET); end;", parametros).ToList();             
        }


    }
}
