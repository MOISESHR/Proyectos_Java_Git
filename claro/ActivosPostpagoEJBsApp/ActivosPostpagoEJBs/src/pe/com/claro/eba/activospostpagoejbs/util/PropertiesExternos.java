package pe.com.claro.eba.activospostpagoejbs.util;

import java.io.Serializable;

import java.util.regex.Pattern;

import org.apache.log4j.Logger;


public class PropertiesExternos implements Serializable {

    private static final long serialVersionUID = 1L;
    private static Logger logger= Logger.getLogger(PropertiesExternos.class);

    private static ReadForeignPropertiesUtil foreignProp =
        ReadForeignPropertiesUtil.getSingleton(System.getProperty("claro.properties") +
                                               "ActivosPostpagoEJBs.properties");
    
    public static final String DB_BSCS_JNDI = foreignProp.getValor("db.bscs.jndi");
    public static final String DB_BSCS_OWNER = foreignProp.getValor("db.bscs.owner");
    public static final int DB_BSCS_TIMEOUT_SECONDS = Integer.parseInt(foreignProp.getValor("db.bscs.timeout.seconds"));
    
    public static final String DB_PVU_JNDI = foreignProp.getValor("db.pvu.jndi");
    public static final String DB_PVU_OWNER = foreignProp.getValor("db.pvu.owner");
    public static final int DB_PVU_TIMEOUT_SECONDS = Integer.parseInt(foreignProp.getValor("db.pvu.timeout.seconds"));
    
    public static final String DB_SGA_JNDI = foreignProp.getValor("db.sga.jndi");
    public static final String DB_SGA_OWNER = foreignProp.getValor("db.sga.owner");
    public static final String DB_SGA_OWNER_OP = foreignProp.getValor("db.sga.owner.op");
    public static final int DB_SGA_TIMEOUT_SECONDS = Integer.parseInt(foreignProp.getValor("db.sga.timeout.seconds"));
    
    public static final String DB_EAI_JNDI = foreignProp.getValor("db.eai.jndi");
    public static final String DB_EAI_OWNER = foreignProp.getValor("db.eai.owner");
    public static final int DB_EAI_TIMEOUT_SECONDS = Integer.parseInt(foreignProp.getValor("db.eai.timeout.seconds"));
    
    public static final String EJB_CONECTOR_CMS_PROVIDER_URL = foreignProp.getValor("ejb.conectorcms.provider.url");
    public static final String EJB_CONECTOR_CMS_JNDI_REMOTE = foreignProp.getValor("ejb.conectorcms.jndi.remote");
    public static final Long EKB_CONECTOR_CMS_REQUEST_TIMEOUT = Long.valueOf( foreignProp.getValor("ejb.conectorcms.request.timeout") );
    
    public static final String CREAR_CONTRATO_AGREGAR_RECURSOS_TIPOS_POSTPAGO_STR = foreignProp.getValor("crearContrato.agregar.recursos.tiposPostpago");
    public static final String[] CREAR_CONTRATO_AGREGAR_RECURSOS_LISTA_TIPOS_POSTPAGO = CREAR_CONTRATO_AGREGAR_RECURSOS_TIPOS_POSTPAGO_STR.split(Pattern.quote(","));
    
    public static final String CREAR_CONTRATO_VALIDAR_SERVICIOS_TIPOS_POSTPAGO_STR = foreignProp.getValor("crearContrato.validar.servicios.tiposPostpago");
    public static final String[] CREAR_CONTRATO_VALIDAR_SERVICIOS_LISTA_TIPOS_POSTPAGO = CREAR_CONTRATO_VALIDAR_SERVICIOS_TIPOS_POSTPAGO_STR.split(Pattern.quote(","));
    
    public static final String CREAR_CONTRATO_APLICA_DNIDS_TIPOS_POSTPAGO_STR = foreignProp.getValor("crearContrato.aplica.dnId.tiposPostpago");
    public static final String[] CREAR_CONTRATO_APLICA_DNIDS_LISTA_TIPOS_POSTPAGO = CREAR_CONTRATO_APLICA_DNIDS_TIPOS_POSTPAGO_STR.split(Pattern.quote(","));
    
    public static final String CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("crearCuentaCompleta.codigo.respuesta.idf0");
    public static final String CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("crearCuentaCompleta.codigo.respuesta.idf1");
    public static final String CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("crearCuentaCompleta.codigo.respuesta.idt1");
    public static final String CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("crearCuentaCompleta.codigo.respuesta.idt2");
    public static final String CREAR_CUENTA_COMPLETA_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("crearCuentaCompleta.codigo.respuesta.idt3");
    
