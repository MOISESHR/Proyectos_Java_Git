package pe.com.claro.eba.activospostpagoejbs.ejbclients;

import java.lang.reflect.Method;

import java.util.List;

import javax.naming.Context;

import org.apache.log4j.Logger;

import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import pe.com.claro.cms.domain.service.LocalTxCmsAdapterOperations;
import pe.com.claro.cms.exception.CmsServiceException;
import pe.com.claro.eba.activospostpagoejbs.beans.CustomerData;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCambiarPlanCMS;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTransferirContrato;
import pe.com.claro.eba.activospostpagoejbs.beans.Servicio;
import pe.com.claro.eba.activospostpagoejbs.types.CambiarPlanType;
import pe.com.claro.eba.activospostpagoejbs.types.ListaServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.ServicioType;
import pe.com.claro.eba.activospostpagoejbs.types.TransferirContratoType;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.eba.activospostpagoejbs.util.Utilitarios;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ActualizarEstadoCuentaRequest;
import pe.com.claro.ebs.services.activospostpago.ws.types.AcuerdoPagoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.CuentaMiembroType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DireccionType;
import pe.com.claro.ebs.services.activospostpago.ws.types.DispositivoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionContratoType;
import pe.com.claro.ebs.services.activospostpago.ws.types.InformacionCuentaType;
import pe.com.claro.ebs.services.activospostpago.ws.types.NumeroDirectorioType;
import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;

import wlai.cms.cmsactivacion_agregarmiembrolarge_request.LAMEMBERNEW;
import wlai.cms.cmsactivacion_agregarmiembrolarge_response.LAMEMBERNEWOutput;
import wlai.cms.cmswli_address_write_request.ADDRESSWRITE;
import wlai.cms.cmswli_address_write_response.ADDRESSWRITEOutput;
import wlai.cms.cmswli_agregardispositivoscontrato_request.CONTRACTDEVICEADD;
import wlai.cms.cmswli_agregardispositivoscontrato_response.CONTRACTDEVICEADDOutput;
import wlai.cms.cmswli_contract_services_add_request.CONTRACTSERVICESADD;
import wlai.cms.cmswli_contract_services_add_request.DirectoryNumbersType;
import wlai.cms.cmswli_contract_services_add_request.ServicesType;
import wlai.cms.cmswli_contract_takeover_request.CONTRACTTAKEOVER;
import wlai.cms.cmswli_contract_takeover_response.CONTRACTTAKEOVEROutput;
import wlai.cms.cmswli_customer_write_request.CUSTOMERWRITE;
import wlai.cms.cmswli_grabarcontrato_request.CONTRACTWRITE;
import wlai.cms.cmswli_grabarformapagos_request.PAYMENTARRANGEMENTWRITE;
import wlai.cms.cmswli_grabarformapagos_response.PAYMENTARRANGEMENTWRITEOutput;
import wlai.cms.cmswli_grabarinfocliente_request.CUSTOMERINFOWRITE;
import wlai.cms.cmswli_grabarinformacioncontrato_request.CONTRACTINFOWRITE;
import wlai.cms.cmswli_grabarinformacioncontrato_request.FieldsType;
import wlai.cms.cmswli_nuevocontrato_request.CONTRACTNEW;
import wlai.cms.cmswli_nuevocontrato_response.CONTRACTNEWOutput;
import wlai.cms.cmswli_product_change_request.PRODUCTCHANGE;
import wlai.cms.cmswli_product_change_request.SequenceOfServicesAndServicePackagesType;
import wlai.cms.cmswli_product_change_response.PRODUCTCHANGEOutput;
import wlai.cms.cmswli_product_change_response.ServicesAddedType;
import wlai.cms.cmswli_product_change_response.ServicesDroppedType;


public class CmsAdapterClientImpl implements CmsAdapterClient {

    private static Logger logger =
        Logger.getLogger(CmsAdapterClientImpl.class);

    private LocalTxCmsAdapterOperations cmsAdapter;
    private Context ic;


