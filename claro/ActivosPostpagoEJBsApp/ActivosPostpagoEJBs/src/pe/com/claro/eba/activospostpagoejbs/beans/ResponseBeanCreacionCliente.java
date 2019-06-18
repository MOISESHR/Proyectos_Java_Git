package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.List;

import pe.com.claro.ebs.services.activospostpago.ws.types.CuentaMiembroType;


public class ResponseBeanCreacionCliente extends ResponseBean{
    
    private List<CustomerData> listClientesCreados;
    private List<CuentaMiembroType> listCuentasFaltantes;
    
    public void setListClientesCreados(List<CustomerData> listClientesCreados) {
        this.listClientesCreados = listClientesCreados;
    }

    public List<CustomerData> getListClientesCreados() {
        return listClientesCreados;
    }

    public void setListCuentasFaltantes(List<CuentaMiembroType> listCuentasFaltantes) {
        this.listCuentasFaltantes = listCuentasFaltantes;
    }

    public List<CuentaMiembroType> getListCuentasFaltantes() {
        return listCuentasFaltantes;
    }

}
