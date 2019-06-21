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
    public class CorreoNegocio: ICorreoNegocio    
    {
        private readonly ICorreoAccesoDatos _ICorreoAccesoDatos;
        public CorreoNegocio(ICorreoAccesoDatos CorreoAccesoDatos)
        {
            _ICorreoAccesoDatos = CorreoAccesoDatos;
        }

        public CorreoResponse ObtenerDatoCorreo(CorreoRequest correoRequest)
        {
            return _ICorreoAccesoDatos.ObtenerDatoCorreo(correoRequest);
        }
        
        public void Dispose()
        {
            _ICorreoAccesoDatos.Dispose();
        }
    }
}
