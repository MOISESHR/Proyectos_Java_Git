package pe.com.claro.eba.activospostpagoejbs.ejbs.ssbs;

import javax.ejb.Stateless;
import javax.ejb.TransactionManagement;
import javax.ejb.TransactionManagementType;

import javax.interceptor.Interceptors;

import org.springframework.beans.factory.annotation.Autowired;

import pe.com.claro.eba.activospostpagoejbs.service.ActivosPostpagoService;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSResponse;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoResponse;
import pe.com.claro.eba.activospostpagoejbs.util.SpringBeanInterceptors;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.GenericResponse;


@Stateless
@TransactionManagement(value = TransactionManagementType.BEAN)
@Interceptors(SpringBeanInterceptors.class)
public class ActivosPostpagoEJBBean implements ActivosPostpagoEJB,
                                               ActivosPostpagoEJBLocal {
    
    
    @Autowired
    private ActivosPostpagoService activosPostpagoService;

    public ActivosPostpagoEJBBean() {
    }

    public CrearClienteUnicoResponse crearCuentaCompleta(CrearClienteUnicoRequest parameters) {
        return activosPostpagoService.crearCuentaCompleta(parameters);
    }
  
    public CrearContratoUnicoResponse crearContrato(CrearContratoUnicoRequest parameters) {
        return activosPostpagoService.crearContratoCompleto(parameters);
    }
  
    public GenericResponse actualizarContrato(ActualizarContratoRequest parameters) {
        return activosPostpagoService.actualizarContrato(parameters);
    }
  
    public GenericResponse agregarServiciosContrato(AgregarServiciosContratoRequest parameters) {
      return activosPostpagoService.agregarServiciosContrato(parameters);
    }
  
    public GenericResponse actualizarEstadoCuenta(ActualizarEstadoCuentaRequest parameters) {
        return activosPostpagoService.actualizarEstadoCuenta(parameters);
    }

    public CambiarPlanCMSResponse cambiarPlanCMS(CambiarPlanCMSRequest parameters) {
        return activosPostpagoService.cambiarPlanCMS(parameters);
    }

    public TransferirContratoResponse transferirContrato(TransferirContratoRequest parameters) {
        return activosPostpagoService.transferirContrato(parameters);
    }
}
