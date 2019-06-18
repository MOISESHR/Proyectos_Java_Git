
package pe.com.claro.eba.activospostpagoejbs.types;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ServicioType complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType name="ServicioType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="spcode" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *         &lt;element name="sncode" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ServicioType", propOrder = { "spcode", "sncode" })
public class ServicioType implements java.io.Serializable {

    protected Long spcode;
    protected Long sncode;

    /**
     * Gets the value of the spcode property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getSpcode() {
        return spcode;
    }

    /**
     * Sets the value of the spcode property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setSpcode(Long value) {
        this.spcode = value;
    }

    /**
     * Gets the value of the sncode property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getSncode() {
        return sncode;
    }

    /**
     * Sets the value of the sncode property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setSncode(Long value) {
        this.sncode = value;
    }

}
