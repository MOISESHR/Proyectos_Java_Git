package pe.com.claro.eba.activospostpagoejbs.beans;

public class ResponseBeanTxEai extends ResponseBean{
    
    private String idTransaccionInterno;


    public void setIdTransaccionInterno(String idTransaccionInterno) {
        this.idTransaccionInterno = idTransaccionInterno;
    }

    public String getIdTransaccionInterno() {
        return idTransaccionInterno;
    }
}
