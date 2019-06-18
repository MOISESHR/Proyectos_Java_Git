package pe.com.claro.eba.activospostpagoejbs.jms.clients;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ejb.commons.beans.MensajeJmsBase;

public interface JMSMessageSender {
    
  ResponseBean sendObjectMessageByOperation(final MensajeJmsBase message,
                                            String operacion,String mensajeLog);
    ResponseBean sendReportErrorObjectMessageByOperation(final MensajeJmsBase message,
                                              String operacion,String mensajeLog);
    
    void reportarError(ResponseBean respError,ActivosPostpagoBean activosBean,
                                   String metodoOrigen, String mensajeTx);
}
