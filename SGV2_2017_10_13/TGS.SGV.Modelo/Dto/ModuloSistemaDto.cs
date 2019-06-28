using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class ModuloSistemaRequest
    { 

    }

    [DataContract]
    public class ModuloSistemaResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ModuloSistemaDto> ModuloSistemaDtoLista { get; set; }
    }

    [DataContract]
    public class ModuloSistemaDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoPadre { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoModulo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Descripcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string RutaPagina { get; set; }
         
        [DataMember(EmitDefaultValue = false)]
        public int? Orden { get; set; }
    }
}
