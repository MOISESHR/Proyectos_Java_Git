package pe.com.claro.eba.activospostpagoejbs.dao;

import java.sql.ResultSet;
import java.sql.SQLException;

import java.sql.Types;

import java.util.List;
import java.util.Map;

import javax.sql.DataSource;

import oracle.jdbc.OracleTypes;

import org.apache.log4j.Logger;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.jdbc.core.SqlOutParameter;
import org.springframework.jdbc.core.SqlParameter;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.jdbc.core.simple.SimpleJdbcCall;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanCore;
import pe.com.claro.eba.activospostpagoejbs.beans.ResponseBeanServicios;
import pe.com.claro.eba.activospostpagoejbs.beans.ServiciosPlanBaseBean;
import pe.com.claro.eba.activospostpagoejbs.dao.mapper.DatosServiciosCorePlanBeanSqlReturnType;
import pe.com.claro.eba.activospostpagoejbs.util.JAXBUtilitarios;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesExternos;
import pe.com.claro.eba.activospostpagoejbs.util.PropertiesInternos;
import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;


public class BscsDAOImpl implements BscsDAO{
    
    private static Logger logger = Logger.getLogger(BscsDAOImpl.class);
    private static final String EJECUTANDO_SP = "EJECUTANDO SP ";
    private static final String PARAMETRO_IN = "PARAMETROS [INPUT]: ";
    private static final String PARAMETRO_OUT = "PARAMETROS [OUPUT]: ";
  
