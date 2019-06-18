package pe.com.claro.eba.activospostpagoejbs.dao.mapper;

import java.sql.CallableStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import org.apache.log4j.Logger;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.jdbc.core.SqlReturnType;

import pe.com.claro.eba.activospostpagoejbs.beans.ServiciosPlanBaseBean;


/**
 * @author emoreno.
 * @clase: DatosProductoBeanSqlReturnType.java
 * @author_company: nombre de la compañía del autor.
 * @fecha_de_creación: dd-mm-yyyy.
 * @fecha_de_ultima_actualización: dd-mm-yyyy.
 * @versión 1.0
 */
 public class DatosServiciosCorePlanBeanSqlReturnType implements SqlReturnType{
  
    private final Logger logger = Logger.getLogger( this.getClass().getName() );
     
   /**
    * mapRow
    * @param  rs
    * @param  numeroFila 
    * @return ServicioLlamadasBean
    */	 
    @SuppressWarnings( "rawtypes" )
    private RowMapper rowMapper = new RowMapper<ServiciosPlanBaseBean>(){
        
            public ServiciosPlanBaseBean mapRow( ResultSet rs, int numeroFila ) throws SQLException{
                
                ServiciosPlanBaseBean objBean = new ServiciosPlanBaseBean();
      
                 objBean.setSnCode(rs.getString("SNCODE"));
                 objBean.setSpCode(rs.getString("SPCODE"));                                        
                 return objBean;
         }
     };
	
   /**
    * getTypeValue
    * @param  cs
    * @param  ix 
    * @param  sqlType 
    * @param  typeName 
    * @return ServicioLlamadasBean
    */
    @SuppressWarnings( { "rawtypes", "unchecked" } )
    @Override
    public Object getTypeValue( CallableStatement cs, int ix, int sqlType, String typeName ) throws SQLException{
        
        ResultSet rs    = null;
        
        try{
            rs    = (ResultSet)cs.getObject( ix );
            List      lista = new ArrayList(); 
            
            for( int i=0; rs.next(); i++ ){
                 lista.add( rowMapper.mapRow( rs, i ) );
            }
            
            return lista;
        }
        catch( SQLException e ){               
               this.logger.info( "ERROR [SQLException]: " + e.getMessage() );
               
               if( (e.getMessage() != null) && 
                   (e.getMessage().startsWith( "Cursor is closed" ) ) ){
                   this.logger.info( "[Cursor is closed]: " );
                   
                   return new ArrayList();
               }
               else{
                    throw e;
               }
        }finally{
            if(rs!=null){
                rs.close();
            }    
        }
    }
	
 }
