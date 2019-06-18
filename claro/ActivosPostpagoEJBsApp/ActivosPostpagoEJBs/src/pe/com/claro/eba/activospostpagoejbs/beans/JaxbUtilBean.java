package pe.com.claro.eba.activospostpagoejbs.beans;

import java.util.Map;

import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.adapters.XmlJavaTypeAdapter;

import pe.com.claro.eba.activospostpagoejbs.util.MapAdapter;


@XmlRootElement
public class JaxbUtilBean {
    
    private Map objectSP;

    public JaxbUtilBean(){
      objectSP= null;
    }

    public JaxbUtilBean(Map objectSP) {
        this.objectSP = objectSP;
    }

    public void setObjectSP(Map objectSP) {
        this.objectSP = objectSP;
    }

    @XmlJavaTypeAdapter(MapAdapter.class)
    public Map getObjectSP() {
        return objectSP;
    }
}
