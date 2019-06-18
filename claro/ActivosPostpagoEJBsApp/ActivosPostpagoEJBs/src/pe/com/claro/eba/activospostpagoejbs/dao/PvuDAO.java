package pe.com.claro.eba.activospostpagoejbs.dao;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;

public interface PvuDAO {
    
    ResponseBean actualizarEstadoContrato(String correlativoSisact,
                                                                 String custCode,
                                                                 String customerId,
                                                                 String estadoContrato,
                                                                String usuarioApp,
                                                              String[] coIds,
                                                    String mensajeTransaccion);
}
