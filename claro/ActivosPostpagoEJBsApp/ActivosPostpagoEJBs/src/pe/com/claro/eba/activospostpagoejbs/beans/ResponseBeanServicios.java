package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.List;

import pe.com.claro.ebs.services.activospostpago.ws.types.ServicioContratoType;

public class ResponseBeanServicios extends ResponseBean{
    
    private List<ServicioContratoType> listaServicios;

    public void setListaServicios(List<ServicioContratoType> listaServicios) {
        this.listaServicios = listaServicios;
    }

    public List<ServicioContratoType> getListaServicios() {
        return listaServicios;
    }
}
