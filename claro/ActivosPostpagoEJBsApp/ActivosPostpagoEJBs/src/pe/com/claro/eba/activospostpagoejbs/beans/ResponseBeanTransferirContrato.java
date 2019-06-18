package pe.com.claro.eba.activospostpagoejbs.beans;

public class ResponseBeanTransferirContrato extends ResponseBean {
    
    private long coId;
    
    public ResponseBeanTransferirContrato() {
        super();
    }

    public void setCoId(long coId) {
        this.coId = coId;
    }

    public long getCoId() {
        return coId;
    }
}
