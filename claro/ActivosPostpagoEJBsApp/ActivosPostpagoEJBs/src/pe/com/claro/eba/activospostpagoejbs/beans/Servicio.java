package pe.com.claro.eba.activospostpagoejbs.beans;

public class Servicio implements java.io.Serializable {
    
    private long spCode;
    private long snCode;
    
    public Servicio() {
        super();
    }

    public void setSpCode(long spCode) {
        this.spCode = spCode;
    }

    public long getSpCode() {
        return spCode;
    }

    public void setSnCode(long snCode) {
        this.snCode = snCode;
    }

    public long getSnCode() {
        return snCode;
    }
}
