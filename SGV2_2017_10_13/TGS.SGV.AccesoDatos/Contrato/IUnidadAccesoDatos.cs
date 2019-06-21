using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto; 

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IUnidadAccesoDatos : IDisposable
    {
        List<ListaDto> ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest);
    }
}
