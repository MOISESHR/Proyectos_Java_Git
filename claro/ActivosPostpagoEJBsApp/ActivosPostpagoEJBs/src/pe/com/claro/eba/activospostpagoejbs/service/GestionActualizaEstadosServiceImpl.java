package pe.com.claro.eba.activospostpagoejbs.service;

import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;
import pe.com.claro.eba.activospostpagoejbs.beans.BeanInsertarCoId;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.dao.PvuDAO;
import pe.com.claro.eba.activospostpagoejbs.dao.SgaDAO;
import pe.com.claro.eba.activospostpagoejbs.jms.clients.JMSMessageSender;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.eba.activospostpagoejbs.util.UtilitariosActivosPostpago;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCamposAdicionalesType;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;


public class GestionActualizaEstadosServiceImpl implements GestionActualizaEstadosService{
    
    private static Logger logger= Logger.getLogger(ActivosPostpagoServiceImpl.class);
    
    @Autowired
    private JMSMessageSender jmsMessageSender;
    
    @Autowired
    private PvuDAO pvuDAO;
    
    @Autowired
    private SgaDAO sgaDAO;
    
    
    public void procesarActualizacionesEstado(ActivosPostpagoBean activosBean) throws ClaroBaseException{
      
        String mensajeTransaccion =
            "[procesarActualizacionesEstado idTx=" + activosBean.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        ResponseBean respAux= null;
        
        try {
            logger.info(mensajeTransaccion +
                        "[INICIO procesarActualizacionesEstado]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(activosBean));
        
            List<String> listaOperaciones= activosBean.getListaOperaciones();
            
            if( listaOperaciones!=null && !listaOperaciones.isEmpty() ){
                
                String operacion= listaOperaciones.get(0);
                
                logger.info(mensajeTransaccion +
                      "Operacion recibida=" + operacion);
                activosBean.setComponenteClienteJMS( PropertiesInternos.COMPONENTE_MDB_ACTUALIZA_ESTADOS  + 
                                                     "." + operacion);
                
                if( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_ESTADO_PVU.equalsIgnoreCase( operacion ) ){
                    
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de actualizacion de estado de contrato...");
                    procesarActualizacionEstadoContrato(activosBean, mensajeTransaccion);
                   
                } else if( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_SOT_SGA.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de actualizacion de SOT...");
                    procesarActualizacionSOT(activosBean, mensajeTransaccion);
                } else if( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_SOT_SGA_CONTRATO.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de actualizacion de SOT para CrearContrato...");
                    procesarActualizacionSOTContrato(activosBean, mensajeTransaccion);
                    
                } else if( PropertiesInternos.NOMBRE_OPERACION_INSERTAR_PROID_SGA.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de insertar ProId en SGA...");
                    insertarProIdSGA(activosBean, mensajeTransaccion);
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
            ResponseBean respError= new ResponseBean();
            respError.setMensajeRespuesta("Se genero una excepcion desconocida en el proceso:" + 
                                          e.getLocalizedMessage());
            jmsMessageSender.reportarError(respError, activosBean, "ProcesarActualizacionesEstado", mensajeTransaccion);
             throw new ClaroBaseException(e);
        } finally {
            logger.info(mensajeTransaccion +
                        "[FIN procesarActualizacionesEstado] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
    }
    
    public void procesarActualizacionEstadoContrato(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarActualizacionEstadoContrato] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        List<ListaCamposAdicionalesType.CampoAdicional> camposAdicionales= null;
        String correlativoSisact= null;
        String customerId= null;
        String custCode= null;
        String estadoContrato= null;
        String[] coIds= null;
        
        try{
            camposAdicionales= activosBean.getData().getListaCamposAdicionales().getCampoAdicional();
            
            correlativoSisact= UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CORRELATIVO_SISACT);
            custCode= UtilitariosActivosPostpago.obtenerUltimoValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CUSTCODE); //Se toma el ultimo
            customerId= UtilitariosActivosPostpago.obtenerUltimoValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CUSTOMER_ID); //se toma el ultimo
            coIds= UtilitariosActivosPostpago.obtenerTodosValores(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_COID);
            estadoContrato= UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_ESTADO_CONTRATO);
            
            if(estadoContrato==null){
                estadoContrato= PropertiesExternos.PVU_ESTADO_CONTRATO_ASIGNADO;
            }
        } catch(Exception e){
            logger.error(mensajeTransaccion + "Hubo un error tratando de obtener los datos necesarios del objeto de entrada:",e);
            return;
        }
        
        logger.info(mensajeTransaccion +
              "Se procede a actualizar contrato...");
                
        ResponseBean resp=  pvuDAO.actualizarEstadoContrato(correlativoSisact, custCode, customerId, 
                                                            estadoContrato, activosBean.getUsrAplicacion(), 
                                                            coIds, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se actualizo estado de contrato PVU exitosamente.");
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                if( PropertiesExternos.PVU_ESTADO_CONTRATO_ASIGNADO.equalsIgnoreCase( estadoContrato ) ){
                    //Si fue el estadoo de asignacion, se debe establecer en CamposAdicionales el estado de activacion 
                    //para que lo tome cuando termine el proceso.
                    ListaCamposAdicionalesType campListType= activosBean.getData().getListaCamposAdicionales();
                    
                    if( campListType==null ){
                        campListType= new ListaCamposAdicionalesType();
                        activosBean.getData().setListaCamposAdicionales( campListType );
                    }
                    
                    List <ListaCamposAdicionalesType.CampoAdicional> listaCampos= campListType.getCampoAdicional();
                    ListaCamposAdicionalesType.CampoAdicional adicional= new ListaCamposAdicionalesType.CampoAdicional();
                    adicional.setNombreCampo( PropertiesInternos.CAMPO_ADICIONAL_ESTADO_CONTRATO );
                    adicional.setValor( PropertiesExternos.PVU_ESTADO_CONTRATO_ACTIVADO );
                    listaCampos.add( adicional );
                }
                
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
            }
        }  else {
            logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
            jmsMessageSender.reportarError(resp, activosBean, "procesarActualizacionEstadoContratoPVU", mensajeTransaccion);
            
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
        }
    }

    public void procesarActualizacionSOT(ActivosPostpagoBean activosBean, String mensajeTx) throws ClaroBaseException {

        String mensajeTransaccion = mensajeTx + "[procesarActualizacionSOT] ";
        ResponseBean respAux = null;
        List<String> listaOperaciones = activosBean.getListaOperaciones();

        List<ListaCamposAdicionalesType.CampoAdicional> camposAdicionales = null;
        String customerId = null;
        String[] codigosSot = null;
        String coId = null;

        try {
            camposAdicionales = activosBean.getData().getListaCamposAdicionales().getCampoAdicional();

            customerId = UtilitariosActivosPostpago.obtenerUltimoValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CUSTOMER_ID); //se toma el ultimo
            coId = UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_COID);
            codigosSot = UtilitariosActivosPostpago.obtenerTodosValores(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CODIGO_SOT);

            if (codigosSot == null || codigosSot.length == 0) {
                logger.error(mensajeTransaccion + "No se ha enviado los codigosSOT necesarios para la transaccion. El proceso terminara.");
                return;
            }

        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error tratando de obtener los datos necesarios del objeto de entrada:", e);
            return;
        }
        for (int i = 0; i < codigosSot.length; i++) {
            logger.info(mensajeTransaccion + "Se verifica si existe SOT...");
            boolean existe = sgaDAO.existeSOT(mensajeTransaccion, codigosSot[i]);

            if (existe == false) {

                logger.info(mensajeTransaccion + "Se procede a actualizar SOT...");
                ResponseBean resp = sgaDAO.actualizarSOT(codigosSot, customerId, coId, mensajeTransaccion);

                if (PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(resp.getCodigoRespuesta())) {

                    logger.info(mensajeTransaccion + "Se actualizo estado de contrato exitosamente.");
                    listaOperaciones.remove(0);

                    if (listaOperaciones.isEmpty()) {
                        logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
                    } else {
                        logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + listaOperaciones.get(0));

                        respAux = jmsMessageSender.sendObjectMessageByOperation(activosBean, listaOperaciones.get(0), mensajeTransaccion);
                    }
                } else {
                    logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + resp.getMensajeRespuesta());
                    jmsMessageSender.reportarError(resp, activosBean, "procesarActualizacionSOT", mensajeTransaccion);

                    throw new ClaroBaseException("Hubo un error en la actualizacion:" + resp.getMensajeRespuesta());
                }
            }
        }
    }
    public void procesarActualizacionSOTContrato(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarActualizacionSOTContrato] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        List<ListaCamposAdicionalesType.CampoAdicional> camposAdicionales = null;
        String customerId = null;
        String codigoSot = null;
        String coId = null;
        
        try{
            camposAdicionales= activosBean.getData().getListaCamposAdicionales().getCampoAdicional();
            
            for(int i=0; i<activosBean.getData().getListaContratos().getContrato().size(); i++){
                customerId = "" + activosBean.getData().getListaContratos().getContrato().get(i).getCustomerId();
                if(customerId != null){
                    break;
                }
            }
            
            coId = UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_COID);
            codigoSot = UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CODIGO_SOT);
            
            if( codigoSot == null || codigoSot.length()==0){
                logger.error(mensajeTransaccion + "No se ha enviado los codigosSOT necesarios para la transaccion. El proceso terminara.");
                return;
            }
            
        } catch(Exception e){
            logger.error(mensajeTransaccion + "Hubo un error tratando de obtener los datos necesarios del objeto de entrada:",e);
            return;
        }
        
        logger.info(mensajeTransaccion + "Se procede a actualizar SOT...");
            
        ResponseBean resp = sgaDAO.actualizarSOTContrato(codigoSot, customerId, coId, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
        
            logger.info(mensajeTransaccion + "Se actualizo estado de contrato exitosamente.");
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
            jmsMessageSender.reportarError(resp, activosBean, "procesarActualizacionSOTContrato", mensajeTransaccion);
        
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                     resp.getMensajeRespuesta());
        }        
    }
    
    public void insertarProIdSGA(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[insertarProIdSGA] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        List<ListaCamposAdicionalesType.CampoAdicional> camposAdicionales= null;
        String customerId= null;
        String[] codigosSot= null;
        String coId= null;
        
        try{
            camposAdicionales= activosBean.getData().getListaCamposAdicionales().getCampoAdicional();
            
            if(PropertiesInternos.TIPO_POSTPAGO_HFC.equalsIgnoreCase(activosBean.getTipoPostpago()) && 
                PropertiesInternos.NOMBRE_METODO_ORIGEN_CREAR_CONTRATOS.equalsIgnoreCase(activosBean.getMetodoOrigen())){
                for(int i=0; i<activosBean.getData().getListaContratos().getContrato().size(); i++){
                    customerId = "" + activosBean.getData().getListaContratos().getContrato().get(i).getCustomerId();
                    if(customerId != null){
                        break;
                    }
                }
            }else{
                customerId= UtilitariosActivosPostpago.obtenerUltimoValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CUSTOMER_ID);
            }
            coId= UtilitariosActivosPostpago.obtenerPrimerValor(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_COID);
            codigosSot= UtilitariosActivosPostpago.obtenerTodosValores(camposAdicionales, PropertiesInternos.CAMPO_ADICIONAL_CODIGO_SOT);
            
            if( codigosSot==null || codigosSot.length==0){
                logger.error(mensajeTransaccion + "No se ha enviado los codigosSOT necesarios para la transaccion. El proceso terminara.");
                return;
            }
            
        } catch(Exception e){
            logger.error(mensajeTransaccion + "Hubo un error tratando de obtener los datos necesarios del objeto de entrada:",e);
            return;
        }
        
        for(int i=0; i<codigosSot.length; i++){
          
            logger.info(mensajeTransaccion + "Se procede a insertar CoId");
            
            BeanInsertarCoId bean = new BeanInsertarCoId();
            bean.setCoId(coId);
            bean.setCodigosSot(codigosSot[i]);
            bean.setCustomerId(customerId);
            bean.setEtapa(PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDF0);
            bean.setDesError(PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDF0);
            
            ResponseBean resp = sgaDAO.insertarCoid(bean, mensajeTransaccion);
            
            if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
              
                logger.info(mensajeTransaccion + "Se actualizo estado de contrato exitosamente.");
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
                jmsMessageSender.reportarError(resp, activosBean, "insertarProIdSGA", mensajeTransaccion);
                
                throw new ClaroBaseException("Hubo un error en insertarProIdSGA:" + 
                             resp.getMensajeRespuesta());
            }
        }
    }    
}
