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
    public class ModuloSistemaAccesoDatos : BaseAcceso, IModuloSistemaAccesoDatos
    {
        public ModuloSistemaAccesoDatos(DbContext context)
            : base(context)
        {
        }

        public ModuloSistemaResponse ListarModuloDirectivo(UsuarioRequest usuarioRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, usuarioRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, usuarioRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERFIL", OracleDbType.Varchar2, usuarioRequest.TipoPerfil, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var consulta= ExecuteQuery<ModuloSistemaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_MODULOS_PERFIL_DIRX(:P_CIP, :P_EMPRESA, :P_TIPO_PERFIL, :P_RESULTADO); end;", parametros).ToList();

            var respuesta = new ModuloSistemaResponse
            {
                ModuloSistemaDtoLista = consulta,
            };

            return respuesta;
        }

        public ModuloSistemaResponse ListarModuloAsignado(UsuarioRequest usuarioRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, usuarioRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, usuarioRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERFIL", OracleDbType.Varchar2, usuarioRequest.TipoPerfil, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var consulta = ExecuteQuery<ModuloSistemaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_MODULOS_PERFIL_ASIG(:P_CIP, :P_EMPRESA, :P_TIPO_PERFIL, :P_RESULTADO); end;", parametros).ToList();

            var respuesta = new ModuloSistemaResponse
            {
                ModuloSistemaDtoLista = consulta,
            };

            return respuesta;
        }

        public ModuloSistemaResponse ListarModuloNoAsignado(UsuarioRequest usuarioRequest)
        {
            object[] parametros = {
                new OracleParameter("P_CIP", OracleDbType.Varchar2, usuarioRequest.Cip, ParameterDirection.Input),
                new OracleParameter("P_EMPRESA", OracleDbType.Varchar2, usuarioRequest.CodigoEmpresa, ParameterDirection.Input),
                new OracleParameter("P_TIPO_PERFIL", OracleDbType.Varchar2, usuarioRequest.TipoPerfil, ParameterDirection.Input),
                new OracleParameter("P_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            var consulta = ExecuteQuery<ModuloSistemaDto>("BEGIN SGVPG_COMUN_V2.LISTAR_MODULOS_PERFIL_NASIG(:P_CIP, :P_EMPRESA, :P_TIPO_PERFIL, :P_RESULTADO); end;", parametros).ToList();

            var respuesta = new ModuloSistemaResponse
            {
                ModuloSistemaDtoLista = consulta,
            };

            return respuesta;
        }

    }
}
