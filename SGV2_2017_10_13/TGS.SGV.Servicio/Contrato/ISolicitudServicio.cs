using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface ISolicitudServicio: IDisposable
    {
        [OperationContract]
        SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest);

        [OperationContract]
        SolicitudResponse ObtenerSolicitudPorCodigo(string CodigoSolicitud);

        [OperationContract]
        Solicitud ActualizaSolicitudTrabajador(string CodigoSolicitud, string CodigoUsuario);

        [OperationContract]
        Resultado<Solicitud> RegistraSolicitudTrabajador(Solicitud solicitud);

        [OperationContract]
        ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest);

        [OperationContract]
        FechaDerechoResponse ObtenerFechaDerecho(FechaDerechoRequest fechaDerechoRequest);


        [OperationContract]
        EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);

        [OperationContract]
        EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest ESRequest);

        [OperationContract]
        EvaluacionSolicitudResponse ProcesoAprobarEvaluacionSolicitudFutura(EvaluacionSolicitudRequest ESRequest);

        [OperationContract]
        EvaluacionSolicitudResponse ProcesoModificarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);

        [OperationContract]
        EvaluacionSolicitudResponse ProcesoRechazarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest);

    }
}
