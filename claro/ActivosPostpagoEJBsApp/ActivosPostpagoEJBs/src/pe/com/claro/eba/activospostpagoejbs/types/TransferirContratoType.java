
package pe.com.claro.eba.activospostpagoejbs.types;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for TransferirContratoType complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType name="TransferirContratoType">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="cargoSubscripcion" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="ratioConversion" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="moneda" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="coId" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="razonTransferencia" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="nuevoCustomerId" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="nuevoPlan" type="{http://www.w3.org/2001/XMLSchema}long"/>
 *         &lt;element name="monedaSecundaria" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *         &lt;element name="ratioConversionSecundaria" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "TransferirContratoType",
         propOrder = { "cargoSubscripcion", "ratioConversion", "moneda",
                       "coId", "razonTransferencia", "nuevoCustomerId",
                       "nuevoPlan", "monedaSecundaria",
                       "ratioConversionSecundaria" })
public class TransferirContratoType implements java.io.Serializable {

    protected boolean cargoSubscripcion;
    protected long ratioConversion;
    protected long moneda;
    protected long coId;
    protected long razonTransferencia;
    protected long nuevoCustomerId;
    protected long nuevoPlan;
    protected Long monedaSecundaria;
    protected Long ratioConversionSecundaria;

    /**
     * Gets the value of the cargoSubscripcion property.
     *
     */
    public boolean isCargoSubscripcion() {
        return cargoSubscripcion;
    }

    /**
     * Sets the value of the cargoSubscripcion property.
     *
     */
    public void setCargoSubscripcion(boolean value) {
        this.cargoSubscripcion = value;
    }

    /**
     * Gets the value of the ratioConversion property.
     *
     */
    public long getRatioConversion() {
        return ratioConversion;
    }

    /**
     * Sets the value of the ratioConversion property.
     *
     */
    public void setRatioConversion(long value) {
        this.ratioConversion = value;
    }

    /**
     * Gets the value of the moneda property.
     *
     */
    public long getMoneda() {
        return moneda;
    }

    /**
     * Sets the value of the moneda property.
     *
     */
    public void setMoneda(long value) {
        this.moneda = value;
    }

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
     * Gets the value of the razonTransferencia property.
     *
     */
    public long getRazonTransferencia() {
        return razonTransferencia;
    }

    /**
     * Sets the value of the razonTransferencia property.
     *
     */
    public void setRazonTransferencia(long value) {
        this.razonTransferencia = value;
    }

    /**
     * Gets the value of the nuevoCustomerId property.
     *
     */
    public long getNuevoCustomerId() {
        return nuevoCustomerId;
    }

    /**
     * Sets the value of the nuevoCustomerId property.
     *
     */
    public void setNuevoCustomerId(long value) {
        this.nuevoCustomerId = value;
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
     * Gets the value of the monedaSecundaria property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getMonedaSecundaria() {
        return monedaSecundaria;
    }

    /**
     * Sets the value of the monedaSecundaria property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setMonedaSecundaria(Long value) {
        this.monedaSecundaria = value;
    }

    /**
     * Gets the value of the ratioConversionSecundaria property.
     *
     * @return
     *     possible object is
     *     {@link Long }
     *
     */
    public Long getRatioConversionSecundaria() {
        return ratioConversionSecundaria;
    }

    /**
     * Sets the value of the ratioConversionSecundaria property.
     *
     * @param value
     *     allowed object is
     *     {@link Long }
     *
     */
    public void setRatioConversionSecundaria(Long value) {
        this.ratioConversionSecundaria = value;
    }

}
