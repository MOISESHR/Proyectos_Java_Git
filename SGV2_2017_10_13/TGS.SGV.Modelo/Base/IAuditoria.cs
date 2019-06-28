using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Base
{
    public interface IAuditoria
    {
        DateTime FechaRegistro { get; set; }

        DateTime? FechaModifica { get; set; }

        int UsuarioRegistro { get; set; }

        int? UsuarioModifica { get; set; }

        string Estado { get; set; }
    }
}
