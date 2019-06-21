using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Agente.Contrato
{
    public interface IUsuarioAgente
    {
        bool ValidarIngreso(string usuarioLogin, string claveUsuario);

        string CambiarCLave(string idUsuario, string claveAnterior, string claveNueva);

        string EnvioClaveUsuario(string nroDocumento);
    }
}
