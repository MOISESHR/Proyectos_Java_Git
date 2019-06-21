using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto; 

namespace TGS.SGV.Negocio.Contrato
{
   public interface IUnidadNegocio : IDisposable
   {
        List<ListaDto> ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest);
    }    
}
