using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class ListaDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string Codigo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Descripcion { get; set; }
    }
}
