package pe.com.claro.eba.activospostpagoejbs.dao;

import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.SQLTimeoutException;

import java.util.Map;

import javax.sql.DataSource;

import oracle.jdbc.OracleCallableStatement;
import oracle.jdbc.OracleTypes;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.core.NestedRuntimeException;
import org.springframework.jdbc.core.SqlOutParameter;
import org.springframework.jdbc.core.SqlParameter;
import org.springframework.jdbc.core.SqlTypeValue;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.jdbc.core.simple.SimpleJdbcCall;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import pe.com.claro.eba.activospostpagoejbs.beans.BeanInsertarCoId;
import pe.com.claro.eba.activospostpagoejbs.beans.JaxbUtilBean;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBean;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;


public class SgaDAOImpl implements SgaDAO{
    
    private static Logger logger = Logger.getLogger(SgaDAOImpl.class);
    
    @Autowired
    @Qualifier("sgaDS")
    private DataSource sgaDS;
    
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean actualizarSOT(final String[] codigosSot,
                                                             String customerId,
                                                             String coId,
                                                String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo = mensajeTransaccion + "[actualizarSOT] ";
        ResponseBean resp= new ResponseBean();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(sgaDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_SGA_OWNER )
                    .withProcedureName( PropertiesInternos.SP_SGA_ACTUALIZAR_SOT )
                    .declareParameters(new SqlParameter("p_cod_sot_list", OracleTypes.ARRAY,
                                                        "cod_sot_type"),
                                new SqlParameter("pc_customerID", OracleTypes.VARCHAR),
                                new SqlParameter("pc_codID", OracleTypes.VARCHAR),
                                new SqlOutParameter("pc_error_code", OracleTypes.VARCHAR),
                                new SqlOutParameter("pc_mensaje", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD SGA, con JNDI=" + 
                      PropertiesExternos.DB_SGA_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_SGA_ACTUALIZAR_SOT);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "p_cod_sot_list",  new SqlTypeValue() {
                                public void setTypeValue(PreparedStatement cs, int index, 
                                                         int sqlType, String typeName) throws SQLException {
                                        //Connection con = cs.getConnection();
                                        OracleCallableStatement ocp= (OracleCallableStatement) cs;
                                        ocp.setPlsqlIndexTable(index, codigosSot, 
                                                               codigosSot.length, 
                                                               codigosSot.length, 
                                                               OracleTypes.NUMBER, 0);
                                        /*
                                        ArrayDescriptor des = ArrayDescriptor.createDescriptor("cod_sot_type", 
                                                                                               con);
                                        ARRAY a = new ARRAY(des, con, codigosSot);
                                        cs.setObject(index, a); */
                                }
                                public String toString(){
                                    
                                    StringBuilder cad= new StringBuilder( codigosSot[0] );
                                    for(int i=1;i<codigosSot.length;i++){
                                        cad.append("|" + codigosSot[i] );
                                    }
                                    return cad.toString();
                                }
                                } )
                .addValue( "pc_customerID",  customerId )
                .addValue( "pc_codID",  coId );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_SGA_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("pc_error_code") );
            resp.setMensajeRespuesta( (String)resultMap.get("pc_mensaje") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD SGA al ejecutar SP " + 
                                         PropertiesInternos.SP_SGA_ACTUALIZAR_SOT);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD SGA, al ejecutar SP " + 
                                         PropertiesInternos.SP_SGA_ACTUALIZAR_SOT);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBean actualizarSOTContrato(String codigoSot,
                                                             String customerId,
                                                             String coId,
                                                String mensajeTransaccion) {
    
        String mensajeTransaccionMetodo = mensajeTransaccion + "[actualizarSOT] ";
        ResponseBean resp= new ResponseBean();
        
        try{
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(sgaDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_SGA_OWNER_OP )
                    .withProcedureName( PropertiesInternos.SP_SGA_ACTUALIZAR_SOT_CONTRATO )
                    .declareParameters(new SqlParameter("p_cod_sot", OracleTypes.NUMBER),
                                new SqlParameter("p_customer_id", OracleTypes.VARCHAR),
                                new SqlParameter("p_cod_id", OracleTypes.VARCHAR),
                                new SqlOutParameter("p_error_code", OracleTypes.VARCHAR),
                                new SqlOutParameter("p_mensaje", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD SGA, con JNDI=" + 
                      PropertiesExternos.DB_SGA_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar = " + PropertiesExternos.DB_SGA_OWNER_OP + "."+
                      PropertiesInternos.SP_SGA_ACTUALIZAR_SOT_CONTRATO);
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "p_cod_sot", codigoSot)
                .addValue( "p_customer_id",  customerId )
                .addValue( "p_cod_id",  coId );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_SGA_TIMEOUT_SECONDS);
          
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                    JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("p_error_code") );
            resp.setMensajeRespuesta( (String)resultMap.get("p_mensaje") );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(NestedRuntimeException e){
              logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
              
              if( e.contains( SQLTimeoutException.class ) ){
                resp.setMensajeRespuesta("Error de timeout en BD SGA al ejecutar SP " + 
                                         PropertiesInternos.SP_SGA_ACTUALIZAR_SOT);
              } else{
                resp.setMensajeRespuesta("Error en conexion a BD SGA, al ejecutar SP " + 
                                         PropertiesInternos.SP_SGA_ACTUALIZAR_SOT);
              }
              resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
        } 
        return resp;
    }
    
  @Transactional(propagation = Propagation.NOT_SUPPORTED)
  public ResponseBean insertarCoid(BeanInsertarCoId bean,
                                   String mensajeTransaccion) {
  
      String mensajeTransaccionMetodo = mensajeTransaccion + "[insertarCoid] ";
      ResponseBean resp= new ResponseBean();
      
      try{
          SimpleJdbcCall procConsulta1= new SimpleJdbcCall(sgaDS)
                  .withoutProcedureColumnMetaDataAccess()
                  .withSchemaName( PropertiesExternos.DB_SGA_OWNER_OP )
                  .withProcedureName( PropertiesInternos.SP_SGA_INSERTAR_COID )
                  .declareParameters(
                              new SqlParameter("p_cod_sot", OracleTypes.NUMBER),
                              new SqlParameter("p_customer_id", OracleTypes.NUMBER),
                              new SqlParameter("p_cod_id", OracleTypes.VARCHAR),
                              new SqlParameter("p_error", OracleTypes.NUMBER),
                              new SqlParameter("p_msg_error", OracleTypes.VARCHAR),
                              new SqlOutParameter("p_error_code", OracleTypes.VARCHAR),
                              new SqlOutParameter("p_mensaje", OracleTypes.VARCHAR));
              
          logger.debug(mensajeTransaccionMetodo + "Insertando BD SGA, con JNDI=" + 
                    PropertiesExternos.DB_SGA_JNDI);
          logger.info(mensajeTransaccionMetodo + "SP a invocar = " +PropertiesExternos.DB_SGA_OWNER_OP + "." +
                    PropertiesInternos.SP_SGA_INSERTAR_COID);
                    
          SqlParameterSource objParametrosIN = new MapSqlParameterSource()
              .addValue( "p_cod_sot",  bean.getCodigosSot() )
              .addValue( "p_customer_id",  bean.getCustomerId() )
              .addValue( "p_cod_id",  bean.getCoId() )
              .addValue( "p_error",  bean.getEtapa() )
              .addValue( "p_msg_error",  bean.getDesError() );
          
          procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_SGA_TIMEOUT_SECONDS);
        
          logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                  JAXBUtilitarios.anyObjectToXmlText(  new JaxbUtilBean(((MapSqlParameterSource) objParametrosIN).getValues()) ) );
        
          Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
            
          resp.setCodigoRespuesta( (String) resultMap.get("p_error_code") );
          resp.setMensajeRespuesta( (String)resultMap.get("p_mensaje") );
          
          logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                    JAXBUtilitarios.anyObjectToXmlText(resp));
          
      } catch(NestedRuntimeException e){
            logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD:", e);
            
            if( e.contains( SQLTimeoutException.class ) ){
              resp.setMensajeRespuesta("Error de timeout en BD SGA al ejecutar SP " + 
                                       PropertiesInternos.SP_SGA_INSERTAR_COID);
            } else{
              resp.setMensajeRespuesta("Error en conexion a BD SGA, al ejecutar SP " + 
                                       PropertiesInternos.SP_SGA_INSERTAR_COID);
            }
            resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
      } 
      return resp;
  }
  
  public boolean existeSOT(String mensaje, String codSOT) {
      
      String men = mensaje + "[existeSOT] - ";
      Boolean existe = false;
      logger.info(men +  "Inicio del metodo existeSOT" );
      
      java.sql.Connection conn = null;
      java.sql.CallableStatement cst = null;
    
      try{
          String sp = PropertiesExternos.DB_SGA_OWNER + "." + PropertiesInternos.FUNCTION_SGA_EXISTE_SOT;
          conn = sgaDS.getConnection();
          cst = conn.prepareCall("begin ? := case " + sp + "(?) when true then 1 else 0 end; end;");
          logger.info(men+"Ejecutando el SP " + sp +
                     " Datos de entrada: " );
          cst.registerOutParameter(1, OracleTypes.INTEGER);
          logger.info(men + "p_codsolot " + Integer.parseInt(codSOT));
          cst.setInt(2, Integer.parseInt(codSOT));
          
          cst.setQueryTimeout(PropertiesExternos.DB_SGA_TIMEOUT_SECONDS);
          cst.execute();
          logger.info(men+"Datos de salida: " + cst.getInt(1));
          logger.info(men+"existe: " + cst.getInt(1));
          if (cst.getInt(1) > Integer.parseInt(PropertiesInternos.CODIGO_ESTANDAR_EXITO)){
              existe = true;
          }
      
      } catch(Exception e){
          logger.error(men +  "Error accediendo a BD:", e);
      }finally{
          try{
              if(cst!=null){
                  cst.close();
              }
              if(conn!=null){
                  conn.close();
              }
          }catch(Exception e){
              logger.error(men+"--------- < Error -  "+e+" > ----------",e);
          }
      }
      logger.info(men + "existe: " + existe);
      logger.info(men +  "Fin del metodo existeSOT" );
      return existe;
  }
}
