package pe.com.claro.eba.activospostpagoejbs.beans;

import java.io.Serializable;

public class CustomerData implements Serializable{

    @SuppressWarnings("compatibility:-8031495492864546799")
    private static final long serialVersionUID = 1L;
    
    private String custCode;
    private long customerId;

    public void setCustCode(String custCode) {
        this.custCode = custCode;
    }

    public String getCustCode() {
        return custCode;
    }

    public void setCustomerId(long customerId) {
        this.customerId = customerId;
    }

    public long getCustomerId() {
        return customerId;
    }
}
