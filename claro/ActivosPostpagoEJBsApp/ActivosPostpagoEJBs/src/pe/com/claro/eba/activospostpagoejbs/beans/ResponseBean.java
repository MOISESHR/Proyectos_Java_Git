package pe.com.claro.eba.activospostpagoejbs.beans;

import java.io.Serializable;

import pe.com.claro.ebs.services.activospostpago.beans.ErrorType;

public class ResponseBean implements Serializable {

    @SuppressWarnings("compatibility:605756171619530851")
    private static final long serialVersionUID = 1L;

    private String codigoRespuesta;
    private String mensajeRespuesta;
    private ErrorType errorData;
    

    public String getCodigoRespuesta() {
        return codigoRespuesta;
    }

    public void setCodigoRespuesta(String codigoRespuesta) {
        this.codigoRespuesta = codigoRespuesta;
    }

    public String getMensajeRespuesta() {
        return mensajeRespuesta;
    }

    public void setMensajeRespuesta(String mensajeRespuesta) {
        this.mensajeRespuesta = mensajeRespuesta;
    }

    public void setErrorData(ErrorType errorData) {
        this.errorData = errorData;
    }

    public ErrorType getErrorData() {
        return errorData;
    }
}
