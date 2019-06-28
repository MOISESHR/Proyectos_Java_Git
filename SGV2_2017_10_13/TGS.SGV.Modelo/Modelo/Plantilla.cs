using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Modelo.Modelo
{
    [DataContract]
    public class Plantilla
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
}
