
package pe.com.claro.eba.activospostpagoejbs.types;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ListaServicioType complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType name="ListaServicioType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="servicio" type="{http://claro.com.pe/eai/ebs/ws/ActivosPostpagoEJB}ServicioType" maxOccurs="unbounded" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ListaServicioType", propOrder = { "servicio" })
public class ListaServicioType implements java.io.Serializable {

    protected List<ServicioType> servicio;

    /**
     * Gets the value of the servicio property.
     *
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the servicio property.
     *
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getServicio().add(newItem);
     * </pre>
     *
     *
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link ServicioType }
     *
     *
     */
    public List<ServicioType> getServicio() {
        if (servicio == null) {
            servicio = new ArrayList<ServicioType>();
        }
        return this.servicio;
    }

}
