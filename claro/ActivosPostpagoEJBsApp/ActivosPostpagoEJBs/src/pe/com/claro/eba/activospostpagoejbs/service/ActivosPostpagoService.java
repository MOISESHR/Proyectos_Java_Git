package pe.com.claro.eba.activospostpagoejbs.service;

import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSResponse;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoResponse;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.GenericResponse;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;

public interface ActivosPostpagoService {
    
    void procesarGestionPostVentaCMS(ActivosPostpagoBean activosBean) throws ClaroBaseException;
    void procesarGestionVentaCMS(ActivosPostpagoBean activosBean) throws ClaroBaseException;
    CrearClienteUnicoResponse crearCuentaCompleta(CrearClienteUnicoRequest request);
    CrearContratoUnicoResponse crearContratoCompleto(CrearContratoUnicoRequest request);
    GenericResponse actualizarEstadoCuenta(ActualizarEstadoCuentaRequest request);
    GenericResponse actualizarContrato(ActualizarContratoRequest request);
    GenericResponse agregarServiciosContrato(AgregarServiciosContratoRequest request);
    CambiarPlanCMSResponse cambiarPlanCMS(CambiarPlanCMSRequest parameters);
    TransferirContratoResponse transferirContrato(TransferirContratoRequest parameters);
}
