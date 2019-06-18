package pe.com.claro.eba.activospostpagoejbs.service;

import java.util.List;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;

import pe.com.claro.eba.activospostpagoejbs.beans.CustomerData;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCambiarPlanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionCliente;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionContratos;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTransferirContrato;
import pe.com.claro.eba.activospostpagoejbs.beans.Servicio;
import pe.com.claro.eba.activospostpagoejbs.jms.clients.JMSMessageSender;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSResponse;
import pe.com.claro.eba.activospostpagoejbs.types.ListaServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.ServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoResponse;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratoUnicoResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratosRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.GenericResponse;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCamposAdicionalesType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCuentaMiembroType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaServiciosContratoType;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;


public class ActivosPostpagoServiceImpl implements ActivosPostpagoService{
    
    private static Logger logger= Logger.getLogger(ActivosPostpagoServiceImpl.class);
    
    @Autowired
    private CmsService cmsService;
    @Autowired
    private JMSMessageSender jmsMessageSender;
    
    public void procesarGestionVentaCMS(ActivosPostpagoBean activosBean) throws ClaroBaseException{
      
        String mensajeTransaccion =
            "[procesarGestionVentaCMS idTx=" + activosBean.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        ResponseBean respAux= null;
        
        try {
            logger.info(mensajeTransaccion +
                        "[INICIO procesarGestionVentaCMS]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(activosBean));
        
            List<String> listaOperaciones= activosBean.getListaOperaciones();
            
            if( listaOperaciones!=null && !listaOperaciones.isEmpty() ){
                
                String operacion= listaOperaciones.get(0);
                
                logger.info(mensajeTransaccion +
                      "Operacion recibida=" + operacion);
                activosBean.setComponenteClienteJMS( PropertiesInternos.COMPONENTE_MDB_GESTION_VENTA_CMS + 
                                                     "." + operacion);
                
                if( PropertiesInternos.NOMBRE_OPERACION_CREAR_CLIENTE.equalsIgnoreCase( operacion ) ){
                    
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de crear cliente...");
                    procesarCreacionCuentasPostpago(activosBean, mensajeTransaccion);
                   
                } else if( PropertiesInternos.NOMBRE_OPERACION_CREAR_CONTRATO.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de crear contratos...");
                    procesarCreacionContratosPostpago(activosBean, mensajeTransaccion);
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
            logger.error(mensajeTransaccion + "Se genero una excepcion en el proceso:",
                         e);
            ResponseBean respError= new ResponseBean();
            respError.setMensajeRespuesta("Se genero una excepcion desconocida en el proceso:" + 
                                          e.getLocalizedMessage());
            jmsMessageSender.reportarError(respError, activosBean, "ProcesarGestionVentaCMS", mensajeTransaccion);
            throw new ClaroBaseException(e);
        } finally {
            logger.info(mensajeTransaccion +
                        "[FIN procesarGestionVentaCMS] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
    }
    
    public void procesarGestionPostVentaCMS(ActivosPostpagoBean activosBean) throws ClaroBaseException{
      
        String mensajeTransaccion =
            "[procesarGestionPostVentaCMS idTx=" + activosBean.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        ResponseBean respAux= null;
        
        try {
            logger.info(mensajeTransaccion +
                        "[INICIO procesarGestionPostVentaCMS]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(activosBean));
        
            List<String> listaOperaciones= activosBean.getListaOperaciones();
            
            if( listaOperaciones!=null && !listaOperaciones.isEmpty() ){
                
                String operacion= listaOperaciones.get(0);
                
                logger.info(mensajeTransaccion +
                      "Operacion recibida=" + operacion);
                activosBean.setComponenteClienteJMS( PropertiesInternos.COMPONENTE_MDB_GESTION_POSTVENTA_CMS  + 
                                                     "." + operacion);
                
                if( PropertiesInternos.NOMBRE_OPERACION_ACTUALIZAR_CONTRATO.equalsIgnoreCase( operacion ) ){
                    
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de actualizar contrato...");
                    procesarActualizacionContrato(activosBean, mensajeTransaccion);
                   
                } else if( PropertiesInternos.NOMBRE_OPERACION_AGREGAR_SERVICIOS_CONTRATO.equalsIgnoreCase( operacion ) ){
                    logger.info(mensajeTransaccion +
                        "Se procede a procesar operacion de agregar servicios a contrato...");
                    procesarAgregacionServiciosContrato(activosBean, mensajeTransaccion);
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
            jmsMessageSender.reportarError(respError, activosBean, "ProcesarGestionPostVentaCMS", mensajeTransaccion);
             throw new ClaroBaseException(e);
        } finally {
            logger.info(mensajeTransaccion +
                        "[FIN procesarGestionPostVentaCMS] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
    }
    
    public void procesarCreacionCuentasPostpago(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarCreacionCuentasPostpago] ";
        ResponseBean respAux= null;
        
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        CrearClienteRequest request= new CrearClienteRequest();
        request.setNombreAplicacion( activosBean.getNombreSistemaOrigen() );
        request.setIdTransaccion( activosBean.getIdTransaccion() );
        request.setCliente( activosBean.getData().getCliente() );
        request.setListaCamposAdicionales( activosBean.getData().getListaCamposAdicionales() );
        request.setTipoPostpago( activosBean.getTipoPostpago() );
        request.setUsuarioAplicacion( activosBean.getUsrAplicacion() );
        
        logger.info(mensajeTransaccion +
              "Se procede a crear cliente con varias cuentas...");
        
        ResponseBeanCreacionCliente resp=  cmsService.crearClienteVariasCuentas(request, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se creo el cliente exitosamente. Datos devueltos:" + 
                        JAXBUtilitarios.anyObjectToXmlText( resp ));
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                ListaContratoType listaContratos= activosBean.getData().getListaContratos();
                
                if( listaContratos!=null && listaContratos.getContrato()!=null
                    && !listaContratos.getContrato().isEmpty() ){
                    logger.info(mensajeTransaccion + "Se procede a settear el customerId de la ultima cuenta creada en los contratos a crear en otra operacion...");
                    long lastCustomerId= resp.getListClientesCreados().get( resp.getListClientesCreados().size()-1 ).getCustomerId();
                    for(ContratoType c:listaContratos.getContrato()){
                      c.setCustomerId( lastCustomerId );
                    }
                } 
                logger.info(mensajeTransaccion + "Se procede a settear los customerId creados en la lista de CamposAdicionales para que este disponible para otra operacion...");
                ListaCamposAdicionalesType.CampoAdicional adicional= null;
                ListaCamposAdicionalesType campListType= activosBean.getData().getListaCamposAdicionales();
                
                if( campListType==null ){
                    campListType= new ListaCamposAdicionalesType();
                    activosBean.getData().setListaCamposAdicionales( campListType );
                }
                
                List <ListaCamposAdicionalesType.CampoAdicional> listaCampos= campListType.getCampoAdicional();
                
                for(CustomerData cd:resp.getListClientesCreados()){
                    adicional= new ListaCamposAdicionalesType.CampoAdicional();
                    adicional.setNombreCampo( PropertiesInternos.CAMPO_ADICIONAL_CUSTOMER_ID );
                    adicional.setValor( String.valueOf( cd.getCustomerId() ) );
                    listaCampos.add( adicional );
                    
                    adicional= new ListaCamposAdicionalesType.CampoAdicional();
                    adicional.setNombreCampo( PropertiesInternos.CAMPO_ADICIONAL_CUSTCODE );
                    adicional.setValor( String.valueOf( cd.getCustCode() ) );
                    listaCampos.add( adicional );
                }
                
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
            }
        } else if( PropertiesInternos.CODIGO_EXITO_INCOMPLETO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){ 
          
                logger.error(mensajeTransaccion + "Hubo un error creando alguna cuenta:" + resp.getMensajeRespuesta());
                
                ListaCuentaMiembroType listaCuentas= new ListaCuentaMiembroType();
                listaCuentas.getCuenta().addAll( resp.getListCuentasFaltantes() );
                
                activosBean.getData().getCliente().setListaCuentas( listaCuentas );
                
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a esta misma cola, pero con las cuentas faltantes");
                jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
                
                jmsMessageSender.reportarError(resp, activosBean, "procesarCreacionCuentasPostpago", mensajeTransaccion);
                
        } else if( PropertiesInternos.CODIGO_ERROR_LISTA_VACIA.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){ 
            logger.info(mensajeTransaccion + "La lista de cuentas esta vacia, por tanto se continuara con la siguiente operacion...");
            
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
        } else {
            logger.error(mensajeTransaccion + "Hubo un error en la creacion del cliente:" + 
                         resp.getMensajeRespuesta());
            
            jmsMessageSender.reportarError(resp, activosBean, "procesarCreacionCuentasPostpago", mensajeTransaccion);
            
            throw new ClaroBaseException("Hubo un error en la creacion del cliente:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public void procesarCreacionContratosPostpago(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarCreacionContratosPostpago] ";
        ResponseBean respAux= null;
        
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        CrearContratosRequest request= new CrearContratosRequest();
        request.setNombreAplicacion( activosBean.getNombreSistemaOrigen() );
        request.setIdTransaccion( activosBean.getIdTransaccion() );
        request.setListaContratos( activosBean.getData().getListaContratos() );
        request.setListaCamposAdicionales( activosBean.getData().getListaCamposAdicionales() );
        request.setTipoPostpago( activosBean.getTipoPostpago() );
        request.setUsuarioAplicacion( activosBean.getUsrAplicacion() );
        
        logger.info(mensajeTransaccion +
              "Se procede a crear contratos...");
        
        ResponseBeanCreacionContratos resp=  cmsService.crearVariosContratos(request, mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se crearon los contratos exitosamente. Datos devueltos:" + 
                        JAXBUtilitarios.anyObjectToXmlText( resp ));
            listaOperaciones.remove(0);
            
            if( listaOperaciones.isEmpty() ){
                logger.info(mensajeTransaccion + "Ya no existen mas operaciones, por tanto se termina el proceso.");
            } else{
                logger.info(mensajeTransaccion + "Se procede a settear los coId creados en la lista de CamposAdicionales para que este disponible para otra operacion...");
                ListaCamposAdicionalesType.CampoAdicional adicional= null;
                ListaCamposAdicionalesType campListType= activosBean.getData().getListaCamposAdicionales();
                
                if( campListType==null ){
                    campListType= new ListaCamposAdicionalesType();
                    activosBean.getData().setListaCamposAdicionales( campListType );
                }
                
                List <ListaCamposAdicionalesType.CampoAdicional> listaCampos= campListType.getCampoAdicional();
                
                for(Long cd:resp.getListaContratosCreados()){
                    adicional= new ListaCamposAdicionalesType.CampoAdicional();
                    adicional.setNombreCampo( PropertiesInternos.CAMPO_ADICIONAL_COID );
                    adicional.setValor( String.valueOf( cd ) );
                    listaCampos.add( adicional );
                }
                
                logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a la cola de la siguiente operacion=" + 
                          listaOperaciones.get(0));
                
                respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                            listaOperaciones.get(0), 
                                                              mensajeTransaccion);
            }
        } else if( PropertiesInternos.CODIGO_EXITO_INCOMPLETO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){ 
          
            ListaContratoType listaContratos= new ListaContratoType();
            listaContratos.getContrato().addAll( resp.getListaContratosFaltantes() );
            
            activosBean.getData().setListaContratos( listaContratos );
            
            logger.info(mensajeTransaccion + "Se procede a enviar el mensaje a esta misma cola, pero con los contratos faltantes");
            respAux= jmsMessageSender.sendObjectMessageByOperation(activosBean, 
                                                        listaOperaciones.get(0), 
                                                          mensajeTransaccion);
            jmsMessageSender.reportarError(resp, activosBean, "procesarCreacionContratosPostpago", mensajeTransaccion);
            
        } else if( PropertiesInternos.CODIGO_ERROR_LISTA_VACIA.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){ 
            logger.info(mensajeTransaccion + "La lista de contratos esta vacia, por tanto se continuara con la siguiente operacion...");
            
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
        } else if( PropertiesInternos.CODIGO_ERROR_CANCELACION.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){ 
            logger.error(mensajeTransaccion + "Hubo un error que invalida ejecucion. Por tanto se termina proceso.");
            
        } else {
            logger.error(mensajeTransaccion + "Hubo un error en la creacion de los contratos:" + 
                         resp.getMensajeRespuesta());
            jmsMessageSender.reportarError(resp, activosBean, "procesarCreacionContratosPostpago", mensajeTransaccion);
            
            throw new ClaroBaseException("Hubo un error en la creacion de los contratos:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public void procesarActualizacionContrato(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarActualizacionContrato] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        ActualizacionContratoType actType= null;
        
        try{
            actType= activosBean.getData().getListaContratos().getContrato().get(0).getActualizacionContrato();
        }catch(Exception e){
            logger.error(mensajeTransaccion +
                  "Error en los datos recibidos. NO Existe la estructura requerida:",e);
            return;
        }
        logger.info(mensajeTransaccion +
              "Se procede a actualizar contrato...");
        
        ResponseBeanCMS resp=  cmsService.actualizarContrato(actType, 
                                                             activosBean.getIdTransaccion(),
                                                             mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se actualizo contrato exitosamente.");
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
            jmsMessageSender.reportarError(resp, activosBean, "procesarActualizacionContrato", mensajeTransaccion);
            
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public void procesarAgregacionServiciosContrato(ActivosPostpagoBean activosBean,String mensajeTx) throws ClaroBaseException{
      
        String mensajeTransaccion = mensajeTx + "[procesarAgregacionServiciosContrato] ";
        ResponseBean respAux= null;
        List<String> listaOperaciones= activosBean.getListaOperaciones();
        
        AgregarServiciosContratoRequest request= new AgregarServiciosContratoRequest();
        request.setNombreAplicacion( activosBean.getNombreSistemaOrigen() );
        request.setIdTransaccion( activosBean.getIdTransaccion() );
        request.setListaCamposAdicionales( activosBean.getData().getListaCamposAdicionales() );
        request.setTipoPostpago( activosBean.getTipoPostpago() );
        request.setUsuarioAplicacion( activosBean.getUsrAplicacion() );
        
        ListaServiciosContratoType listaServs= null;
        try{
            listaServs= activosBean.getData().getListaContratos().getContrato().get(0).getListaServicios();
            request.setListaServicios( listaServs );
        }catch(Exception e){
            logger.error(mensajeTransaccion +
                  "Error en los datos recibidos. NO Existe la estructura requerida:",e);
            return;
        }
        logger.info(mensajeTransaccion +
              "Se procede a agregar servicios a contrato...");
        
        ResponseBeanCMS resp=  cmsService.agregarServiciosContrato(request, 
                                                             activosBean.getIdTransaccion(),
                                                             mensajeTransaccion);
        
        if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
          
            logger.info(mensajeTransaccion + "Se actualizo contrato exitosamente.");
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
            jmsMessageSender.reportarError(resp, activosBean, "procesarAgregacionServiciosContrato", mensajeTransaccion);
            
            throw new ClaroBaseException("Hubo un error en la actualizacion:" + 
                         resp.getMensajeRespuesta());
        }
    }
    
    public CrearClienteUnicoResponse crearCuentaCompleta(CrearClienteUnicoRequest request) {
        
        String mensajeTransaccion =
            "[crearCuentaCompleta idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        CrearClienteUnicoResponse response= new CrearClienteUnicoResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO crearCuentaCompleta]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a crear cuenta completa...");
            
            ResponseBeanCMS respCMS= cmsService.crearCuentaCompleta(request.getCliente().getCuenta(), 
                                           request.getCliente().getListaDirecciones(), 
                                           request.getCliente().getAcuerdoPago(), 
                                           request.getCliente().getInformacionCuenta(), 
                                           request.getIdTransaccion(), 
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                
                  logger.info(mensajeTransaccion + "Se creo cuenta completa exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDF0 );
                  response.setCustomerId( respCMS.getCustomerData().getCustomerId() );
                  response.setCustCode( respCMS.getCustomerData().getCustCode() );
                  logger.info("Se creo cuenta completa exitosamente con custCode="+ respCMS.getCustomerData().getCustCode());
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT1 + e.getLocalizedMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN crearCuentaCompleta] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;
    }
    
    public CrearContratoUnicoResponse crearContratoCompleto(CrearContratoUnicoRequest request) {
        
        String mensajeTransaccion =
            "[crearContratoCompleto idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        CrearContratoUnicoResponse response= new CrearContratoUnicoResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO crearContratoCompleto]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a crear contrato completo...");
            
            ResponseBeanCMS respCMS= cmsService.crearContratoCompleto(request.getContrato(), 
                                           request.getListaCamposAdicionales(),
                                           request.getTipoPostpago(),
                                           request.getIdTransaccion(),
                                           mensajeTransaccion,
                                            request.getNombreAplicacion());
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                
                  logger.info(mensajeTransaccion + "Se creo contrato exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDF0 );
                  response.setCoId( respCMS.getCoId() );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else if( PropertiesInternos.CODIGO_ERROR_BSCS_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){ 
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT4 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT4 + respCMS.getMensajeRespuesta() );
              } else if( PropertiesInternos.CODIGO_ERROR_BSCS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){ 
                  logger.error(mensajeTransaccion + "Hubo un error en la creacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT5 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT5 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN crearContratoCompleto] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;
    }
    
    public GenericResponse actualizarEstadoCuenta(ActualizarEstadoCuentaRequest request){
        String mensajeTransaccion =
            "[actualizarEstadoCuenta idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        GenericResponse response= new GenericResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO actualizarEstadoCuenta]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a actualizar el estado de cuenta...");
            
            ResponseBeanCMS respCMS= cmsService.actualizarEstadoCuenta(request, 
                                           request.getIdTransaccion(),
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                
                  logger.info(mensajeTransaccion + "Se actualizo cuenta exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDF0 );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN actualizarEstadoCuenta] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;        
    }
    
    public GenericResponse actualizarContrato(ActualizarContratoRequest request){
        String mensajeTransaccion =
            "[actualizarContrato idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        GenericResponse response= new GenericResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO actualizarContrato]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a actualizar el estado de contrato...");
            
            ResponseBeanCMS respCMS= cmsService.actualizarContrato(request.getActualizacionContrato(), 
                                           request.getIdTransaccion(),
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                
                  logger.info(mensajeTransaccion + "Se actualizo contrato exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDF0 );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la actualizacion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN actualizarContrato] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;        
    }
    
    public GenericResponse agregarServiciosContrato(AgregarServiciosContratoRequest request){
        String mensajeTransaccion =
            "[agregarServiciosContrato idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        GenericResponse response= new GenericResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO agregarServiciosContrato]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a agregar servicios a contrato...");
            
            ResponseBeanCMS respCMS= cmsService.agregarServiciosContrato(request, 
                                           request.getIdTransaccion(),
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                
                  logger.info(mensajeTransaccion + "Se realizo la transaccion exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDF0 );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN agregarServiciosContrato] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;        
    }

    public CambiarPlanCMSResponse cambiarPlanCMS(CambiarPlanCMSRequest request) {
        String mensajeTransaccion =
            "[cambiarPlanCMS idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        CambiarPlanCMSResponse response= new CambiarPlanCMSResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO cambiarPlanCMS]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a agregar servicios a contrato...");
            
            ResponseBeanCambiarPlanCMS respCMS= cmsService.cambiarPlanCMS(request, 
                                           request.getIdTransaccion(),
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  
                  response.setServiciosEliminados(new ListaServicioType());
                  for(Servicio row : respCMS.getServiciosEliminados()){
                      ServicioType a = new ServicioType();
                      a.setSpcode(row.getSpCode());
                      a.setSncode(row.getSnCode());
                      response.getServiciosEliminados().getServicio().add(a);
                  }                  
                  response.setServiciosAgregados(new ListaServicioType());
                  for(Servicio row : respCMS.getServiciosAgregados()){
                      ServicioType a = new ServicioType();
                      a.setSpcode(row.getSpCode());
                      a.setSncode(row.getSnCode());
                      response.getServiciosAgregados().getServicio().add(a);
                  }
                  
                  logger.info(mensajeTransaccion + "Se realizo la transaccion exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDF0 );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN cambiarPlanCMS] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;
    }

    public TransferirContratoResponse transferirContrato(TransferirContratoRequest request) {
        String mensajeTransaccion =
            "[transferirContrato idTx=" + request.getIdTransaccion() + "] ";
        long t1 = System.currentTimeMillis();
        TransferirContratoResponse response= new TransferirContratoResponse();
        
        try {
            response.setIdTransaccion( request.getIdTransaccion() );
            logger.info(mensajeTransaccion +
                        "[INICIO transferirContrato]");
            logger.info(mensajeTransaccion + "Datos de entrada:" +
                        JAXBUtilitarios.anyObjectToXmlText(request));
            
            logger.info(mensajeTransaccion + "Se procede a agregar servicios a contrato...");
            
            ResponseBeanTransferirContrato respCMS= cmsService.transferirContrato(request, 
                                           request.getIdTransaccion(),
                                           mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  
                  response.setNewCoId(respCMS.getCoId());
                  
                  logger.info(mensajeTransaccion + "Se realizo la transaccion exitosamente.");
                  response.setCodigoRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDF0 );
                  response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDF0 );
                  
              } else if( PropertiesInternos.CODIGO_ERROR_COMMIT_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDF1 );
                  response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDF1 );
              } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT2 );
                  response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT2 );
              } else if( PropertiesInternos.CODIGO_ERROR_CMS.equalsIgnoreCase( respCMS.getCodigoRespuesta() ) ){
                  logger.error(mensajeTransaccion + "Hubo un error en la transaccion:" + 
                               respCMS.getMensajeRespuesta());
                  response.setCodigoRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT3 );
                  response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT3 + respCMS.getMensajeRespuesta() );
              } else {
                  response.setCodigoRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT1 );
                  response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT1 + respCMS.getMensajeRespuesta() );
              }
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Hubo un error en el proceso:",
                         e);
            response.setCodigoRespuesta( PropertiesExternos.CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT1 );
            response.setMensajeRespuesta( PropertiesExternos.TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT1 + e.getMessage() );
        } finally {
            logger.info( mensajeTransaccion + "Response a retornar:" + 
                       JAXBUtilitarios.jaxBToXmlText(response) );
            logger.info(mensajeTransaccion +
                        "[FIN transferirContrato] Tiempo total de proceso(ms):" +
                        (System.currentTimeMillis() - t1));
        } 
        
        return response;
    }
}
