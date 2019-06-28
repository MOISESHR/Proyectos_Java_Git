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
    public class Trabajador : Auditoria
    {
        [DataMember(EmitDefaultValue = false)]
        public string IdTrabajador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreTrabajador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Unidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipConfig { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreLider { get; set; }
         
    }
}
