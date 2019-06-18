package pe.com.claro.eba.activospostpagoejbs.util;

import pe.com.claro.ebs.services.activospostpago.beans.ErrorType;

public class ActivosFlowException extends RuntimeException{

    @SuppressWarnings("compatibility:-112299979700883555")
    private static final long serialVersionUID = 1L;

    private ErrorType errorData;

    public ActivosFlowException(){
        super();
    }
    
    public ActivosFlowException(Exception e){
        super(e);
    }
    
    public ActivosFlowException(String message){
        super(message);
    }
    
    public ActivosFlowException(String message,Exception e){
        super(message,e);
    }
    
    public ActivosFlowException(ErrorType errorData){
        super(errorData.getDescripcionError());
        this.errorData= errorData;
    }

    public void setErrorData(ErrorType errorData) {
        this.errorData = errorData;
    }

    public ErrorType getErrorData() {
        return errorData;
    }
}
