package pe.com.claro.eba.activospostpagoejbs.util;

import java.io.Serializable;

import java.util.ResourceBundle;


public class PropertiesInternos implements Serializable {

    private static final long serialVersionUID = 1L;

    private static ResourceBundle localProp =
        ResourceBundle.getBundle("application");


    public static final String CODIGO_ESTANDAR_EXITO =
        localProp.getString("codigo.estandar.exito");
    public static final String CODIGO_ESTANDAR_ERROR =
        localProp.getString("codigo.estandar.error");
    public static final String CODIGO_ESTANDAR_ERROR_RECURSO_CAIDO =
        localProp.getString("codigo.estandar.error.recurso.caido");
    public static final String CODIGO_ERROR_COMMIT_CMS =
        localProp.getString("codigo.error.commit.cms");
    public static final String CODIGO_ERROR_CMS =
        localProp.getString("codigo.error.cms");
    public static final String CODIGO_ERROR_BSCS =
      localProp.getString("codigo.error.bscs");
    public static final String CODIGO_ERROR_BSCS_CAIDO =
    localProp.getString("codigo.error.bscs.caido");
    public static final String CODIGO_ERROR_LISTA_VACIA =
        localProp.getString("codigo.error.lista.vacia");
    public static final String CODIGO_EXITO_INCOMPLETO =
      localProp.getString("codigo.exito.incompleto");
  public static final String CODIGO_ERROR_ESTADO_ARCHIVAR =
    localProp.getString("codigo.error.estado.archivar");
  public static final String CODIGO_ERROR_CANCELACION =
    localProp.getString("codigo.error.cancelacion");
  

  public static final String COMPONENTE_MDB_ACTUALIZA_ESTADOS =
    localProp.getString("componente.mdb.gestionactualizaestados.nombre");
  public static final String COMPONENTE_MDB_GESTION_VENTA_CMS =
    localProp.getString("componente.mdb.gestionventacms.nombre");
  public static final String COMPONENTE_MDB_GESTION_POSTVENTA_CMS =
    localProp.getString("componente.mdb.gestionpostventacms.nombre");
  public static final String COMPONENTE_MDB_GESTION_TX_EAI =
    localProp.getString("componente.mdb.gestiontxeai.nombre");
  public static final String COMPONENTE_EJB_ACTIVOS_POSTPAGO =
    localProp.getString("componente.ejb.ActivosPostpagoEJBBean.nombre");
    
    public static final String NOMBRE_OPERACION_CREAR_CLIENTE =
      localProp.getString("nombre.operacion.crearCliente");
  public static final String NOMBRE_OPERACION_CREAR_CONTRATO =
    localProp.getString("nombre.operacion.crearContrato");
  public static final String NOMBRE_OPERACION_ACTUALIZAR_CONTRATO =
    localProp.getString("nombre.operacion.actualizarContrato");
  public static final String NOMBRE_OPERACION_AGREGAR_SERVICIOS_CONTRATO =
    localProp.getString("nombre.operacion.agregarServiciosContrato");
  public static final String NOMBRE_OPERACION_ACTUALIZAR_SOT_SGA =
    localProp.getString("nombre.operacion.actualizarSOTSGA");
  public static final String NOMBRE_OPERACION_INSERTAR_PROID_SGA =
    localProp.getString("nombre.operacion.insertarProIdSGA");
  public static final String NOMBRE_OPERACION_ACTUALIZAR_SOT_SGA_CONTRATO =
    localProp.getString("nombre.operacion.actualizarSOTSGAContrato");
  public static final String NOMBRE_OPERACION_ACTUALIZAR_ESTADO_PVU =
    localProp.getString("nombre.operacion.actualizarEstadoPVU");
  public static final String NOMBRE_OPERACION_REGISTRAR_TX_EAI =
    localProp.getString("nombre.operacion.registrarTxEAI");
  public static final String NOMBRE_OPERACION_ACTUALIZAR_ERROR_TX_EAI =
    localProp.getString("nombre.operacion.actualizarErrorTxEAI");
  public static final String NOMBRE_OPERACION_FINALIZAR_TX_EAI =
    localProp.getString("nombre.operacion.finalizarTxEAI");
  
  public static final String SP_VALIDAR_SERVICIOS_BSCS =
    localProp.getString("sp.validar.servicios.bscs");
    public static final String SP_PVU_ACTUALIZAR_ESTADO =
      localProp.getString("sp.pvu.actualizar.estado");
    public static final String SP_SGA_ACTUALIZAR_SOT =
      localProp.getString("sp.sga.actualizar.sot");
    public static final String SP_SGA_ACTUALIZAR_SOT_CONTRATO =
      localProp.getString("sp.sga.actualizar.sot.contrato");
   public static final String SP_SGA_INSERTAR_COID =
     localProp.getString("sp.sga.insertar.coid");
   public static final String FUNCTION_SGA_EXISTE_SOT =
    localProp.getString("function.sga.existe.sot");
   public static final String SpBuscarServCoreXPlan = 
      localProp.getString("sp.buscarservcorexplan");

    public static final String SP_EAI_REGISTRAR_TX =
      localProp.getString("sp.eai.registrar.tx");
    public static final String SP_EAI_ACTUALIZAR_ERROR_TX =
      localProp.getString("sp.eai.actualizar.error.tx");
    public static final String SP_EAI_FINALIZAR_TX =
      localProp.getString("sp.eai.finalizar.tx");
    public static final String SP_EAI_ARCHIVAR_TX =
      localProp.getString("sp.eai.archivar.tx");
    
    public static final String CAMPO_ADICIONAL_CORRELATIVO_SISACT =
      localProp.getString("campo.adicional.correlativoSISACT");
    public static final String CAMPO_ADICIONAL_CUSTCODE =
      localProp.getString("campo.adicional.custCode");
    public static final String CAMPO_ADICIONAL_CUSTOMER_ID =
      localProp.getString("campo.adicional.customerId");
    public static final String CAMPO_ADICIONAL_COID =
      localProp.getString("campo.adicional.coId");
    public static final String CAMPO_ADICIONAL_ESTADO_CONTRATO =
      localProp.getString("campo.adicional.estadoContrato");
    public static final String CAMPO_ADICIONAL_CODIGO_SOT =
      localProp.getString("campo.adicional.codigoSOT");
    public static final String TIPO_POSTPAGO_HFC =
      localProp.getString("tipopostpago.hfc");
    public static final String NOMBRE_METODO_ORIGEN_EJECUTAR_PRE_ACTIVACION =
      localProp.getString("nombre.metodo.origen.ejecutarPreActivacion");
    public static final String NOMBRE_METODO_ORIGEN_CREAR_CONTRATOS =
      localProp.getString("nombre.metodo.origen.crearContratos");
    public static final String CAMPO_ADICIONAL_ID_NUMERO_TELEFONO =
      localProp.getString("campo.adicional.idNumeroTelefono");
    public static final String CAMPO_ADICIONAL_LINEA_PRINCIPAL =
      localProp.getString("campo.adicional.lineaPrincipal");
    
}
