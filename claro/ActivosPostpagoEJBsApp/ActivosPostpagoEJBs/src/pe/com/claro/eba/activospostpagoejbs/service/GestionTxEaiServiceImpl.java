package pe.com.claro.eba.activospostpagoejbs.service;

import java.util.List;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTxEai;
import pe.com.claro.eba.activospostpagoejbs.dao.EaiDAO;
import pe.com.claro.eba.activospostpagoejbs.jms.clients.JMSMessageSender;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;


public class GestionTxEaiServiceImpl implements GestionTxEaiService{
    
    private static Logger logger= Logger.getLogger(ActivosPostpagoServiceImpl.class);
    
    @Autowired
    private JMSMessageSender jmsMessageSender;
    
    @Autowired
    private EaiDAO eaiDAO;
        
    
    public void procesarTransaccionesEAI(ActivosPostpagoBean activosBean) throws ClaroBaseException{
      
        String mensajeTransaccion =
            "[procesarTransaccionesEAI idTx=" + activosBean.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        ResponseBean respAux= null;
        
        try {
            logger.info(mensajeTransaccion +
                        "[INICIO procesarTransaccionesEAI]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(activosBean));
        
            List<String> listaOperaciones= activosBean.getListaOperaciones();
            
            if( listaOperaciones!=null && !listaOperaciones.isEmpty() ){
                
                String operacion= listaOperaciones.get(0);
                
                logger.info(mensajeTransaccion +
                      "Operacion recibida=" + operacion);
                activosBean.setComponenteClienteJMS( PropertiesInternos.COMPONENTE_MDB_GESTION_TX_EAI  + 
                                                     "." + operacion);
                
                if( PropertiesInternos.NOMBRE_OPERACION_REGISTRAR_TX_EAI.equalsIgnoreCase( operacion ) ){
                    
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de registrar transaccion en EAI...");
                    procesarRegistroTransaccion(activosBean, mensajeTransaccion);
                   
                } else if( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_ERROR_TX_EAI.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de actualizacion de error de Tx EAI...");
                    procesarActualizacionErrorTransaccion(activosBean, mensajeTransaccion);
                } else if( PropertiesInternos.NOMBRE_OPERACION_FINALIZAR_TX_EAI.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de finalizacion de transaccion de EAI...");
                    procesarFinalizacionTransaccion(activosBean, mensajeTransaccion);
                } else{
                    logger.info(mensajeTransaccion + "La operacion no corresponde a este MDB, por tanto se continuara con la siguiente operacion...");
                    listaOperaciones.remove(0);
                    
                    if( listaOperaciones.isEmpty() ){
                        logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
                    } else{
                        logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                                listaOperaciones.get(0));
                        respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                              listaOperaciones.get(0), 
                                                                mensajeTransaccion);
                    }
                }
                
            } else {
                logger.info(mensajeTransaccion +
                        "No existen operaciones a realizar, por lo tanto no se hara nada.");
            }
            
        } catch (ClaroBaseException e) {
            logger.error(mensajeTransaccion + "Hubo un error:" + e.getMessage());
            throw e;
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
             throw new ClaroBaseException(e);
        } finally {
            logger.info(mensajeTransaccion +
                        "[FIN procesarTransaccionesEAI] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
    }
    
    public void procesarArchivamientoTxEAI(ActivosPostpagoBean activosBean) throws ClaroBaseException{
      
        String mensajeTransaccion =
            "[procesarArchivamientoTxEAI idTx=" + activosBean.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        ResponseBean respAux= null;
        
        try {
            logger.info(mensajeTransaccion +
                        "[INICIO procesarArchivamientoTxEAI]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(activosBean));
        
            ResponseBean respArch= eaiDAO.archivarTransaccion(activosBean, mensajeTransaccion);
            
            if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respArch.getCodigoRespuesta() ) ){
                logger.info(mensajeTransaccion + "Se ARCHIVO el registro exitosamente. Por lo que se termina el proceso.");
                
            }  else if( PropertiesInternos.CODIGO_ERROR_ESTADO_ARCHIVAR.equalsIgnoreCase( respArch.getCodigoRespuesta() ) ){
                logger.info(mensajeTransaccion + "No se puede archivar debido a:" + respArch.getMensajeRespuesta());
                logger.info(mensajeTransaccion + "Esto significa que se debe reprocesar la operacion pendiente.");
                
                List<String> listaOperaciones= activosBean.getListaOperaciones();
                
                if( listaOperaciones==null || listaOperaciones.isEmpty() ){
                    logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
                } else{
                    logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la operacion=" + 
                              listaOperaciones.get(0));
                    
                    respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                                listaOperaciones.get(0), 
                                                                  mensajeTransaccion);
                }
            }  else {
                logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                             respArch.getMensajeRespuesta());
                throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                             respArch.getMensajeRespuesta());
            }
            
        } catch (ClaroBaseException e) {
            logger.error(mensajeTransaccion + "Hubo un error:" + e.getMessage());
            throw e;
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
             throw new ClaroBaseException(e);
        } finally {
            logger.info(mensajeTransaccion +
                        "[FIN procesarArchivamientoTxEAI] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
    }
    
    public void procesarRegistroTransaccion(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarRegistroTransaccion] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        logger.info(mensajeTransaccion +
              "Se procede a registrar transaccion...");
                
        ResponseBeanTxEai resp=  eaiDAO.registrarTransaccion(activosBean, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se realizo el registro exitosamente. Se settea idTransaccionInterno en el objeto de entrada.");
            activosBean.setIdTransaccionInternoDB( resp.getIdTransaccionInterno() );
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
                
            }
        }  else {
            logger.error(mensajeTransaccion + "Hubo un error en el registro:" + 
                         resp.getMensajeRespuesta());
            throw new ClaroBaseException("Hubo un error en el registro:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public void procesarActualizacionErrorTransaccion(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarActualizacionErrorTransaccion] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        logger.info(mensajeTransaccion +
              "Se procede a actualizar error de transaccion...");
                
        ResponseBean resp=  eaiDAO.actualizarErrorTransaccion(activosBean, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se realizo la actualizacion exitosamente.");
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
                
            }
        }  else {
            logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public void procesarFinalizacionTransaccion(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarFinalizacionTransaccion] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        logger.info(mensajeTransaccion +
              "Se procede a finalizar transaccion en EAI...");
                
        ResponseBean resp=  eaiDAO.finalizarTransaccion(activosBean, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se realizo la actualizacion exitosamente.");
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
                
            }
        }  else {
            logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
}
