
package pe.com.claro.eba.activospostpagoejbs.types;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for CambiarPlanType complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType name="CambiarPlanType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="coId" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="nuevoPlan" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="nuevoSpcode" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *         &lt;element name="antiguoSpcode" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "CambiarPlanType",
         propOrder = { "coId", "nuevoPlan", "nuevoSpcode", "antiguoSpcode" })
public class CambiarPlanType implements java.io.Serializable {

    protected long coId;
    protected long nuevoPlan;
    protected Long nuevoSpcode;
    protected Long antiguoSpcode;

    /**
     * Gets the value of the coId property.
     *
     */
    public long getCoId() {
        return coId;
    }

    /**
     * Sets the value of the coId property.
     *
     */
    public void setCoId(long value) {
        this.coId = value;
    }

    /**
     * Gets the value of the nuevoPlan property.
     *
     */
    public long getNuevoPlan() {
        return nuevoPlan;
    }

    /**
     * Sets the value of the nuevoPlan property.
     *
     */
    public void setNuevoPlan(long value) {
        this.nuevoPlan = value;
    }

    /**
     * Gets the value of the nuevoSpcode property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getNuevoSpcode() {
        return nuevoSpcode;
    }

    /**
     * Sets the value of the nuevoSpcode property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setNuevoSpcode(Long value) {
        this.nuevoSpcode = value;
    }

    /**
     * Gets the value of the antiguoSpcode property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getAntiguoSpcode() {
        return antiguoSpcode;
    }

    /**
     * Sets the value of the antiguoSpcode property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setAntiguoSpcode(Long value) {
        this.antiguoSpcode = value;
    }

}
