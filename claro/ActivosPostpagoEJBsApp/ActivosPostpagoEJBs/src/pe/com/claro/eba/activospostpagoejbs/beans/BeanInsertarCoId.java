package pe.com.claro.eba.activospostpagoejbs.beans;

import java.io.Serializable;

public class BeanInsertarCoId implements Serializable{

    @SuppressWarnings("compatibility:6890013806202461907")
    private static final long serialVersionUID = 1L;
    private String codigosSot;
    private String customerId;
    private String coId;
    private String etapa;
    private String desError;

    public void setCodigosSot(String codigosSot) {
        this.codigosSot = codigosSot;
    }

    public String getCodigosSot() {
        return codigosSot;
    }

    public void setCustomerId(String customerId) {
        this.customerId = customerId;
    }

    public String getCustomerId() {
        return customerId;
    }

    public void setCoId(String coId) {
        this.coId = coId;
    }

    public String getCoId() {
        return coId;
    }

    public void setEtapa(String etapa) {
        this.etapa = etapa;
    }

    public String getEtapa() {
        return etapa;
    }

    public void setDesError(String desError) {
        this.desError = desError;
    }

    public String getDesError() {
        return desError;
    }
}
