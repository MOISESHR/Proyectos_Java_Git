using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{    
    [DataContract]
    public class UnidadDtoResponse
    {
             
    }

    [DataContract]
    public class UnidadDtoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string Cip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUnidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreUnidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }
    }
   
}
