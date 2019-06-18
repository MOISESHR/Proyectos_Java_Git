package pe.com.claro.eba.activospostpagoejbs.ejbclients;

import java.util.List;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCambiarPlanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTransferirContrato;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanType;
import pe.com.claro.eba.activospostpagoejbs.types.ListaServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AcuerdoPagoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.CuentaMiembroType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DireccionType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DispositivoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionCuentaType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;


public interface CmsAdapterClient {

    ResponseBean iniciarConeccionCMS(String idTransaccion,
                                     String mensajeTransaccion);

    ResponseBean cerrarConeccionCMS(String mensajeTransaccion);

    ResponseBean iniciarTransaccionCMS(String mensajeTransaccion);

    ResponseBean hacerCommitTransaccionCMS(String mensajeTransaccion);

    ResponseBean hacerRollbackTransaccionCMS(String mensajeTransaccion);

    ResponseBeanCMS agregarMiembroLarge (CuentaMiembroType cuenta,
                                             String mensajeTransaccion);
  
    ResponseBeanCMS escribirDireccionCMS (DireccionType direccion,
                                             String mensajeTransaccion);

    ResponseBeanCMS actualizarFormaPagoCuenta (AcuerdoPagoType acuerdo,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS escribirInformacionCuenta (InformacionCuentaType infoCuenta,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS escribirCuentaCMS (ActualizarEstadoCuentaRequest req,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS crearNuevoContrato (ContratoType req,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS agregarDispositivo (DispositivoType req,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS agregarServiciosContratoCMS (List<ServicioContratoType> req,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS escribirContrato (ActualizacionContratoType req,
                                             String mensajeTransaccion);
    
    ResponseBeanCMS grabarInfoContrato (InformacionContratoType req,
                                             String mensajeTransaccion);

    ResponseBeanCambiarPlanCMS cambiarPlanCMS(CambiarPlanType cambiarPlanType,
                                          ListaServicioType listaServicioType,
                                          String mensajeTransaccion);

    ResponseBeanTransferirContrato transferirContrato(TransferirContratoType transferirContratoType,
                                                             ListaServicioType listaServicioType,
                                                             String mensajeTransaccion);
}
