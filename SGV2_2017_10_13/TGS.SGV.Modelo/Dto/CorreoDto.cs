using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class CorreoDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string EstadoCorreo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoDe { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoParaEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoParaPersona { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ContadorAdm { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CuentaCorreo { get; set; }

        [DataMember(EmitDefaultValue = false)]        
        public string CorreoCopia { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoAsunto{ get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoCuerpo { get; set; }
    }

    [DataContract]
    public class CorreoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CorreoEnvio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Empresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoMando { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AdministradorEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Usuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCC { get; set; }
    }


    [DataContract]
    public class CorreoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public CorreoDto CorreoDto { get; set; }
    }

}
