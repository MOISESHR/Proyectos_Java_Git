﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGS.SGV.Agente.WsAutenticacion {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UsuarioBE", Namespace="http://schemas.datacontract.org/2004/07/RRHH.Acceso.CapaComun.Entidades")]
    [System.SerializableAttribute()]
    public partial class UsuarioBE : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ID_OPCIONField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PERFC_CODIGOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string STD_ID_PERSONField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string STD_N_FAM_NAME_1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string STD_N_FIRST_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string STD_N_MAIDEN_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_CLAVEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_CODIGOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_CORREOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_ETIQUETAField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime USUAC_FECBLOQUEOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime USUAC_FECCAMBIOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime USUAC_FECEXPIRACField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime USUAC_FECHA_ACT_ETQField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime USUAC_FECINICIOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_FLAG_ETIQField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_FONOBEEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_FONOCORField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_FONODIRField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_FONOMOVField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_INDBLOQField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_TIPOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USUAC_TIPOPERMField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID_OPCION {
            get {
                return this.ID_OPCIONField;
            }
            set {
                if ((object.ReferenceEquals(this.ID_OPCIONField, value) != true)) {
                    this.ID_OPCIONField = value;
                    this.RaisePropertyChanged("ID_OPCION");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PERFC_CODIGO {
            get {
                return this.PERFC_CODIGOField;
            }
            set {
                if ((object.ReferenceEquals(this.PERFC_CODIGOField, value) != true)) {
                    this.PERFC_CODIGOField = value;
                    this.RaisePropertyChanged("PERFC_CODIGO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STD_ID_PERSON {
            get {
                return this.STD_ID_PERSONField;
            }
            set {
                if ((object.ReferenceEquals(this.STD_ID_PERSONField, value) != true)) {
                    this.STD_ID_PERSONField = value;
                    this.RaisePropertyChanged("STD_ID_PERSON");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STD_N_FAM_NAME_1 {
            get {
                return this.STD_N_FAM_NAME_1Field;
            }
            set {
                if ((object.ReferenceEquals(this.STD_N_FAM_NAME_1Field, value) != true)) {
                    this.STD_N_FAM_NAME_1Field = value;
                    this.RaisePropertyChanged("STD_N_FAM_NAME_1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STD_N_FIRST_NAME {
            get {
                return this.STD_N_FIRST_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.STD_N_FIRST_NAMEField, value) != true)) {
                    this.STD_N_FIRST_NAMEField = value;
                    this.RaisePropertyChanged("STD_N_FIRST_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STD_N_MAIDEN_NAME {
            get {
                return this.STD_N_MAIDEN_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.STD_N_MAIDEN_NAMEField, value) != true)) {
                    this.STD_N_MAIDEN_NAMEField = value;
                    this.RaisePropertyChanged("STD_N_MAIDEN_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_CLAVE {
            get {
                return this.USUAC_CLAVEField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_CLAVEField, value) != true)) {
                    this.USUAC_CLAVEField = value;
                    this.RaisePropertyChanged("USUAC_CLAVE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_CODIGO {
            get {
                return this.USUAC_CODIGOField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_CODIGOField, value) != true)) {
                    this.USUAC_CODIGOField = value;
                    this.RaisePropertyChanged("USUAC_CODIGO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_CORREO {
            get {
                return this.USUAC_CORREOField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_CORREOField, value) != true)) {
                    this.USUAC_CORREOField = value;
                    this.RaisePropertyChanged("USUAC_CORREO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_ETIQUETA {
            get {
                return this.USUAC_ETIQUETAField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_ETIQUETAField, value) != true)) {
                    this.USUAC_ETIQUETAField = value;
                    this.RaisePropertyChanged("USUAC_ETIQUETA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime USUAC_FECBLOQUEO {
            get {
                return this.USUAC_FECBLOQUEOField;
            }
            set {
                if ((this.USUAC_FECBLOQUEOField.Equals(value) != true)) {
                    this.USUAC_FECBLOQUEOField = value;
                    this.RaisePropertyChanged("USUAC_FECBLOQUEO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime USUAC_FECCAMBIO {
            get {
                return this.USUAC_FECCAMBIOField;
            }
            set {
                if ((this.USUAC_FECCAMBIOField.Equals(value) != true)) {
                    this.USUAC_FECCAMBIOField = value;
                    this.RaisePropertyChanged("USUAC_FECCAMBIO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime USUAC_FECEXPIRAC {
            get {
                return this.USUAC_FECEXPIRACField;
            }
            set {
                if ((this.USUAC_FECEXPIRACField.Equals(value) != true)) {
                    this.USUAC_FECEXPIRACField = value;
                    this.RaisePropertyChanged("USUAC_FECEXPIRAC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime USUAC_FECHA_ACT_ETQ {
            get {
                return this.USUAC_FECHA_ACT_ETQField;
            }
            set {
                if ((this.USUAC_FECHA_ACT_ETQField.Equals(value) != true)) {
                    this.USUAC_FECHA_ACT_ETQField = value;
                    this.RaisePropertyChanged("USUAC_FECHA_ACT_ETQ");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime USUAC_FECINICIO {
            get {
                return this.USUAC_FECINICIOField;
            }
            set {
                if ((this.USUAC_FECINICIOField.Equals(value) != true)) {
                    this.USUAC_FECINICIOField = value;
                    this.RaisePropertyChanged("USUAC_FECINICIO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_FLAG_ETIQ {
            get {
                return this.USUAC_FLAG_ETIQField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_FLAG_ETIQField, value) != true)) {
                    this.USUAC_FLAG_ETIQField = value;
                    this.RaisePropertyChanged("USUAC_FLAG_ETIQ");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_FONOBEE {
            get {
                return this.USUAC_FONOBEEField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_FONOBEEField, value) != true)) {
                    this.USUAC_FONOBEEField = value;
                    this.RaisePropertyChanged("USUAC_FONOBEE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_FONOCOR {
            get {
                return this.USUAC_FONOCORField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_FONOCORField, value) != true)) {
                    this.USUAC_FONOCORField = value;
                    this.RaisePropertyChanged("USUAC_FONOCOR");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_FONODIR {
            get {
                return this.USUAC_FONODIRField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_FONODIRField, value) != true)) {
                    this.USUAC_FONODIRField = value;
                    this.RaisePropertyChanged("USUAC_FONODIR");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_FONOMOV {
            get {
                return this.USUAC_FONOMOVField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_FONOMOVField, value) != true)) {
                    this.USUAC_FONOMOVField = value;
                    this.RaisePropertyChanged("USUAC_FONOMOV");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_INDBLOQ {
            get {
                return this.USUAC_INDBLOQField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_INDBLOQField, value) != true)) {
                    this.USUAC_INDBLOQField = value;
                    this.RaisePropertyChanged("USUAC_INDBLOQ");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_TIPO {
            get {
                return this.USUAC_TIPOField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_TIPOField, value) != true)) {
                    this.USUAC_TIPOField = value;
                    this.RaisePropertyChanged("USUAC_TIPO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USUAC_TIPOPERM {
            get {
                return this.USUAC_TIPOPERMField;
            }
            set {
                if ((object.ReferenceEquals(this.USUAC_TIPOPERMField, value) != true)) {
                    this.USUAC_TIPOPERMField = value;
                    this.RaisePropertyChanged("USUAC_TIPOPERM");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsAutenticacion.IServicioAutenticacion")]
    public interface IServicioAutenticacion {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/Ingresar", ReplyAction="http://tempuri.org/IServicioAutenticacion/IngresarResponse")]
        bool Ingresar(string usuario, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/Ingresar", ReplyAction="http://tempuri.org/IServicioAutenticacion/IngresarResponse")]
        System.Threading.Tasks.Task<bool> IngresarAsync(string usuario, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/GenerarKey", ReplyAction="http://tempuri.org/IServicioAutenticacion/GenerarKeyResponse")]
        string GenerarKey(string idUsuario, string idSistema, string opcion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/GenerarKey", ReplyAction="http://tempuri.org/IServicioAutenticacion/GenerarKeyResponse")]
        System.Threading.Tasks.Task<string> GenerarKeyAsync(string idUsuario, string idSistema, string opcion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/ValidarKeyASP", ReplyAction="http://tempuri.org/IServicioAutenticacion/ValidarKeyASPResponse")]
        string ValidarKeyASP(string keyGenerado, string codigoSistemaReceptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/ValidarKeyASP", ReplyAction="http://tempuri.org/IServicioAutenticacion/ValidarKeyASPResponse")]
        System.Threading.Tasks.Task<string> ValidarKeyASPAsync(string keyGenerado, string codigoSistemaReceptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/ValidarKey", ReplyAction="http://tempuri.org/IServicioAutenticacion/ValidarKeyResponse")]
        TGS.SGV.Agente.WsAutenticacion.UsuarioBE ValidarKey(string keyGenerado, string codigoSistemaReceptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/ValidarKey", ReplyAction="http://tempuri.org/IServicioAutenticacion/ValidarKeyResponse")]
        System.Threading.Tasks.Task<TGS.SGV.Agente.WsAutenticacion.UsuarioBE> ValidarKeyAsync(string keyGenerado, string codigoSistemaReceptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/CambiarContrasenia", ReplyAction="http://tempuri.org/IServicioAutenticacion/CambiarContraseniaResponse")]
        void CambiarContrasenia(string idUsuario, string contraseniaAntigua, string contraseniaNueva);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/CambiarContrasenia", ReplyAction="http://tempuri.org/IServicioAutenticacion/CambiarContraseniaResponse")]
        System.Threading.Tasks.Task CambiarContraseniaAsync(string idUsuario, string contraseniaAntigua, string contraseniaNueva);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/OlvidarContrasenia", ReplyAction="http://tempuri.org/IServicioAutenticacion/OlvidarContraseniaResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="mensaje")]
        string OlvidarContrasenia(string numeroDocumento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioAutenticacion/OlvidarContrasenia", ReplyAction="http://tempuri.org/IServicioAutenticacion/OlvidarContraseniaResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="mensaje")]
        System.Threading.Tasks.Task<string> OlvidarContraseniaAsync(string numeroDocumento);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioAutenticacionChannel : TGS.SGV.Agente.WsAutenticacion.IServicioAutenticacion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioAutenticacionClient : System.ServiceModel.ClientBase<TGS.SGV.Agente.WsAutenticacion.IServicioAutenticacion>, TGS.SGV.Agente.WsAutenticacion.IServicioAutenticacion {
        
        public ServicioAutenticacionClient() {
        }
        
        public ServicioAutenticacionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioAutenticacionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioAutenticacionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioAutenticacionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Ingresar(string usuario, string password) {
            return base.Channel.Ingresar(usuario, password);
        }
        
        public System.Threading.Tasks.Task<bool> IngresarAsync(string usuario, string password) {
            return base.Channel.IngresarAsync(usuario, password);
        }
        
        public string GenerarKey(string idUsuario, string idSistema, string opcion) {
            return base.Channel.GenerarKey(idUsuario, idSistema, opcion);
        }
        
        public System.Threading.Tasks.Task<string> GenerarKeyAsync(string idUsuario, string idSistema, string opcion) {
            return base.Channel.GenerarKeyAsync(idUsuario, idSistema, opcion);
        }
        
        public string ValidarKeyASP(string keyGenerado, string codigoSistemaReceptor) {
            return base.Channel.ValidarKeyASP(keyGenerado, codigoSistemaReceptor);
        }
        
        public System.Threading.Tasks.Task<string> ValidarKeyASPAsync(string keyGenerado, string codigoSistemaReceptor) {
            return base.Channel.ValidarKeyASPAsync(keyGenerado, codigoSistemaReceptor);
        }
        
        public TGS.SGV.Agente.WsAutenticacion.UsuarioBE ValidarKey(string keyGenerado, string codigoSistemaReceptor) {
            return base.Channel.ValidarKey(keyGenerado, codigoSistemaReceptor);
        }
        
        public System.Threading.Tasks.Task<TGS.SGV.Agente.WsAutenticacion.UsuarioBE> ValidarKeyAsync(string keyGenerado, string codigoSistemaReceptor) {
            return base.Channel.ValidarKeyAsync(keyGenerado, codigoSistemaReceptor);
        }
        
        public void CambiarContrasenia(string idUsuario, string contraseniaAntigua, string contraseniaNueva) {
            base.Channel.CambiarContrasenia(idUsuario, contraseniaAntigua, contraseniaNueva);
        }
        
        public System.Threading.Tasks.Task CambiarContraseniaAsync(string idUsuario, string contraseniaAntigua, string contraseniaNueva) {
            return base.Channel.CambiarContraseniaAsync(idUsuario, contraseniaAntigua, contraseniaNueva);
        }
        
        public string OlvidarContrasenia(string numeroDocumento) {
            return base.Channel.OlvidarContrasenia(numeroDocumento);
        }
        
        public System.Threading.Tasks.Task<string> OlvidarContraseniaAsync(string numeroDocumento) {
            return base.Channel.OlvidarContraseniaAsync(numeroDocumento);
        }
    }
}
