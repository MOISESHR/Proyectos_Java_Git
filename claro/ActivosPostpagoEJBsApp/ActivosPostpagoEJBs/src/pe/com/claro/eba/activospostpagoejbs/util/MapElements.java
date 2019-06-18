package pe.com.claro.eba.activospostpagoejbs.util;

import javax.xml.bind.annotation.XmlAttribute;

class MapElements
{
  @XmlAttribute public String  campo;
  @XmlAttribute public String valor;
  
  private MapElements() {} //Required by JAXB

  public MapElements(String key, String value)
  {
    this.campo   = key;
    this.valor = value;
  }
}