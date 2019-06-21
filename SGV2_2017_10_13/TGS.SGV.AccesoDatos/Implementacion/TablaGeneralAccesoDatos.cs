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
    public class TablaGeneralAccesoDatos : BaseAcceso, ITablaGeneralAccesoDatos    
    {
        public TablaGeneralAccesoDatos(DbContext context)
            : base(context)
        {
        }
        public List<ListaDto> ListarTablaGeneral(string codigoFiltro)
        {
            object[] parametros = {
                new OracleParameter("P_CODIGO_TABLA", OracleDbType.Varchar2, codigoFiltro, ParameterDirection.Input),
                new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<ListaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_TABLA_GENERAL(:P_CODIGO_TABLA, :P_RECORDSET); end;", parametros).ToList();                       
            return consulta;
        }
    }    
}
