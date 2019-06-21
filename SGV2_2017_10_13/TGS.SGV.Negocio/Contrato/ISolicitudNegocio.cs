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
    public interface ISolicitudNegocio : IDisposable
    {
        SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest);
        SolicitudResponse ObtenerSolicitudPorCodigo(string codigoSolicitud);
        Solicitud ActualizaSolicitudTrabajador(string codigoSolicitud, string codigoUsuario);                               
        Resultado<Solicitud> RegistraSolicitudTrabajador(Solicitud solicitud);
        SolicitudResponse ObtenerSolicitudRegistrada(SolicitudRequest solicitudRequest);
        Resultado<Solicitud> InsertaSolicitudIndividual(Solicitud solicitud);        
        ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest);
        



        EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);
        EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest ESRequest);
        EvaluacionSolicitudResponse ProcesoAprobarEvaluacionSolicitudFutura(EvaluacionSolicitudRequest ESRequest);
        EvaluacionSolicitudResponse ProcesoModificarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);
        EvaluacionSolicitudResponse ProcesoRechazarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);
        EvaluacionSolicitudResponse AprobarSolicitudes(string codigoSolicitud, string cipUsuario, string codigoPerfil);
        Resultado<SolicitudParametroRequest> ValidarFechaCorte(SolicitudResponse solicitud, string codigoPerfil);
        int CreaGocePrev(EvaluacionSolicitudRequest oRequest);
        int ActualizarAprobado(SolicitudRequest solicitud);
        int ActualizarAprobadoRegula(SolicitudRequest solicitud);
        int ActualizarSolicitudMasiva(SolicitudRequest solicitud);
        EvaluacionSolicitudResponse AprobarSolicitudesRegularizacion(string CadenaCodigosSolicitud, string cipUsuario, string codigoPerfil);

    }
}
