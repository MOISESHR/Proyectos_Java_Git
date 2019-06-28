using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface ISolicitudAccesoDatos : IDisposable
    {
        SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest);
        SolicitudResponse ObtenerSolicitudPorCodigo(string codigoSolicitud);
        Solicitud ActualizaSolicitudTrabajador(string codigoSolicitud, string codigoUsuario);
        int RegistraSolicitudTrabajador(Solicitud solicitud);
        SolicitudResponse ObtenerSolicitudRegistrada(SolicitudRequest solicitudRequest);
        int InsertaSolicitudIndividual(Solicitud solicitud);               
       
        SolicitudParametroResponse ValidarCruceFechaSolicitud(SolicitudParametroRequest solicitudParametroRequest);
               
                
        ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest);
        ComboFeriadoResponse ObtenerComboFeriado(ComboFeriadoRequest comboFeriadoRequest);        
        
        
       

        #region Evaluacion de Solicitud
        SolicitudResponse ObtenerSolicitud(string codigoSolicitud);
        EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest EvaluacionSolicitudRequest);
        EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest EvaluacionSolicitudRequest);
        int CreaGocePrev(EvaluacionSolicitudRequest oRequest);
        int ActualizarAprobado(SolicitudRequest solicitud);
        int ActualizarAprobadoRegula(SolicitudRequest solicitud);
        int ActualizarSolicitudMasiva(SolicitudRequest solicitud);

        #endregion
    }
}
