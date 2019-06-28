﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGS.SGV.Web.WsPerfilUsuarioServicio {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsPerfilUsuarioServicio.IPerfilUsuarioServicio")]
    public interface IPerfilUsuarioServicio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorEmpresa", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorEmpresaResponse")]
        System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorEmpresa", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorEmpresaResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto>> ListarAdministradorEmpresaAsync(string empresa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorCCR", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorCCRResponse")]
        System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto> ListarAdministradorCCR(string unidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorCCR", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarAdministradorCCRResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto>> ListarAdministradorCCRAsync(string unidad);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarPerfilUsuario", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarPerfilUsuarioResponse")]
        TGS.SGV.Modelo.Dto.PerfilDtoResponse ListarPerfilUsuario(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ListarPerfilUsuario", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ListarPerfilUsuarioResponse")]
        System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.PerfilDtoResponse> ListarPerfilUsuarioAsync(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ObtenerPerfilAdicional", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ObtenerPerfilAdicionalResponse")]
        string ObtenerPerfilAdicional(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPerfilUsuarioServicio/ObtenerPerfilAdicional", ReplyAction="http://tempuri.org/IPerfilUsuarioServicio/ObtenerPerfilAdicionalResponse")]
        System.Threading.Tasks.Task<string> ObtenerPerfilAdicionalAsync(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPerfilUsuarioServicioChannel : TGS.SGV.Web.WsPerfilUsuarioServicio.IPerfilUsuarioServicio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PerfilUsuarioServicioClient : System.ServiceModel.ClientBase<TGS.SGV.Web.WsPerfilUsuarioServicio.IPerfilUsuarioServicio>, TGS.SGV.Web.WsPerfilUsuarioServicio.IPerfilUsuarioServicio {
        
        public PerfilUsuarioServicioClient() {
        }
        
        public PerfilUsuarioServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PerfilUsuarioServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PerfilUsuarioServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PerfilUsuarioServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto> ListarAdministradorEmpresa(string empresa) {
            return base.Channel.ListarAdministradorEmpresa(empresa);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto>> ListarAdministradorEmpresaAsync(string empresa) {
            return base.Channel.ListarAdministradorEmpresaAsync(empresa);
        }
        
        public System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto> ListarAdministradorCCR(string unidad) {
            return base.Channel.ListarAdministradorCCR(unidad);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<TGS.SGV.Modelo.Dto.PerfilUsuarioDto>> ListarAdministradorCCRAsync(string unidad) {
            return base.Channel.ListarAdministradorCCRAsync(unidad);
        }
        
        public TGS.SGV.Modelo.Dto.PerfilDtoResponse ListarPerfilUsuario(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest) {
            return base.Channel.ListarPerfilUsuario(usuarioRequest);
        }
        
        public System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.PerfilDtoResponse> ListarPerfilUsuarioAsync(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest) {
            return base.Channel.ListarPerfilUsuarioAsync(usuarioRequest);
        }
        
        public string ObtenerPerfilAdicional(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest) {
            return base.Channel.ObtenerPerfilAdicional(usuarioRequest);
        }
        
        public System.Threading.Tasks.Task<string> ObtenerPerfilAdicionalAsync(TGS.SGV.Modelo.Dto.UsuarioRequest usuarioRequest) {
            return base.Channel.ObtenerPerfilAdicionalAsync(usuarioRequest);
        }
    }
}
