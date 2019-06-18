package pe.com.claro.eba.activospostpagoejbs.dao;

import java.util.List;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCore;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanServicios;
import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;

public interface BscsDAO {
    
  ResponseBeanServicios validarServiciosAdicionales(List<ServicioContratoType> listaServicios,
                                                      long planTarifario,
                                                  String mensajeTransaccion);
  
  public ResponseBeanCore buscarServCoreXPlan(String msgMet, int codPlanTarif);
  
}
