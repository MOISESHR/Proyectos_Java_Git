﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGS.SGV.Web.WsGoceVacacionalServicio {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsGoceVacacionalServicio.IGoceVacacionalServicio")]
    public interface IGoceVacacionalServicio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGoceVacacionalServicio/ListarObtenerGoce", ReplyAction="http://tempuri.org/IGoceVacacionalServicio/ListarObtenerGoceResponse")]
        TGS.SGV.Modelo.Dto.GoceVacacionalResponse ListarObtenerGoce(TGS.SGV.Modelo.Dto.GoceVacacionalRequest goceVacacionalRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGoceVacacionalServicio/ListarObtenerGoce", ReplyAction="http://tempuri.org/IGoceVacacionalServicio/ListarObtenerGoceResponse")]
        System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.GoceVacacionalResponse> ListarObtenerGoceAsync(TGS.SGV.Modelo.Dto.GoceVacacionalRequest goceVacacionalRequest);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGoceVacacionalServicioChannel : TGS.SGV.Web.WsGoceVacacionalServicio.IGoceVacacionalServicio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GoceVacacionalServicioClient : System.ServiceModel.ClientBase<TGS.SGV.Web.WsGoceVacacionalServicio.IGoceVacacionalServicio>, TGS.SGV.Web.WsGoceVacacionalServicio.IGoceVacacionalServicio {
        
        public GoceVacacionalServicioClient() {
        }
        
        public GoceVacacionalServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GoceVacacionalServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GoceVacacionalServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GoceVacacionalServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TGS.SGV.Modelo.Dto.GoceVacacionalResponse ListarObtenerGoce(TGS.SGV.Modelo.Dto.GoceVacacionalRequest goceVacacionalRequest) {
            return base.Channel.ListarObtenerGoce(goceVacacionalRequest);
        }
        
        public System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.GoceVacacionalResponse> ListarObtenerGoceAsync(TGS.SGV.Modelo.Dto.GoceVacacionalRequest goceVacacionalRequest) {
            return base.Channel.ListarObtenerGoceAsync(goceVacacionalRequest);
        }
    }
}
