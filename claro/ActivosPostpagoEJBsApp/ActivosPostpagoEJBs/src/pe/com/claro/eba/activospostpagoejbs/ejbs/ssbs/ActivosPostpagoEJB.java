package pe.com.claro.eba.activospostpagoejbs.ejbs.ssbs;

import javax.ejb.Remote;

import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSResponse;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.GenericResponse;

@Remote
public interface ActivosPostpagoEJB {
    
   CrearClienteUnicoResponse crearCuentaCompleta(CrearClienteUnicoRequest parameters);
   CrearContratoUnicoResponse crearContrato(CrearContratoUnicoRequest parameters);
   GenericResponse actualizarContrato(ActualizarContratoRequest parameters);  
   GenericResponse agregarServiciosContrato(AgregarServiciosContratoRequest parameters);
   GenericResponse actualizarEstadoCuenta(ActualizarEstadoCuentaRequest parameters);
   CambiarPlanCMSResponse cambiarPlanCMS(CambiarPlanCMSRequest parameters);
   TransferirContratoResponse transferirContrato(TransferirContratoRequest parameters);
}
