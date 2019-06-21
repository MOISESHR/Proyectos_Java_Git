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
    public class TablaGeneral : Auditoria
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoFiltro { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Codigo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Descripcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Valor { get; set; }
         
    }
}
