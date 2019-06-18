package pe.com.claro.eba.activospostpagoejbs.jms.clients;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;

import org.apache.log4j.Logger;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesException;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.eba.activospostpagoejbs.util.ServiceLocatorException;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ebs.services.activospostpago.beans.ErrorType;
import pe.com.claro.ejb.commons.beans.MensajeJmsBase;


public class JMSMessageSenderImpl implements JMSMessageSender{
    
    private static Logger logger= Logger.getLogger(JMSMessageSenderImpl.class);
    
    private Map<String,JMSClient> hashJMSClients= new HashMap<String,JMSClient>();
    private Map<String,JMSClient> hashErrorJMSClients= new HashMap<String,JMSClient>();
    
    public ResponseBean sendObjectMessageByOperation(final MensajeJmsBase message,
                                          String operacion,String mensajeLog) {
      
      String mensajeTransaccionMetodo= mensajeLog + "[sendObjectMessageByOperation] ";
      ResponseBean resp= new ResponseBean();
      
      try{
          logger.debug(mensajeTransaccionMetodo + "Se procede a obtener referencia de cliente JMS...");
          
          JMSClient client= obtainJmsClientByMethod(operacion, mensajeTransaccionMetodo);
          
          logger.debug(mensajeTransaccionMetodo + "Referencia obtenida: ProviderURL=" + client.getProviderUrl() + 
                       "; ConnectionFactory=" + client.getJndiConnectionFactory() + 
                       "; Queue=" + client.getJndiQueue() );
          
          logger.info(mensajeTransaccionMetodo + "Se procede a enviar message:" + 
                      JAXBUtilitarios.anyObjectToXmlText( message ));
          
          client.sendMessage(message, mensajeTransaccionMetodo);
        
          logger.info(mensajeTransaccionMetodo + "Message enviado exitosamente.");
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
      } catch(PropertiesException e){
          logger.error("Error de configuracion en properties:" + e.getMessage());
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
          resp.setMensajeRespuesta( "Error de configuracion en properties:" + e.getMessage() );
      } catch(ServiceLocatorException e){
          logger.error("Error en lookup de un recurso JMS:" + e.getMessage());
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
          resp.setMensajeRespuesta( "Error en lookup de un recurso JMS:" + e.getMessage() );
      } catch(Exception e){
          logger.error("Error enviando JMS message:",e);
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
          resp.setMensajeRespuesta( "Error enviando mensaje a cola JMS" );
      }
      
      return resp;
    }
    
    public ResponseBean sendReportErrorObjectMessageByOperation(final MensajeJmsBase message,
                                          String operacion,String mensajeLog) {
      
      String mensajeTransaccionMetodo= mensajeLog + "[sendReportErrorObjectMessageByOperation] ";
      ResponseBean resp= new ResponseBean();
      
      try{
          logger.debug(mensajeTransaccionMetodo + "Se procede a obtener referencia de cliente JMS de Reporte de Error...");
          
          JMSClient client= obtainJmsClientErrorReportByMethod(operacion, mensajeTransaccionMetodo);
          
          logger.debug(mensajeTransaccionMetodo + "Referencia obtenida: ProviderURL=" + client.getProviderUrl() + 
                       "; ConnectionFactory=" + client.getJndiConnectionFactory() + 
                       "; Queue=" + client.getJndiQueue() );
          
          logger.info(mensajeTransaccionMetodo + "Se procede a enviar message:" + 
                      JAXBUtilitarios.anyObjectToXmlText( message ));
          
          client.sendMessage(message, mensajeTransaccionMetodo);
        
          logger.info(mensajeTransaccionMetodo + "Message enviado exitosamente.");
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
      } catch(PropertiesException e){
          logger.info(mensajeTransaccionMetodo + "No se realizara envio de mensaje JMS debido a que no esta configurada la cola JMS de reporte de error:" + e.getMessage());
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR );
          resp.setMensajeRespuesta( "No se realizara envio de mensaje JMS debido a que no esta configurada la cola JMS de reporte de error:" + e.getMessage() );
      } catch(ServiceLocatorException e){
          logger.error(mensajeTransaccionMetodo + "Error en lookup de un recurso JMS:" + e.getMessage());
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
          resp.setMensajeRespuesta( "Error en lookup de un recurso JMS:" + e.getMessage() );
      } catch(Exception e){
          logger.error(mensajeTransaccionMetodo + "Error enviando JMS message:",e);
          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
          resp.setMensajeRespuesta( "Error enviando mensaje a cola JMS" );
      }
      
