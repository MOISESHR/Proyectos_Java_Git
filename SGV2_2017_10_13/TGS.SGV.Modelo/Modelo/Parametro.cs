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
    public class Parametro
    {  
        [DataMember(EmitDefaultValue = false)]
        public string ValorParametro { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Unidad { get; set; }
    }
}
