package pe.com.claro.eba.activospostpagoejbs.service;

import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;

import org.springframework.beans.BeansException;
import org.springframework.beans.factory.BeanFactory;
import org.springframework.beans.factory.BeanFactoryAware;
import org.springframework.beans.factory.annotation.Autowired;

import pe.com.claro.eba.activospostpagoejbs.beans.CustomerData;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCambiarPlanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCore;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionCliente;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCreacionContratos;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanServicios;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTransferirContrato;
import pe.com.claro.eba.activospostpagoejbs.beans.ServiciosPlanBaseBean;
import pe.com.claro.eba.activospostpagoejbs.dao.BscsDAO;
import pe.com.claro.eba.activospostpagoejbs.ejbclients.CmsAdapterClient;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanCMSRequest;
import pe.com.claro.eba.activospostpagoejbs.types.ServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoRequest;
import pe.com.claro.eba.activospostpagoejbs.util.ActivosFlowException;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.eba.activospostpagoejbs.util.UtilitariosActivosPostpago;
import pe.com.claro.ebs.services.activospostpago.beans.ErrorType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AcuerdoPagoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.AgregarServiciosContratoRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearClienteRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CrearContratosRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.CuentaMiembroType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DireccionType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DispositivoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionCuentaType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCamposAdicionalesType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaDireccionType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaDispositivosType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaNumerosDirectorioType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ListaServiciosContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.NumeroDirectorioType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;


public class CmsServiceImpl implements CmsService, BeanFactoryAware{
    
    private static Logger logger= Logger.getLogger(CmsServiceImpl.class);
  
    private BeanFactory beanFactory;
    
    @Autowired
    private BscsDAO bscsDAO;
    
    
    public ResponseBeanCMS crearCuentaCompleta(CuentaMiembroType cuenta,
                                               ListaDireccionType listDirecciones,
                                               AcuerdoPagoType acuerdoPago,
                                               InformacionCuentaType infoCuenta,
                                               String idTransaccion,
                                               String mensajeTx){
        
        String mensajeTransaccion = mensajeTx + "[crearCuentaCompleta] ";
        ResponseBeanCMS resp= new ResponseBeanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBeanCMS respAux= null;
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
          
        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
          
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a crear cuenta Large...");
              
                  ResponseBeanCMS respCuenta= cmsAdapterClient.agregarMiembroLarge(cuenta, mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respCuenta.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se creo cuenta con customerId=" + respCuenta.getCustomerData().getCustomerId());
                      logger.info(mensajeTransaccion + "Se creo cuenta con custCode="+ respCuenta.getCustomerData().getCustCode());
                      
                      if( listDirecciones!=null && listDirecciones.getDireccion()!=null &&
                          ! listDirecciones.getDireccion().isEmpty()){
                            
                          logger.info(mensajeTransaccion + "Se procede a asociar direcciones...");
                          List<DireccionType> listaDireccs= listDirecciones.getDireccion();
                          DireccionType d= null;
                          
                          for(int i=0;i<listaDireccs.size();i++){
                              d= listaDireccs.get(i);
                              d.setCustomerId( respCuenta.getCustomerData().getCustomerId() );
                              logger.info(mensajeTransaccion + "[" + (i+1) + "] Asociando Direccion de " +  
                                          d.getDescripcionDireccion() + " ...");
                              respAux= cmsAdapterClient.escribirDireccionCMS(d, mensajeTransaccion + "[" + (i+1) + "]");
                              
                              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                                
                                  logger.info(mensajeTransaccion + "[" + (i+1) + "] Se asocio direccion.");
                              } else {
                                  logger.error(mensajeTransaccion + "[" + (i+1) + "] Hubo un error asociando direccion:" + 
                                               respAux.getMensajeRespuesta());
                                  resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                                  resp.setMensajeRespuesta( respAux.getMensajeRespuesta() );
                                  throw new ActivosFlowException( new ErrorType("CrearDirecciones","Hubo un error asociando direccion de " +
                                                                                d.getDescripcionDireccion() + ". " +
                                                                                respAux.getMensajeRespuesta()));
                              }
                          }
                          logger.info(mensajeTransaccion + "Direcciones asociadas correctamente.");
                      } else {
                          logger.info(mensajeTransaccion + "No hay direcciones que asociar.");
                      }
                      
                      if( acuerdoPago!= null ){
                          logger.info(mensajeTransaccion + "Se procede a actualizar forma de pago...");
                          acuerdoPago.setCustomerId( respCuenta.getCustomerData().getCustomerId() );
                          respAux= cmsAdapterClient.actualizarFormaPagoCuenta (acuerdoPago, mensajeTransaccion);
                          
                          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                            
                              logger.info(mensajeTransaccion + "Se actualizo forma de pago.");
                          } else {
                              logger.error(mensajeTransaccion + "Hubo un error actualizando:" + 
                                           respAux.getMensajeRespuesta());
                              resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                              resp.setMensajeRespuesta( respAux.getMensajeRespuesta() );
                              throw new ActivosFlowException( new ErrorType("ActualizarFormaPagoCuenta","Hubo un error actualizando forma de pago:" +
                                                                            respAux.getMensajeRespuesta()));
                          }
                      } else {
                          logger.info(mensajeTransaccion + "No hay datos para actualizar forma de pago.");
                      }
                      
