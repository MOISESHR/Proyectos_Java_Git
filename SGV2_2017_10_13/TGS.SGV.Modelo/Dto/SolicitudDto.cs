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
    public class SolicitudRequest : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EstadoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

    }
    [DataContract]
    public class SolicitudResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<SolicitudDto> SolicitudDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public SolicitudDto SolicitudDto { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class SolicitudDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CalculoDias { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EstadoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEstado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoAprobador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FlagAdelantoVacaciones { get; set; }  

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
