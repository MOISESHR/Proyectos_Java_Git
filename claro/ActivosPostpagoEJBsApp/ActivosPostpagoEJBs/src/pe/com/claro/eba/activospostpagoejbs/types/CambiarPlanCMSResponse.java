
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
 *         &lt;element name="codigoRespuesta" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="mensajeRespuesta" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element name="serviciosEliminados" type="{http://claro.com.pe/eai/ebs/ws/ActivosPostpagoEJB}ListaServicioType"/>
 *         &lt;element name="serviciosAgregados" type="{http://claro.com.pe/eai/ebs/ws/ActivosPostpagoEJB}ListaServicioType"/>
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
         propOrder = { "idTransaccion", "codigoRespuesta", "mensajeRespuesta",
                       "serviciosEliminados", "serviciosAgregados" })
@XmlRootElement(name = "CambiarPlanCMSResponse")
public class CambiarPlanCMSResponse implements java.io.Serializable {

    @XmlElement(required = true)
    protected String idTransaccion;
    @XmlElement(required = true)
    protected String codigoRespuesta;
    @XmlElement(required = true)
    protected String mensajeRespuesta;
    @XmlElement(required = true)
    protected ListaServicioType serviciosEliminados;
    @XmlElement(required = true)
    protected ListaServicioType serviciosAgregados;

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
     * Gets the value of the codigoRespuesta property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getCodigoRespuesta() {
        return codigoRespuesta;
    }

    /**
     * Sets the value of the codigoRespuesta property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setCodigoRespuesta(String value) {
        this.codigoRespuesta = value;
    }

    /**
     * Gets the value of the mensajeRespuesta property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getMensajeRespuesta() {
        return mensajeRespuesta;
    }

    /**
     * Sets the value of the mensajeRespuesta property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setMensajeRespuesta(String value) {
        this.mensajeRespuesta = value;
    }

    /**
     * Gets the value of the serviciosEliminados property.
     *
     * @return
     *     possible object is
     *     {@link ListaServicioType }
     *
     */
    public ListaServicioType getServiciosEliminados() {
        return serviciosEliminados;
    }

    /**
     * Sets the value of the serviciosEliminados property.
     *
     * @param value
     *     allowed object is
     *     {@link ListaServicioType }
     *
     */
    public void setServiciosEliminados(ListaServicioType value) {
        this.serviciosEliminados = value;
    }

    /**
     * Gets the value of the serviciosAgregados property.
     *
     * @return
     *     possible object is
     *     {@link ListaServicioType }
     *
     */
    public ListaServicioType getServiciosAgregados() {
        return serviciosAgregados;
    }

    /**
     * Sets the value of the serviciosAgregados property.
     *
     * @param value
     *     allowed object is
     *     {@link ListaServicioType }
     *
     */
    public void setServiciosAgregados(ListaServicioType value) {
        this.serviciosAgregados = value;
    }

}
