﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGS.SGV.Web.WsTablaGeneralServicio {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsTablaGeneralServicio.ITablaGeneralServicio")]
    public interface ITablaGeneralServicio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITablaGeneralServicio/ListarTablaGeneral", ReplyAction="http://tempuri.org/ITablaGeneralServicio/ListarTablaGeneralResponse")]
        TGS.SGV.Modelo.Dto.ListaDto[] ListarTablaGeneral(string CodigoFiltro);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITablaGeneralServicio/ListarTablaGeneral", ReplyAction="http://tempuri.org/ITablaGeneralServicio/ListarTablaGeneralResponse")]
        System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.ListaDto[]> ListarTablaGeneralAsync(string CodigoFiltro);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITablaGeneralServicioChannel : TGS.SGV.Web.WsTablaGeneralServicio.ITablaGeneralServicio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TablaGeneralServicioClient : System.ServiceModel.ClientBase<TGS.SGV.Web.WsTablaGeneralServicio.ITablaGeneralServicio>, TGS.SGV.Web.WsTablaGeneralServicio.ITablaGeneralServicio {
        
        public TablaGeneralServicioClient() {
        }
        
        public TablaGeneralServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TablaGeneralServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TablaGeneralServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TablaGeneralServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TGS.SGV.Modelo.Dto.ListaDto[] ListarTablaGeneral(string CodigoFiltro) {
            return base.Channel.ListarTablaGeneral(CodigoFiltro);
        }
        
        public System.Threading.Tasks.Task<TGS.SGV.Modelo.Dto.ListaDto[]> ListarTablaGeneralAsync(string CodigoFiltro) {
            return base.Channel.ListarTablaGeneralAsync(CodigoFiltro);
        }
    }
}
