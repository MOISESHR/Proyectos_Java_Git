using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IUsuarioAccesoDatos : IDisposable
    {
        Usuario ObtenerUsuario(string codigoUsuario);
    }
}
