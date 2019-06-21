using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Strings;

namespace TGS.SGV.Negocio.Implementacion
{
    public class GoceVacacionalNegocio: IGoceVacacionalNegocio
    {
        private readonly IGoceVacacionalAccesoDatos _IGoceVacacionalAccesoDatos;
        private readonly ITrabajadorNegocio _ITrabajadorNegocio;
        private readonly IParametroNegocio _IParametroNegocio;

        public GoceVacacionalNegocio(IGoceVacacionalAccesoDatos goceVacacionalAccesoDatos,
            ITrabajadorNegocio trabajadorNegocio,
            IParametroNegocio parametroNegocio)
        {
            _IGoceVacacionalAccesoDatos = goceVacacionalAccesoDatos;
            _ITrabajadorNegocio = trabajadorNegocio;
            _IParametroNegocio = parametroNegocio;
        }

        public GoceVacacionalResponse ListarObtenerGoce(GoceVacacionalRequest goceVacacionalRequest)
        {

            var resultadoTrabajador = _ITrabajadorNegocio.ObtenerDatosTrabajador(goceVacacionalRequest.CodigoUsuario);

            var oParametroInterno = new ParametroRequest()
            {
                Codigo = Constantes.ParametrosInternos.PeriodoAnteriorReprogram,
                Empresa = resultadoTrabajador.TrabajadorDto.CodigoEmpresa
            };
            var resultadoParametroInterno = _IParametroNegocio.ObtenerParametroInterno(oParametroInterno);

            if (resultadoTrabajador.TrabajadorDto.TipoEmpleado != Constantes.TipoEmpleado.Directivo)
            {
                if (Convert.ToInt32(resultadoParametroInterno.ParametroDto.TipoPago).Equals(Convert.ToInt32(Constantes.Generales.ValorCero)))
                {
                    return _IGoceVacacionalAccesoDatos.ListarObtenerGoce(goceVacacionalRequest);
                }
                else
                {                    
                    return _IGoceVacacionalAccesoDatos.ListarObtenerGoceReprogramado(goceVacacionalRequest);
                }
            }
            else
            {                
                return _IGoceVacacionalAccesoDatos.ListarObtenerGoceDirectivo(goceVacacionalRequest);
            }            
        }
        public void Dispose()
        {
            _IGoceVacacionalAccesoDatos.Dispose();
        }

    }
}
