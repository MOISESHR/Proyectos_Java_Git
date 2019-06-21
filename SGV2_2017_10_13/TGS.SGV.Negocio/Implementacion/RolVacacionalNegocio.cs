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
    public class RolVacacionalNegocio : IRolVacacionalNegocio    
    {
        private readonly IRolVacacionalAccesoDatos _IRolVacacionalAccesoDatos;
        private readonly IParametroNegocio _IParametroNegocio;
        public RolVacacionalNegocio(IRolVacacionalAccesoDatos RolVacacionalAccesoDatos, IParametroNegocio parametroNegocio)
        {
            _IRolVacacionalAccesoDatos = RolVacacionalAccesoDatos;
            _IParametroNegocio = parametroNegocio;
        }
        public RolVacacionalResponse ObtenerFechaDerecho(RolVacacionalRequest parametro)
        {
            return _IRolVacacionalAccesoDatos.ObtenerFechaDerecho(parametro);
        }
        public RolVacacionalResponse ObtenerFechaDerechoRegulariza(string cipEmpleado)
        {
            return _IRolVacacionalAccesoDatos.ObtenerFechaDerechoRegulariza(cipEmpleado);
        }
        public RolVacacionalResponse ObtenerDiasDisponiblesPagos(RolVacacionalRequest parametro)
        {
            return _IRolVacacionalAccesoDatos.ObtenerDiasDisponiblesPagos(parametro);
        }
        public int ValidarModificarFechaPago(RolVacacionalRequest parametro)
        {
            return _IRolVacacionalAccesoDatos.ValidarModificarFechaPago(parametro);
        }
        public FechaDerechoResponse ListarRolesFechaDerecho(FechaDerechoRequest fechaDerechoRequest)
        {
            return _IRolVacacionalAccesoDatos.ListarRolesFechaDerecho(fechaDerechoRequest);
        }

        public FechaDerechoResponse ListarRolesFechaDerechoVacio(FechaDerechoRequest fechaDerechoRequest)
        {
            return _IRolVacacionalAccesoDatos.ListarRolesFechaDerechoVacio(fechaDerechoRequest);
        }

        public FechaDerechoResponse ObtenerFechaDerecho(FechaDerechoRequest fechaDerechoRequest)
        {
            var oRolesFechaDerecho = new FechaDerechoRequest()
            {
                CodigoUsuario = fechaDerechoRequest.CodigoUsuario,
            };
            var resultadoRolesFechaDerecho = ListarRolesFechaDerecho(oRolesFechaDerecho);

            if (resultadoRolesFechaDerecho.FechaDerechoDtoLista[0].MaximaFechaDerecho.Equals(string.Empty))
            {
                resultadoRolesFechaDerecho = ListarRolesFechaDerechoVacio(oRolesFechaDerecho);
            }

            if (resultadoRolesFechaDerecho.FechaDerechoDtoLista.Count > 0)
            {
                return (resultadoRolesFechaDerecho);
            }
            return null;
        }

        public SolicitudParametroResponse ValidarRolVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IRolVacacionalAccesoDatos.ValidarRolVacacional(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarRol(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoRol = ValidarRolVacacional(solicitudParametroRequest);

            if (resultadoRol.SolicitudParametroDto.CodigoSalida == Constantes.RespuestaRolVacacional.RolVacacional)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.NoTieneRol, resultadoRol.SolicitudParametroDto.CodigoCip));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public SolicitudParametroResponse ValidarCruceGoceVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IRolVacacionalAccesoDatos.ValidarCruceGoceVacacional(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarCruceGoce(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoCruceGoce = ValidarCruceGoceVacacional(solicitudParametroRequest);

            if (resultadoCruceGoce.SolicitudParametroDto.CruceGoce > 0)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceGoceRegistrado, resultadoCruceGoce.SolicitudParametroDto.CodigoCip));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public SolicitudParametroResponse ValidarRolActivoVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IRolVacacionalAccesoDatos.ValidarRolActivoVacacional(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarRolActivo(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoRolActivo = _IParametroNegocio.ValidarMandoParametro(solicitudParametroRequest);

            if (resultadoRolActivo.SolicitudParametroDto.ValidaRol > 0 & resultadoRolActivo.SolicitudParametroDto.ContenidoIntervalo == 0)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.NoTieneRolActivo, resultadoRolActivo.SolicitudParametroDto.CodigoCip));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public void Dispose()
        {
            _IRolVacacionalAccesoDatos.Dispose();
        }

    }
}
