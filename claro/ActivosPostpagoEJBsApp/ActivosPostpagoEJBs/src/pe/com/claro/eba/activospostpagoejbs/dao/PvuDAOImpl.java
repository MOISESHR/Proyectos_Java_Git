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
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;


public class PvuDAOImpl implements PvuDAO{
    
    private static Logger logger = Logger.getLogger(PvuDAOImpl.class);
    
    @Autowired
    @Qualifier("pvuDS")
    private DataSource pvuDS;
    
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean actualizarEstadoContrato(String correlativoSisact,
                                                             String custCode,
                                                             String customerId,
                                                             String estadoContrato,
                                                            String usuarioApp,
                                                          String[] coIdsArray,
                                                String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo = mensajeTransaccion + "[actualizarEstadoContrato] ";
        ResponseBean resp= new ResponseBean();
        StringBuffer codIDs= null ;
        
        try{
            codIDs = new StringBuffer();
            for(int i=0;i<coIdsArray.length;i++){
                codIDs.append(i+1+";"+ coIdsArray[i] +"|");
            }
            
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(pvuDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_PVU_OWNER )
                    .withProcedureName( PropertiesInternos.SP_PVU_ACTUALIZAR_ESTADO )
                    .declareParameters(new SqlParameter("P_NRO_CONTRATO", OracleTypes.VARCHAR),
                                new SqlParameter("P_CODIGO_BSCS", OracleTypes.VARCHAR),
                                new SqlParameter("P_CUSTOMER_ID", OracleTypes.VARCHAR),
                                new SqlParameter("P_ESTADO", OracleTypes.VARCHAR),
                                new SqlParameter("P_USUARIO", OracleTypes.VARCHAR),
                                new SqlParameter("P_USUARIO_DES", OracleTypes.VARCHAR),
                                new SqlParameter("P_OBSERVACION", OracleTypes.VARCHAR),
                                new SqlParameter("P_CONTRATO_BSCS", OracleTypes.VARCHAR),
                                new SqlOutParameter("P_RETORNO", OracleTypes.VARCHAR),
                                new SqlOutParameter("P_MENSAJE", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD PVU, con JNDI=" + 
                      PropertiesExternos.DB_PVU_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_PVU_ACTUALIZAR_ESTADO);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "P_NRO_CONTRATO",  correlativoSisact )
                .addValue( "P_CODIGO_BSCS",  custCode )
                .addValue( "P_CUSTOMER_ID",  customerId )
                .addValue( "P_ESTADO",  estadoContrato )
                .addValue( "P_USUARIO",  usuarioApp )
                .addValue( "P_CONTRATO_BSCS", codIDs.toString())
                .addValue( "P_USUARIO_DES", null)
                .addValue( "P_OBSERVACION", null);
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_PVU_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("P_RETORNO") );
            resp.setMensajeRespuesta( (String)resultMap.get("P_MENSAJE") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD PVU al ejecutar SP " + 
                                         PropertiesInternos.SP_PVU_ACTUALIZAR_ESTADO);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD PVU, al ejecutar SP " + 
                                         PropertiesInternos.SP_PVU_ACTUALIZAR_ESTADO);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
}
