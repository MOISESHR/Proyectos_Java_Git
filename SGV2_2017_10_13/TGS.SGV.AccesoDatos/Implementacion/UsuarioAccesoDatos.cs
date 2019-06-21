using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.AccesoDatos.Implementacion
{
    public class UsuarioAccesoDatos : BaseAcceso , IUsuarioAccesoDatos
    {
        public UsuarioAccesoDatos(DbContext context)
            : base(context)
        {
        }

        public Usuario ObtenerUsuario (string codigoUsuario)
        { 
            object[] parametros = {
                new OracleParameter("pcodusuario", OracleDbType.Varchar2, codigoUsuario, ParameterDirection.Input),
                new OracleParameter("pcur_retorno", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            
            return ExecuteQuery<Usuario>("BEGIN ccopg_seguridad.ccoss_devolverusuario2(:pcodusuario, :pcur_retorno); end;", parametros).FirstOrDefault();
        }
    }
}