    @Autowired
    @Qualifier("bscsDS")
    private DataSource bscsDS;
    
    
    @Transactional(propagation = Propagation.NOT_SUPPORTED)
    public ResponseBeanServicios validarServiciosAdicionales(List<ServicioContratoType> listaServicios,
                                                    long planTarifario,
                                                String mensajeTransaccion) {
  
        String mensajeTransaccionMetodo = mensajeTransaccion + "[validarServiciosAdicionales] ";
        ResponseBeanServicios resp= new ResponseBeanServicios();
        long tiempoInicio = System.currentTimeMillis();
        logger.info(mensajeTransaccion + "[INICIO] [validarServiciosAdicionales]");
        try{
            ServicioContratoType sc= listaServicios.get(0);
            
            StringBuilder snCodes= new StringBuilder( String.valueOf( sc.getSnCode() ) );
            StringBuilder spCodes= new StringBuilder( String.valueOf( sc.getSpCode() ) );
            
            for(int i=1;i<listaServicios.size();i++){
                sc= listaServicios.get(i);
                snCodes.append( "|" + sc.getSnCode() );
                spCodes.append( "|" + sc.getSpCode() );
            }
            
            SimpleJdbcCall procConsulta1= new SimpleJdbcCall(bscsDS)
                    .withoutProcedureColumnMetaDataAccess()
                    .withSchemaName( PropertiesExternos.DB_BSCS_OWNER )
                    .withProcedureName( PropertiesInternos.SP_VALIDAR_SERVICIOS_BSCS )
                    .declareParameters(new SqlParameter("P_TMCODE", OracleTypes.VARCHAR),
                                new SqlParameter("P_SNCODES", OracleTypes.VARCHAR),
                                new SqlParameter("P_SPCODES", OracleTypes.VARCHAR),
                                new SqlOutParameter("P_CURSOR", OracleTypes.CURSOR,new RowMapper<ServicioContratoType>() {
                                                        public ServicioContratoType mapRow(ResultSet rs, int rowNum)
                                                                throws SQLException {
                                                            ServicioContratoType c= new ServicioContratoType();
                                                            
                                                            c.setSnCode( rs.getLong("SNCODE") );
                                                            c.setSpCode( rs.getLong("SPCODE") );
                                                            
                                                            return c;
                                                        }
                                                    }),
                                new SqlOutParameter("P_RESULTADO", OracleTypes.VARCHAR),
                                new SqlOutParameter("P_MSGERR", OracleTypes.VARCHAR));
                
            logger.debug(mensajeTransaccionMetodo + "Consultando BD BSCS, con JNDI=" + 
                      PropertiesExternos.DB_BSCS_JNDI);
            logger.info(mensajeTransaccionMetodo + "SP a invocar=" + 
                      PropertiesInternos.SP_VALIDAR_SERVICIOS_BSCS);
            logger.info(mensajeTransaccionMetodo + "Datos a enviar:" + 
                        "P_TMCODE=" + planTarifario + 
                        "; P_SNCODES=" + snCodes + "; P_SPCODES=" + spCodes );
                      
            SqlParameterSource objParametrosIN = new MapSqlParameterSource()
                .addValue( "P_TMCODE",  planTarifario )
                .addValue( "P_SNCODES",  snCodes )
                .addValue( "P_SPCODES",  spCodes );
            
            procConsulta1.getJdbcTemplate().setQueryTimeout(PropertiesExternos.DB_BSCS_TIMEOUT_SECONDS);
          
            Map<String,Object> resultMap= procConsulta1.execute(objParametrosIN);
              
            resp.setCodigoRespuesta( (String) resultMap.get("P_RESULTADO") );
            resp.setMensajeRespuesta( (String)resultMap.get("P_MSGERR") );
            
            List<ServicioContratoType> list= (List<ServicioContratoType>) resultMap.get("P_CURSOR");
            resp.setListaServicios( list );
            
            logger.info(mensajeTransaccionMetodo +  "Datos devueltos:" +
                      JAXBUtilitarios.anyObjectToXmlText(resp));
            
        } catch(ArrayIndexOutOfBoundsException e){
            logger.error(mensajeTransaccionMetodo +  "Error con indice accediendo a lista: ", e);
            resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR );
            resp.setMensajeRespuesta("Error con indice accediendo a lista");
        } catch(Exception e){
            logger.error(mensajeTransaccionMetodo +  "Error accediendo a BD: ", e);
            resp.setCodigoRespuesta( PropertiesInternos.CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO );
            resp.setMensajeRespuesta("Error en conexion a BD BSCS");
        }
        logger.info(mensajeTransaccion + "[FIN] [validarServiciosAdicionales] Tiempo del Proceso :"+(System.currentTimeMillis()-tiempoInicio)+" milisegundos");
        return resp;
    }
    
  public ResponseBeanCore buscarServCoreXPlan(String msgMet, int codPlanTarif){
    
    String cadenaMensaje = msgMet + "[BuscarServCoreXPlan]";
    
    this.logger.info( cadenaMensaje + "[INICIO] - METODO: [BuscarServCoreXPlan - DAO] " );
    
    JdbcTemplate objJdbcTemplate            = null;
    ResponseBeanCore objDataBaseServiciosPlan = null;
    SimpleJdbcCall objJdbcCall              = null;  
    String OWNER                            = null;
    String PROCEDURE                        = null;
    long   tiempoInicio                     = System.currentTimeMillis();
    
    try{
        
        //bscsDS.setLoginTimeout(PropertiesExternos.DB_BSCS_TIMEOUT_SECONDS);
                    
        this.logger.info( cadenaMensaje + "DATA SOURCE (JNDI): [" + PropertiesExternos.DB_BSCS_JNDI + "]" );
       
        OWNER     = PropertiesExternos.DB_BSCS_OWNER;
        PROCEDURE = PropertiesInternos.SpBuscarServCoreXPlan;            
        
        this.logger.info( cadenaMensaje + EJECUTANDO_SP+" [" + OWNER + "." + PROCEDURE + "]" );                          
         
        SqlParameterSource objParametrosIN = new MapSqlParameterSource()                                      
            .addValue( "P_TMCODE", codPlanTarif, Types.INTEGER );
                    
        this.logger.info( cadenaMensaje + PARAMETRO_IN + "P_TMCODE: [" + codPlanTarif + "]" );
        
        objJdbcCall = new SimpleJdbcCall( this.bscsDS );
        objJdbcTemplate = objJdbcCall.getJdbcTemplate();
        objJdbcTemplate.setQueryTimeout( PropertiesExternos.DB_BSCS_TIMEOUT_SECONDS );
        
        Map<String, Object> objParametrosOUT = objJdbcCall
         .withoutProcedureColumnMetaDataAccess()
         .withSchemaName(  OWNER )
         .withProcedureName( PROCEDURE )     
         .declareParameters(                                                                      
                             new SqlParameter( "P_TMCODE", Types.INTEGER ),                                 

                             new SqlOutParameter( "P_RESULTADO", Types.VARCHAR ),
                             new SqlOutParameter( "P_MSGERR", Types.VARCHAR ),
                             new SqlOutParameter( "P_CURSOR", oracle.jdbc.OracleTypes.CURSOR, null, new DatosServiciosCorePlanBeanSqlReturnType())
                            )
            
        .execute( objParametrosIN ); 
        
        this.logger.info( cadenaMensaje +PARAMETRO_OUT +" [" + objParametrosOUT + "]" );
         
        List<ServiciosPlanBaseBean> listaOUT = (List<ServiciosPlanBaseBean>)objParametrosOUT.get( "P_CURSOR" );            
        String P_RESULTADO  = (String)objParametrosOUT.get( "P_RESULTADO" );                 
        String P_MSGERR  = (String)objParametrosOUT.get( "P_MSGERR" );                             

        this.logger.info( cadenaMensaje + PARAMETRO_OUT +"P_RESULTADO : ["+ P_RESULTADO + "]" );                  
        this.logger.info( cadenaMensaje + PARAMETRO_OUT +"P_DN_STATUS : ["+ P_MSGERR + "]" ); 
        this.logger.info( cadenaMensaje +PARAMETRO_OUT+ "P_CURSOR [TAMAÑO]: [" + listaOUT.size() + "]" );

        objDataBaseServiciosPlan = new ResponseBeanCore();            
        objDataBaseServiciosPlan.setObjetoRespuesta(null);
        
         if( (listaOUT != null) && !listaOUT.isEmpty() ){                        
              objDataBaseServiciosPlan.setObjetoRespuesta( listaOUT );
         }
         else{
             objDataBaseServiciosPlan.setObjetoRespuesta(null);
         }            
        
    } catch( Exception e ){
         this.logger.error( cadenaMensaje + "ERROR: [Exception] - [" + e.getMessage() + "] ", e );
         
    }finally{
         this.logger.info( cadenaMensaje + "Tiempo TOTAL Proceso: [" + (System.currentTimeMillis()-tiempoInicio) + " milisegundos ]" );
         this.logger.info( cadenaMensaje + "[FIN] - METODO: [BuscarServCoreXPlan  - DAO] " );
    }
    return objDataBaseServiciosPlan;
  }
}
