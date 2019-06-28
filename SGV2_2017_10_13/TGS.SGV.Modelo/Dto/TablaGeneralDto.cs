using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.Modelo.Dto
{    
    [DataContract]
    public class TablaGeneralRequest : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoFiltro { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class TablaGeneralResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<TablaGeneralDto> TablaGeneralDto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class TablaGeneralDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string Codigo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Descripcion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Valor { get; set; }
    }
}

