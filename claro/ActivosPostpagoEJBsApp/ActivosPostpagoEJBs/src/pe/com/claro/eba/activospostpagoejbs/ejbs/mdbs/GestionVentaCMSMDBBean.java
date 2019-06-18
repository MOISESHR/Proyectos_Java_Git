package pe.com.claro.eba.activospostpagoejbs.ejbs.mdbs;

import javax.annotation.Resource;

import javax.ejb.MessageDriven;

import javax.ejb.MessageDrivenContext;

import javax.interceptor.Interceptors;

import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.ObjectMessage;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;

import pe.com.claro.eba.activospostpagoejbs.service.ActivosPostpagoService;
import pe.com.claro.eba.activospostpagoejbs.util.SpringBeanInterceptors;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;

@MessageDriven()
@Interceptors(SpringBeanInterceptors.class)
public class GestionVentaCMSMDBBean implements MessageListener {
    
    
    private static Logger logger = Logger.getLogger(GestionVentaCMSMDBBean.class);
  
    @Resource
    private MessageDrivenContext context;
  
    @Autowired
    private ActivosPostpagoService activosPostpagoService;
  
  
    /**
     * Este metodo es invocado cada vez que el MDB obtiene un mensaje de una
     * cola JMS.
     * @param message Mensaje JMS recibido. Todos los mensajes deberian ser
     * ObjectMessages y extender de la clase MensajeJmsBase
     */
    public void onMessage(Message message) {
  
        ActivosPostpagoBean msg = null;
        String mensajeLog = null;
        try {
            /**
             * En entorno de produccion no deberia haber problemas en el cast de los objetos.
             * Por tanto, solo se redirecciona al Service.
             * Sin embargo igual se atrapa la excepcion para pintar en el log.
             */
            ObjectMessage obj = (ObjectMessage)message;
            msg = (ActivosPostpagoBean)obj.getObject();
  
            mensajeLog = "[onMessage idTx=" + msg.getIdTransaccion() + "] ";
  
            activosPostpagoService.procesarGestionVentaCMS(msg);
  
        } catch (ClassCastException e) {
            /**
             * Siempre capturar esta exception a parte para diferenciarla.
             */
            logger.error("Se ha recibido un JMS Message de una clase distinta a la esperada. Ver detalle:",
                         e);
        } catch (ClaroBaseException e) {
            /**
             * Para los errores que fueron controlados en el proceso.
             * Solo pintar el message de la exception, mas no la traza
             */
            logger.error(mensajeLog +
                         "Hubo un error controlado procesando message:" +
                         e.getMessage());
            logger.info(mensajeLog +
                        "Se regresara el JMS message a la cola...");
            context.setRollbackOnly();
        } catch (Exception t) {
            /**
             * Para los errores desconocidos, regresar el mensaje a la cola.
             * Pintar la traza del error producido.
             */
            logger.error(mensajeLog + "Hubo un error procesando message:", t);
            logger.info(mensajeLog +
                        "Se regresara el JMS message a la cola...");
            context.setRollbackOnly();
        }
    }
}
