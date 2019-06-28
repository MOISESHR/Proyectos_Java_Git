using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class ParametroAccesoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public ParametroAccesoDto ParametroAccesoDto { get; set; } 
    }

    [DataContract] 
    public class ParametroAccesoDto
    {
        [DataMember(EmitDefaultValue = false)]
        public int GoceFuturo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmpresaSuccessFactor { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PerfilDenegado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }
    }
}
