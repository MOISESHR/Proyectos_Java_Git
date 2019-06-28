using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IParametroAccesoDatos:  IDisposable
    {
        ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest);

        ParametroResponse ObtenerParametro(ParametroRequest parametroRequest);

        SolicitudParametroResponse ValidarFeriadoComboParametro(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarComboContinuadoParametro(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarComboSigueParametro(SolicitudParametroRequest solicitudParametroRequest);

        ParametroResponse ParametroPorOpcion(ParametroRequest parametroRequest);

        ParametroResponse ParametroTipoPago(ParametroRequest parametroRequest);

        IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario);

        SolicitudParametroResponse ValidarMandoParametro(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarDiasDisponibleParametro(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarDiasMayorParametros(SolicitudParametroRequest solicitudParametroRequest);

        IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio);

        FechaGoceResponse ObtenerGoceFuturo(string codigoUsuario);

        SolicitudParametroResponse ValidarFeriadoFestivo(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarCruceFechaLicencia(SolicitudParametroRequest solicitudParametroRequest);

    }
}
