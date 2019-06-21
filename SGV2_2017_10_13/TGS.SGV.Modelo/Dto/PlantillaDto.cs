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
    public class PlantillaDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string TipoGeneracion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoDe { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoPara { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoCC { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoAsunto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CorreoCuerpo { get; set; }
    }

    [DataContract]
    public class PlantillaRequest : IPaginacion
    {

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCorreo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Empleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Mando { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AdmEmp { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipCC { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class PlantillaResponse 
    {
        [DataMember(EmitDefaultValue = false)]
        public List<PlantillaDto> PlantillaDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PlantillaDto PlantillaDto { get; set; }


    }

}
