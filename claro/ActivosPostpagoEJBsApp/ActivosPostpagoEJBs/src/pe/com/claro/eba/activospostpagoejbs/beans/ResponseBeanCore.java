package pe.com.claro.eba.activospostpagoejbs.beans;

import java.math.BigDecimal;


public class ResponseBeanCore {

    private String resultado;
    private String mensaje;
    private Object objetoRespuesta;
    
    private BigDecimal deudaPago;
    private BigDecimal deudaEquipo;
    private BigDecimal montoRAPend;
    private String ejecutaCmd;
    private String dnID;
    private String dnStatus;


    public void setResultado(String resultado) {
        this.resultado = resultado;
    }

    public String getResultado() {
        return resultado;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setObjetoRespuesta(Object objetoRespuesta) {
        this.objetoRespuesta = objetoRespuesta;
    }

    public Object getObjetoRespuesta() {
        return objetoRespuesta;
    }

    public void setDeudaPago(BigDecimal deudaPago) {
        this.deudaPago = deudaPago;
    }

    public BigDecimal getDeudaPago() {
        return deudaPago;
    }

    public void setDeudaEquipo(BigDecimal deudaEquipo) {
        this.deudaEquipo = deudaEquipo;
    }

    public BigDecimal getDeudaEquipo() {
        return deudaEquipo;
    }

    public void setMontoRAPend(BigDecimal montoRAPend) {
        this.montoRAPend = montoRAPend;
    }

    public BigDecimal getMontoRAPend() {
        return montoRAPend;
    }

    public void setEjecutaCmd(String ejecutaCmd) {
        this.ejecutaCmd = ejecutaCmd;
    }

    public String getEjecutaCmd() {
        return ejecutaCmd;
    }

    public void setDnID(String dnID) {
        this.dnID = dnID;
    }

    public String getDnID() {
        return dnID;
    }

    public void setDnStatus(String dnStatus) {
        this.dnStatus = dnStatus;
    }

    public String getDnStatus() {
        return dnStatus;
    }
}