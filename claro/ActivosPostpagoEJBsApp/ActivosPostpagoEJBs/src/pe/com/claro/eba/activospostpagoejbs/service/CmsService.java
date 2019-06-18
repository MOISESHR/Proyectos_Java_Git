package pe.com.claro.eba.activospostpagoejbs.service;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCambiarPlanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionCliente;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionContratos;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTransferirContrato;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AcuerdoPagoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratosRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CuentaMiembroType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionCuentaType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCamposAdicionalesType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaDireccionType;

public interface CmsService {
    
    ResponseBeanCMS crearCuentaCompleta(CuentaMiembroType cuenta,
                                                 ListaDireccionType listDirecciones,
                                                 AcuerdoPagoType acuerdoPago,
                                                 InformacionCuentaType infoCuenta,
                                                 String idTransaccion,
                                                 String mensajeTx);
    
    ResponseBeanCreacionCliente crearClienteVariasCuentas(CrearClienteRequest request,
                                                        String mensajeTx);
    
    ResponseBeanCMS crearContratoCompleto(ContratoType contrato,
                                          ListaCamposAdicionalesType listaCamposAdicionales,
                                                   String tipoPostpago,
                                                 String idTransaccion,
                                                 String mensajeTx,
                                                String nombreAplicacion);
    
    ResponseBeanCreacionContratos crearVariosContratos(CrearContratosRequest request,
                                                       String mensajeTx);
  
    ResponseBeanCMS actualizarEstadoCuenta(ActualizarEstadoCuentaRequest request,
                                                   String idTransaccion,
                                                   String mensajeTx);
    ResponseBeanCMS actualizarContrato(ActualizacionContratoType request,
                                                   String idTransaccion,
                                                   String mensajeTx);
    ResponseBeanCMS agregarServiciosContrato(AgregarServiciosContratoRequest request,
                                                   String idTransaccion,
                                                   String mensajeTx);

    ResponseBeanCambiarPlanCMS cambiarPlanCMS(CambiarPlanCMSRequest request,
                                                     String idTransaccion,
                                                     String mensajeTx);

    ResponseBeanTransferirContrato transferirContrato(TransferirContratoRequest request,
                                                             String idTransaccion,
                                                             String mensajeTx);
}
