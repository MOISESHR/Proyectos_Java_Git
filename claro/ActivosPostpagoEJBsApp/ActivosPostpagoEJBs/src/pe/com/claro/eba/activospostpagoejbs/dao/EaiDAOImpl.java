package pe.com.claro.eba.activospostpagoejbs.dao;

import java.sql.SQLTimeoutException;

import java.util.Map;

import javax.sql.DataSource;

import oracle.jdbc.OracleTypes;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.core.NestedRuntimeException;
import org.springframework.jdbc.core.SqlOutParameter;
import org.springframework.jdbc.core.SqlParameter;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.jdbc.core.simple.SimpleJdbcCall;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import pe.com.claro.eba.activospostpagoejbs.beans.JaxbUtilBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanTxEai;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;


public class EaiDAOImpl implements EaiDAO {

    private static Logger logger = Logger.getLogger(EaiDAOImpl.class);

    @Autowired
    @Qualifier("eaiDS")
    private DataSource eaiDS;


    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanTxEai registrarTransaccion(ActivosPostpagoBean activosBean,
                                                String mensajeTransaccion) {

        String mensajeTransaccionMetodo = mensajeTransaccion + "[registrarTransaccion] ";
        ResponseBeanTxEai resp= new ResponseBeanTxEai();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(eaiDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_EAI_OWNER )
                    .withProcedureName( PropertiesInternos.SP_EAI_REGISTRAR_TX )
                    .declareParameters(new SqlParameter("in_id_tx_app", OracleTypes.VARCHAR),
                                new SqlParameter("in_aplicacion", OracleTypes.VARCHAR),
                                new SqlParameter("in_ip_aplicacion", OracleTypes.VARCHAR),
                                new SqlParameter("in_usr_aplicacion", OracleTypes.VARCHAR),
                                new SqlParameter("in_tipo_postpago", OracleTypes.VARCHAR),
                                new SqlParameter("in_operacion_origen", OracleTypes.VARCHAR),
                                new SqlParameter("in_request", OracleTypes.CLOB),
                                new SqlOutParameter("out_cod_rpta", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_msg_rpta", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_id_tx_interno", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD EAI, con JNDI=" + 
                      PropertiesExternos.DB_EAI_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_EAI_REGISTRAR_TX);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "in_id_tx_app",  activosBean.getIdTransaccion() )
                .addValue( "in_aplicacion",  activosBean.getNombreSistemaOrigen() )
                .addValue( "in_ip_aplicacion",  activosBean.getIpSistemaOrigen() )
                .addValue( "in_usr_aplicacion",  activosBean.getUsrAplicacion() )
                .addValue( "in_tipo_postpago",  activosBean.getTipoPostpago() )
                .addValue( "in_operacion_origen",  activosBean.getMetodoOrigen() )
                .addValue( "in_request",  JAXBUtilitarios.anyObjectToXmlText( activosBean ) );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_EAI_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("out_cod_rpta") );
            resp.setMensajeRespuesta( (String)resultMap.get("out_msg_rpta") );
            resp.setIdTransaccionInterno( (String)resultMap.get("out_id_tx_interno") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD EAI al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_REGISTRAR_TX);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD EAI, al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_REGISTRAR_TX);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean actualizarErrorTransaccion(ActivosPostpagoBean activosBean,
                                                String mensajeTransaccion) {

        String mensajeTransaccionMetodo = mensajeTransaccion + "[actualizarErrorTransaccion] ";
        ResponseBean resp= new ResponseBean();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(eaiDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_EAI_OWNER )
                    .withProcedureName( PropertiesInternos.SP_EAI_ACTUALIZAR_ERROR_TX )
                    .declareParameters(new SqlParameter("in_id_tx_interno", OracleTypes.VARCHAR),
                                new SqlParameter("in_etapa_error", OracleTypes.VARCHAR),
                                new SqlParameter("in_descripcion_error", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_cod_rpta", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_msg_rpta", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD EAI, con JNDI=" + 
                      PropertiesExternos.DB_EAI_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_EAI_ACTUALIZAR_ERROR_TX);
            
            String etapa= activosBean.getDatosError().getEtapaError();
            String descripcion= activosBean.getDatosError().getDescripcionError();
            
            if( etapa.length()>PropertiesExternos.EAI_ETAPA_ERROR_MAX_LENGHT )
                etapa= etapa.substring(0, PropertiesExternos.EAI_ETAPA_ERROR_MAX_LENGHT);
            
            if( descripcion.length()>PropertiesExternos.EAI_DESCRIPCION_ERROR_MAX_LENGHT )
                descripcion= descripcion.substring(0, PropertiesExternos.EAI_DESCRIPCION_ERROR_MAX_LENGHT);
            
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "in_id_tx_interno",  activosBean.getIdTransaccionInternoDB() )
                .addValue( "in_etapa_error", etapa )
                .addValue( "in_descripcion_error", descripcion );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_EAI_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("out_cod_rpta") );
            resp.setMensajeRespuesta( (String)resultMap.get("out_msg_rpta") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD EAI al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_ACTUALIZAR_ERROR_TX);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD EAI, al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_ACTUALIZAR_ERROR_TX);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean finalizarTransaccion(ActivosPostpagoBean activosBean,
                                                String mensajeTransaccion) {

        String mensajeTransaccionMetodo = mensajeTransaccion + "[finalizarTransaccion] ";
        ResponseBean resp= new ResponseBean();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(eaiDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_EAI_OWNER )
                    .withProcedureName( PropertiesInternos.SP_EAI_FINALIZAR_TX )
                    .declareParameters(new SqlParameter("in_id_tx_interno", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_cod_rpta", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_msg_rpta", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD EAI, con JNDI=" + 
                      PropertiesExternos.DB_EAI_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_EAI_FINALIZAR_TX);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "in_id_tx_interno",  activosBean.getIdTransaccionInternoDB() );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_EAI_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("out_cod_rpta") );
            resp.setMensajeRespuesta( (String)resultMap.get("out_msg_rpta") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD EAI al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_FINALIZAR_TX);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD EAI, al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_FINALIZAR_TX);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean archivarTransaccion(ActivosPostpagoBean activosBean,
                                                String mensajeTransaccion) {
  
        String mensajeTransaccionMetodo = mensajeTransaccion + "[archivarTransaccion] ";
        ResponseBean resp= new ResponseBean();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(eaiDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_EAI_OWNER )
                    .withProcedureName( PropertiesInternos.SP_EAI_ARCHIVAR_TX )
                    .declareParameters(new SqlParameter("in_id_tx_interno", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_cod_rpta", OracleTypes.VARCHAR),
                                new SqlOutParameter("out_msg_rpta", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD EAI, con JNDI=" + 
                      PropertiesExternos.DB_EAI_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_EAI_ARCHIVAR_TX);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "in_id_tx_interno",  activosBean.getIdTransaccionInternoDB() );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_EAI_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("out_cod_rpta") );
            resp.setMensajeRespuesta( (String)resultMap.get("out_msg_rpta") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD EAI al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_ARCHIVAR_TX);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD EAI, al ejecutar SP " + 
                                         PropertiesInternos.SP_EAI_ARCHIVAR_TX);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    
}