                      if( infoCuenta!=null ){
                          logger.info(mensajeTransaccion + "Se procede a actualizar informacion adicional de cliente...");
                          infoCuenta.setCustomerId( respCuenta.getCustomerData().getCustomerId() );
                          respAux= cmsAdapterClient.escribirInformacionCuenta (infoCuenta, mensajeTransaccion);
                          
                          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                            
                              logger.info(mensajeTransaccion + "Se actualizo informacion adicional de cliente.");
                          } else {
                              logger.error(mensajeTransaccion + "Hubo un error actualizando:" + 
                                           respAux.getMensajeRespuesta());
                              resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                              resp.setMensajeRespuesta( respAux.getMensajeRespuesta() );
                              throw new ActivosFlowException( new ErrorType("EscribirInformacionCuenta","Hubo un error actualizando informacion adicional de cliente:" +
                                                                            respAux.getMensajeRespuesta()));
                          }
                      } else {
                          logger.info(mensajeTransaccion + "No hay datos para actualizar informacion adicional de la cuenta.");
                      }
                      
                      logger.info(mensajeTransaccion + "Se creo cuenta exitosamente. Se procede a commitear transaccion CMS...");
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                          resp.setCustomerData( respCuenta.getCustomerData() );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error creando cuenta:" +  respCuenta.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respCuenta.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respCuenta.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("CreacionCuentaLarge","Hubo un error tratando de crear cuenta en CMS:" +
                                                                  respCuenta.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error iniciando transaccion:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            resp.setErrorData( e.getErrorData() );
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
    
    public ResponseBeanCreacionCliente crearClienteVariasCuentas(CrearClienteRequest request,String mensajeTx){
      
        String mensajeTransaccion = mensajeTx +"[crearClienteVariasCuentas] ";
        ResponseBeanCreacionCliente resp= new ResponseBeanCreacionCliente();
        ResponseBeanCMS respAux= null;
        
        try{
            if( request.getCliente()!=null && request.getCliente().getListaCuentas()!=null &&
                ! request.getCliente().getListaCuentas().getCuenta().isEmpty()){
                  
                List<CuentaMiembroType> listaCuentas= request.getCliente().getListaCuentas().getCuenta();
                CuentaMiembroType cAux= null;
                long customerIdPadre= 0;
                List<CuentaMiembroType> listaCuentasFaltantes= new ArrayList<CuentaMiembroType>( listaCuentas );
                
                resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                resp.setListClientesCreados( new ArrayList<CustomerData>() );
                
                logger.info(mensajeTransaccion + "Se procede a crear lista de cuentas. Nro de cuentas:" + 
                            listaCuentas.size());
                
                for(int i=0;i<listaCuentas.size();i++){
                    cAux= listaCuentas.get(i);
                    logger.info(mensajeTransaccion + "Total Nro de cuentas:" + listaCuentas.size() + 
                                "; NroCuentasFaltantes:" + listaCuentasFaltantes.size());
                    logger.info(mensajeTransaccion + "Se procede a crear cuenta[" + (i+1) + "]...");
                    
                    if( cAux.getIdCuentaPadre()==null && customerIdPadre!=0){
                        cAux.setIdCuentaPadre( customerIdPadre );
                        listaCuentasFaltantes.get(0).setIdCuentaPadre( customerIdPadre );
                    }
                    
                    respAux= crearCuentaCompleta(cAux, 
                                                request.getCliente().getListaDirecciones(),
                                                request.getCliente().getAcuerdoPago(),
                                                request.getCliente().getInformacionCuenta(),
                                                request.getIdTransaccion(),
                                                mensajeTransaccion + "[" + (i+1) + "]");
                    
                    if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                      
                        logger.info(mensajeTransaccion + "[" + (i+1) + "] Se creo cuenta completa exitosamente.");
                        listaCuentasFaltantes.remove( 0 );
                        customerIdPadre= respAux.getCustomerData().getCustomerId();
                        resp.getListClientesCreados().add( respAux.getCustomerData() );
                    } else {
                        logger.error(mensajeTransaccion + "[" + (i+1) + "] Hubo un error en la creacion:" + 
                                     respAux.getMensajeRespuesta());
                        resp.setErrorData( respAux.getErrorData() );
                        if( listaCuentas.size()==listaCuentasFaltantes.size() ){
                            resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                            resp.setMensajeRespuesta( respAux.getMensajeRespuesta() );
                            
                        } else{
                            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_EXITO_INCOMPLETO);
                            resp.setMensajeRespuesta("Se crearon " + (listaCuentas.size()-listaCuentasFaltantes.size())
                                                     +" cuentas, pero ocurrio un error creando las sgtes:" + respAux.getMensajeRespuesta());
                            resp.setListCuentasFaltantes( listaCuentasFaltantes );
                        }
                        break;
                    }
                }
                if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
                  logger.info(mensajeTransaccion + "Cuentas creadas correctamente.");
                }
          
          } else {
              logger.error(mensajeTransaccion + "La lista de cuentas es nula o vacia.");
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_LISTA_VACIA);
              resp.setMensajeRespuesta("La lista de cuentas es nula o vacia.");
          }
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
        } 
        
        return resp;
    }
    
    public ResponseBeanCMS crearContratoCompleto(ContratoType contrato,
                                                 ListaCamposAdicionalesType listaCamposAdicionales,
                                                 String tipoPostpago,
                                               String idTransaccion,
                                               String mensajeTx,
                                                 String nombreAplicacion){
        String mensajeTransaccion = mensajeTx + "[crearContratoCompleto] ";
        ResponseBeanCMS resp= new ResponseBeanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBeanCMS respAux= null;
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
      
        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion + "1.- Iniciar transacción CMS");
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a crear contrato...");
                  logger.info(mensajeTransaccion + "2.-  Crear contrato en CMS");
                  ResponseBeanCMS respContrato= cmsAdapterClient.crearNuevoContrato(contrato, mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respContrato.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se creo contrato con coId=" + respContrato.getCoId());

                      if( aplicaAgregarRecursos(tipoPostpago)){
                          logger.info(mensajeTransaccion + "3.- Agregar recursos en CMS");
                          logger.info(mensajeTransaccion + "Para el tipo de Postpago=" + tipoPostpago + 
                                      ", aplica agregar recursos o dispositivos.");
                          ListaDispositivosType listaDispos= contrato.getListaDispositivos();
                          
                          if( listaDispos!=null && listaDispos.getDispositivo()!=null && 
                              !listaDispos.getDispositivo().isEmpty()){
                              DispositivoType d= null;
                              
                              for(int i=0;i<listaDispos.getDispositivo().size();i++){
                                  d= listaDispos.getDispositivo().get(i);
                                  d.setCoId( respContrato.getCoId() );
                                  logger.info(mensajeTransaccion + "[" + (i+1) + "] Asociando dispositivo...");
                                  respAux= cmsAdapterClient.agregarDispositivo(d, mensajeTransaccion + "[" + (i+1) + "]");
                                  
                                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                                    
                                      logger.info(mensajeTransaccion + "[" + (i+1) + "] Se asocio dispositivo.");
                                  } else {
                                      logger.error(mensajeTransaccion + "[" + (i+1) + "] Hubo un error asociando dispositivo:" + 
                                                   respAux.getMensajeRespuesta());
                                      resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                                      resp.setMensajeRespuesta( respAux.getMensajeRespuesta()  );
                                      throw new ActivosFlowException( new ErrorType("AgregarDispositivo","Hubo un error asociando dispositivo" +
                                                                                    respAux.getMensajeRespuesta()));
                                  }
                              }
                              logger.info(mensajeTransaccion + "Dispositivos asociados correctamente.");
                          } else{
                              logger.info(mensajeTransaccion + "No existen recursos o dispositivos que agregar.");
                          }
                      } else {
                          logger.info(mensajeTransaccion + "Para el tipo de Postpago=" + tipoPostpago + 
                                      ", NO aplica agregar recursos o dispositivos.");
                      }
                      
                      if( contrato.getListaServicios()!=null && contrato.getListaServicios().getServicio()!=null
                          && !contrato.getListaServicios().getServicio().isEmpty()){  
                      
                          List<ServicioContratoType> listServicios= new ArrayList<ServicioContratoType>();
                          
                          if( aplicaValidacionServicios(tipoPostpago) ){
                              logger.info(mensajeTransaccion + "4.- Validar servicios adicionales");
                              logger.info(mensajeTransaccion + "Para el tipo de Postpago=" + tipoPostpago + 
                                          ", aplica validacion de servicios.");
                          
                              ResponseBeanServicios respVal= bscsDAO.validarServiciosAdicionales(contrato.getListaServicios().getServicio(), 
                                                                                        contrato.getPlanTarifario(), mensajeTransaccion);
                              
                              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respVal.getCodigoRespuesta() ) ){
                                
                                  logger.info(mensajeTransaccion + "Los servicios son validos.");
                                  listServicios.addAll(respVal.getListaServicios());
                              } else {
                                  logger.error(mensajeTransaccion + "Hubo un error en la validacion de los servicios:" + 
                                               respVal.getMensajeRespuesta());
                                  
                                  if( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO.equalsIgnoreCase( respVal.getCodigoRespuesta() ) ){
                                      resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_BSCS_CAIDO );
                                  } else if( PropertiesInternos.CODIGO_ESTANDAR_ERROR.equalsIgnoreCase( respVal.getCodigoRespuesta() ) ){
                                      resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR );
                                  } else {
                                      resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_BSCS );
                                  } 
                                  resp.setMensajeRespuesta( "Error ejecutando validacion de servicios:" + respVal.getMensajeRespuesta()  );
                                  throw new ActivosFlowException( new ErrorType("ValidacionServiciosAdicionales","Hubo un error en la validacion de los servicios:" +
                                                                                respVal.getMensajeRespuesta()));
                              }
                          } else {
                              logger.info(mensajeTransaccion + "Para el tipo de Postpago=" + tipoPostpago + 
                                        ", NO aplica validacion de servicios.");
                          }
                          listServicios.addAll( contrato.getListaServicios().getServicio() );
                          
                          for(ServicioContratoType sc:listServicios){
                              sc.setCoId( respContrato.getCoId() );
                          }
                          
                          if(aplicaEnviarDnIdConServicio(tipoPostpago)){

                              logger.info(mensajeTransaccion + "5.- Obtener datos adicionales");
                              if( listaCamposAdicionales!=null && listaCamposAdicionales.getCampoAdicional()!=null &&
                                  ! listaCamposAdicionales.getCampoAdicional().isEmpty()){

                                  long sncode= PropertiesExternos.obtenerSnCodeParaDnIdPorTipoPostpago(tipoPostpago, 
                                                                                                         mensajeTransaccion);
                                  String idNumeroTelefono= UtilitariosActivosPostpago.obtenerPrimerValor(listaCamposAdicionales.getCampoAdicional(), 
                                                                                                      PropertiesInternos.CAMPO_ADICIONAL_ID_NUMERO_TELEFONO) ;
                                  String lineaPrincipal= UtilitariosActivosPostpago.obtenerPrimerValor(listaCamposAdicionales.getCampoAdicional(), 
                                                                                                      PropertiesInternos.CAMPO_ADICIONAL_LINEA_PRINCIPAL);
                                  
                                  if( idNumeroTelefono!=null && lineaPrincipal!=null ){
                                  
                                      logger.info(mensajeTransaccion + "De acuerdo al tipo de Postpago=" + tipoPostpago + 
                                              ", aplica enviar dn_id en el servicio con sncode=" + sncode);
                                      for(ServicioContratoType sc:listServicios){
                                          if( sncode == sc.getSnCode() ){
                                              ListaNumerosDirectorioType listaNums= new ListaNumerosDirectorioType();
                                              NumeroDirectorioType num= new NumeroDirectorioType();
                                              num.setIdNumeroTelefono( Long.parseLong( idNumeroTelefono ));
                                              num.setLineaPrincipal( Boolean.parseBoolean( lineaPrincipal ));
                                              
                                              listaNums.getNumeroDirectorio().add( num );
                                              sc.setListaNumerosDirectorio( listaNums );
                                                  
                                              break;
                                          }
                                      }
                                  } else {
                                    logger.error(mensajeTransaccion + "No se ha recibido el idNumeroTelefono o lineaPrincipal."); 
                                    resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_CANCELACION );
                                    resp.setMensajeRespuesta( "No se ha recibido el idNumeroTelefono o lineaPrincipal."  );
                                    throw new ActivosFlowException( new ErrorType("CreacionContrato","No se ha recibido el idNumeroTelefono o lineaPrincipal."));
                                  }
                              } else {
                                  logger.error(mensajeTransaccion + "No se ha recibido el idNumeroTelefono."); 
                                  resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_CANCELACION );
                                  resp.setMensajeRespuesta( "No se ha recibido el idNumeroTelefono."  );
                                  throw new ActivosFlowException( new ErrorType("CreacionContrato","No se ha recibido el idNumeroTelefono."));
                              }
                          }
                          
                          logger.info(mensajeTransaccion + "Se procede a agregar los servicios al contrato...");
                          logger.info(mensajeTransaccion + "6.- Agregar servicios adicionales");
                          respAux= cmsAdapterClient.agregarServiciosContratoCMS(listServicios, mensajeTransaccion);
                          
                          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                            
                              logger.info(mensajeTransaccion + "Se agregaron los servicios exitosamente.");
                          } else {
                              logger.error(mensajeTransaccion + "Hubo un error agregando los servicios:" + 
                                           respAux.getMensajeRespuesta());
                              resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                              resp.setMensajeRespuesta( respAux.getMensajeRespuesta()  );
                              throw new ActivosFlowException( new ErrorType("AgregarServiciosContrato","Hubo un error agregando los servicios:" +
                                                                            respAux.getMensajeRespuesta()));
                          }
                      } else{
                          logger.info(mensajeTransaccion + "No existen servicios que agregar.");
                      }
                      
                      //Agregar los serviciosCORE
                      if(nombreAplicacion.equals(PropertiesExternos.SIACPOST_NOMBRE) && contrato.getListaServicios().getServicio().isEmpty() ){
                          logger.info(mensajeTransaccion + "7.- Buscar Servicios Core");
                          ResponseBeanCore respVal= bscsDAO.buscarServCoreXPlan(mensajeTransaccion, (int)contrato.getPlanTarifario());
                          List<ServiciosPlanBaseBean> listaPlan = (List<ServiciosPlanBaseBean>)respVal.getObjetoRespuesta();
                          List<ServicioContratoType> listServiciosTemp= new ArrayList<ServicioContratoType>();
                          
                          logger.info(mensajeTransaccion + "Para el tipo de Postpago=" + tipoPostpago + 
                                          ", aplica validacion de servicios CORE.");
                          logger.info(mensajeTransaccion + "Cantidad de Servicios Core: " + listaPlan.size());
                          
                          for(int k=0; k<listaPlan.size(); k++){                           
                            ServicioContratoType servicioContrato = new ServicioContratoType(); 
                            servicioContrato.setSnCode(Long.parseLong(listaPlan.get(k).getSnCode()));
                            servicioContrato.setSpCode(Long.parseLong(listaPlan.get(k).getSpCode()));
                            servicioContrato.setProfileId(PropertiesExternos.CREAR_CONTRATO_PROFILE_ID);
                            listServiciosTemp.add(servicioContrato);
                          }
                          
                          for(ServicioContratoType sc:listServiciosTemp){
                              sc.setCoId( respContrato.getCoId() );
                          }
                          
                          if(aplicaEnviarDnIdConServicio(tipoPostpago)){
                              logger.info(mensajeTransaccion + "8.- Obtener datos adicionales");
                              if( listaCamposAdicionales!=null && listaCamposAdicionales.getCampoAdicional()!=null &&
                                  ! listaCamposAdicionales.getCampoAdicional().isEmpty()){
  
                                  long sncode= PropertiesExternos.obtenerSnCodeParaDnIdPorTipoPostpago(tipoPostpago, 
                                                                                                         mensajeTransaccion);
                                  String idNumeroTelefono= UtilitariosActivosPostpago.obtenerPrimerValor(listaCamposAdicionales.getCampoAdicional(), 
                                                                                                      PropertiesInternos.CAMPO_ADICIONAL_ID_NUMERO_TELEFONO) ;
                                  String lineaPrincipal= UtilitariosActivosPostpago.obtenerPrimerValor(listaCamposAdicionales.getCampoAdicional(), 
                                                                                                      PropertiesInternos.CAMPO_ADICIONAL_LINEA_PRINCIPAL);
                                  
                                  if( idNumeroTelefono!=null && lineaPrincipal!=null ){
                                  
                                      logger.info(mensajeTransaccion + "De acuerdo al tipo de Postpago=" + tipoPostpago + 
                                              ", aplica enviar dn_id en el servicio con sncode=" + sncode);
                                      for(ServicioContratoType sc:listServiciosTemp){
                                          if( sncode == sc.getSnCode() ){
                                              ListaNumerosDirectorioType listaNums= new ListaNumerosDirectorioType();
                                              NumeroDirectorioType num= new NumeroDirectorioType();
                                              num.setIdNumeroTelefono( Long.parseLong( idNumeroTelefono ));
                                              num.setLineaPrincipal( Boolean.parseBoolean( lineaPrincipal ));
                                              
                                              listaNums.getNumeroDirectorio().add( num );
                                              sc.setListaNumerosDirectorio( listaNums );
                                              break;
                                          }
                                      }
                                  } else {
                                    logger.error(mensajeTransaccion + "No se ha recibido el idNumeroTelefono o lineaPrincipal."); 
                                    resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_CANCELACION );
                                    resp.setMensajeRespuesta( "No se ha recibido el idNumeroTelefono o lineaPrincipal."  );
                                    throw new ActivosFlowException( new ErrorType("CreacionContrato","No se ha recibido el idNumeroTelefono o lineaPrincipal."));
                                  }
                              } else {
                                  logger.error(mensajeTransaccion + "No se ha recibido el idNumeroTelefono."); 
                                  resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ERROR_CANCELACION );
                                  resp.setMensajeRespuesta( "No se ha recibido el idNumeroTelefono."  );
                                  throw new ActivosFlowException( new ErrorType("CreacionContrato","No se ha recibido el idNumeroTelefono."));
                              }
                          }

                          logger.info(mensajeTransaccion + "Se procede a agregar los servicios core al contrato...");
                          logger.info(mensajeTransaccion + "9.- Agregar servicios Core");
                          respAux= cmsAdapterClient.agregarServiciosContratoCMS(listServiciosTemp, mensajeTransaccion);
                          
                          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                            
                              logger.info(mensajeTransaccion + "Se agregaron los servicios core exitosamente.");
                          } else {
                              logger.error(mensajeTransaccion + "Hubo un error agregando los servicios core:" + 
                                           respAux.getMensajeRespuesta());
                              resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                              resp.setMensajeRespuesta( respAux.getMensajeRespuesta()  );
                              throw new ActivosFlowException( new ErrorType("AgregarServiciosContrato","Hubo un error agregando los servicios core:" +
                                                                            respAux.getMensajeRespuesta()));
                          }
                      } 
                      
                      //Fin Agregar los serviciosCORE
                      
                      logger.info(mensajeTransaccion + "Se procede a actualizar estado de contrato...");
                      logger.info(mensajeTransaccion + "10.- Actualizar estado de contrato");
                      contrato.getActualizacionContrato().setCoId( respContrato.getCoId() );
                      respAux= cmsAdapterClient.escribirContrato (contrato.getActualizacionContrato(), mensajeTransaccion);//10.-
                      
                      if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                        
                          logger.info(mensajeTransaccion + "Se actualizo el estado del contrato exitosamente.");
                      } else {
                          logger.error(mensajeTransaccion + "Hubo un error actualizando:" + 
                                       respAux.getMensajeRespuesta());
                          resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                          resp.setMensajeRespuesta( respAux.getMensajeRespuesta()  );
                          throw new ActivosFlowException( new ErrorType("ActualizacionEstadoContrato",
                                                                        respAux.getMensajeRespuesta()));
                      }
                    
                      
                      if( contrato.getInformacionContrato()!=null && contrato.getInformacionContrato().getListaCampos()!=null ){
                          logger.info(mensajeTransaccion + "Se procede a actualizar informacion adicional del contrato...");
                          logger.info(mensajeTransaccion + "11.- Actualizar informacion de contrato");
                          contrato.getInformacionContrato().setCoId( respContrato.getCoId() );
                          respAux= cmsAdapterClient.grabarInfoContrato (contrato.getInformacionContrato(), mensajeTransaccion);
                          
                          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                            
                              logger.info(mensajeTransaccion + "Se actualizo informacion adicional del contrato.");
                          } else {
                              logger.error(mensajeTransaccion + "Hubo un error actualizando:" + 
                                           respAux.getMensajeRespuesta());
                              resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                              resp.setMensajeRespuesta( respAux.getMensajeRespuesta()  );
                              throw new ActivosFlowException( new ErrorType("ActualizaInfContrato","Hubo un error actualizando informacion adicional de contrato:" +
                                                                            respAux.getMensajeRespuesta()));
                          }
                      } else {
                          logger.info(mensajeTransaccion + "No hay datos para actualizar informacion adicional del contrato.");
                      }
                      
                      logger.info(mensajeTransaccion + "Se creo contrato exitosamente. Se procede a commitear transaccion CMS...");
                      logger.info(mensajeTransaccion + "12.- Commitear transacción CMS");
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                          resp.setCoId( respContrato.getCoId() );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          logger.info(mensajeTransaccion + "13.- Realizar rollback de transacción CMS");                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error creando contrato:" +  respContrato.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respContrato.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respContrato.getMensajeRespuesta()  );
                      throw new ActivosFlowException( new ErrorType("CreacionContrato","Hubo un error tratando de crear un contrato en CMS:" +
                                                                  respContrato.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            logger.info(mensajeTransaccion + "13.- Realizar rollback de transacción CMS");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            resp.setErrorData( e.getErrorData() );
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              logger.info(mensajeTransaccion + "13.- Realizar rollback de transacción CMS");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
    
    public ResponseBeanCreacionContratos crearVariosContratos(CrearContratosRequest request,String mensajeTx){
      
        String mensajeTransaccion = mensajeTx +"[crearVariosContratos] ";
        ResponseBeanCreacionContratos resp= new ResponseBeanCreacionContratos();
        ResponseBeanCMS respAux= null;
        try{
            if( request.getListaContratos()!=null && request.getListaContratos().getContrato()!=null &&
                ! request.getListaContratos().getContrato().isEmpty()){
                  
                List<ContratoType> listaContratos= request.getListaContratos().getContrato();
                ContratoType cAux= null;
                List<ContratoType> listaContratosFaltantes= new ArrayList<ContratoType>( listaContratos );
                
                resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                resp.setListaContratosCreados( new ArrayList<Long>() );
                
                logger.info(mensajeTransaccion + "Se procede a crear lista de contratos. Nro de contratos:" + 
                            listaContratos.size());
                
                for(int i=0;i<listaContratos.size();i++){
                    cAux= listaContratos.get(i);
                    logger.info(mensajeTransaccion + "Total Nro de contratos:" + listaContratos.size() + 
                                "; NroContratosFaltantes:" + listaContratosFaltantes.size());
                    logger.info(mensajeTransaccion + "Se procede a crear contrato[" + (i+1) + "]...");
                    
                    respAux= crearContratoCompleto(cAux, 
                                                request.getListaCamposAdicionales(),
                                                request.getTipoPostpago(),
                                                request.getIdTransaccion(),
                                                mensajeTransaccion + "[" + (i+1) + "]",
                                                   request.getNombreAplicacion()); 
                    
                    if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAux.getCodigoRespuesta() ) ){
                      
                        logger.info(mensajeTransaccion + "[" + (i+1) + "] Se creo contrato completo exitosamente.");
                        listaContratosFaltantes.remove( 0 );
                        resp.getListaContratosCreados().add( respAux.getCoId() );
                    } else {
                        logger.error(mensajeTransaccion + "[" + (i+1) + "] Hubo un error en la creacion:" + 
                                     respAux.getMensajeRespuesta());
                        resp.setErrorData( respAux.getErrorData() );
                        if( listaContratos.size()==listaContratosFaltantes.size() ){
                            resp.setCodigoRespuesta( respAux.getCodigoRespuesta() );
                            resp.setMensajeRespuesta( respAux.getMensajeRespuesta() );
                            
                        } else{
                            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_EXITO_INCOMPLETO);
                            resp.setMensajeRespuesta("Se crearon " + (listaContratos.size()-listaContratosFaltantes.size())
                                                     +" contratos, pero ocurrio un error creando los sgtes:" + respAux.getMensajeRespuesta());
                            resp.setListaContratosFaltantes( listaContratosFaltantes );
                        }
                        break;
                    }
                }
                if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( resp.getCodigoRespuesta() ) ){
                  logger.info(mensajeTransaccion + "Contratos creados correctamente.");
                }
          
          } else {
              logger.error(mensajeTransaccion + "La lista de contratos es nula o vacia.");
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_LISTA_VACIA);
              resp.setMensajeRespuesta("La lista de contratos es nula o vacia.");
          }
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
        } 
        
        return resp;
    }
    
    public ResponseBeanCMS actualizarEstadoCuenta(ActualizarEstadoCuentaRequest request,
                                               String idTransaccion,
                                               String mensajeTx){
        
        String mensajeTransaccion = mensajeTx + "[actualizarEstadoCuenta] ";
        ResponseBeanCMS resp= new ResponseBeanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;

        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
          
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a actualizar estado de cuenta CMS...");
              
                  ResponseBeanCMS respActualiza= cmsAdapterClient.escribirCuentaCMS(request, mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respActualiza.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se actualizo exitosamente. Se procede a commitear transaccion CMS...");
                      
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error actualizando:" +  respActualiza.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respActualiza.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respActualiza.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("ActualizaEstadoCuenta","Error tratando de actualizar la cuenta en CMS:" +
                                                                  respActualiza.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
    
    public ResponseBeanCMS actualizarContrato(ActualizacionContratoType request,
                                               String idTransaccion,
                                               String mensajeTx){
        
        String mensajeTransaccion = mensajeTx + "[actualizarContrato] ";
        ResponseBeanCMS resp= new ResponseBeanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
      
        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
          
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a actualizar estado de contrato CMS...");
              
                  ResponseBeanCMS respActualiza= cmsAdapterClient.escribirContrato(request, mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respActualiza.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se actualizo exitosamente. Se procede a commitear transaccion CMS...");
                      
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error actualizando:" +  respActualiza.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respActualiza.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respActualiza.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("ActualizaEstadoContrato","Error tratando de actualizar el contrato en CMS:" +
                                                                  respActualiza.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
    
    public ResponseBeanCMS agregarServiciosContrato(AgregarServiciosContratoRequest request,
                                               String idTransaccion,
                                               String mensajeTx){
        
        String mensajeTransaccion = mensajeTx + "[agregarServiciosContrato] ";
        ResponseBeanCMS resp= new ResponseBeanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
        
        try{
            List<ServicioContratoType> listaServicios= request.getListaServicios().getServicio();
            
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
          
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a agregar servicios al contrato CMS...");
              
                  ResponseBeanCMS respActualiza= cmsAdapterClient.agregarServiciosContratoCMS(listaServicios, mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respActualiza.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se actualizo exitosamente. Se procede a commitear transaccion CMS...");
                      
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error en transaccion:" +  respActualiza.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respActualiza.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respActualiza.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("AgregarServiciosContrato","Error tratando de agregar servicios al contrato en CMS:" +
                                                                  respActualiza.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
        
    private boolean aplicaAgregarRecursos(String tipoPostpago){
      
        for(String s:PropertiesExternos.CREAR_CONTRATO_AGREGAR_RECURSOS_LISTA_TIPOS_POSTPAGO){
            if( s.equalsIgnoreCase( tipoPostpago ) ){
                return true;
            }
        }
        return false;
    }
    
    private boolean aplicaValidacionServicios(String tipoPostpago){
      
        for(String s:PropertiesExternos.CREAR_CONTRATO_VALIDAR_SERVICIOS_LISTA_TIPOS_POSTPAGO){
            if( s.equalsIgnoreCase( tipoPostpago ) ){
                return true;
            }
        }
        return false;
    }
    
    private boolean aplicaEnviarDnIdConServicio(String tipoPostpago){
      
        for(String s:PropertiesExternos.CREAR_CONTRATO_APLICA_DNIDS_LISTA_TIPOS_POSTPAGO){
            if( s.equalsIgnoreCase( tipoPostpago ) ){
                return true;
            }
        }
        return false;
    }
    
    public void setBeanFactory(BeanFactory beanFactory) 
            throws BeansException    {
        this.beanFactory = beanFactory;     
    }

    public ResponseBeanCambiarPlanCMS cambiarPlanCMS(CambiarPlanCMSRequest request,
                                                     String idTransaccion,
                                                     String mensajeTx) {
        String mensajeTransaccion = mensajeTx + "[cambiarPlanCMS] ";
        ResponseBeanCambiarPlanCMS resp= new ResponseBeanCambiarPlanCMS();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
        
        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion + "1.- Iniciar transacción CMS");
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a agregar servicios al contrato CMS...");
                  logger.info(mensajeTransaccion + "2.- Cambiar Plan CMS");
                  ResponseBeanCambiarPlanCMS respActualiza= cmsAdapterClient.cambiarPlanCMS(request.getCambioPlan(), request.getServicios(), mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respActualiza.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se actualizo exitosamente. Se procede a commitear transaccion CMS...");
                      logger.info(mensajeTransaccion + "3.- Commitear transaccion CMS");
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          
                          resp.setServiciosEliminados(respActualiza.getServiciosEliminados());
                          resp.setServiciosAgregados(respActualiza.getServiciosAgregados());
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error en transaccion:" +  respActualiza.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respActualiza.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respActualiza.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("CambiarPlanCMS","Error tratando de Cambiar Plan en CMS:" +
                                                                  respActualiza.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }

    public ResponseBeanTransferirContrato transferirContrato(TransferirContratoRequest request,
                                                             String idTransaccion,
                                                             String mensajeTx) {
        String mensajeTransaccion = mensajeTx + "[transferirContrato] ";
        ResponseBeanTransferirContrato resp= new ResponseBeanTransferirContrato();
        CmsAdapterClient cmsAdapterClient= (CmsAdapterClient) beanFactory.getBean("cmsAdapterClient");
        ResponseBean respAbreConexion= null;
        ResponseBean respInitTxCms= null;
        
        try{
          logger.info(mensajeTransaccion + "Se procede a realizar apertura de conecccion a CMS...");
          respAbreConexion= cmsAdapterClient.iniciarConeccionCMS(idTransaccion, mensajeTransaccion);
          
          if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion + "1.- Iniciar transacción CMS");          
              logger.info(mensajeTransaccion + "Se procede a iniciar Transaccion en CMS...");
              respInitTxCms= cmsAdapterClient.iniciarTransaccionCMS(mensajeTransaccion);
            
              if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              
                  logger.info(mensajeTransaccion + "Inicio de transaccion exitoso. Se procede a agregar servicios al contrato CMS...");
                  logger.info(mensajeTransaccion + "2.- Transferir contrato");                                                                                     
                  ResponseBeanTransferirContrato respActualiza= cmsAdapterClient.transferirContrato(request.getTransferencia(), request.getServicios(), mensajeTransaccion);
                  
                  if( PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respActualiza.getCodigoRespuesta() ) ){
                  
                      logger.info(mensajeTransaccion + "Se actualizo exitosamente. Se procede a commitear transaccion CMS...");
                      logger.info(mensajeTransaccion + "3.- Commitear transaccion CMS");
                      ResponseBean respCommitCms= cmsAdapterClient.hacerCommitTransaccionCMS(mensajeTransaccion);
                      
                      if(  PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase(respCommitCms.getCodigoRespuesta()) ){
                          logger.info(mensajeTransaccion + "Commit exitoso.");
                          
                          resp.setCoId(respActualiza.getCoId());
                          resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_EXITO );
                      } else{
                          logger.error(mensajeTransaccion + "Error realizando commit. Se procedera a realizar rollback en CMS...");
                          logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
                          
                          cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
                          resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_COMMIT_CMS);
                          resp.setMensajeRespuesta("Error realizando commit:" + respCommitCms.getMensajeRespuesta());
                      }
                  } else {
                      logger.error(mensajeTransaccion + "Error en transaccion:" +  respActualiza.getMensajeRespuesta());
                      resp.setCodigoRespuesta( respActualiza.getCodigoRespuesta() );
                      resp.setMensajeRespuesta( respActualiza.getMensajeRespuesta() );
                      throw new ActivosFlowException( new ErrorType("TransferirContrato","Error tratando de Transferir Contrato en CMS:" +
                                                                  respActualiza.getMensajeRespuesta()));
                  }
              } else{
                  logger.error(mensajeTransaccion + "Error iniciando transaccion:" +  respInitTxCms.getMensajeRespuesta());
                  resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
                  resp.setMensajeRespuesta("Error realizando commit:" + respInitTxCms.getMensajeRespuesta());
              }
          } else {
              logger.error(mensajeTransaccion + "Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
              resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
              resp.setMensajeRespuesta("Error tratando de conectarse al ConectorCMS:" +  respAbreConexion.getMensajeRespuesta());
          }
        } catch(ActivosFlowException e){
            logger.error(mensajeTransaccion +
                         "Ocurrio un error dentro del flujo: " + e.getMessage() + 
                         ". Se procedera a realizar rollback en CMS...");
            logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
            cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            
        } catch(Exception e){
            logger.error(mensajeTransaccion +
                         "Ocurrio una exception: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Ocurrio una excepcion:" + e.getMessage());
            
            if( respInitTxCms!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respInitTxCms.getCodigoRespuesta() ) ){
              logger.info(mensajeTransaccion +
                           "Se procedera a realizar rollback en CMS...");
              logger.info(mensajeTransaccion + "4.- Realizar rollback de Tx");
              cmsAdapterClient.hacerRollbackTransaccionCMS(mensajeTransaccion);
            }
        } finally {
            if( respAbreConexion!=null && 
                PropertiesInternos.CODIGO_ESTANDAR_EXITO.equalsIgnoreCase( respAbreConexion.getCodigoRespuesta() ) )
                cmsAdapterClient.cerrarConeccionCMS(mensajeTransaccion);
            
            cmsAdapterClient= null;
        }
        
        return resp;
    }
}
