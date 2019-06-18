package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.List;

import pe.com.claro.ebs.services.activospostpago.ws.types.DispositivoType;

public class ResponseBeanAgregaDispositivos extends ResponseBean{
    
    private List<DispositivoType> listaDispositivosFaltantes;


    public void setListaDispositivosFaltantes(List<DispositivoType> listaDispositivosFaltantes) {
        this.listaDispositivosFaltantes = listaDispositivosFaltantes;
    }

    public List<DispositivoType> getListaDispositivosFaltantes() {
        return listaDispositivosFaltantes;
    }
}
