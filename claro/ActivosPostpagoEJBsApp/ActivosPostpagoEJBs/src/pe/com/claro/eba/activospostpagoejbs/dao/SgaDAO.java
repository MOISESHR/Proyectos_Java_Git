package pe.com.claro.eba.activospostpagoejbs.dao;

import pe.com.claro.eba.activospostpagoejbs.beans.BeanInsertarCoId;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;

public interface SgaDAO {
    
    ResponseBean actualizarSOT(final String[] codigosSot,
                                                                 String customerId,
                                                                 String coId,
                                                    String mensajeTransaccion);
    ResponseBean actualizarSOTContrato(String codigoSot, String customerId,
                                                         String coId,
                                                  String mensajeTransaccion);
  
    ResponseBean insertarCoid(BeanInsertarCoId bean,
                                   String mensajeTransaccion);
    
    boolean existeSOT(String men, String codSOT);
}
