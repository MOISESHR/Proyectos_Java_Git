package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.ArrayList;
import java.util.List;

public class ResponseBeanCambiarPlanCMS extends ResponseBean {
    
    private List<Servicio> serviciosEliminados;
    private List<Servicio> serviciosAgregados;
    
    public ResponseBeanCambiarPlanCMS() {
        super();
    }


    public void setServiciosEliminados(List<Servicio> serviciosEliminados) {
        this.serviciosEliminados = serviciosEliminados;
    }

    public List<Servicio> getServiciosEliminados() {
        if(serviciosEliminados==null)serviciosEliminados=new ArrayList<Servicio>();
        return serviciosEliminados;
    }

    public void setServiciosAgregados(List<Servicio> serviciosAgregados) {
        this.serviciosAgregados = serviciosAgregados;
    }

    public List<Servicio> getServiciosAgregados() {
        if(serviciosAgregados==null)serviciosAgregados=new ArrayList<Servicio>();
        return serviciosAgregados;
    }
}
