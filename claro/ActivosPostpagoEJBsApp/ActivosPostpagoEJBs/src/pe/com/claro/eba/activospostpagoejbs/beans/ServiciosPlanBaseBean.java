package pe.com.claro.eba.activospostpagoejbs.beans;

import java.io.Serializable;

public class ServiciosPlanBaseBean implements Serializable{

      @SuppressWarnings("compatibility:-1710699507130008279")
      private static final long serialVersionUID = 1L;
      
      private String snCode;
      private String spCode;
      private String coServ;
      private String cargoFijo;
      private String periodos;
      private String estado;

    public static long getSerialVersionUID() {
        return serialVersionUID;
    }

    public void setSnCode(String snCode) {
        this.snCode = snCode;
    }

    public String getSnCode() {
        return snCode;
    }

    public void setSpCode(String spCode) {
        this.spCode = spCode;
    }

    public String getSpCode() {
        return spCode;
    }

    public void setCoServ(String coServ) {
        this.coServ = coServ;
    }

    public String getCoServ() {
        return coServ;
    }

    public void setCargoFijo(String cargoFijo) {
        this.cargoFijo = cargoFijo;
    }

    public String getCargoFijo() {
        return cargoFijo;
    }

    public void setPeriodos(String periodos) {
        this.periodos = periodos;
    }

    public String getPeriodos() {
        return periodos;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public String getEstado() {
        return estado;
    }
}
