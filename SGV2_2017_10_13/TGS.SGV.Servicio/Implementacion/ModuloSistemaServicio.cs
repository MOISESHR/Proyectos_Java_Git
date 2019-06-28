using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;

namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/ModuloSistemaServicio", Name = "ModuloSistemaServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class ModuloSistemaServicio : IModuloSistemaServicio
    {
        private readonly IModuloSistemaNegocio _IModuloSistemaNegocio;

        public ModuloSistemaServicio(IModuloSistemaNegocio moduloSistemaNegocio)
        {
            _IModuloSistemaNegocio = moduloSistemaNegocio;
        }

        public ModuloSistemaResponse ListarModuloUsuario(UsuarioRequest usuarioRequest)
        {
            return _IModuloSistemaNegocio.ListarModuloUsuario(usuarioRequest);
        }

        
        public void Dispose()
        {
            _IModuloSistemaNegocio.Dispose();
        }
    }
}
