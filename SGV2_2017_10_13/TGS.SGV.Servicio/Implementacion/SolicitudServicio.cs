using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Servicio.Error;

namespace TGS.SGV.Servicio.Implementacion
{
    [ServiceBehavior(Namespace = "http://Tgestiona/SGV/SolicitudServicio", Name = "SolicitudServicio", ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    [ServiceErrorBehavior(typeof(ErrorHandler))]
    public class SolicitudServicio : ISolicitudServicio
    {
        private readonly ISolicitudNegocio _ISolicitudNegocio;
        private readonly IRolVacacionalNegocio _IRolVacacionalNegocio;
        public SolicitudServicio(ISolicitudNegocio ISolicitudNegocio,
            IRolVacacionalNegocio IRolVacacionalNegocio
            )
        {
            _ISolicitudNegocio = ISolicitudNegocio;
            _IRolVacacionalNegocio = IRolVacacionalNegocio;
        }
        public SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest)
        {
            return _ISolicitudNegocio.ListarSolicitud(solicitudRequest);
        }
        public ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest)
        {
            return _ISolicitudNegocio.ListarComboFeriado(comboFeriadoRequest); 
        }

        public SolicitudResponse ObtenerSolicitudPorCodigo(string CodigoSolicitud)
        {
            return _ISolicitudNegocio.ObtenerSolicitudPorCodigo(CodigoSolicitud);
        }
        public Solicitud ActualizaSolicitudTrabajador(string CodigoSolicitud, string CodigoUsuario)
        {
            return _ISolicitudNegocio.ActualizaSolicitudTrabajador(CodigoSolicitud, CodigoUsuario);
        }
        public Resultado<Solicitud> RegistraSolicitudTrabajador(Solicitud solicitud)
        {
            return _ISolicitudNegocio.RegistraSolicitudTrabajador(solicitud);
        }
        public FechaDerechoResponse ObtenerFechaDerecho(FechaDerechoRequest fechaDerechoRequest)
        {
            return _IRolVacacionalNegocio.ObtenerFechaDerecho(fechaDerechoRequest);
        }


        #region Evaluacion de Solicitud


        public EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudNegocio.ListarEvaluacionSolicitud(ESRequest);
        }
        public EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudNegocio.ListarEvaluacionSolicitudDPZ(ESRequest);
        }
        public EvaluacionSolicitudResponse ProcesoAprobarEvaluacionSolicitudFutura(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudNegocio.ProcesoAprobarEvaluacionSolicitudFutura(ESRequest);
        }
        public EvaluacionSolicitudResponse ProcesoModificarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudNegocio.ProcesoModificarEvaluacionSolicitud(ESRequest);
        }
        public EvaluacionSolicitudResponse ProcesoRechazarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudNegocio.ProcesoRechazarEvaluacionSolicitud(ESRequest);
        }
        #endregion

        public void Dispose()
        {
            _ISolicitudNegocio.Dispose();
        }

    }
}
