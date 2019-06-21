using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class MenuDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoOpcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreOpcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DescripcionOpcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UrlPagina { get; set; }

    }
}
