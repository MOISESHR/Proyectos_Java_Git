package pe.com.claro.eba.activospostpagoejbs.beans;


public class ResponseBeanCMS extends ResponseBean{
    
    private long coId;
    private String objectId;
    private CustomerData customerData;

    public void setCoId(long coId) {
        this.coId = coId;
    }

    public long getCoId() {
        return coId;
    }

    public void setObjectId(String objectId) {
        this.objectId = objectId;
    }

    public String getObjectId() {
        return objectId;
    }

    public void setCustomerData(CustomerData customerData) {
        this.customerData = customerData;
    }

    public CustomerData getCustomerData() {
        return customerData;
    }


}
