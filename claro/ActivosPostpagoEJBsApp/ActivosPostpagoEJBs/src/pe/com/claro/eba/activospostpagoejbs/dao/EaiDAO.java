package pe.com.claro.eba.activospostpagoejbs.dao;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTxEai;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;


public interface EaiDAO {

    ResponseBeanTxEai registrarTransaccion(ActivosPostpagoBean activosBean,
                                                    String mensajeTransaccion);
    ResponseBean actualizarErrorTransaccion(ActivosPostpagoBean activosBean,
                                                    String mensajeTransaccion);
    ResponseBean finalizarTransaccion(ActivosPostpagoBean activosBean,
                                                    String mensajeTransaccion);
    ResponseBean archivarTransaccion(ActivosPostpagoBean activosBean,
                                                  String mensajeTransaccion);
    
}
