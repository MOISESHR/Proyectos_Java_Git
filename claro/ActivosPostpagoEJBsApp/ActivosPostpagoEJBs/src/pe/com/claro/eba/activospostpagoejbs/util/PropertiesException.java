package pe.com.claro.eba.activospostpagoejbs.util;

public class PropertiesException extends RuntimeException{
    
    public PropertiesException(){
        super();
    }
    
    public PropertiesException(Exception e){
        super(e);
    }
    
    public PropertiesException(String message){
        super(message);
    }
    
    public PropertiesException(String message,Exception e){
        super(message,e);
    }
}
