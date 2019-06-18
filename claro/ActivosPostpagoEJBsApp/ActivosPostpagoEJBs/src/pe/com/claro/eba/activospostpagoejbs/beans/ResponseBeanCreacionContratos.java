package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.List;

import pe.com.claro.ebs.services.activospostpago.ws.types.ContratoType;


public class ResponseBeanCreacionContratos extends ResponseBean{
    
    private List<Long> listaContratosCreados;
    private List<ContratoType> listaContratosFaltantes;

    public void setListaContratosCreados(List<Long> listaContratosCreados) {
        this.listaContratosCreados = listaContratosCreados;
    }

    public List<Long> getListaContratosCreados() {
        return listaContratosCreados;
    }

    public void setListaContratosFaltantes(List<ContratoType> listaContratosFaltantes) {
        this.listaContratosFaltantes = listaContratosFaltantes;
    }

    public List<ContratoType> getListaContratosFaltantes() {
        return listaContratosFaltantes;
    }

}
