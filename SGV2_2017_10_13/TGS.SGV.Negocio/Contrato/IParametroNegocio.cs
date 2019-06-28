using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Negocio.Contrato
{
    public interface IParametroNegocio: IDisposable
    {
        ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest);

        ParametroResponse ObtenerParametro(ParametroRequest parametroRequest);

        ParametroAccesoResponse ObtenerParametroAcceso(UsuarioRequest usuarioRequest);

        IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio);

        Resultado<IndicadorTrabajadorRequest> ValidarIndicadorTrabajador(IndicadorTrabajadorRequest indicadorTrabajadorRequest);

        FechaGoceResponse ObtenerGoceFuturo(string codigoUsuario);

        SolicitudParametroResponse ValidarFeriadoFestivo(SolicitudParametroRequest solicitudParametroRequest);

        IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario);

        ParametroResponse ParametroPorOpcion(ParametroRequest parametroRequest);

        ParametroResponse ParametroTipoPago(ParametroRequest parametroRequest);

        SolicitudParametroResponse ValidarComboSigueParametro(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarComboSigueVacacional(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarFeriadoComboParametro(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarFeriadoCombo(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarComboContinuadoParametro(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarComboSigue(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarDiasDisponibleParametro(SolicitudParametroRequest solicitudParametroRequest);


        Resultado<SolicitudParametroRequest> ValidarDiasDisponible(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarDiasMayorParametros(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarDiasMayor(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarCruceFechaLicencia(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarLicencia(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<FechaGoceRequest> ValidarFechaGoce(Solicitud solicitudRequest);

        Resultado<SolicitudParametroRequest> ValidarMando(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarMandoParametro(SolicitudParametroRequest solicitudParametroRequest);


    }
}