      return resp;
    }
    
    public void reportarError(ResponseBean respError,ActivosPostpagoBean activosBean,
                               String metodoOrigen, String mensajeTx){
        String mensajeTransaccion= mensajeTx + "[reportarError] ";
        try{
            String operacion= activosBean.getListaOperaciones().get(0);
            
            if( respError.getErrorData()!=null){
                activosBean.setDatosError( respError.getErrorData() );
            } else {
                activosBean.setDatosError( new ErrorType(metodoOrigen,
                                                         respError.getMensajeRespuesta()) );
            }
            List<String> listaoperaciones= new ArrayList<String>(1);
            listaoperaciones.add( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_ERROR_TX_EAI );
            
            activosBean.setListaOperaciones( listaoperaciones );
            
            logger.info(mensajeTransaccion + "Se procede a reportar el error a la cola JMS de error de la operacion=" + 
                    operacion);
            ResponseBean respReporte= sendReportErrorObjectMessageByOperation(activosBean, 
                                                  operacion, 
                                                    mensajeTransaccion);
            
            if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respReporte.getCodigoRespuesta() ) ){
                logger.info(mensajeTransaccion + "Se realizo correctamente el envio de mensaje JMS de reporte de error.");
            } 
        } catch(Exception e){
            logger.error(mensajeTransaccion + "Error desconocido tratando de hacer el reporte de error:",e);
        }
    }
    
    private JMSClient obtainJmsClientByMethod(String operation,String mensajeTx) throws PropertiesException,ServiceLocatorException{
        
        JMSClient jmsClient= null;
        
        if( hashJMSClients.containsKey( operation ) ){
            jmsClient= hashJMSClients.get(operation);
        } else{
            String providerUrl= PropertiesExternos.obtenerJMSProviderUrlPorMetodo(operation, mensajeTx);
            String connFactory= PropertiesExternos.obtenerJMSConnFactoryPorMetodo(operation, mensajeTx);
            String queue= PropertiesExternos.obtenerJMSQueuePorMetodo(operation, mensajeTx);
            
            Properties parm = new Properties();
            parm.setProperty("java.naming.factory.initial",
                             "weblogic.jndi.WLInitialContextFactory");
            parm.setProperty("java.naming.provider.url",
                             providerUrl);
            
            jmsClient= new JMSClient(parm,connFactory,queue);
            
            hashJMSClients.put(operation, jmsClient);
        }
        
        return jmsClient;
    }
    
    private JMSClient obtainJmsClientErrorReportByMethod(String operation,String mensajeTx) throws PropertiesException,ServiceLocatorException{
        
        JMSClient jmsClient= null;
        
        if( hashErrorJMSClients.containsKey( operation ) ){
            jmsClient= hashErrorJMSClients.get(operation);
        } else{
            String providerUrl= PropertiesExternos.obtenerJMSProviderUrlPorMetodo(operation, mensajeTx);
            String connFactory= PropertiesExternos.obtenerJMSConnFactoryPorMetodo(operation, mensajeTx);
            String queue= PropertiesExternos.obtenerJMSReporteErrorQueuePorMetodo(operation, mensajeTx);
            
            Properties parm = new Properties();
            parm.setProperty("java.naming.factory.initial",
                             "weblogic.jndi.WLInitialContextFactory");
            parm.setProperty("java.naming.provider.url",
                             providerUrl);
            
            jmsClient= new JMSClient(parm,connFactory,queue);
            
            hashErrorJMSClients.put(operation, jmsClient);
        }
        
        return jmsClient;
    }
}
