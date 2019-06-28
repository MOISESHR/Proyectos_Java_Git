using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;

namespace TGS.SGV.Modelo.Dto
{

    [DataContract]
    public class PerfilDtoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<PerfilDto> PerfilDtoLista { get; set; }         
    }
    
    [DataContract]
    public class PerfilDtoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string Cip { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string EmpresaSuccessFactor { get; set; }
    }

    [DataContract]
    public class PerfilDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombrePerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Ambito { get; set; }

    }
}