    public CmsAdapterClientImpl() {
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean iniciarConeccionCMS(String idTransaccion,
                                            String mensajeTx) {

        ResponseBean resp = new ResponseBean();
        String mensajeTransaccion =  mensajeTx + "[iniciarConeccionCMS] ";
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [iniciarConeccionCMS]");
        try {
            logger.info(mensajeTransaccion + "Obteniendo InitialContext de Provider:" +
                        PropertiesExternos.EJB_CONECTOR_CMS_PROVIDER_URL);
            weblogic.jndi.Environment env= new weblogic.jndi.Environment();
            env.setProviderURL(PropertiesExternos.EJB_CONECTOR_CMS_PROVIDER_URL );
            env.setRequestTimeout( PropertiesExternos.EKB_CONECTOR_CMS_REQUEST_TIMEOUT );
            env.setRMIClientTimeout( PropertiesExternos.EKB_CONECTOR_CMS_REQUEST_TIMEOUT );

            ic= env.getInitialContext();
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Se genero una excepcion:", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error obteniendo InitialContextFactory a servidor destino.");
            logger.info(mensajeTransaccion + "[FIN] [iniciarConeccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
            return resp;
        }

        try {
            logger.info(mensajeTransaccion +
                        "InitialContext obtenido. Buscando en destino EJB:" +
                        PropertiesExternos.EJB_CONECTOR_CMS_JNDI_REMOTE);
            Object obj =
                ic.lookup(PropertiesExternos.EJB_CONECTOR_CMS_JNDI_REMOTE);
            cmsAdapter = (LocalTxCmsAdapterOperations)obj;
        } catch (Exception e) {
            logger.error(mensajeTransaccion + "Se genero una excepcion:", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error buscando EJB ConectorCMS en servidor destino.");
            logger.info(mensajeTransaccion + "[FIN] [iniciarConeccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
            return resp;
        }

        try {
            logger.info(mensajeTransaccion +
                        "Se tiene coneccion a EJB ConectorCMS. Ejecutando createTraceableConnection, transaccionCliente enviado:" +
                        idTransaccion);
            cmsAdapter.createTraceableConnection(idTransaccion);

            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (Exception e) {
            logger.error(mensajeTransaccion +
                         "Se genero una excepcion en la conexion con CMS. Descripcion:",
                         e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error iniciando coneccion con o en Conector CMS.");
        }
        logger.info(mensajeTransaccion + "[FIN] [iniciarConeccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean cerrarConeccionCMS(String mensajeTx) {

        ResponseBean resp = new ResponseBean();
        String mensajeTransaccion =  mensajeTx +  "[cerrarConeccionCMS] ";

        try {
            logger.info(mensajeTransaccion + "Ejecutando removeConnection...");
            cmsAdapter.removeConnection();
            logger.info(mensajeTransaccion +
                        "Se cerro la conexion exitosamente.");

            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (Exception e) {
            logger.error(mensajeTransaccion +
                         "Se genero una excepcion cerrando la conexion con CMS. Descripcion:",
                         e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error cerrando coneccion de Conector CMS a CMS.");
        } finally{
            try{
                ic.close();
            } catch(Exception e){
                logger.error(mensajeTransaccion + "Error cerrando InitialContext:",e);
            }
            
        }
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean iniciarTransaccionCMS(String mensajeTx) {

        ResponseBean resp = new ResponseBean();
        String mensajeTransaccion =  mensajeTx +  "[iniciarTransaccionCMS] ";
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [iniciarTransaccionCMS]");

        try {
            logger.info(mensajeTransaccion + "Ejecutando beginTransaction...");
            cmsAdapter.beginTransaction();

            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (Exception e) {
            logger.error(mensajeTransaccion +
                         "Se genero una excepcion iniciando Transaccion con CMS. Descripcion:",
                         e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error iniciando transaccion CMS.");
        }
        logger.info(mensajeTransaccion + "[FIN] [iniciarTransaccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean hacerCommitTransaccionCMS(String mensajeTx) {

        ResponseBean resp = new ResponseBean();
        String mensajeTransaccion =  mensajeTx +  "[hacerCommitTransaccionCMS] ";
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [hacerCommitTransaccionCMS]");

        try {
            logger.info(mensajeTransaccion +
                        "Ejecutando commitTransaction...");
            cmsAdapter.commitTransaction();

            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (Exception e) {
            logger.error(mensajeTransaccion +
                         "Se genero una excepcion. Descripcion:", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error realizando commit a  transaccion CMS.");
        }
        logger.info(mensajeTransaccion + "[FIN] [hacerCommitTransaccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean hacerRollbackTransaccionCMS(String mensajeTx) {

        ResponseBean resp = new ResponseBean();
        String mensajeTransaccion =  mensajeTx +  "[hacerRollbackTransaccionCMS] ";
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [hacerRollbackTransaccionCMS]");

        try {
            logger.info(mensajeTransaccion +
                        "Ejecutando rollbackTransaction...");
            cmsAdapter.rollbackTransaction();
            logger.info(mensajeTransaccion +
                        "Se realizo rollback de Tx en CMS");

            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (Exception e) {
            logger.error(mensajeTransaccion +
                         "Se genero una excepcion. Descripcion:", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error realizando rollback a transaccion CMS.");
        }
        logger.info(mensajeTransaccion + "[FIN] [hacerRollbackTransaccionCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS agregarMiembroLarge (CuentaMiembroType cuenta,
                                           String mensajeTransaccion) {

        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[agregarMiembroLarge] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);

        try {
            LAMEMBERNEW c = new LAMEMBERNEW();
            c.setAREACODE( cuenta.getCodigoArea() );
            c.setCOSTID( cuenta.getIdCentroCosto() );
            c.setCSBILLCYCLE( cuenta.getCicloFacturacion() );
            c.setCSCLIMIT( cuenta.getLimiteCredito() );
            c.setCSIDHIGH( cuenta.getIdCuentaPadre() );
            c.setCSLEVELCODE( cuenta.getNivelCuenta() );
            c.setCSPASSWORD( cuenta.getRepresentanteLegal() );
            c.setCSPAYMNTRESP( cuenta.isResponsablePago() );
            c.setCSSTATUS( cuenta.getEstadoCuenta() );
            c.setPRGCODE( cuenta.getTipoCliente() );
            c.setRPCODE( cuenta.getPlanCuenta() );
            c.setRSCODE( cuenta.getCodigoEstadoRazon() );
            c.setTERMCODE( cuenta.getFrecuenciaFacturacion() );
            c.setTRADECODE( cuenta.getCodigoProfesionCliente() );

            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));

            LAMEMBERNEWOutput output = cmsAdapter.agregarMiembroLarge(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));

            if (output != null) {
                resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
                CustomerData cd= new CustomerData();
                cd.setCustomerId( output.getCSID());
                cd.setCustCode( output.getCSCODE() );
                resp.setCustomerData( cd );
            } else {
                resp.setMensajeRespuesta("Response devuelto es NULL");
            }
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.agregarMiembroLarge: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.agregarMiembroLarge: " + 
                                     e.getLocalizedMessage());
        }

        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS escribirDireccionCMS (DireccionType direccion,
                                           String mensajeTransaccion) {
  
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[escribirDireccionCMS] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
  
        try {
            ADDRESSWRITE c = new ADDRESSWRITE();
            c.setCSID( direccion.getCustomerId() );
            c.setADRSEQ( direccion.getIdSeqDireccion() );
            c.setTTLID( direccion.getIdTitulo() );
            c.setADRLNAME( direccion.getApellidos() );
            c.setADRFNAME( direccion.getNombres() );
            c.setADRMNAME( direccion.getInicialesNombre() );
            c.setADRNAME( direccion.getRazonSocial() );
            c.setADRBIRTHDT( direccion.getFechaNacimiento() );
            c.setADRSTREET( direccion.getProvincia() );
            c.setADRZIP( direccion.getCodigoPostal() );
            c.setADRCITY( direccion.getDepartamento() );
            c.setADRNOTE1( direccion.getDireccion() );
            c.setADRNOTE2( direccion.getReferenciaDireccion() );
            c.setADRNOTE3( direccion.getDistrito() );
            c.setADRPHN1( direccion.getTelefonoContacto1() );
            c.setADRPHN1AREA( direccion.getAreaTelefonoContacto1() );
            c.setADRPHN2( direccion.getTelefonoContacto2() );
            c.setADRPHN2AREA( direccion.getAreaTelefonoContacto2() );
            c.setADREMAIL( direccion.getEmail() );
            c.setIDTYPECODE( direccion.getTipoDocumento() );
            c.setADRCOMPNO( direccion.getNroDocumento1() );
            c.setADRIDNO( direccion.getNroDocumento2() );
            c.setADRSEX( direccion.getSexo() );
            c.setADRCHECK( direccion.isCheck() );
            c.setCOUNTRYID( direccion.getIdPais() );
            c.setLNGCODE( direccion.getSegmentoCliente() );
            c.setADRNATIONALITY( direccion.getNacionalidad() );
            c.setADRROLES( direccion.getRol() );
            c.setADRDRIVELICENCE( direccion.getCodigoNicho() );
            c.setADRLOCATION1( direccion.getCodigoUbigeo() );
            c.setADRLOCATION2( direccion.getCodigoPlano() );
    
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
  
            ADDRESSWRITEOutput output = cmsAdapter.escribirDireccionCMS(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
  
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
            resp.setObjectId( output.getOBJECTID());
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.escribirDireccionCMS: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.escribirDireccionCMS: " + 
                                     e.getLocalizedMessage());
        }
  
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS actualizarFormaPagoCuenta (AcuerdoPagoType acuerdo,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[actualizarFormaPagoCuenta] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
    
        try {
            PAYMENTARRANGEMENTWRITE c = new PAYMENTARRANGEMENTWRITE();
            c.setCSID( acuerdo.getCustomerId() );
            c.setCSPSEQNO( acuerdo.getIdSeqAcuerdoPago() );
            c.setCSPPMNTID( acuerdo.getIdFormaPago() );
    
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            PAYMENTARRANGEMENTWRITEOutput output = cmsAdapter.actualizarFormaPagoCuenta(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
            resp.setObjectId( output.getOBJECTID());
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.actualizarFormaPagoCuenta: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.actualizarFormaPagoCuenta: " + 
                                     e.getLocalizedMessage());
        }
    
        return resp;
    }

    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS escribirInformacionCuenta (InformacionCuentaType infoCuenta,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[escribirInformacionCuenta] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
    
        try {
            CUSTOMERINFOWRITE c = new CUSTOMERINFOWRITE();
            c.setCUSTOMERID( infoCuenta.getCustomerId() );
            
            String methodName= null;
            Method method= null;
            InformacionCuentaType.ListaChecks listChecksType= infoCuenta.getListaChecks();
            
            if( listChecksType!=null ){
              logger.info(mensajeTransaccionMetodo + "Setteando checks...");
              List<InformacionCuentaType.ListaChecks.Check> listChecks= listChecksType.getCheck();
              
              for(InformacionCuentaType.ListaChecks.Check check:listChecks){
                methodName= "setCHECK"  + String.format("%02d", check.getIndice());
                
                try {    
                    method= Utilitarios.obtainSetterMethod(methodName, 
                                                       CUSTOMERINFOWRITE.class, 
                                                       Boolean.class);
                
                    method.invoke(c, check.isValor());
                } catch (Exception e) {
                    logger.error(mensajeTransaccionMetodo + "Error al tratar de invocar dinamicamente el metodo:" +
                                  methodName + ". Revisar si el indice recibido es valido.",e);
                }
              }
            }
            
            InformacionCuentaType.ListaCombos listCombosType= infoCuenta.getListaCombos();
            
            if( listCombosType!=null ){
              logger.info(mensajeTransaccionMetodo + "Setteando combos...");
              List<InformacionCuentaType.ListaCombos.Combo> listCombos= listCombosType.getCombo();
              
              for(InformacionCuentaType.ListaCombos.Combo combo:listCombos){
                methodName= "setCOMBO"  + String.format("%02d", combo.getIndice());
                
                try {    
                    method= Utilitarios.obtainSetterMethod(methodName, 
                                                       CUSTOMERINFOWRITE.class, 
                                                       String.class);
                
                    method.invoke(c, combo.getValor());
                } catch (Exception e) {
                    logger.error(mensajeTransaccionMetodo + "Error al tratar de invocar dinamicamente el metodo:" +
                                  methodName + ". Revisar si el indice recibido es valido.",e);
                }
              }
            }
            
            InformacionCuentaType.ListaTexts listTextsType= infoCuenta.getListaTexts();
            
            if( listTextsType!=null ){
              logger.info(mensajeTransaccionMetodo + "Setteando texts...");
              List<InformacionCuentaType.ListaTexts.Text> listTexts= listTextsType.getText();
              
              for(InformacionCuentaType.ListaTexts.Text text:listTexts){
                methodName= "setTEXT"  + String.format("%02d", text.getIndice());
                
                try {    
                    method= Utilitarios.obtainSetterMethod(methodName, 
                                                       CUSTOMERINFOWRITE.class, 
                                                       String.class);
                
                    method.invoke(c, text.getValor());
                } catch (Exception e) {
                    logger.error(mensajeTransaccionMetodo + "Error al tratar de invocar dinamicamente el metodo:" +
                                  methodName + ". Revisar si el indice recibido es valido.",e);
                }
              }
            }
                
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            String output = cmsAdapter.escribirInformacionCuenta(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
            resp.setObjectId( output );
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.escribirInformacionCuenta: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.escribirInformacionCuenta: " + 
                                     e.getLocalizedMessage());
        }
    
        return resp;
    }


    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS escribirCuentaCMS (ActualizarEstadoCuentaRequest req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[escribirCuentaCMS] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
    
        try {
            CUSTOMERWRITE c = new CUSTOMERWRITE();
            c.setCSID( req.getCustomerId() );
            c.setRSCODE( req.getCodigoEstadoRazon() );
            c.setCSSTATUS( req.getEstadoCuenta() );
    
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            String output = cmsAdapter.escribirCuentaCMS(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
            resp.setObjectId( output);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.escribirCuentaCMS: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.escribirCuentaCMS: " + 
                                     e.getLocalizedMessage());
        }
    
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS crearNuevoContrato (ContratoType req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[crearNuevoContrato] ";
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [crearNuevoContrato]");
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
    
        try {
            CONTRACTNEW c = new CONTRACTNEW();
            c.setCSID( req.getCustomerId() );
            c.setNEWRP( req.getPlanTarifario() );
            c.setSUBMID( req.getIdSubMercado() );
            c.setSCCODE( req.getIdMercado() );
            c.setPLCODE( req.getRed() );
            c.setCOITEMBILL( req.isEstadoUmbral() );
            c.setCOTHRESHOLD( req.getCantidadUmbral() );
            c.setCOARCHIVE( req.isArchivoLlamadas() );
        
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            CONTRACTNEWOutput output = cmsAdapter.crearNuevoContrato(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
            resp.setCoId( output.getCOID() );
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.crearNuevoContrato: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.crearNuevoContrato: " + 
                                     e.getLocalizedMessage());
        }
        logger.info(mensajeTransaccion + "[FIN] [crearNuevoContrato] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos"); 
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS agregarDispositivo (DispositivoType req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[agregarDispositivo] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [agregarDispositivo]");
        
        try {
            CONTRACTDEVICEADD c = new CONTRACTDEVICEADD();
            c.setCOID( req.getCoId() );
            c.setRESID( req.getIdDispositivo() );
            c.setRESTYPE( req.getTipoDispositivo() );
        
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            CONTRACTDEVICEADDOutput output = cmsAdapter.agregarDispositivo(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.agregarDispositivo: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.agregarDispositivo: " + 
                                     e.getLocalizedMessage());
        }
        logger.info(mensajeTransaccion + "[FIN] [agregarDispositivo] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS agregarServiciosContratoCMS (List<ServicioContratoType> req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[agregarServiciosContratoCMS] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);

        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [agregarServiciosContratoCMS]");
        try {
            CONTRACTSERVICESADD c = new CONTRACTSERVICESADD();
            c.setCOID( req.get(0).getCoId() );
            List<ServicesType> services= c.getServices();
            ServicesType sAux= null;
            
            for(ServicioContratoType s:req){
                sAux= new ServicesType();
                sAux.setSNCODE( s.getSnCode() );
                sAux.setSPCODE( s.getSpCode() );
                sAux.setPROFILEID( s.getProfileId() );
                
                if( s.getCamposAdicionalesDcto()!=null ){
                  sAux.setADVANCEACCESSOVWTYPE( s.getCamposAdicionalesDcto().getTipoCostoServicioAvanzado() );
                  sAux.setADVANCEACCESSOVW( s.getCamposAdicionalesDcto().getCostoServicioAvanzado() );
                  sAux.setADVANCEACCESSOVWPRD( s.getCamposAdicionalesDcto().getPeriodoCostoServicioAvanzado() );
                }
                
                if( s.getCamposAdicionalesCargo()!=null ){
                  sAux.setCOSACCESSTYPE( s.getCamposAdicionalesCargo().getTipoCostoServicio() );
                  sAux.setCOSACCESS( s.getCamposAdicionalesCargo().getCostoServicio() );
                  sAux.setCOSACCESSPRD( s.getCamposAdicionalesCargo().getPeriodoCostoServicio() );
                }
                if( s.getListaNumerosDirectorio()!=null && s.getListaNumerosDirectorio().getNumeroDirectorio()!=null &&
                    !s.getListaNumerosDirectorio().getNumeroDirectorio().isEmpty()){
                      
                      List<NumeroDirectorioType> directs= s.getListaNumerosDirectorio().getNumeroDirectorio();
                      DirectoryNumbersType dCms= null;
                      
                      for(NumeroDirectorioType d:directs){
                          dCms= new DirectoryNumbersType();
                          dCms.setDNID( d.getIdNumeroTelefono() );
                          dCms.setMAINDIRNUM( d.isLineaPrincipal() );
                        
                          sAux.getDirectoryNumbers().add( dCms );
                      }
                }
                services.add( sAux );
            }
        
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            String output = cmsAdapter.agregarServiciosContratoCMS(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (ArrayIndexOutOfBoundsException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error de indices en un Array: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
            resp.setMensajeRespuesta("Error de indices en un Array: " + 
                                     e.getMessage());
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.agregarServiciosContratoCMS: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.agregarServiciosContratoCMS: " + 
                                     e.getMessage());
        }
        logger.info(mensajeTransaccion + "[FIN] [agregarServiciosContratoCMS] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS escribirContrato (ActualizacionContratoType req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[escribirContrato] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [escribirContrato]");
    
        try {
            CONTRACTWRITE c = new CONTRACTWRITE();
            c.setCOID( req.getCoId() );
            c.setCOSTATE( req.getEstado() );
            c.setREASON( req.getRazon() );
        
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            String output = cmsAdapter.escribirContrato(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.escribirContrato: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.escribirContrato: " + 
                                     e.getMessage());
        }
        logger.info(mensajeTransaccion + "[FIN] [escribirContrato] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCMS grabarInfoContrato (InformacionContratoType req,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[grabarInfoContrato] ";
        ResponseBeanCMS resp = new ResponseBeanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [grabarInfoContrato]");
    
        try {
            CONTRACTINFOWRITE c = new CONTRACTINFOWRITE();
            c.setCOID( req.getCoId() );
            
            List<FieldsType> fields= c.getFields();
            FieldsType sAux= null;
            List<InformacionContratoType.ListaCampos.Campo> listaCampos= req.getListaCampos().getCampo();
            
            for(InformacionContratoType.ListaCampos.Campo s:listaCampos){
                sAux= new FieldsType();
                sAux.setCOFIELDINDEX( s.getIndice() );
                sAux.setCOFIELDTYPE( s.getTipo() );
                sAux.setCOFIELDVALUE( s.getValor() );
                
                fields.add( sAux );
            }
        
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            String output = cmsAdapter.grabarInfoContrato(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
    
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.grabarInfoContrato: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.grabarInfoContrato: " + 
                                     e.getMessage());
        }
        logger.info(mensajeTransaccion + "[FIN] [grabarInfoContrato] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }
    
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanCambiarPlanCMS cambiarPlanCMS (CambiarPlanType req, ListaServicioType servicios,
                                           String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[cambiarPlanCMS] ";
        ResponseBeanCambiarPlanCMS resp = new ResponseBeanCambiarPlanCMS();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
    
        try {
            PRODUCTCHANGE c = new PRODUCTCHANGE();
            c.setCOID( req.getCoId() );
            c.setCONEWRPCODE( req.getNuevoPlan() );
            c.setCONEWSPCODE( req.getNuevoSpcode() );
            c.setCOOLDSPCODE( req.getAntiguoSpcode() );
            if(null != servicios)
              for(ServicioType row : servicios.getServicio()){
                  SequenceOfServicesAndServicePackagesType a = new SequenceOfServicesAndServicePackagesType();
                  a.setSPCODE(row.getSpcode());
                  a.setSNCODE(row.getSncode());
                  c.getSequenceOfServicesAndServicePackages().add(a);
              }
            
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
    
            PRODUCTCHANGEOutput output = cmsAdapter.cambiarPlanCMS(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
            
            for(ServicesDroppedType row : output.getServicesDropped()){
                Servicio e = new Servicio();
                e.setSpCode(row.getCOSPCODE());
                e.setSnCode(row.getCOSNCODE());
                resp.getServiciosEliminados().add(e);
            }
            
            for(ServicesAddedType row : output.getServicesAdded()){
                Servicio e = new Servicio();
                e.setSpCode(row.getCOSPCODE());
                e.setSnCode(row.getCOSNCODE());
                resp.getServiciosAgregados().add(e);
            }
            
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.cambiarPlanCMS: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.cambiarPlanCMS: " + 
                                     e.getMessage());
        }
    
        return resp;
    }

    public ResponseBeanTransferirContrato transferirContrato(TransferirContratoType req,
                                                             ListaServicioType servicios,
                                                             String mensajeTransaccion) {
        String mensajeTransaccionMetodo =
            mensajeTransaccion + "[transferirContrato] ";
        ResponseBeanTransferirContrato resp = new ResponseBeanTransferirContrato();
        resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR);
        
        try {
            CONTRACTTAKEOVER c = new CONTRACTTAKEOVER();
            c.setCHARGESUBS( req.isCargoSubscripcion() );
            if(req.getRatioConversion()!=1){ c.setCOCONVRATETYPE( req.getRatioConversion() ); }
            c.setCOCURRID( req.getMoneda() );
            c.setCOID( req.getCoId() );
            c.setCOREASON( req.getRazonTransferencia() );
            c.setNEWCSID( req.getNuevoCustomerId() );
            if(req.getNuevoPlan()!=0){ c.setNEWRP( req.getNuevoPlan() );}
            if(req.getMonedaSecundaria()!=1) {    c.setSECCONTRCURRID( req.getMonedaSecundaria() ); }
            if(req.getRatioConversionSecundaria()!=0){ c.setSECCONVRATETYPECONTRACT( req.getRatioConversionSecundaria() );}
            
            if(null != servicios)
              for(ServicioType row : servicios.getServicio()){
                  wlai.cms.cmswli_contract_takeover_request.ServicesType a = new wlai.cms.cmswli_contract_takeover_request.ServicesType();
                  a.setSPCODE(row.getSpcode());
                  a.setSNCODE(row.getSncode());
                  c.getServices().add(a);
              }
            
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" +
                        JAXBUtilitarios.anyObjectToXmlText(c));
        
            CONTRACTTAKEOVEROutput output = cmsAdapter.transferirContrato(c);
            
            logger.info(mensajeTransaccionMetodo + "Datos devueltos: " +
                        JAXBUtilitarios.anyObjectToXmlText(output));
            
            resp.setCoId(output.getNEWCOID());
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_EXITO);
        } catch (CmsServiceException e) {
            logger.error(mensajeTransaccionMetodo +
                         "Exception lanzada por CMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ERROR_CMS);
            resp.setMensajeRespuesta("Exception ejecutando ConectorCMS.cambiarPlanCMS: " + 
                                     e.getLocalizedMessage());
        } catch (Exception e) {
            logger.error(mensajeTransaccionMetodo +
                         "Error accediendo a ConectorCMS: ", e);
            resp.setCodigoRespuesta(PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO);
            resp.setMensajeRespuesta("Error ejecutando ConectorCMS.cambiarPlanCMS: " + 
                                     e.getMessage());
        }
        
        return resp;
    }
}
