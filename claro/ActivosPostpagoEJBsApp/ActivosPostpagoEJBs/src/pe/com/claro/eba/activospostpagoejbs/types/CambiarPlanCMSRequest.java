
package pe.com.claro.eba.activospostpagoejbs.types;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="idTransaccion" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="nombreAplicacion" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="ipAplicacion" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="usuarioAplicacion" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="cambioPlan" type="{http://claro.com.pe/eai/ebs/ws/ActivosPostpagoEJB}CambiarPlanType"/>
 *         &lt;element name="servicios" type="{http://claro.com.pe/eai/ebs/ws/ActivosPostpagoEJB}ListaServicioType"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "",
         propOrder = { "idTransaccion", "nombreAplicacion", "ipAplicacion",
                       "usuarioAplicacion", "cambioPlan", "servicios" })
@XmlRootElement(name = "CambiarPlanCMSRequest")
public class CambiarPlanCMSRequest implements java.io.Serializable {

    @XmlElement(required = true)
    protected String idTransaccion;
    @XmlElement(required = true)
    protected String nombreAplicacion;
    @XmlElement(required = true)
    protected String ipAplicacion;
    @XmlElement(required = true)
    protected String usuarioAplicacion;
    @XmlElement(required = true)
    protected CambiarPlanType cambioPlan;
    @XmlElement(required = true)
    protected ListaServicioType servicios;

    /**
     * Gets the value of the idTransaccion property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getIdTransaccion() {
        return idTransaccion;
    }

    /**
     * Sets the value of the idTransaccion property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setIdTransaccion(String value) {
        this.idTransaccion = value;
    }

    /**
     * Gets the value of the nombreAplicacion property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getNombreAplicacion() {
        return nombreAplicacion;
    }

    /**
     * Sets the value of the nombreAplicacion property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setNombreAplicacion(String value) {
        this.nombreAplicacion = value;
    }

    /**
     * Gets the value of the ipAplicacion property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getIpAplicacion() {
        return ipAplicacion;
    }

    /**
     * Sets the value of the ipAplicacion property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setIpAplicacion(String value) {
        this.ipAplicacion = value;
    }

    /**
     * Gets the value of the usuarioAplicacion property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getUsuarioAplicacion() {
        return usuarioAplicacion;
    }

    /**
     * Sets the value of the usuarioAplicacion property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setUsuarioAplicacion(String value) {
        this.usuarioAplicacion = value;
    }

    /**
     * Gets the value of the cambioPlan property.
     *
     * @return
     *     possible object is
     *     {@link CambiarPlanType }
     *
     */
    public CambiarPlanType getCambioPlan() {
        return cambioPlan;
    }

    /**
     * Sets the value of the cambioPlan property.
     *
     * @param value
     *     allowed object is
     *     {@link CambiarPlanType }
     *
     */
    public void setCambioPlan(CambiarPlanType value) {
        this.cambioPlan = value;
    }

    /**
     * Gets the value of the servicios property.
     *
     * @return
     *     possible object is
     *     {@link ListaServicioType }
     *
     */
    public ListaServicioType getServicios() {
        return servicios;
    }

    /**
     * Sets the value of the servicios property.
     *
     * @param value
     *     allowed object is
     *     {@link ListaServicioType }
     *
     */
    public void setServicios(ListaServicioType value) {
        this.servicios = value;
    }

}