    public static final String CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("crearCuentaCompleta.mensaje.respuesta.idf0");
    public static final String CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("crearCuentaCompleta.mensaje.respuesta.idf1");
    public static final String CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("crearCuentaCompleta.mensaje.respuesta.idt1");
    public static final String CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("crearCuentaCompleta.mensaje.respuesta.idt2");
    public static final String CREAR_CUENTA_COMPLETA_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("crearCuentaCompleta.mensaje.respuesta.idt3");
    
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("crearContrato.codigo.respuesta.idf0");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("crearContrato.codigo.respuesta.idf1");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("crearContrato.codigo.respuesta.idt1");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("crearContrato.codigo.respuesta.idt2");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("crearContrato.codigo.respuesta.idt3");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT4 = foreignProp.getValor("crearContrato.codigo.respuesta.idt4");
  public static final String CREAR_CONTRATO_COMPLETO_CODIGO_RESPUESTA_IDT5 = foreignProp.getValor("crearContrato.codigo.respuesta.idt5");
  
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("crearContrato.mensaje.respuesta.idf0");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("crearContrato.mensaje.respuesta.idf1");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("crearContrato.mensaje.respuesta.idt1");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("crearContrato.mensaje.respuesta.idt2");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("crearContrato.mensaje.respuesta.idt3");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT4 = foreignProp.getValor("crearContrato.mensaje.respuesta.idt4");
  public static final String CREAR_CONTRATO_COMPLETO_MENSAJE_RESPUESTA_IDT5 = foreignProp.getValor("crearContrato.mensaje.respuesta.idt5");

    public static final String ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("actualizarEstadoCuenta.codigo.respuesta.idf0");
    public static final String ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("actualizarEstadoCuenta.codigo.respuesta.idf1");
    public static final String ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("actualizarEstadoCuenta.codigo.respuesta.idt1");
    public static final String ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("actualizarEstadoCuenta.codigo.respuesta.idt2");
    public static final String ACTUALIZAR_CUENTA_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("actualizarEstadoCuenta.codigo.respuesta.idt3");
    
    public static final Long CREAR_CONTRATO_PROFILE_ID = Long.valueOf( foreignProp.getValor("crearcontrato.profile.id") );
    
    public static final String ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("actualizarEstadoCuenta.mensaje.respuesta.idf0");
    public static final String ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("actualizarEstadoCuenta.mensaje.respuesta.idf1");
    public static final String ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("actualizarEstadoCuenta.mensaje.respuesta.idt1");
    public static final String ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("actualizarEstadoCuenta.mensaje.respuesta.idt2");
    public static final String ACTUALIZAR_CUENTA_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("actualizarEstadoCuenta.mensaje.respuesta.idt3");

    public static final String ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("actualizarContrato.codigo.respuesta.idf0");
    public static final String ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("actualizarContrato.codigo.respuesta.idf1");
    public static final String ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("actualizarContrato.codigo.respuesta.idt1");
    public static final String ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("actualizarContrato.codigo.respuesta.idt2");
    public static final String ACTUALIZAR_CONTRATO_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("actualizarContrato.codigo.respuesta.idt3");
    
    public static final String ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("actualizarContrato.mensaje.respuesta.idf0");
    public static final String ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("actualizarContrato.mensaje.respuesta.idf1");
    public static final String ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("actualizarContrato.mensaje.respuesta.idt1");
    public static final String ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("actualizarContrato.mensaje.respuesta.idt2");
    public static final String ACTUALIZAR_CONTRATO_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("actualizarContrato.mensaje.respuesta.idt3");
    
    public static final String AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("agregarServiciosContrato.codigo.respuesta.idf0");
    public static final String AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("agregarServiciosContrato.codigo.respuesta.idf1");
    public static final String AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("agregarServiciosContrato.codigo.respuesta.idt1");
    public static final String AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("agregarServiciosContrato.codigo.respuesta.idt2");
    public static final String AGREGAR_SERVICIOS_CONTRATO_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("agregarServiciosContrato.codigo.respuesta.idt3");
    
    public static final String AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("agregarServiciosContrato.mensaje.respuesta.idf0");
    public static final String AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("agregarServiciosContrato.mensaje.respuesta.idf1");
    public static final String AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("agregarServiciosContrato.mensaje.respuesta.idt1");
    public static final String AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("agregarServiciosContrato.mensaje.respuesta.idt2");
    public static final String AGREGAR_SERVICIOS_CONTRATO_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("agregarServiciosContrato.mensaje.respuesta.idt3");
    
    public static final String CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("cambiarPlanCMS.codigo.respuesta.idf0");
    public static final String CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("cambiarPlanCMS.codigo.respuesta.idf1");
    public static final String CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("cambiarPlanCMS.codigo.respuesta.idt1");
    public static final String CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("cambiarPlanCMS.codigo.respuesta.idt2");
    public static final String CAMBIAR_PLAN_CMS_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("cambiarPlanCMS.codigo.respuesta.idt3");
    
    public static final String CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("cambiarPlanCMS.mensaje.respuesta.idf0");
    public static final String CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("cambiarPlanCMS.mensaje.respuesta.idf1");
    public static final String CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("cambiarPlanCMS.mensaje.respuesta.idt1");
    public static final String CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("cambiarPlanCMS.mensaje.respuesta.idt2");
    public static final String CAMBIAR_PLAN_CMS_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("cambiarPlanCMS.mensaje.respuesta.idt3");
    
