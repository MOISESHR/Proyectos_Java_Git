using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface ITrabajadorAccesoDatos : IDisposable
    {
        TrabajadorResponse ObtenerDatosTrabajador(string codigoUsuario);

        List<ListaDto> ListarTrabajadorPorMando(TrabajadorRequest trabajadorRequest);
    }
}
