﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IModuloSistemaAccesoDatos : IDisposable
    {
        ModuloSistemaResponse ListarModuloDirectivo(UsuarioRequest usuarioRequest);
        ModuloSistemaResponse ListarModuloAsignado(UsuarioRequest usuarioRequest);
        ModuloSistemaResponse ListarModuloNoAsignado(UsuarioRequest usuarioRequest);
    }
}