package pe.com.claro.eba.activospostpagoejbs.util;

import java.util.HashMap;
import java.util.Map;

import javax.xml.bind.annotation.adapters.XmlAdapter;


public class MapAdapter extends XmlAdapter<MapElements[], Map<String, Object>> {

    public MapElements[] marshal(Map<String, Object> arg0) {
        MapElements[] mapElements = new MapElements[arg0.size()];
        int i = 0;
        for (Map.Entry<String, Object> entry : arg0.entrySet()){
               
            mapElements[i++] =
                new MapElements(entry.getKey(), (entry.getValue() == null) ? null:
                                                              entry.getValue().toString());
        }
        return mapElements;
    }

    public Map<String, Object> unmarshal(MapElements[] arg0) {
        Map<String, Object> r = new HashMap<String, Object>();
        for (MapElements mapelement : arg0)
            r.put(mapelement.campo, mapelement.valor);
        return r;
    }

}