    public static final String TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDF0 = foreignProp.getValor("transferirContrato.codigo.respuesta.idf0");
    public static final String TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDF1 = foreignProp.getValor("transferirContrato.codigo.respuesta.idf1");
    public static final String TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT1 = foreignProp.getValor("transferirContrato.codigo.respuesta.idt1");
    public static final String TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT2 = foreignProp.getValor("transferirContrato.codigo.respuesta.idt2");
    public static final String TRANSFERIR_CONTRATO_CODIGO_RESPUESTA_IDT3 = foreignProp.getValor("transferirContrato.codigo.respuesta.idt3");
    
    public static final String TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDF0 = foreignProp.getValor("transferirContrato.mensaje.respuesta.idf0");
    public static final String TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDF1 = foreignProp.getValor("transferirContrato.mensaje.respuesta.idf1");
    public static final String TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT1 = foreignProp.getValor("transferirContrato.mensaje.respuesta.idt1");
    public static final String TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT2 = foreignProp.getValor("transferirContrato.mensaje.respuesta.idt2");
    public static final String TRANSFERIR_CONTRATO_MENSAJE_RESPUESTA_IDT3 = foreignProp.getValor("transferirContrato.mensaje.respuesta.idt3");
    
    public static final String PVU_ESTADO_CONTRATO_ASIGNADO = foreignProp.getValor("pvu.estado.contrato.asignado");
    public static final String PVU_ESTADO_CONTRATO_ACTIVADO = foreignProp.getValor("pvu.estado.contrato.activado");
    
    public static final int EAI_ETAPA_ERROR_MAX_LENGHT = Integer.parseInt( foreignProp.getValor("eai.etapa.error.max.length") );
    public static final int EAI_DESCRIPCION_ERROR_MAX_LENGHT = Integer.parseInt( foreignProp.getValor("eai.descripcion.error.max.length") );
    public static final String SIACPOST_NOMBRE = foreignProp.getValor("crearContrato.validar.nombreAplicacion");
    
    public static String obtenerJMSProviderUrlPorMetodo(String metodo,
                                                        String mensajeLog) throws PropertiesException{
        
        String cad= null;
        String var= null;
        
        try{
            cad= metodo + ".jms.providerURL";
            var= foreignProp.getValor( cad );
        } catch(Exception ne){
            logger.info(mensajeLog + "No existe ProviderURL JMS para la operacion=" + cad);
            throw new PropertiesException("No existe la siguiente variable en el archivo properties:" + 
                                          cad,ne);
        }
  
        return var;
    }
    
    public static String obtenerJMSConnFactoryPorMetodo(String metodo,
                                                        String mensajeLog) throws PropertiesException{
        
        String cad= null;
        String var= null;
        
        try{
            cad= metodo + ".jms.connfactory";
            var= foreignProp.getValor( cad );
        } catch(Exception ne){
            logger.info(mensajeLog + "No existe ConnectionFactory JMS para la operacion=" + cad);
            throw new PropertiesException("No existe la siguiente variable en el archivo properties:" + 
                                          cad,ne);
        }
    
        return var;
    }
    
    public static String obtenerJMSQueuePorMetodo(String metodo,
                                                        String mensajeLog) throws PropertiesException{
        
        String cad= null;
        String var= null;
        
        try{
            cad= metodo + ".jms.queue";
            var= foreignProp.getValor( cad );
        } catch(Exception ne){
            logger.info(mensajeLog + "No existe Queue JMS para la operacion=" + cad);
            throw new PropertiesException("No existe la siguiente variable en el archivo properties:" + 
                                          cad,ne);
        }
    
        return var;
    }
    
    public static String obtenerJMSReporteErrorQueuePorMetodo(String metodo,
                                                        String mensajeLog) throws PropertiesException{
        
        String cad= null;
        String var= null;
        
        try{
            cad= metodo + ".reporteError.jms.queue";
            var= foreignProp.getValor( cad );
        } catch(Exception ne){
            logger.info(mensajeLog + "No existe Queue JMS de Reporte de Error para la operacion=" + cad);
            throw new PropertiesException("No existe la siguiente variable en el archivo properties:" + 
                                          cad,ne);
        }
    
        return var;
    }
    
    public static long obtenerSnCodeParaDnIdPorTipoPostpago(String tipoPostpago,
                                                        String mensajeLog) throws PropertiesException{
        
        String cad= null;
        long var= 0;
        
        try{
            cad= "crearContrato." + tipoPostpago + ".sncode.con.dnid";
            var= Long.parseLong( foreignProp.getValor( cad ) );
        } catch(Exception ne){
            logger.info(mensajeLog + "No existe dn_id configurado para el tipo postpago:" + cad);
            throw new PropertiesException("No existe dn_id configurado para el tipo postpago en el archivo properties:" + 
                                          cad,ne);
        }
    
        return var;
    }
}
