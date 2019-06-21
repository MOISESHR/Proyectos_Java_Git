using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato; 

namespace TGS.SGV.Negocio.Implementacion
{
    public class UnidadNegocio : IUnidadNegocio
    {
        private readonly IUnidadAccesoDatos _IUnidadAccesoDatos;
        public UnidadNegocio(IUnidadAccesoDatos unidadAccesoDatos)
        {
            _IUnidadAccesoDatos = unidadAccesoDatos;
        }
        public List<ListaDto> ListarCCRPerfil(UnidadDtoRequest unidadDtoRequest)
        {
            return _IUnidadAccesoDatos.ListarCCRPerfil(unidadDtoRequest);
        }
        public void Dispose()
        {
            _IUnidadAccesoDatos.Dispose();
        }
    }
}
